#pragma once

#include <plugify/language_module.hpp>
#include <plugify/assembly_loader.hpp>
#include <plugify/method.hpp>
#include <plugify/logger.hpp>
#include <plugify/profiler.hpp>
#include <plugify/mem_addr.hpp>
#include <plugify/provider.hpp>
#include <plugify/extension.hpp>
#include <plugify/call.hpp>
#include <plugify/callback.hpp>

#include "host_instance.hpp"
#include "managed_assembly.hpp"

using namespace plugify;

namespace netlm {
	using ScriptResult = plg::string;

	struct ScriptMethod {
		MethodInfo method;
		bool error;

		ScriptMethod(ManagedObject instance, std::string_view methodName);
	};

	class ScriptInstance {
	public:
		ScriptInstance(const Extension& plugin, ManagedGuid assembly, Type& type);
		~ScriptInstance();

		const Extension& GetPlugin() const { return _plugin; }
		const ManagedObject& GetManagedObject() const { return _instance; }
		const ManagedGuid& GetAssemblyId() const { return _assembly; }

		bool operator==(const ScriptInstance& other) const { return _instance == other._instance; }
		explicit operator bool() const { return static_cast<bool>(_instance); }

		ScriptResult InvokeOnStart() const;
		ScriptResult InvokeOnUpdate(float dt) const;
		ScriptResult InvokeOnEnd() const;

		bool HasStart() const;
		bool HasUpdate() const;
		bool HasEnd() const;

	private:
		const Extension& _plugin;
		ManagedGuid _assembly;
		ManagedObject _instance;
		ScriptMethod _update;
		ScriptMethod _start;
		ScriptMethod _end;
	};

	struct SharpMethodData;

	using HandleData = std::pair<ManagedHandle, ManagedHandle>;
	using ScriptMap = std::map<UniqueId, ScriptInstance>;
	using FunctionList = std::vector<SharpMethodData>;
	using ArgumentList = std::inplace_vector<const void*, Signature::kMaxFuncArgs>;

	struct SharpMethodData {
		JitCallback jitCallback;
		std::unique_ptr<HandleData> sharpFunction;
	};

	class DotnetLanguageModule final : public ILanguageModule {
	public:
		DotnetLanguageModule() = default;

		// ILanguageModule
		Result<InitData> Initialize(const Provider& provider, const Extension& module) override;
		Result<void> Shutdown() override;
		Result<void> OnUpdate(std::chrono::milliseconds dt) override;

		Result<LoadData> OnPluginLoad(const Extension& plugin) override;
		Result<void> OnPluginStart(const Extension& plugin) override;
		Result<void> OnPluginUpdate(const Extension& plugin, std::chrono::milliseconds dt) override;
		Result<void> OnPluginEnd(const Extension& plugin) override;
		Result<void> OnMethodExport(const Extension& plugin) override;

		bool IsDebugBuild() const noexcept override;

		const ScriptMap& GetScripts() const { return _scripts; }
		ScriptInstance* FindScript(UniqueId pluginId);
		std::shared_ptr<Method> FindMethod(std::string_view name) const;

		const std::unique_ptr<Provider>& GetProvider() { return _provider; }
		const std::shared_ptr<ILogger>& GetLogger() { return _logger; }
		const std::shared_ptr<IProfiler>& GetProfiler() const { return _profiler; }

		static Result<SharpMethodData> GenerateMethodExport(const Method& method, ManagedAssembly &assembly);

		static void InternalCall(const Method* method, MemAddr data, uint64_t* p, size_t count, void* ret);
		static void DelegateCall(const Method* method, MemAddr data, uint64_t* p, size_t count, void* ret);

	private:
		static void ExceptionCallback(std::string_view message);
		static void MessageCallback(std::string_view message, MessageLevel level);

	private:
		std::unique_ptr<Provider> _provider;
		std::shared_ptr<ILogger> _logger;
		std::shared_ptr<IProfiler> _profiler;

		HostInstance _host;
		AssemblyLoader _loader;

		ScriptMap _scripts;
		FunctionList _functions;
	};

	extern DotnetLanguageModule g_netlm;
}
