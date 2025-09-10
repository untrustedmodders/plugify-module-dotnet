#include "host_instance.hpp"
#include "gc.hpp"
#include "managed_functions.hpp"
#include "utils.hpp"
#include "native_string.hpp"

#include <plg/format.hpp>
#include <plugify/assembly.hpp>

#include <dotnet/coreclr_delegates.h>
#include <dotnet/error_codes.h>
#include <dotnet/hostfxr.h>

#if NETLM_PLATFORM_WINDOWS
	#include <ShlObj_core.h>
#else
	#include <dlfcn.h>
#endif

using namespace netlm;
using namespace plugify;

namespace {
	hostfxr_initialize_for_runtime_config_fn hostfxr_initialize_for_runtime_config;
	hostfxr_get_runtime_delegate_fn hostfxr_get_runtime_delegate;
	hostfxr_set_runtime_property_value_fn hostfxr_set_runtime_property_value;
	hostfxr_close_fn hostfxr_close;
	hostfxr_set_error_writer_fn hostfxr_set_error_writer;

	load_assembly_and_get_function_pointer_fn load_assembly_and_get_function_pointer;
	//get_function_pointer_fn get_function_pointer;
	//load_assembly_fn load_assembly;
}

extern const char* hostfxr_str_error(int32_t error);

namespace {
	MessageCallbackFn MessageCallback;
	MessageLevel MessageFilter;
	ExceptionCallbackFn ExceptionCallback;
}

void HandleDeleter::operator()(void* handle) const noexcept {
	hostfxr_close(handle);
}

Res<void> HostInstance::Initialize(HostSettings settings) {
	if (_ctx) {
		return std::unexpected("Host instance already initialized");
	}

	// Setup settings
	_settings = std::move(settings);

	MessageCallback = _settings.messageCallback;
	MessageFilter = _settings.messageFilter;
	ExceptionCallback = _settings.exceptionCallback;

	// Load HostFXR
	if (auto result = LoadHostFXR(); !result) {
		MessageCallback(std::format("Failed to initialize hostfxr: {}", result.error()), MessageLevel::Error);
		return result;
	}

	// Initialize runtime host
	if (auto result = InitializeRuntimeHost(); !result) {
		return result;
	}

	return {};
}

void HostInstance::Shutdown() {
	MessageCallback("Shutting down .NET runtime", MessageLevel::Info);
	Managed.ShutdownFptr();
	_ctx.reset();
	MessageCallback("Shut down .NET runtime", MessageLevel::Info);
}

#if NETLM_PLATFORM_WINDOWS
template <typename TFunc>
TFunc LoadFunctionPtr(void* libraryHandle, const char* functionName) {
	return (TFunc)::GetProcAddress((HMODULE)libraryHandle, functionName);
}
#else
template <typename TFunc>
TFunc LoadFunctionPtr(void* libraryHandle, const char* functionName) {
	return (TFunc)::dlsym(libraryHandle, functionName);
}
#endif

fs::path GetHostFXRPath() {
#if NETLM_PLATFORM_WINDOWS
	// Find the Program Files folder
	TCHAR pf[MAX_PATH];
	SHGetSpecialFolderPath(nullptr, pf, CSIDL_PROGRAM_FILES, FALSE);

	fs::path basePath(pf);
	basePath /= "dotnet/host/fxr/";

	auto searchPaths = std::array {
		basePath
	};
#elif NETLM_PLATFORM_LINUX
	auto searchPaths = std::array {
		fs::path("/usr/lib/dotnet/host/fxr/"),
		fs::path("/usr/share/dotnet/host/fxr/"),
	};
#elif NETLM_PLATFORM_APPLE
	auto searchPaths = std::array {
		fs::path("/usr/local/share/dotnet/host/fxr/")
	};
#endif

	auto name = fs::path(NETLM_LIBRARY_PREFIX "hostfxr" NETLM_LIBRARY_SUFFIX);

	for (const auto& path : searchPaths) {
		if (!fs::exists(path))
			continue;

		for (const auto& dir : fs::recursive_directory_iterator(path)) {
			if (!dir.is_directory())
				continue;

			auto dirPath = dir.path().string();

			if (dirPath.find(NETLM_DOTNET_TARGET_VERSION_MAJOR_STR) == std::string::npos)
				continue;

			auto res = dir / name;
			assert(fs::exists(res));
			return res;
		}
	}

	return {};
}

Res<void> HostInstance::LoadHostFXR() {
    auto hostfxrPath = _settings.hostfxrPath;
    if (!fs::exists(hostfxrPath)) {
        hostfxrPath = GetHostFXRPath();
        if (!fs::exists(hostfxrPath)) {
            return std::unexpected("Could not find hostfxr library");
        }
    }

    MessageCallback(std::format("Loading hostfxr from: {}", hostfxrPath.string()), MessageLevel::Info);

    // Load the CoreCLR library
    void* libraryHandle = nullptr;

#if NETLM_PLATFORM_WINDOWS
    libraryHandle = ::LoadLibraryW(hostfxrPath.c_str());
	HMODULE pinnedHandle = nullptr;
	if (!::GetModuleHandleExW(
		GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS | GET_MODULE_HANDLE_EX_FLAG_PIN,
		reinterpret_cast<LPCWSTR>(libraryHandle), &pinnedHandle)
	) {
		return std::unexpected(std::format("Failed to pin hostfxr library from: {}", hostfxrPath.string()));
	}
#else
    libraryHandle = ::dlopen(hostfxrPath.c_str(), RTLD_NOW | RTLD_GLOBAL | RTLD_NODELETE);
#endif

    if (!libraryHandle) {
        return std::unexpected(std::format("Failed to load hostfxr library from: {}", hostfxrPath.string()));
    }

    // Load all required functions
    hostfxr_initialize_for_runtime_config = LoadFunctionPtr<hostfxr_initialize_for_runtime_config_fn>(libraryHandle, "hostfxr_initialize_for_runtime_config");
    hostfxr_get_runtime_delegate = LoadFunctionPtr<hostfxr_get_runtime_delegate_fn>(libraryHandle, "hostfxr_get_runtime_delegate");
    hostfxr_set_runtime_property_value = LoadFunctionPtr<hostfxr_set_runtime_property_value_fn>(libraryHandle, "hostfxr_set_runtime_property_value");
    hostfxr_close = LoadFunctionPtr<hostfxr_close_fn>(libraryHandle, "hostfxr_close");
    hostfxr_set_error_writer = LoadFunctionPtr<hostfxr_set_error_writer_fn>(libraryHandle, "hostfxr_set_error_writer");

    // Validate all functions were loaded
    if (!hostfxr_initialize_for_runtime_config) {
        return std::unexpected("Failed to load hostfxr_initialize_for_runtime_config");
    }
    if (!hostfxr_get_runtime_delegate) {
        return std::unexpected("Failed to load hostfxr_get_runtime_delegate");
    }
    if (!hostfxr_set_runtime_property_value) {
        return std::unexpected("Failed to load hostfxr_set_runtime_property_value");
    }
    if (!hostfxr_close) {
        return std::unexpected("Failed to load hostfxr_close");
    }
    if (!hostfxr_set_error_writer) {
        return std::unexpected("Failed to load hostfxr_set_error_writer");
    }

    MessageCallback("Loaded hostfxr functions", MessageLevel::Info);
    return {};
}

Res<void> HostInstance::InitializeRuntimeHost() {
    auto rootAssemblyPath = _settings.rootDirectory / "Plugify.dll";
    if (!fs::exists(rootAssemblyPath)) {
        return std::unexpected("Failed to find Plugify.dll");
    }

    auto runtimeConfigPath = _settings.rootDirectory / "Plugify.runtimeconfig.json";
    if (!fs::exists(runtimeConfigPath)) {
        return std::unexpected("Failed to find Plugify.runtimeconfig.json");
    }

    hostfxr_handle cxt = nullptr;
    int32_t result = hostfxr_initialize_for_runtime_config(runtimeConfigPath.c_str(), nullptr, &cxt);
    std::unique_ptr<void, HandleDeleter> context(cxt);

    if ((result < 0 || result > 2) || !cxt) {
        return std::unexpected(std::format("Failed to initialize hostfxr: {:x} ({})",
                                          uint32_t(result), hostfxr_str_error(result)));
    }

    result = hostfxr_get_runtime_delegate(cxt, hdt_load_assembly_and_get_function_pointer,
                                         reinterpret_cast<void**>(&load_assembly_and_get_function_pointer));
    if (result != 0 || !load_assembly_and_get_function_pointer) {
        return std::unexpected(std::format("hdt_load_assembly_and_get_function_pointer failed: {:x} ({})",
                                          uint32_t(result), hostfxr_str_error(result)));
    }

    _ctx = std::move(context);

    hostfxr_set_error_writer([](const char_t* message) {
        MessageCallback(NETLM_UTF8(message), MessageLevel::Error);
    });

    // Load managed functions
    if (auto res = LoadManagedFunctions(rootAssemblyPath); !res) {
        return res;
    }

    // Initialize managed host
    Managed.InitializeFptr(
        [](String msg, MessageLevel level) {
            if (int(MessageFilter) & int(level)) {
                std::string message = msg;
                MessageCallback(message, level);
            }
        },
        [](String msg) {
            std::string message = msg;
            if (!ExceptionCallback) {
                MessageCallback(message, MessageLevel::Error);
                return;
            }
            ExceptionCallback(message);
        });

    return {};
}

Res<void*> HostInstance::GetDelegate(const char_t* assemblyPath, const char_t* typeName, const char_t* methodName, const char_t* delegateTypeName) {
	void* delegatePtr = nullptr;

	int32_t result = load_assembly_and_get_function_pointer(
			assemblyPath,
			typeName,
			methodName,
			delegateTypeName,
			nullptr,
			&delegatePtr);

	if (result != 0) {
		return std::unexpected(std::format("Failed to get delegate: {}::{} - {:x} ({})", NETLM_UTF8(typeName), NETLM_UTF8(methodName), uint32_t(result), hostfxr_str_error(result)));
	}

	return delegatePtr;
}

Res<void> HostInstance::LoadManagedFunctions(const fs::path& assemblyPath) {
    const char_t* path = assemblyPath.c_str();

    // Define a macro to simplify delegate loading and checking
    #define LOAD_DELEGATE(field, typeName, methodName) \
        do { \
            auto result = GetDelegate(path, typeName, methodName); \
            if (!result) { \
                return std::unexpected(result.error()); \
            } \
            Managed.field = reinterpret_cast<decltype(Managed.field)>(result.value()); \
        } while(0)

    // Core functions
    LOAD_DELEGATE(InitializeFptr, NETLM_NSTR("Plugify.ManagedHost, Plugify"), NETLM_NSTR("Initialize"));
    LOAD_DELEGATE(ShutdownFptr, NETLM_NSTR("Plugify.ManagedHost, Plugify"), NETLM_NSTR("Shutdown"));

    // Assembly loading functions
    LOAD_DELEGATE(LoadManagedAssemblyFptr, NETLM_NSTR("Plugify.AssemblyLoader, Plugify"), NETLM_NSTR("LoadAssembly"));
    LOAD_DELEGATE(UnloadManagedAssemblyFptr, NETLM_NSTR("Plugify.AssemblyLoader, Plugify"), NETLM_NSTR("UnloadAssembly"));
    LOAD_DELEGATE(GetLastLoadStatusFptr, NETLM_NSTR("Plugify.AssemblyLoader, Plugify"), NETLM_NSTR("GetLastLoadStatus"));
    LOAD_DELEGATE(GetAssemblyNameFptr, NETLM_NSTR("Plugify.AssemblyLoader, Plugify"), NETLM_NSTR("GetAssemblyName"));

    // Type interface functions
    LOAD_DELEGATE(GetAssemblyTypesFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetAssemblyTypes"));
    LOAD_DELEGATE(GetTypeFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetType"));
    LOAD_DELEGATE(GetFullTypeNameFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetFullTypeName"));
    LOAD_DELEGATE(GetAssemblyQualifiedNameFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetAssemblyQualifiedName"));
    LOAD_DELEGATE(GetBaseTypeFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetBaseType"));
    LOAD_DELEGATE(GetTypeSizeFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetTypeSize"));
    LOAD_DELEGATE(IsTypeSubclassOfFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("IsTypeSubclassOf"));
    LOAD_DELEGATE(IsTypeAssignableToFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("IsTypeAssignableTo"));
    LOAD_DELEGATE(IsTypeAssignableFromFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("IsTypeAssignableFrom"));
    LOAD_DELEGATE(IsTypeSZArrayFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("IsTypeSZArray"));
    LOAD_DELEGATE(IsTypeByRefFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("IsTypeByRef"));
    LOAD_DELEGATE(GetElementTypeFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetElementType"));

    // Method/Field/Property functions
    LOAD_DELEGATE(GetTypeMethodsFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetTypeMethods"));
    LOAD_DELEGATE(GetTypeFieldsFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetTypeFields"));
    LOAD_DELEGATE(GetTypePropertiesFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetTypeProperties"));
    LOAD_DELEGATE(GetTypeMethodFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetTypeMethod"));
    LOAD_DELEGATE(GetTypeFieldFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetTypeField"));
    LOAD_DELEGATE(GetTypePropertyFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetTypeProperty"));

    // Attribute functions
    LOAD_DELEGATE(HasTypeAttributeFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("HasTypeAttribute"));
    LOAD_DELEGATE(GetTypeAttributesFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetTypeAttributes"));
    LOAD_DELEGATE(GetTypeManagedTypeFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetTypeManagedType"));

    // Static method invocation
    LOAD_DELEGATE(InvokeStaticMethodFptr, NETLM_NSTR("Plugify.ManagedObject, Plugify"), NETLM_NSTR("InvokeStaticMethod"));
    LOAD_DELEGATE(InvokeStaticMethodRetFptr, NETLM_NSTR("Plugify.ManagedObject, Plugify"), NETLM_NSTR("InvokeStaticMethodRet"));

    // MethodInfo functions
    LOAD_DELEGATE(GetMethodInfoNameFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetMethodInfoName"));
    LOAD_DELEGATE(GetMethodInfoFunctionAddressFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetMethodInfoFunctionAddress"));
    LOAD_DELEGATE(GetMethodInfoReturnTypeFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetMethodInfoReturnType"));
    LOAD_DELEGATE(GetMethodInfoParameterTypesFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetMethodInfoParameterTypes"));
    LOAD_DELEGATE(GetMethodInfoAccessibilityFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetMethodInfoAccessibility"));
    LOAD_DELEGATE(GetMethodInfoAttributesFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetMethodInfoAttributes"));
    LOAD_DELEGATE(GetMethodInfoParameterAttributesFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetMethodInfoParameterAttributes"));
    LOAD_DELEGATE(GetMethodInfoReturnAttributesFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetMethodInfoReturnAttributes"));

    // FieldInfo functions
    LOAD_DELEGATE(GetFieldInfoNameFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetFieldInfoName"));
    LOAD_DELEGATE(GetFieldInfoTypeFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetFieldInfoType"));
    LOAD_DELEGATE(GetFieldInfoAccessibilityFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetFieldInfoAccessibility"));
    LOAD_DELEGATE(GetFieldInfoAttributesFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetFieldInfoAttributes"));

    // PropertyInfo functions
    LOAD_DELEGATE(GetPropertyInfoNameFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetPropertyInfoName"));
    LOAD_DELEGATE(GetPropertyInfoTypeFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetPropertyInfoType"));
    LOAD_DELEGATE(GetPropertyInfoAttributesFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetPropertyInfoAttributes"));

    // Attribute functions
    LOAD_DELEGATE(GetAttributeFieldValueFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetAttributeFieldValue"));
    LOAD_DELEGATE(GetAttributeTypeFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetAttributeType"));

    // Interop and object management
    LOAD_DELEGATE(SetInternalCallsFptr, NETLM_NSTR("Plugify.Interop.InternalCallsManager, Plugify"), NETLM_NSTR("SetInternalCalls"));
    LOAD_DELEGATE(CreateObjectFptr, NETLM_NSTR("Plugify.ManagedObject, Plugify"), NETLM_NSTR("CreateObject"));
    LOAD_DELEGATE(InvokeMethodFptr, NETLM_NSTR("Plugify.ManagedObject, Plugify"), NETLM_NSTR("InvokeMethod"));
    LOAD_DELEGATE(InvokeMethodRetFptr, NETLM_NSTR("Plugify.ManagedObject, Plugify"), NETLM_NSTR("InvokeMethodRet"));
    LOAD_DELEGATE(InvokeDelegateFptr, NETLM_NSTR("Plugify.ManagedObject, Plugify"), NETLM_NSTR("InvokeDelegate"));
    LOAD_DELEGATE(InvokeDelegateRetFptr, NETLM_NSTR("Plugify.ManagedObject, Plugify"), NETLM_NSTR("InvokeDelegateRet"));

    // Field operations
    LOAD_DELEGATE(SetFieldValueFptr, NETLM_NSTR("Plugify.ManagedObject, Plugify"), NETLM_NSTR("SetFieldValue"));
    LOAD_DELEGATE(GetFieldValueFptr, NETLM_NSTR("Plugify.ManagedObject, Plugify"), NETLM_NSTR("GetFieldValue"));
    LOAD_DELEGATE(GetFieldPointerFptr, NETLM_NSTR("Plugify.ManagedObject, Plugify"), NETLM_NSTR("GetFieldPointer"));

    // Property operations
    LOAD_DELEGATE(SetPropertyValueFptr, NETLM_NSTR("Plugify.ManagedObject, Plugify"), NETLM_NSTR("SetPropertyValue"));
    LOAD_DELEGATE(GetPropertyValueFptr, NETLM_NSTR("Plugify.ManagedObject, Plugify"), NETLM_NSTR("GetPropertyValue"));

    // Object lifecycle
    LOAD_DELEGATE(DestroyObjectFptr, NETLM_NSTR("Plugify.ManagedObject, Plugify"), NETLM_NSTR("DestroyObject"));

    // Garbage collection
    LOAD_DELEGATE(CollectGarbageFptr, NETLM_NSTR("Plugify.GarbageCollector, Plugify"), NETLM_NSTR("CollectGarbage"));
    LOAD_DELEGATE(WaitForPendingFinalizersFptr, NETLM_NSTR("Plugify.GarbageCollector, Plugify"), NETLM_NSTR("WaitForPendingFinalizers"));

    // Type checking functions
    LOAD_DELEGATE(IsClassFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("IsClass"));
    LOAD_DELEGATE(IsEnumFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("IsEnum"));
    LOAD_DELEGATE(IsValueTypeFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("IsValueType"));

    // Enum functions
    LOAD_DELEGATE(GetEnumNamesFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetEnumNames"));
    LOAD_DELEGATE(GetEnumValuesFptr, NETLM_NSTR("Plugify.TypeInterface, Plugify"), NETLM_NSTR("GetEnumValues"));

    #undef LOAD_DELEGATE

    return {};
}

// https://github.com/dotnet/runtime/blob/main/docs/design/features/host-error-codes.md
const char* hostfxr_str_error(int32_t error) {
	switch (static_cast<StatusCode>(error)) {
		case Success:
			return "Operation was successful";
		case Success_HostAlreadyInitialized:
			return "Initialization was successful, but another host context is already initialized.";
		case Success_DifferentRuntimeProperties:
			return "Initialization was successful, but another host context is already initialized and the requested context specified some runtime properties which are not the same to the already initialized context";
		case InvalidArgFailure:
			return "One of the specified arguments for the operation is invalid";
		case CoreHostLibLoadFailure:
			return "There was a failure loading a dependent library";
		case CoreHostLibMissingFailure:
			return "One of the dependent libraries is missing";
		case CoreHostEntryPointFailure:
			return "One of the dependent libraries is missing a required entry point.";
		case CoreHostCurHostFindFailure:
			return "Either the location of the current module could not be determined or the location is not in the right place relative to other expected components";
		case CoreClrResolveFailure:
			return "Failed to resolve the coreclr library";
		case CoreClrBindFailure:
			return "The loaded coreclr library does not have one of the required entry points";
		case CoreClrInitFailure:
			return "The call to coreclr_initialize failed";
		case CoreClrExeFailure:
			return "The call to coreclr_execute_assembly failed";
		case ResolverInitFailure:
			return "Initialization of the hostpolicy dependency resolver failed";
		case ResolverResolveFailure:
			return "Resolution of dependencies in hostpolicy failed";
		case LibHostCurExeFindFailure:
			return "Failure to determine the location of the current executable";
		case LibHostInitFailure:
			return "Initialization of the hostpolicy library failed";
		case LibHostExecModeFailure:
			return "Execution of the hostpolicy mode failed";
		case LibHostSdkFindFailure:
			return "Failure to find the requested SDK";
		case LibHostInvalidArgs:
			return "Arguments to hostpolicy are invalid";
		case InvalidConfigFile:
			return "The .runtimeconfig.json file is invalid";
		case AppArgNotRunnable:
			return "Used internally when the command line for dotnet.exe does not contain path to the application to run";
		case AppHostExeNotBoundFailure:
			return "apphost failed to determine which application to run";
		case FrameworkMissingFailure:
			return "It was not possible to find a compatible framework version";
		case HostApiFailed:
			return "hostpolicy could not calculate NATIVE_DLL_SEARCH_DIRECTORIES";
		case HostApiBufferTooSmall:
			return "Buffer specified to an API is not big enough to fit the requested value";
		case LibHostUnknownCommand:
			return "Returned by hostpolicy if corehost_main_with_output_buffer is called with unsupported host commands";
		case LibHostAppRootFindFailure:
			return "Returned by apphost if the imprinted application path does not exist";
		case SdkResolverResolveFailure:
			return "Returned from hostfxr_resolve_sdk2 when it failed to find matching SDK";
		case FrameworkCompatFailure:
			return "During processing of .runtimeconfig.json there were two framework references to the same framework which were not compatible";
		case FrameworkCompatRetry:
			return "Error used internally if the processing of framework references from .runtimeconfig.json reached a point where it needs to reprocess another already processed framework reference";
		//case AppHostExeNotBundle:
		//	return "Error reading the bundle footer metadata from a single-file apphost";
		case BundleExtractionFailure:
			return "Error extracting single-file apphost bundle";
		case BundleExtractionIOError:
			return "Error reading or writing files during single-file apphost bundle extraction";
		case LibHostDuplicateProperty:
			return "The .runtimeconfig.json specified by the app contains a runtime property which is also produced by the hosting layer";
		case HostApiUnsupportedVersion:
			return "Feature which requires certain version of the hosting layer binaries was used on a version which does not support it";
		case HostInvalidState:
			return "Error code returned by the hosting APIs in hostfxr if the current state is incompatible with the requested operation";
		case HostPropertyNotFound:
			return "Property requested by hostfxr_get_runtime_property_value does not exist";
		case CoreHostIncompatibleConfig:
			return "Error returned by hostfxr_initialize_for_runtime_config if the component being initialized requires framework which is not available or incompatible with the frameworks loaded by the runtime already in the process";
		case HostApiUnsupportedScenario:
			return "Error returned by hostfxr_get_runtime_delegate when hostfxr does not currently support requesting the given delegate type using the given context";
		case HostFeatureDisabled:
			return "Error returned by hostfxr_get_runtime_delegate when managed feature support for native host is disabled";
		default:
			return "Unknown error";
	}
}
