#include "module.hpp"
#include "attribute.hpp"
#include "managed_assembly.hpp"
#include "managed_functions.hpp"
#include "memory.hpp"
#include "method_info.hpp"
#include "type.hpp"
#include "utils.hpp"

#include <module_export.h>

#include <plugify/compat_format.hpp>
#include <plugify/language_module.hpp>
#include <plugify/log.hpp>
#include <plugify/module.hpp>
#include <plugify/string.hpp>
#include <plugify/any.hpp>
#include <plugify/vector.hpp>
#include <plugify/plugify_provider.hpp>
#include <plugify/plugin.hpp>
#include <plugify/version.hpp>
#include <plugify/plugin_descriptor.hpp>
#include <plugify/plugin_reference_descriptor.hpp>

#include <cpptrace/cpptrace.hpp>

#if NETLM_PLATFORM_WINDOWS
#undef FindResource
#endif

#define LOG_PREFIX "[NETLM] "

using namespace plugify;
using namespace netlm;

InitResult DotnetLanguageModule::Initialize(std::weak_ptr<IPlugifyProvider> provider, ModuleHandle module) {
	if (!((_provider = provider.lock()))) {
		return ErrorData{ "Provider not exposed" };
	}

	fs::path baseDir(module.GetBaseDir());

	if (!_host.Initialize({
		.hostfxrPath = baseDir / "dotnet/host/fxr/9.0.0/" NETLM_LIBRARY_PREFIX "hostfxr" NETLM_LIBRARY_SUFFIX,
		.rootDirectory = baseDir / "api",
		.messageCallback = MessageCallback,
		.exceptionCallback = ExceptionCallback,
	})) {
		return ErrorData{ "Could not initialize hostfxr environment" };
	}

	_provider->Log(LOG_PREFIX "Inited!", Severity::Debug);

	_rt = std::make_shared<asmjit::JitRuntime>();

	return InitResultData{{ .hasUpdate = false }};
}

void DotnetLanguageModule::Shutdown() {
	_scripts.clear();
	_functions.clear();

	_loader.Unload();
	_host.Shutdown();

	_rt.reset();
	_provider.reset();
}

void DotnetLanguageModule::OnUpdate(DateTime /*dt*/) {
}

LoadResult DotnetLanguageModule::OnPluginLoad(PluginHandle plugin) {
	fs::path assemblyPath(plugin.GetBaseDir());
	assemblyPath /= plugin.GetDescriptor().GetEntryPoint();

	ManagedAssembly& assembly = _loader.LoadAssembly(assemblyPath);
	if (!assembly) {
		return ErrorData{ _loader.GetError() };
	}

	Type& pluginClassType = assembly.GetTypeByBaseType("Plugify.Plugin");
	if (!pluginClassType) {
		return ErrorData{"Failed to find 'Plugify.Plugin' class implementation"};
	}

	std::vector<std::string> exportErrors;

	std::span<const MethodHandle> exportedMethods = plugin.GetDescriptor().GetExportedMethods();
	std::vector<MethodData> methods;
	methods.reserve(exportedMethods.size());

	for (const auto& method : exportedMethods) {
		auto separated = Utils::Split(method.GetFunctionName(), ".");
		size_t size = separated.size();
		bool noNamespace = (size == 2);
		if (size != 3 && !noNamespace) {
			exportErrors.emplace_back(std::format("Invalid function format: '{}'. Please provide name in that format: 'Namespace.Class.Method' or 'Namespace.MyParentClass+MyNestedClass.Method' or 'Class.Method'", method.GetFunctionName()));
			continue;
		}

		Type& type = assembly.GetType(noNamespace ? separated[size-2] : std::string_view(separated[0].data(), separated[size-1].data() - 1));
		if (!type) {
			exportErrors.emplace_back(std::format("Failed to find class '{}'", method.GetFunctionName()));
			continue;
		}

		MethodInfo methodInfo = type.GetMethod(separated[size-1]);
		if (!methodInfo) {
			exportErrors.emplace_back(std::format("Failed to find method '{}'", method.GetFunctionName()));
			continue;
		}

		ValueType returnType = methodInfo.GetReturnType().GetManagedType().type;
		ValueType methodReturnType = method.GetReturnType().GetType();
		if (returnType != methodReturnType) {
			exportErrors.emplace_back(std::format("Method '{}' has invalid return type '{}' when it should have '{}'", method.GetFunctionName(), ValueUtils::ToString(methodReturnType), ValueUtils::ToString(returnType)));
			continue;
		}

		const auto& parameterTypes = methodInfo.GetParameterTypes();

		size_t paramCount = parameterTypes.size();
		std::span<const PropertyHandle> paramTypes = method.GetParamTypes();
		if (paramCount != paramTypes.size()) {
			exportErrors.emplace_back(std::format("Method '{}' has invalid parameter count {} when it should have {}", method.GetFunctionName(), paramTypes.size(), paramCount));
			continue;
		}

		bool methodFail = false;

		for (size_t i = 0; i < paramCount; ++i) {
			ValueType paramType = parameterTypes[i].GetManagedType().type;
			ValueType methodParamType = paramTypes[i].GetType();
			if (paramType != methodParamType) {
				methodFail = true;
				exportErrors.emplace_back(std::format("Method '{}' has invalid param type '{}' at index {} when it should have '{}'", method.GetFunctionName(), ValueUtils::ToString(methodParamType), i, ValueUtils::ToString(paramType)));
				continue;
			}
		}

		if (methodFail)
			continue;

		auto data = std::make_unique<HandleData>(type.GetHandle(), methodInfo.GetHandle());

		JitCallback callback(_rt);
		MemAddr methodAddr = callback.GetJitFunc(method, &InternalCall, data.get());
		if (!methodAddr) {
			exportErrors.emplace_back(std::format("Method '{}' has JIT generation error: {}", method.GetFunctionName(), callback.GetError()));
			continue;
		}
		_functions.emplace_back(std::move(callback), std::move(data));

		methods.emplace_back(method, methodAddr);
	}

	if (!exportErrors.empty()) {
		std::string errorString = "Methods export error(s): " + exportErrors[0];
		for (auto it = std::next(exportErrors.begin()); it != exportErrors.end(); ++it) {
			std::format_to(std::back_inserter(errorString), ", {}", *it);
		}
		return ErrorData{ std::move(errorString) };
	}

	const auto [it, result] = _scripts.try_emplace(plugin.GetId(), plugin, assembly.GetAssemblyID(), pluginClassType);
	if (!result) {
		return ErrorData{ std::format("Save plugin data to map unsuccessful") };
	}

	const auto& [_, script] = *it;
	return LoadResultData{ std::move(methods), &script, { script.HasUpdate(), script.HasStart(), script.HasEnd(), !exportedMethods.empty() } };
}

void DotnetLanguageModule::OnPluginStart(PluginHandle plugin) {
	plugin.GetData().RCast<ScriptInstance*>()->InvokeOnStart();
}

void DotnetLanguageModule::OnPluginUpdate(PluginHandle plugin, DateTime dt) {
	plugin.GetData().RCast<ScriptInstance*>()->InvokeOnUpdate(dt.AsSeconds());
}

void DotnetLanguageModule::OnPluginEnd(PluginHandle plugin) {
	plugin.GetData().RCast<ScriptInstance*>()->InvokeOnEnd();
}

bool DotnetLanguageModule::IsDebugBuild() {
	return NETLM_IS_DEBUG;
}

void DotnetLanguageModule::OnMethodExport(PluginHandle plugin) {
	auto className = std::format("{}.{}", plugin.GetName(), plugin.GetName());

	ScriptInstance* script = FindScript(plugin.GetId());
	if (script) {
		auto& assemblyId = script->GetAssemblyId();
		// Add as C# calls (direct)
		auto& ownerAssembly = _loader.FindAssembly(assemblyId);
		assert(ownerAssembly);

		for (const auto& [method, _] : plugin.GetMethods()) {
			auto separated= Utils::Split(method.GetFunctionName(), ".");
			size_t size = separated.size();

			bool noNamespace = (size == 2);
			assert(size == 3 || noNamespace);
			Type& type = ownerAssembly.GetType(noNamespace ? separated[size-2] : std::string_view(separated[0].data(), separated[size-1].data() - 1));
			assert(type);
			MethodInfo methodInfo = type.GetMethod(separated[size-1]);
			assert(methodInfo);

			void* addr = methodInfo.GetFunctionAddress();

			for (auto& assembly : _loader.GetLoadedAssemblies()) {
				// No self export
				if (assembly.GetAssemblyID() == assemblyId)
					continue;

				assembly.AddInternalCall(className, method.GetName(), addr);
			}
		}

	} else {
		// Add as C++ calls
		for (const auto& [method, addr] : plugin.GetMethods()) {
			auto variableName = std::format("__{}", method.GetName());
			for (auto& assembly : _loader.GetLoadedAssemblies()) {
				assembly.AddInternalCall(className, variableName, addr);
			}
		}
	}

	constexpr bool warnOnMissing = NETLM_IS_DEBUG;

	for (auto& assembly : _loader.GetLoadedAssemblies()) {
		assembly.UploadInternalCalls(warnOnMissing);
	}
}

ScriptInstance* DotnetLanguageModule::FindScript(UniqueId pluginId) {
	auto it = _scripts.find(pluginId);
	if (it != _scripts.end())
		return &std::get<ScriptInstance>(*it);
	return nullptr;
}

MethodHandle DotnetLanguageModule::FindMethod(std::string_view name) {
	auto separated = Utils::Split(name, ".");
	if (separated.size() != 2)
		return {};

	auto plugin = _provider->FindPlugin(separated[0]);
	if (plugin) {
		for (const auto& method : plugin.GetDescriptor().GetExportedMethods()) {
			if (auto prototype = method.FindPrototype(separated[1])) {
				return prototype;
			}
		}
	}
	return {};
}

template<typename TFunc>
static void ManagedCall(MethodHandle method, MemAddr data, const JitCallback::Parameters* p, size_t count, const JitCallback::Return* ret, TFunc&& func) {
	PropertyHandle retProp = method.GetReturnType();
	ValueType retType = retProp.GetType();
	std::span<const PropertyHandle> paramProps = method.GetParamTypes();

	ArgumentList args;
	args.reserve(count);

	for (size_t i = 0; i < count; ++i) {
		const auto& param = paramProps[i];
		if (param.IsReference()) {
			args.emplace_back(p->GetArgument<void*>(i));
		} else {
			switch (param.GetType()) {
				// Value types
				case ValueType::Bool:
				case ValueType::Char8:
				case ValueType::Char16:
				case ValueType::Int8:
				case ValueType::Int16:
				case ValueType::Int32:
				case ValueType::Int64:
				case ValueType::UInt8:
				case ValueType::UInt16:
				case ValueType::UInt32:
				case ValueType::UInt64:
				case ValueType::Pointer:
				case ValueType::Float:
				case ValueType::Double:
					args.emplace_back(p->GetArgumentPtr(i));
					break;
				// Ref types
				case ValueType::Function:
				case ValueType::Vector2:
				case ValueType::Vector3:
				case ValueType::Vector4:
				case ValueType::Matrix4x4:
				case ValueType::String:
				case ValueType::Any:
				case ValueType::ArrayBool:
				case ValueType::ArrayChar8:
				case ValueType::ArrayChar16:
				case ValueType::ArrayInt8:
				case ValueType::ArrayInt16:
				case ValueType::ArrayInt32:
				case ValueType::ArrayInt64:
				case ValueType::ArrayUInt8:
				case ValueType::ArrayUInt16:
				case ValueType::ArrayUInt32:
				case ValueType::ArrayUInt64:
				case ValueType::ArrayPointer:
				case ValueType::ArrayFloat:
				case ValueType::ArrayDouble:
				case ValueType::ArrayString:
				case ValueType::ArrayAny:
				case ValueType::ArrayVector2:
				case ValueType::ArrayVector3:
				case ValueType::ArrayVector4:
				case ValueType::ArrayMatrix4x4:
					args.emplace_back(p->GetArgument<void*>(i));
					break;
				default:
					std::puts(LOG_PREFIX "Unsupported types!\n");
					std::terminate();
					break;
			}
		}
	}

	switch (retType) {
		case ValueType::String:
			ret->ConstructAt<plg::string>();
			break;
		case ValueType::Any:
			ret->ConstructAt<plg::any>();
			break;
		case ValueType::ArrayBool:
			ret->ConstructAt<plg::vector<bool>>();
			break;
		case ValueType::ArrayChar8:
			ret->ConstructAt<plg::vector<char>>();
			break;
		case ValueType::ArrayChar16:
			ret->ConstructAt<plg::vector<char16_t>>();
			break;
		case ValueType::ArrayInt8:
			ret->ConstructAt<plg::vector<int8_t>>();
			break;
		case ValueType::ArrayInt16:
			ret->ConstructAt<plg::vector<int16_t>>();
			break;
		case ValueType::ArrayInt32:
			ret->ConstructAt<plg::vector<int32_t>>();
			break;
		case ValueType::ArrayInt64:
			ret->ConstructAt<plg::vector<int64_t>>();
			break;
		case ValueType::ArrayUInt8:
			ret->ConstructAt<plg::vector<uint8_t>>();
			break;
		case ValueType::ArrayUInt16:
			ret->ConstructAt<plg::vector<uint16_t>>();
			break;
		case ValueType::ArrayUInt32:
			ret->ConstructAt<plg::vector<uint32_t>>();
			break;
		case ValueType::ArrayUInt64:
			ret->ConstructAt<plg::vector<uint64_t>>();
			break;
		case ValueType::ArrayPointer:
			ret->ConstructAt<plg::vector<uintptr_t>>();
			break;
		case ValueType::ArrayFloat:
			ret->ConstructAt<plg::vector<float>>();
			break;
		case ValueType::ArrayDouble:
			ret->ConstructAt<plg::vector<double>>();
			break;
		case ValueType::ArrayString:
			ret->ConstructAt<plg::vector<plg::string>>();
			break;
		case ValueType::ArrayAny:
			ret->ConstructAt<plg::vector<plg::any>>();
			break;
		case ValueType::ArrayVector2:
			ret->ConstructAt<plg::vector<plg::vec2>>();
			break;
		case ValueType::ArrayVector3:
			ret->ConstructAt<plg::vector<plg::vec3>>();
			break;
		case ValueType::ArrayVector4:
			ret->ConstructAt<plg::vector<plg::vec4>>();
			break;
		case ValueType::ArrayMatrix4x4:
			ret->ConstructAt<plg::vector<plg::mat4x4>>();
			break;
		default:
			break;
	}

	if (retType != ValueType::Void) {
		func(data, args, ret->GetReturnPtr());
	} else {
		func(data, args, std::nullopt);
	}
}

// C++ to C#
void DotnetLanguageModule::InternalCall(MethodHandle method, MemAddr data, const JitCallback::Parameters* p, size_t count, const JitCallback::Return* ret) {
	ManagedCall(method, data, p, count, ret, [](MemAddr dt, ArgumentList& args, std::optional<void*> retPtr) {
		const auto& [typeHandle, methodHandle] = *dt.RCast<HandleData*>();
		Type type(typeHandle);
		if (retPtr.has_value()) {
			type.InvokeStaticMethodRetInternal(methodHandle, args.data(), args.size(), *retPtr);
		} else {
			type.InvokeStaticMethodInternal(methodHandle, args.data(), args.size());
		}
	});
}

// C++ to C#
void DotnetLanguageModule::DelegateCall(MethodHandle method, MemAddr data, const JitCallback::Parameters* p, size_t count, const JitCallback::Return* ret) {
	ManagedCall(method, data, p, count, ret, [](MemAddr dt, ArgumentList& args, std::optional<void*> retPtr) {
		ManagedHandle delegateHandle = dt.CCast<ManagedHandle>();
		if (retPtr.has_value()) {
			ManagedObject::InvokeDelegateRetInternal(delegateHandle, args.data(), args.size(), *retPtr);
		} else {
			ManagedObject::InvokeDelegateInternal(delegateHandle, args.data(), args.size());
		}
	});
}

/*_________________________________________________*/

void DotnetLanguageModule::ExceptionCallback(std::string_view message) {
	if (!g_netlm._provider)
		return;

	g_netlm._provider->Log(std::format(LOG_PREFIX "[Exception] {}", message), Severity::Error);

	std::stringstream stream;
	cpptrace::generate_trace().print(stream);
	g_netlm._provider->Log(stream.str(), Severity::Error);
}

void DotnetLanguageModule::MessageCallback(std::string_view message, MessageLevel level) {
	if (!g_netlm._provider)
		return;

	Severity severity;
	switch (level) {
		case MessageLevel::Info:
			severity = Severity::Info;
			break;
		case MessageLevel::Warning:
			severity = Severity::Warning;
			break;
		case MessageLevel::Error:
			severity = Severity::Error;
			break;
		default:
			return;
	}

	g_netlm._provider->Log(std::format(LOG_PREFIX "{}", message), severity);
}

/*_________________________________________________*/

ScriptInstance::ScriptInstance(PluginHandle plugin, ManagedGuid assembly, Type& type) : _plugin{plugin}, _assembly{assembly}, _instance{type.CreateInstance()} {
	PluginDescriptorHandle desc = plugin.GetDescriptor();
	std::span<const PluginReferenceDescriptorHandle> dependencies = desc.GetDependencies();

	// use plg::string as currently plugify use custom string implementation

	plg::vector<plg::string> deps;
	deps.reserve(dependencies.size());
	for (const auto& dependency : dependencies) {
		deps.emplace_back(dependency.GetName());
	}

	_instance.SetPropertyValue("Id", plg::to_string(plugin.GetId()));
	_instance.SetPropertyValue("Name", plg::string(plugin.GetName()));
	_instance.SetPropertyValue("FullName", plg::string(plugin.GetFriendlyName()));
	_instance.SetPropertyValue("Description", plg::string(desc.GetDescription()));
	_instance.SetPropertyValue("Version", plg::string(desc.GetVersionName()));
	_instance.SetPropertyValue("Author", plg::string(desc.GetCreatedBy()));
	_instance.SetPropertyValue("Website", plg::string(desc.GetCreatedByURL()));
	_instance.SetPropertyValue("BaseDir", plg::string(NETLM_UTF8(plugin.GetBaseDir())));
	_instance.SetPropertyValue("ConfigsDir", plg::string(NETLM_UTF8(plugin.GetConfigsDir())));
	_instance.SetPropertyValue("DataDir", plg::string(NETLM_UTF8(plugin.GetDataDir())));
	_instance.SetPropertyValue("LogsDir", plg::string(NETLM_UTF8(plugin.GetLogsDir())));
	_instance.SetPropertyValue("Dependencies", deps);

	_update = _instance.GetType().GetMethod("OnPluginUpdate");
	_start = _instance.GetType().GetMethod("OnPluginStart");
	_end = _instance.GetType().GetMethod("OnPluginEnd");
}

ScriptInstance::~ScriptInstance() {
	_instance.Destroy();
};

void ScriptInstance::InvokeOnStart() const {
	_instance.InvokeMethodRaw(_start);
}

void ScriptInstance::InvokeOnUpdate(float dt) const {
	_instance.InvokeMethodRaw(_update, dt);
}

void ScriptInstance::InvokeOnEnd() const {
	_instance.InvokeMethodRaw(_end);
}

bool ScriptInstance::HasStart() const {
	return _start;
}

bool ScriptInstance::HasUpdate() const {
	return _update;
}

bool ScriptInstance::HasEnd() const {
	return _end;
}

namespace netlm {
	DotnetLanguageModule g_netlm;
}

extern "C" {
	NETLM_EXPORT bool IsModuleLoaded(const char* moduleName, const char* versionName, bool minimum) {
		if (std::string_view version = versionName; !version.empty())
			return g_netlm.GetProvider()->IsModuleLoaded(moduleName, plg::version(version), minimum);
		else
			return g_netlm.GetProvider()->IsModuleLoaded(moduleName, std::nullopt, minimum);
	}

	NETLM_EXPORT bool IsPluginLoaded(const char* pluginName, const char* versionName, bool minimum) {
		if (std::string_view version = versionName; !version.empty())
			return g_netlm.GetProvider()->IsPluginLoaded(pluginName, plg::version(version), minimum);
		else
			return g_netlm.GetProvider()->IsPluginLoaded(pluginName, std::nullopt, minimum);
	}

	NETLM_EXPORT const char* FindPluginResource(UniqueId pluginId, const char* path) {
		ScriptInstance* script = g_netlm.FindScript(pluginId);
		if (script) {
			auto resource = script->GetPlugin().FindResource(NETLM_PSTR(path));
			if (resource.has_value()) {
				return Memory::StringToHGlobalAnsi(NETLM_UTF8(*resource));
			}
		}
		return nullptr;
	}

	NETLM_EXPORT ILanguageModule* GetLanguageModule() {
		return &g_netlm;
	}
}
