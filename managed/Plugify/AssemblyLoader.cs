using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace Plugify;

using static ManagedHost;

internal enum AssemblyLoadStatus
{
    Success, FileNotFound, FileLoadFailure, InvalidFilePath, InvalidAssembly, UnknownError
}
        
internal class AssemblyNameEqualityComparer : EqualityComparer<AssemblyName>
{
    public override bool Equals(AssemblyName? x, AssemblyName? y) => AssemblyName.ReferenceMatchesDefinition(x, y);
    public override int GetHashCode(AssemblyName obj) => obj.Name?.GetHashCode() ?? 0;
}

internal static class AssemblyLoader
{
    private static AssemblyLoadStatus LastLoadStatus = AssemblyLoadStatus.Success;
    private static readonly Dictionary<Type, AssemblyLoadStatus> AssemblyLoadErrorLookup = new();

    private static readonly AssemblyNameEqualityComparer NameEqualityComparer = new();
    
    public static readonly Dictionary<Guid, PluginLoadContextWrapper> LoadedAssemblies = new();
    public static readonly Dictionary<AssemblyName, List<GCHandle>> AllocatedHandles = new([], NameEqualityComparer);
    
    public static readonly AssemblyLoadContext MainLoadContext = AssemblyLoadContext.GetLoadContext(Assembly.GetExecutingAssembly()) ?? AssemblyLoadContext.Default;
    public static readonly HashSet<AssemblyName> SharedAssemblies = new([Assembly.GetExecutingAssembly().GetName()], NameEqualityComparer);

    static AssemblyLoader()
    {
        AssemblyLoadErrorLookup.Add(typeof(BadImageFormatException), AssemblyLoadStatus.InvalidAssembly);
        AssemblyLoadErrorLookup.Add(typeof(FileNotFoundException), AssemblyLoadStatus.FileNotFound);
        AssemblyLoadErrorLookup.Add(typeof(FileLoadException), AssemblyLoadStatus.FileLoadFailure);
        AssemblyLoadErrorLookup.Add(typeof(ArgumentNullException), AssemblyLoadStatus.InvalidFilePath);
        AssemblyLoadErrorLookup.Add(typeof(ArgumentException), AssemblyLoadStatus.InvalidFilePath);
    }

    public static bool TryGetAssembly(Guid guid, [MaybeNullWhen(false)] out PluginLoadContextWrapper context)
    {
        return LoadedAssemblies.TryGetValue(guid, out context);
    }

    [UnmanagedCallersOnly]
    private static Guid LoadAssembly(NativeString assemblyFilePath, Bool32 shouldRemoveExtension, Bool32 isCollectible)
    {
        try
        {
            string? assemblyPath = assemblyFilePath;
            
            if (string.IsNullOrEmpty(assemblyPath))
            {
                LastLoadStatus = AssemblyLoadStatus.InvalidFilePath;
                return Guid.Empty;
            }

            if (!File.Exists(assemblyPath))
            {
                LogMessage($"Failed to load assembly '{assemblyPath}', file not found.", MessageLevel.Error);
                LastLoadStatus = AssemblyLoadStatus.FileNotFound;
                return Guid.Empty;
            }
            
            string assemblyName = shouldRemoveExtension ? Path.GetFileNameWithoutExtension(assemblyPath) : assemblyPath;

            LogMessage($"Loading assembly '{assemblyPath}'.", MessageLevel.Info);
            var wrapper = PluginLoadContextWrapper.CreateAndLoadFromAssemblyName(new AssemblyName(assemblyName), assemblyPath, isCollectible);

            LoadedAssemblies.Add(wrapper.Id, wrapper);
            LastLoadStatus = AssemblyLoadStatus.Success;
            return wrapper.Id;
        }
        catch (Exception e)
        {
            AssemblyLoadErrorLookup.TryGetValue(e.GetType(), out LastLoadStatus);
            HandleException(e);
            return Guid.Empty;
        }
    }
    
    [UnmanagedCallersOnly]
    private static Bool32 UnloadAssembly(Guid assemblyId)
    {
        try
        {
            if (!TryGetAssembly(assemblyId, out var wrapper))
            {
                LogMessage($"Cannot unload assembly '{assemblyId}', it was either never loaded or already unloaded.", MessageLevel.Warning);
                return false;
            }

            if (!wrapper.IsCollectible)
            {
                throw new InvalidOperationException("Cannot unload an assembly that's not set to IsCollectible.");
            }

            LogMessage($"Unloading assembly {wrapper.FullName}...", MessageLevel.Info);

            if (wrapper.Assemblies != null)
            {
                foreach (var assembly in wrapper.Assemblies)
                {
                    var assemblyName = assembly.GetName();

    				if (!AllocatedHandles.TryGetValue(assemblyName, out var handles))
					{
						continue;
					}

					foreach (var handle in handles)
					{
						if (!handle.IsAllocated || handle.Target == null)
						{
							continue;
						}

						LogMessage(
							$"Found unfreed object '{handle.Target}' from assembly '{assemblyName}'. Deallocating.",
							MessageLevel.Info);
						handle.Free();
					}

					AllocatedHandles.Remove(assemblyName);
				}
			}

			wrapper.Unload();

			int startTimeMs = Environment.TickCount;
			bool takingTooLong = false;

			while (wrapper.IsAlive)
			{
				GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
				GC.WaitForPendingFinalizers();

				if (!wrapper.IsAlive)
				{
					break;
				}
                
				int elapsedTimeMs = Environment.TickCount - startTimeMs;

				if (!takingTooLong && elapsedTimeMs >= 200)
				{
					takingTooLong = true;
					LogMessage("Unloading assembly took longer than expected.", MessageLevel.Warning);
				}
				else if (elapsedTimeMs >= 1000)
				{
					LogMessage("Failed to unload assemblies. Possible causes: Strong GC handles, running threads, etc.", MessageLevel.Error);
					return false;
				}
			}

			LoadedAssemblies.Remove(assemblyId);
			LogMessage($"{wrapper.FullName} unloaded successfully!", MessageLevel.Info);
			return true;
		}
		catch (Exception e)
		{
			HandleException(e);
			return false;
		}
	}

	[UnmanagedCallersOnly]
	private static AssemblyLoadStatus GetLastLoadStatus() => LastLoadStatus;

	[UnmanagedCallersOnly]
	private static NativeString GetAssemblyName(Guid assemblyId)
	{
		if (!TryGetAssembly(assemblyId, out var wrapper))
		{
			LogMessage($"Couldn't get assembly name for assembly '{assemblyId}', assembly not in dictionary.", MessageLevel.Error);
			return "";
		}

		return wrapper.FullName;
	}

	public static void RegisterHandle(Assembly assembly, GCHandle handle)
	{
		var assemblyName = assembly.GetName();
		
		if (!AllocatedHandles.TryGetValue(assemblyName, out var handles))
		{
			handles = [];
			AllocatedHandles.Add(assemblyName, handles);
		}

		handles.Add(handle);
	}
	
	public static Assembly? ResolveAssembly(AssemblyName assemblyName)
	{
		foreach (var wrapper in LoadedAssemblies.Values)
		{
			if (!wrapper.IsAlive || !wrapper.Assembly.TryGetTarget(out var assembly))
			{
				continue;
			}

			if (AssemblyName.ReferenceMatchesDefinition(assembly.GetName(), assemblyName))
			{
				return assembly;
			}
		}

		LogMessage($"Cannot resolve '{assemblyName.Name!}'", MessageLevel.Error);
		return null;
	}
}
