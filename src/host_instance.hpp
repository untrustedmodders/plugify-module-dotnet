#pragma once

#include "core.hpp"

namespace netlm {
	enum class MessageLevel {
        Info = 1 << 0,
        Warning = 1 << 1,
        Error = 1 << 2,
        All = Info | Warning | Error
    };

    using MessageCallbackFn = std::function<void(std::string_view, MessageLevel)>;
    using ExceptionCallbackFn = std::function<void(std::string_view)>;

    struct HostSettings {
        fs::path hostfxrPath;
        fs::path rootDirectory; // The file path to plugify.runtimeconfig.json

        MessageCallbackFn messageCallback;
        MessageLevel messageFilter = MessageLevel::All;

        ExceptionCallbackFn exceptionCallback;
    };

    struct HandleDeleter {
        void operator()(void* handle) const noexcept;
    };

    template <typename T>
    using Res = std::expected<T, std::string>;

    class HostInstance {
    public:
        std::expected<void, std::string> Initialize(HostSettings settings);
        void Shutdown();

    private:
        Res<void> LoadHostFXR();
        Res<void> InitializeRuntimeHost();
        Res<void> LoadManagedFunctions(const fs::path& assemblyPath);

        static Res<void*> GetDelegate(const char_t* assemblyPath, const char_t* typeName, const char_t* methodName, const char_t* delegateType = NETLM_UNMANAGED_CALLERS_ONLY);

    private:
        HostSettings _settings;
        std::unique_ptr<void, HandleDeleter> _ctx;
    };
}
