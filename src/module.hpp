#include <plugify/language_module.hpp>
#include <plugify/method.hpp>
#include <plugify/plugin.hpp>
#include <plugify/jit/call.hpp>
#include <plugify/jit/callback.hpp>

#include "host_instance.hpp"
#include "managed_assembly.hpp"

namespace netlm {
	class ScriptInstance {
	public:
		ScriptInstance(plugify::PluginHandle plugin, ManagedGuid assembly, Type& type);
		~ScriptInstance();

		plugify::PluginHandle GetPlugin() const { return _plugin; }
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
		plugify::PluginHandle _plugin;
		ManagedGuid _assembly;
		ManagedObject _instance;
		MethodInfo _update;
		MethodInfo _start;
		MethodInfo _end;
	};

	using ScriptMap = std::unordered_map<plugify::UniqueId, ScriptInstance>;
	using HandleData = std::pair<ManagedHandle, ManagedHandle>;
	using FunctionList = std::vector<std::pair<plugify::JitCallback, std::unique_ptr<HandleData>>>;
	using ArgumentList = std::vector<const void*>;

	class DotnetLanguageModule final : public plugify::ILanguageModule {
	public:
		DotnetLanguageModule() = default;

		// ILanguageModule
		plugify::InitResult Initialize(std::weak_ptr<plugify::IPlugifyProvider> provider, plugify::ModuleHandle module) override;
		void Shutdown() override;
		void OnUpdate(plugify::DateTime dt) override;
		plugify::LoadResult OnPluginLoad(plugify::PluginHandle plugin) override;
		void OnPluginStart(plugify::PluginHandle plugin) override;
		void OnPluginUpdate(plugify::PluginHandle plugin, plugify::DateTime dt) override;
		void OnPluginEnd(plugify::PluginHandle plugin) override;
		void OnMethodExport(plugify::PluginHandle plugin) override;
		bool IsDebugBuild() override;

		const ScriptMap& GetScripts() const { return _scripts; }
		ScriptInstance* FindScript(plugify::UniqueId pluginId);
		plugify::MethodHandle FindMethod(std::string_view name);

		const std::shared_ptr<plugify::IPlugifyProvider>& GetProvider() { return _provider; }
		const std::shared_ptr<asmjit::JitRuntime>& GetRuntime() { return _rt; }

		static void InternalCall(plugify::MethodHandle method, plugify::MemAddr data, const plugify::JitCallback::Parameters* p, size_t count, const plugify::JitCallback::Return* ret);
		static void DelegateCall(plugify::MethodHandle method, plugify::MemAddr data, const plugify::JitCallback::Parameters* p, size_t count, const plugify::JitCallback::Return* ret);

	private:
		static void ExceptionCallback(std::string_view message);
		static void MessageCallback(std::string_view message, MessageLevel level);

	private:
		std::shared_ptr<plugify::IPlugifyProvider> _provider;
		std::shared_ptr<asmjit::JitRuntime> _rt;

		HostInstance _host;
		AssemblyLoader _loader;

		ScriptMap _scripts;
		FunctionList _functions;
	};

	extern DotnetLanguageModule g_netlm;
}
