#include "module.h"
#include "managed_assembly.h"
#include "managed_functions.h"
#include "method_info.h"
#include "attribute.h"
#include "type.h"
#include "memory.h"
#include "utils.h"

#include <module_export.h>

#include <plugify/compat_format.h>
#include <plugify/language_module.h>
#include <plugify/log.h>
#include <plugify/module.h>
#include <plugify/string.h>
#include <plugify/plugify_provider.h>
#include <plugify/plugin.h>
#include <plugify/plugin_descriptor.h>
#include <plugify/plugin_reference_descriptor.h>
#include <thread>

#if NETLM_PLATFORM_WINDOWS
#undef FindResource
#endif

#define LOG_PREFIX "[NETLM] "

using namespace plugify;
using namespace netlm;

InitResult DotnetLanguageModule::Initialize(std::weak_ptr<IPlugifyProvider> provider, ModuleRef module) {
	if (!(_provider = provider.lock())) {
		return ErrorData{ "Provider not exposed" };
	}

	fs::path baseDir(module.GetBaseDir());

	if (!_host.Initialize({
		.hostfxrPath = baseDir / "dotnet/host/fxr/8.0.3/" NETLM_LIBRARY_PREFIX "hostfxr" NETLM_LIBRARY_SUFFIX,
		.rootDirectory = baseDir / "api",
		.messageCallback = MessageCallback,
		.exceptionCallback = ExceptionCallback,
	})) {
		return ErrorData{ "Could not initialize hostfxr environment" };
	}

	_alc = _host.CreateAssemblyLoadContext("PlugifyContext");

	_provider->Log(LOG_PREFIX "Inited!", Severity::Debug);

	_rt = std::make_shared<asmjit::JitRuntime>();

	return InitResultData{};
}

void DotnetLanguageModule::Shutdown() {
	_scripts.clear();
	_functions.clear();

	_host.UnloadAssemblyLoadContext(_alc);
	_host.Shutdown();

	DetectLeaks();

	_rt.reset();
	_provider.reset();
}

LoadResult DotnetLanguageModule::OnPluginLoad(PluginRef plugin) {
	fs::path assemblyPath(plugin.GetBaseDir());
	assemblyPath /= plugin.GetDescriptor().GetEntryPoint();

	ManagedAssembly& assembly = _alc.LoadAssembly(assemblyPath);
	if (!assembly) {
		return ErrorData{ _alc.GetError() };
	}

	Type& pluginClassType = assembly.GetTypeByBaseType("Plugify.Plugin");
	if (!pluginClassType) {
		return ErrorData{"Failed to find 'Plugify.Plugin' class implementation"};
	}

	std::vector<std::string> methodErrors;

	std::span<const MethodRef> exportedMethods = plugin.GetDescriptor().GetExportedMethods();
	std::vector<MethodData> methods;
	methods.reserve(exportedMethods.size());

	for (const auto& method : exportedMethods) {
		auto separated = Utils::Split(method.GetFunctionName(), ".");
		size_t size = separated.size();
		bool noNamespace = (size == 2);
		if (size != 3 && !noNamespace) {
			methodErrors.emplace_back(std::format("Invalid function format: '{}'. Please provide name in that format: 'Namespace.Class.Method' or 'Namespace.MyParentClass+MyNestedClass.Method' or 'Class.Method'", method.GetFunctionName()));
			continue;
		}

		Type& type = assembly.GetType(noNamespace ? separated[size-2] : std::string_view(separated[0].data(), separated[size-1].data() - 1));
		if (!type) {
			methodErrors.emplace_back(std::format("Failed to find class '{}'", method.GetFunctionName()));
			continue;
		}

		MethodInfo methodInfo = type.GetMethod(separated[size-1]);
		if (!methodInfo) {
			methodErrors.emplace_back(std::format("Failed to find method '{}'", method.GetFunctionName()));
			continue;
		}

		ValueType returnType = methodInfo.GetReturnType().GetManagedType().type;
		ValueType methodReturnType = method.GetReturnType().GetType();
		if (returnType != methodReturnType) {
			methodErrors.emplace_back(std::format("Method '{}' has invalid return type '{}' when it should have '{}'", method.GetFunctionName(), ValueUtils::ToString(methodReturnType), ValueUtils::ToString(returnType)));
			continue;
		}

		const auto& parameterTypes = methodInfo.GetParameterTypes();

		size_t paramCount = parameterTypes.size();
		std::span<const PropertyRef> paramTypes = method.GetParamTypes();
		if (paramCount != paramTypes.size()) {
			methodErrors.emplace_back(std::format("Method '{}' has invalid parameter count {} when it should have {}", method.GetFunctionName(), paramTypes.size(), paramCount));
			continue;
		}

		bool methodFail = false;

		for (size_t i = 0; i < paramCount; ++i) {
			ValueType paramType = parameterTypes[i].GetManagedType().type;
			ValueType methodParamType = paramTypes[i].GetType();
			if (paramType != methodParamType) {
				methodFail = true;
				methodErrors.emplace_back(std::format("Method '{}' has invalid param type '{}' at index {} when it should have '{}'", method.GetFunctionName(), ValueUtils::ToString(methodParamType), i, ValueUtils::ToString(paramType)));
				continue;
			}
		}

		if (methodFail)
			continue;

		auto data = std::make_unique<HandleData>(type.GetHandle(), methodInfo.GetHandle());

		JitCallback callback(_rt);
		MemAddr methodAddr = callback.GetJitFunc(method, &InternalCall, data.get());
		if (!methodAddr) {
			methodErrors.emplace_back(std::format("Method '{}' has JIT generation error: {}", method.GetFunctionName(), callback.GetError()));
			continue;
		}
		_functions.emplace_back(std::move(callback), std::move(data));

		methods.emplace_back(method, methodAddr);
	}

	if (!methodErrors.empty()) {
		std::string funcs(methodErrors[0]);
		for (auto it = std::next(methodErrors.begin()); it != methodErrors.end(); ++it) {
			std::format_to(std::back_inserter(funcs), ", {}", *it);
		}
		return ErrorData{ std::format("Not found {} method function(s)", funcs) };
	}

	const auto [_, result] = _scripts.try_emplace(plugin.GetId(), plugin,  assembly.GetAssemblyID(), pluginClassType);
	if (!result) {
		return ErrorData{ "Plugin key duplicate" };
	}

	return LoadResultData{ std::move(methods) };
}

void DotnetLanguageModule::OnPluginStart(PluginRef plugin) {
	ScriptInstance* script = FindScript(plugin.GetId());
	if (script) {
		script->InvokeOnStart();
	}
}

void DotnetLanguageModule::OnPluginEnd(PluginRef plugin) {
	ScriptInstance* script = FindScript(plugin.GetId());
	if (script) {
		script->InvokeOnEnd();
	}
}

bool DotnetLanguageModule::IsDebugBuild() {
	return NETLM_IS_DEBUG;
}

void DotnetLanguageModule::OnMethodExport(PluginRef plugin) {
	auto className = std::format("{}.{}", plugin.GetName(), plugin.GetName());

	ScriptInstance* script = FindScript(plugin.GetId());
	if (script) {
		auto& assemblyId = script->GetAssemblyId();
		// Add as C# calls (direct)
		auto& ownerAssembly = _alc.FindAssembly(assemblyId);
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

			for (auto& [id, assembly] : _alc.GetLoadedAssemblies()) {
				// No self export
				if (id == assemblyId)
					continue;

				assembly.AddInternalCall(className, method.GetName(), addr);
			}
		}

	} else {
		// Add as C++ calls
		for (const auto& [method, addr] : plugin.GetMethods()) {
			auto variableName = std::format("__{}", method.GetName());
			for (auto& [_, assembly] : _alc.GetLoadedAssemblies()) {
				assembly.AddInternalCall(className, variableName, addr);
			}
		}
	}

	const bool warnOnMissing = NETLM_IS_DEBUG;

	for (auto& [_, assembly] : _alc.GetLoadedAssemblies()) {
		assembly.UploadInternalCalls(warnOnMissing);
	}
}

ScriptInstance* DotnetLanguageModule::FindScript(UniqueId pluginId) {
	auto it = _scripts.find(pluginId);
	if (it != _scripts.end())
		return &std::get<ScriptInstance>(*it);
	return nullptr;
}

MethodRef DotnetLanguageModule::FindMethod(ManagedGuid assemblyId, std::string_view name) {
	for (const auto & [_, script] : _scripts) {
		if (script.GetAssemblyId() == assemblyId) {
			for (const auto& method : script.GetPlugin().GetDescriptor().GetExportedMethods()) {
				auto prototype = method.FindPrototype(name);
				if (prototype.has_value()) {
					return *prototype;
				}
			}
			return {};
		}
	}
	return {};
}

template<typename TFunc>
static void ManagedCall(MethodRef method, MemAddr data, const JitCallback::Parameters* p, uint8_t count, const JitCallback::Return* ret, TFunc&& func) {
	PropertyRef retProp = method.GetReturnType();
	ValueType retType = retProp.GetType();
	std::span<const PropertyRef> paramProps = method.GetParamTypes();

	bool hasRet = ValueUtils::IsHiddenParam(retType);

	ArgumentList args;
	args.reserve(hasRet ? count - 1 : count);

	for (uint8_t i = hasRet, j = 0; i < count; ++i, ++j) {
		const auto& param = paramProps[j];
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
					args.emplace_back(p->GetArgument<void*>(i));
					break;
				default:
					std::puts(LOG_PREFIX "Unsupported types!\n");
					std::terminate();
					break;
			}
		}
	}

	void* retPtr = hasRet ? p->GetArgument<void*>(0) : ret->GetReturnPtr();

	if (hasRet) {
		switch (retType) {
			case ValueType::String:
				std::construct_at(reinterpret_cast<plg::string*>(retPtr), plg::string());
				break;
			case ValueType::ArrayBool:
				std::construct_at(reinterpret_cast<std::vector<bool>*>(retPtr), std::vector<bool>());
				break;
			case ValueType::ArrayChar8:
				std::construct_at(reinterpret_cast<std::vector<char>*>(retPtr), std::vector<char>());
				break;
			case ValueType::ArrayChar16:
				std::construct_at(reinterpret_cast<std::vector<char16_t>*>(retPtr), std::vector<char16_t>());
				break;
			case ValueType::ArrayInt8:
				std::construct_at(reinterpret_cast<std::vector<int8_t>*>(retPtr), std::vector<int8_t>());
				break;
			case ValueType::ArrayInt16:
				std::construct_at(reinterpret_cast<std::vector<int16_t>*>(retPtr), std::vector<int16_t>());
				break;
			case ValueType::ArrayInt32:
				std::construct_at(reinterpret_cast<std::vector<int32_t>*>(retPtr), std::vector<int32_t>());
				break;
			case ValueType::ArrayInt64:
				std::construct_at(reinterpret_cast<std::vector<int64_t>*>(retPtr), std::vector<int64_t>());
				break;
			case ValueType::ArrayUInt8:
				std::construct_at(reinterpret_cast<std::vector<uint8_t>*>(retPtr), std::vector<uint8_t>());
				break;
			case ValueType::ArrayUInt16:
				std::construct_at(reinterpret_cast<std::vector<uint16_t>*>(retPtr), std::vector<uint16_t>());
				break;
			case ValueType::ArrayUInt32:
				std::construct_at(reinterpret_cast<std::vector<uint32_t>*>(retPtr), std::vector<uint32_t>());
				break;
			case ValueType::ArrayUInt64:
				std::construct_at(reinterpret_cast<std::vector<uint64_t>*>(retPtr), std::vector<uint64_t>());
				break;
			case ValueType::ArrayPointer:
				std::construct_at(reinterpret_cast<std::vector<uintptr_t>*>(retPtr), std::vector<uintptr_t>());
				break;
			case ValueType::ArrayFloat:
				std::construct_at(reinterpret_cast<std::vector<float>*>(retPtr), std::vector<float>());
				break;
			case ValueType::ArrayDouble:
				std::construct_at(reinterpret_cast<std::vector<double>*>(retPtr), std::vector<double>());
				break;
			case ValueType::ArrayString:
				std::construct_at(reinterpret_cast<std::vector<plg::string>*>(retPtr), std::vector<plg::string>());
				break;
			default:
				break;
		}
	}

	if (retType != ValueType::Void) {
		func(data, args, retPtr);
	} else {
		func(data, args, std::nullopt);
	}

	if (hasRet) {
		ret->SetReturn(retPtr);
	}
}

// C++ to C#
void DotnetLanguageModule::InternalCall(MethodRef method, MemAddr data, const JitCallback::Parameters* p, uint8_t count, const JitCallback::Return* ret) {
	ManagedCall(method, data, p, count, ret, [](MemAddr dt, ArgumentList& args, std::optional<void*> retPtr) {
		const auto& [typeHandle, methodHandle] = *dt.CCast<HandleData*>();
		Type type(typeHandle);
		if (retPtr.has_value()) {
			type.InvokeStaticMethodRetInternal(methodHandle, args.data(), args.size(), *retPtr);
		} else {
			type.InvokeStaticMethodInternal(methodHandle, args.data(), args.size());
		}
	});
}

// C++ to C#
void DotnetLanguageModule::DelegateCall(MethodRef method, MemAddr data, const JitCallback::Parameters* p, uint8_t count, const JitCallback::Return* ret) {
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
	g_netlm._provider->Log(stream.str(), Severity::Debug);
}

void DotnetLanguageModule::MessageCallback(std::string_view message, MessageLevel level) {
	if (!g_netlm._provider)
		return;

	plugify::Severity severity;
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

extern std::map<type_index, int32_t> g_numberOfMalloc;
extern std::map<type_index, int32_t> g_numberOfAllocs;
extern std::string_view GetTypeName(type_index type);

void DotnetLanguageModule::DetectLeaks() {
	for (const auto& [type, count] : g_numberOfMalloc) {
		if (count > 0) {
			g_netlm._provider->Log(std::format(LOG_PREFIX "Memory leaks detected: {} allocations. Related to {}!", count, GetTypeName(type)), Severity::Error);
		}
	}

	for (const auto& [type, count] : g_numberOfAllocs) {
		if (count > 0) {
			g_netlm._provider->Log(std::format(LOG_PREFIX "Memory leaks detected: {} allocations. Related to {}!", count, GetTypeName(type)), Severity::Error);
		}
	}

	g_numberOfMalloc.clear();
	g_numberOfAllocs.clear();
}

/*_________________________________________________*/

ScriptInstance::ScriptInstance(PluginRef plugin, ManagedGuid assembly, Type& type) : _plugin{plugin}, _assembly{assembly}, _instance{type.CreateInstance()} {
	PluginDescriptorRef desc = plugin.GetDescriptor();
	std::span<const PluginReferenceDescriptorRef> dependencies = desc.GetDependencies();

	// use plg::string as currently plugify use custom string implementation

	std::vector<plg::string> deps;
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
	_instance.SetPropertyValue("Dependencies", deps);
}

ScriptInstance::~ScriptInstance() {
	_instance.Destroy();
};

void ScriptInstance::InvokeOnStart() const {
	MethodInfo onStartMethod = _instance.GetType().GetMethod("OnStart");
	if (onStartMethod) {
		_instance.InvokeMethodRaw(onStartMethod);
	}
}

void ScriptInstance::InvokeOnEnd() const {
	MethodInfo onEndMethod = _instance.GetType().GetMethod("OnEnd");
	if (onEndMethod) {
		_instance.InvokeMethodRaw(onEndMethod);
	}
}

namespace netlm {
	DotnetLanguageModule g_netlm;
}

extern "C" {
	NETLM_EXPORT const char* GetBaseDir() {
		return Memory::StringToHGlobalAnsi(NETLM_UTF8(g_netlm.GetProvider()->GetBaseDir()));
	}

	NETLM_EXPORT bool IsModuleLoaded(const char* moduleName, int version, bool minimum) {
		auto requiredVersion = (version >= 0 && version != INT_MAX) ? std::make_optional(version) : std::nullopt;
		return g_netlm.GetProvider()->IsModuleLoaded(moduleName, requiredVersion, minimum);
	}

	NETLM_EXPORT bool IsPluginLoaded(const char* pluginName, int version, bool minimum) {
		auto requiredVersion = (version >= 0 && version != INT_MAX) ? std::make_optional(version) : std::nullopt;
		return g_netlm.GetProvider()->IsPluginLoaded(pluginName, requiredVersion, minimum);
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