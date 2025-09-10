#pragma once

#include <plugify/language_module.hpp>
#include <plugify/assembly_loader.hpp>
#include <plugify/method.hpp>
#include <plugify/logger.hpp>
#include <plugify/mem_addr.hpp>
#include <plugify/provider.hpp>
#include <plugify/extension.hpp>
#include <plugify/call.hpp>
#include <plugify/callback.hpp>

#include "host_instance.hpp"
#include "managed_assembly.hpp"

using namespace plugify;

namespace netlm {
	class ScriptInstance {
	public:
		ScriptInstance(const Extension& plugin, ManagedGuid assembly, Type& type);
		~ScriptInstance();

		const Extension& GetPlugin() const { return _plugin; }
		const ManagedObject& GetManagedObject() const { return _instance; }
		const ManagedGuid& GetAssemblyId() const { return _assembly; }

		bool operator==(const ScriptInstance& other) const { return _instance == other._instance; }
		operator bool() const { return _instance; }

		void InvokeOnStart() const;
		void InvokeOnUpdate(float dt) const;
		void InvokeOnEnd() const;

		bool HasStart() const;
		bool HasUpdate() const;
		bool HasEnd() const;

	private:
		const Extension& _plugin;
		ManagedGuid _assembly;
		ManagedObject _instance;
		MethodInfo _update;
		MethodInfo _start;
		MethodInfo _end;
	};

	struct SharpMethodData;

	using HandleData = std::pair<ManagedHandle, ManagedHandle>;
	using ScriptMap = std::unordered_map<UniqueId, ScriptInstance>;
	using FunctionList = std::vector<SharpMethodData>;
	using ArgumentList = std::vector<const void*>;

	struct SharpMethodData {
		JitCallback jitCallback;
		std::unique_ptr<HandleData> sharpFunction;
	};

	class DotnetLanguageModule final : public ILanguageModule {
	public:
		DotnetLanguageModule() = default;

		// ILanguageModule
		Result<InitData> Initialize(const Provider& provider, const Extension& module) override;
		void Shutdown() override;
		void OnUpdate(std::chrono::milliseconds dt) override;

		Result<LoadData> OnPluginLoad(const Extension& plugin) override;
		void OnPluginStart(const Extension& plugin) override;
		void OnPluginUpdate(const Extension& plugin, std::chrono::milliseconds dt) override;
		void OnPluginEnd(const Extension& plugin) override;
		void OnMethodExport(const Extension& plugin) override;
		bool IsDebugBuild() override;

		const ScriptMap& GetScripts() const { return _scripts; }
		ScriptInstance* FindScript(UniqueId pluginId);
		const Method* FindMethod(std::string_view name) const;

		const std::unique_ptr<Provider>& GetProvider() { return _provider; }
		static Result<SharpMethodData> GenerateMethodExport(const Method& method, ManagedAssembly &assembly);

		static void InternalCall(const Method* method, MemAddr data, uint64_t* p, size_t count, void* ret);
		static void DelegateCall(const Method* method, MemAddr data, uint64_t* p, size_t count, void* ret);

	private:
		static void ExceptionCallback(std::string_view message);
		static void MessageCallback(std::string_view message, MessageLevel level);

	private:
		std::unique_ptr<Provider> _provider;

		HostInstance _host;
		AssemblyLoader _loader;

		ScriptMap _scripts;
		FunctionList _functions;
	};

	extern DotnetLanguageModule g_netlm;
}
