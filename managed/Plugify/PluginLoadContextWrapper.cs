using System.Reflection;
using System.Runtime.Loader;

namespace Plugify;

internal class PluginLoadContextWrapper
{
    private PluginLoadContext? _pluginLoadContext;

    private PluginLoadContextWrapper(PluginLoadContext pluginLoadContext, Assembly assembly)
    {
        _pluginLoadContext = pluginLoadContext;
        Id = Guid.NewGuid();
        FullName = assembly.FullName;
        Assembly = new WeakReference<Assembly>(assembly);
    }

    internal Guid Id { get; private set; }
    internal string? FullName { get; private set; }
    internal IEnumerable<Assembly>? Assemblies => _pluginLoadContext?.Assemblies;
    internal bool IsCollectible => _pluginLoadContext?.IsCollectible ?? true;
    internal bool IsAlive => _pluginLoadContext != null;
        
    // Be careful using this. Any hard reference at the wrong time will prevent the plugin from being unloaded.
    // Thus breaking hot reloading.
    public WeakReference<Assembly> Assembly { get; }

    public static PluginLoadContextWrapper CreateAndLoadFromAssemblyName(AssemblyName assemblyName, string pluginPath, bool isCollectible)
    {
        PluginLoadContext context = new PluginLoadContext(pluginPath, isCollectible);
        Assembly assembly = context.LoadFromAssemblyName(assemblyName);
        PluginLoadContextWrapper wrapper = new PluginLoadContextWrapper(context, assembly);
        
        if (!wrapper.IsAlive)
        {
            throw new Exception($"Failed to load assembly from: {pluginPath}");
        }
       
        return wrapper;
    }

    internal void Unload()
    {
        _pluginLoadContext?.Unload();
        _pluginLoadContext = null;
    }
}
