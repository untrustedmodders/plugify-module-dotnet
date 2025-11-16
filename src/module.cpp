#include "module.hpp"
#include "attribute.hpp"
#include "managed_assembly.hpp"
#include "managed_functions.hpp"
#include "memory.hpp"
#include "method_info.hpp"
#include "type.hpp"
#include "utils.hpp"

#include <plg/format.hpp>
#include <plg/version.hpp>
#include <plg/string.hpp>
#include <plg/any.hpp>
#include <plg/vector.hpp>

#if __has_include(<stacktrace>)
#include <stacktrace>
#define HAS_STACKTRACE 1
#else
#define HAS_STACKTRACE 0
#endif

#include <exception>
#include <module_export.h>

#define LOG_PREFIX "[NETLM] "

using namespace plugify;
using namespace netlm;

Result<InitData> DotnetLanguageModule::Initialize(const Provider& provider, const Extension& module) {
	_provider = std::make_unique<Provider>(provider);

	auto result = _host.Initialize({
		.hostfxrPath = module.GetLocation() / "dotnet/host/fxr/10.0.0/" NETLM_LIBRARY_PREFIX "hostfxr" NETLM_LIBRARY_SUFFIX,
		.rootDirectory = module.GetLocation() / "api",
		.messageCallback = MessageCallback,
		.exceptionCallback = ExceptionCallback,
	});
	if (!result) {
		return MakeError(std::move(result.error()));
	}

	_provider->Log(LOG_PREFIX "Inited!", Severity::Debug);

	return InitData{{ .hasUpdate = false }};
}

void DotnetLanguageModule::Shutdown() {
	_scripts.clear();
	_functions.clear();

	_loader.Unload();
	_host.Shutdown();

	_provider.reset();
}

void DotnetLanguageModule::OnUpdate([[maybe_unused]] std::chrono::milliseconds dt) {
}

Result<SharpMethodData> DotnetLanguageModule::GenerateMethodExport(const Method &method, ManagedAssembly &assembly) {
	auto separated = Utils::Split(method.GetFuncName(), ".");
	size_t size = separated.size();
	bool noNamespace = (size == 2);
	if (size != 3 && !noNamespace) {
		return MakeError("invalid function format: '{}'. Provide name in that format: 'Namespace.Class.Method' or 'Namespace.MyParentClass+MyNestedClass.Method' or 'Class.Method'", method.GetFuncName());
	}

	std::string_view className = noNamespace ? separated[size-2] : std::string_view(separated[0].data(), separated[size-1].data() - 1);
	Type& type = assembly.GetType(className);
	if (!type) {
		return MakeError("failed to find class '{}'", className);
	}

	std::string_view methodName = separated[size-1];
	MethodInfo methodInfo = type.GetMethod(methodName);
	if (!methodInfo) {
		return MakeError("failed to find method '{}'", methodName);
	}

	ValueType returnType = methodInfo.GetReturnType().GetManagedType().type;
	ValueType methodReturnType = method.GetRetType().GetType();
	if (returnType != methodReturnType) {
		return MakeError("invalid return type '{}' when it should have '{}'", plg::enum_to_string(methodReturnType), plg::enum_to_string(returnType));
	}

	const auto& parameterTypes = methodInfo.GetParameterTypes();

	size_t paramCount = parameterTypes.size();
	const std::inplace_vector<Property, Signature::kMaxFuncArgs>& paramTypes = method.GetParamTypes();
	if (paramCount != paramTypes.size()) {
		return MakeError("invalid parameter count {} when it should have {}", paramTypes.size(), paramCount);
	}

	for (size_t i = 0; i < paramCount; ++i) {
		ValueType paramType = parameterTypes[i].GetManagedType().type;
		ValueType methodParamType = paramTypes[i].GetType();
		if (paramType != methodParamType) {
			return MakeError("invalid param type '{}' at index {} when it should have '{}'", plg::enum_to_string(methodParamType), i, plg::enum_to_string(paramType));
		}
	}

	auto data = std::make_unique<HandleData>(type.GetHandle(), methodInfo.GetHandle());

	JitCallback callback{};
	MemAddr methodAddr = callback.GetJitFunc(method, &InternalCall, data.get());
	if (!methodAddr) {
		return MakeError("jit error: {}", callback.GetError());
	}

	return SharpMethodData{ std::move(callback), std::move(data) };
}

Result<LoadData> DotnetLanguageModule::OnPluginLoad(const Extension& plugin) {
	std::filesystem::path assemblyPath(plugin.GetLocation());
	assemblyPath /= plugin.GetEntry();

	ManagedAssembly& assembly = _loader.LoadAssembly(assemblyPath);
	if (!assembly) {
		return MakeError(_loader.GetError());
	}

	Type& pluginClassType = assembly.GetTypeByBaseType("Plugify.Plugin");
	if (!pluginClassType) {
		return MakeError("Failed to find 'Plugify.Plugin' class implementation");
	}

	std::vector<std::string> exportErrors;

	const std::vector<Method>& exportedMethods = plugin.GetMethods();
	std::vector<MethodData> methods;
	methods.reserve(exportedMethods.size());

	for (size_t i = 0; i < exportedMethods.size(); ++i) {
		const auto& method = exportedMethods[i];
		Result<SharpMethodData> generateResult = GenerateMethodExport(method, assembly);
		if (!generateResult) {
			exportErrors.emplace_back(std::format("{:>3}. {} {}", i + 1, method.GetName(), generateResult.error()));
			if (constexpr size_t kMaxDisplay = 100; exportErrors.size() >= kMaxDisplay) {
				exportErrors.emplace_back(std::format("... and {} more", exportedMethods.size() - kMaxDisplay));
				break;
			}
			continue;
		}
		methods.emplace_back(method, generateResult->jitCallback.GetFunction());
		_functions.emplace_back(std::move(*generateResult));
	}

	if (!exportErrors.empty()) {
		return MakeError("Invalid methods:\n{}", plg::join(exportErrors, "\n"));
	}

	const auto [it, result] = _scripts.try_emplace(plugin.GetId(), plugin, assembly.GetAssemblyID(), pluginClassType);
	if (!result) {
		return MakeError("Save plugin data to map unsuccessful");
	}

	const auto& [_, script] = *it;
	return LoadData{ std::move(methods), &script, { script.HasUpdate(), script.HasStart(), script.HasEnd(), !exportedMethods.empty() } };
}

void DotnetLanguageModule::OnPluginStart(const Extension& plugin) {
	plugin.GetUserData().RCast<ScriptInstance*>()->InvokeOnStart();
}

void DotnetLanguageModule::OnPluginUpdate(const Extension& plugin, std::chrono::milliseconds dt) {
	plugin.GetUserData().RCast<ScriptInstance*>()->InvokeOnUpdate(std::chrono::duration<float>(dt).count());
}

void DotnetLanguageModule::OnPluginEnd(const Extension& plugin) {
	plugin.GetUserData().RCast<ScriptInstance*>()->InvokeOnEnd();
}

bool DotnetLanguageModule::IsDebugBuild() {
	return NETLM_IS_DEBUG;
}

void DotnetLanguageModule::OnMethodExport(const Extension& plugin) {
	auto className = std::format("{}.{}", plugin.GetName(), plugin.GetName());

	if (auto* script = FindScript(plugin.GetId())) {
		auto& assemblyId = script->GetAssemblyId();
		// Add as C# calls (direct)
		auto& ownerAssembly = _loader.FindAssembly(assemblyId);
		assert(ownerAssembly);

		for (const auto& [method, _] : plugin.GetMethodsData()) {
			auto separated= Utils::Split(method.GetFuncName(), ".");
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
		for (const auto& [method, addr] : plugin.GetMethodsData()) {
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

std::shared_ptr<Method> DotnetLanguageModule::FindMethod(std::string_view name) const {
	if (auto separated = Utils::Split(name, "."); separated.size() == 2) {
		if (auto plugin = _provider->FindExtension(separated[0])) {
			for (const auto& method : plugin->GetMethods()) {
				if (auto prototype = method.FindPrototype(separated[1])) {
					return prototype;
				}
			}
		}
	}
	_provider->Log(std::format(LOG_PREFIX "FindMethod failed to find: '{}'", name), Severity::Error);
	return {};
}

template<typename TFunc>
static void ManagedCall(const Method& method, MemAddr data, uint64_t* p, size_t count, void* r, TFunc&& func) {
	ValueType retType = method.GetRetType().GetType();
	const std::inplace_vector<Property, Signature::kMaxFuncArgs>& paramProps = method.GetParamTypes();

	ParametersSpan params(p, count);
	ReturnSlot ret(r, ValueUtils::SizeOf(retType));
	
	ArgumentList args;
	args.reserve(count);

	for (size_t i = 0; i < count; ++i) {
		const auto& param = paramProps[i];
		if (param.IsRef()) {
			args.emplace_back(params.Get<void*>(i));
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
					args.emplace_back(&p[i]);
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
					args.emplace_back(params.Get<void*>(i));
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
			ret.Construct<plg::string>();
			break;
		case ValueType::Any:
			ret.Construct<plg::any>();
			break;
		case ValueType::ArrayBool:
			ret.Construct<plg::vector<bool>>();
			break;
		case ValueType::ArrayChar8:
			ret.Construct<plg::vector<char>>();
			break;
		case ValueType::ArrayChar16:
			ret.Construct<plg::vector<char16_t>>();
			break;
		case ValueType::ArrayInt8:
			ret.Construct<plg::vector<int8_t>>();
			break;
		case ValueType::ArrayInt16:
			ret.Construct<plg::vector<int16_t>>();
			break;
		case ValueType::ArrayInt32:
			ret.Construct<plg::vector<int32_t>>();
			break;
		case ValueType::ArrayInt64:
			ret.Construct<plg::vector<int64_t>>();
			break;
		case ValueType::ArrayUInt8:
			ret.Construct<plg::vector<uint8_t>>();
			break;
		case ValueType::ArrayUInt16:
			ret.Construct<plg::vector<uint16_t>>();
			break;
		case ValueType::ArrayUInt32:
			ret.Construct<plg::vector<uint32_t>>();
			break;
		case ValueType::ArrayUInt64:
			ret.Construct<plg::vector<uint64_t>>();
			break;
		case ValueType::ArrayPointer:
			ret.Construct<plg::vector<uintptr_t>>();
			break;
		case ValueType::ArrayFloat:
			ret.Construct<plg::vector<float>>();
			break;
		case ValueType::ArrayDouble:
			ret.Construct<plg::vector<double>>();
			break;
		case ValueType::ArrayString:
			ret.Construct<plg::vector<plg::string>>();
			break;
		case ValueType::ArrayAny:
			ret.Construct<plg::vector<plg::any>>();
			break;
		case ValueType::ArrayVector2:
			ret.Construct<plg::vector<plg::vec2>>();
			break;
		case ValueType::ArrayVector3:
			ret.Construct<plg::vector<plg::vec3>>();
			break;
		case ValueType::ArrayVector4:
			ret.Construct<plg::vector<plg::vec4>>();
			break;
		case ValueType::ArrayMatrix4x4:
			ret.Construct<plg::vector<plg::mat4x4>>();
			break;
		default:
			break;
	}

	if (retType != ValueType::Void) {
		func(data, args, r);
	} else {
		func(data, args, std::nullopt);
	}
}

// C++ to C#
void DotnetLanguageModule::InternalCall(const Method* method, MemAddr data, uint64_t* p, size_t count, void* ret) {
	ManagedCall(*method, data, p, count, ret, [](MemAddr dt, ArgumentList& args, std::optional<void*> retPtr) {
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
void DotnetLanguageModule::DelegateCall(const Method* method, MemAddr data, uint64_t* p, size_t count, void* ret) {
	ManagedCall(*method, data, p, count, ret, [](MemAddr dt, ArgumentList& args, std::optional<void*> retPtr) {
		auto delegateHandle = dt.CCast<ManagedHandle>();
		if (retPtr.has_value()) {
			ManagedObject::InvokeDelegateRetInternal(delegateHandle, args.data(), args.size(), *retPtr);
		} else {
			ManagedObject::InvokeDelegateInternal(delegateHandle, args.data(), args.size());
		}
	});
}

/*_________________________________________________*/

void DotnetLanguageModule::ExceptionCallback(std::string_view message) {
	if (const auto& provider = g_netlm.GetProvider()) {
		provider->Log(std::format(LOG_PREFIX "[Exception] {}", std::string_view(message)), Severity::Error);
#if HAS_STACKTRACE
		auto trace = std::stacktrace::current();
		provider->Log(std::to_string(trace), Severity::Error);
#endif		
	}
}

void DotnetLanguageModule::MessageCallback(std::string_view message, MessageLevel level) {
	if (const auto& provider = g_netlm.GetProvider()) {
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

		provider->Log(std::format(LOG_PREFIX "{}", message), severity);
	}
}

/*_________________________________________________*/

ScriptInstance::ScriptInstance(const Extension& plugin, ManagedGuid assembly, Type& type) : _plugin{plugin}, _assembly{assembly}, _instance{type.CreateInstance()} {
	const std::vector<Dependency>& dependencies = plugin.GetDependencies();

	// use plg::string as currently plugify use custom string implementation

	plg::vector<plg::string> deps;
	deps.reserve(dependencies.size());
	for (const auto& dependency : dependencies) {
		deps.emplace_back(dependency.GetName());
	}

	_instance.SetPropertyValue("Id", plg::to_string(plugin.GetId()));
	_instance.SetPropertyValue("Name", plg::string(plugin.GetName()));
	_instance.SetPropertyValue("Description", plg::string(plugin.GetDescription()));
	_instance.SetPropertyValue("Version", plg::string(plugin.GetVersionString()));
	_instance.SetPropertyValue("Author", plg::string(plugin.GetAuthor()));
	_instance.SetPropertyValue("Website", plg::string(plugin.GetWebsite()));
	_instance.SetPropertyValue("License", plg::string(plugin.GetLicense()));
	_instance.SetPropertyValue("Location", plg::string(plg::as_string(plugin.GetLocation())));
	_instance.SetPropertyValue("Dependencies", deps);

	// TODO
	_instance.SetPropertyValue("BaseDir", plg::string(plg::as_string(g_netlm.GetProvider()->GetBaseDir())));
	_instance.SetPropertyValue("ExtensionsDir", plg::string(plg::as_string(g_netlm.GetProvider()->GetExtensionsDir())));
	_instance.SetPropertyValue("ConfigsDir", plg::string(plg::as_string(g_netlm.GetProvider()->GetConfigsDir())));
	_instance.SetPropertyValue("DataDir", plg::string(plg::as_string(g_netlm.GetProvider()->GetDataDir())));
	_instance.SetPropertyValue("LogsDir", plg::string(plg::as_string(g_netlm.GetProvider()->GetLogsDir())));
	_instance.SetPropertyValue("CacheDir", plg::string(plg::as_string(g_netlm.GetProvider()->GetCacheDir())));

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
	NETLM_EXPORT bool IsExtensionLoaded(const char* name, const char* constraint) {
		if (constraint) {
			plg::range_set<> range;
			plg::parse(constraint, range);
			return g_netlm.GetProvider()->IsExtensionLoaded(name, std::move(range));
		}
		else
			return g_netlm.GetProvider()->IsExtensionLoaded(name);
	}

	NETLM_EXPORT ILanguageModule* GetLanguageModule() {
		return &g_netlm;
	}
}
