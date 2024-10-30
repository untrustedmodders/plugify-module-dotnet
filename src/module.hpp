#include <plugify/language_module.hpp>
#include <plugify/method.hpp>
#include <plugify/plugin.hpp>
#include <plugify/jit/call.hpp>
#include <plugify/jit/callback.hpp>

#include "host_instance.hpp"

#include <asmjit/asmjit.h>
#include <cpptrace/cpptrace.hpp>

namespace netlm {
	class ScriptInstance {
	public:
		ScriptInstance(plugify::PluginRef plugin, ManagedGuid assembly, Type& type);
		~ScriptInstance();

		plugify::PluginRef GetPlugin() const { return _plugin; }
		const ManagedObject& GetManagedObject() const { return _instance; }
		const ManagedGuid& GetAssemblyId() const { return _assembly; }

		bool operator==(const ScriptInstance& other) const { return _instance == other._instance; }
		operator bool() const { return _instance; }

	private:
		void InvokeOnStart() const;
		void InvokeOnEnd() const;

	private:
		plugify::PluginRef _plugin;
		ManagedGuid _assembly;
		ManagedObject _instance;

		friend class DotnetLanguageModule;
	};

	using ScriptMap = std::map<plugify::UniqueId, ScriptInstance>;
	using HandleData = std::pair<ManagedHandle, ManagedHandle>;
	using FunctionList = std::vector<std::pair<plugify::JitCallback, std::unique_ptr<HandleData>>>;
	using ArgumentList = std::vector<const void*>;

	class DotnetLanguageModule final : public plugify::ILanguageModule {
	public:
		DotnetLanguageModule() = default;

		// ILanguageModule
		plugify::InitResult Initialize(std::weak_ptr<plugify::IPlugifyProvider> provider, plugify::ModuleRef module) override;
		void Shutdown() override;
		plugify::LoadResult OnPluginLoad(plugify::PluginRef plugin) override;
		void OnPluginStart(plugify::PluginRef plugin) override;
		void OnPluginEnd(plugify::PluginRef plugin) override;
		void OnMethodExport(plugify::PluginRef plugin) override;
		bool IsDebugBuild() override;

		const ScriptMap& GetScripts() const { return _scripts; }
		ScriptInstance* FindScript(plugify::UniqueId pluginId);
		plugify::MethodRef FindMethod(ManagedGuid assemblyId, std::string_view name);

		const std::shared_ptr<plugify::IPlugifyProvider>& GetProvider() { return _provider; }
		const std::shared_ptr<asmjit::JitRuntime>& GetRuntime() { return _rt; }

		static void InternalCall(plugify::MethodRef method, plugify::MemAddr data, const plugify::JitCallback::Parameters* p, uint8_t count, const plugify::JitCallback::Return* ret);
		static void DelegateCall(plugify::MethodRef method, plugify::MemAddr data, const plugify::JitCallback::Parameters* p, uint8_t count, const plugify::JitCallback::Return* ret);

	private:
		static void ExceptionCallback(std::string_view message);
		static void MessageCallback(std::string_view message, MessageLevel level);

	private:
		std::shared_ptr<plugify::IPlugifyProvider> _provider;
		std::shared_ptr<asmjit::JitRuntime> _rt;

		HostInstance _host;
		AssemblyLoadContext _alc;

		ScriptMap _scripts;
		FunctionList _functions;
	};

	extern DotnetLanguageModule g_netlm;
}
