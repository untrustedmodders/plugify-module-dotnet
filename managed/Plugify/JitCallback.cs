using System.Runtime.InteropServices;

namespace Plugify;

using static ManagedHost;

public partial class JitCallback : SafeHandle
{
    public JitCallback(Delegate target) : base(nint.Zero, true)
    {
        var targetType = target.GetType();
        var assembly = targetType.Assembly;
        
        if (!AssemblyLoader.TryGetGuid(assembly, out Guid assemblyId))
        {
            LogMessage($"Couldn't get assembly id for assembly '{assembly}', assembly not in dictionary.", MessageLevel.Error);
            return;
        }
        
        var delegateHandle = GCHandle.Alloc(target, GCHandleType.Normal);
        AssemblyLoader.RegisterHandle(assembly, delegateHandle);
        handle = NewCallback(assemblyId, targetType.FullName!, GCHandle.ToIntPtr(delegateHandle));
    }

    public override bool IsInvalid => handle == nint.Zero;

    public nint Function => GetCallbackFunction(handle);
	
    public string Error => GetCallbackError(handle);

    protected override bool ReleaseHandle()
    {
        DeleteCallback(handle);
        return true;
    }
	
    [LibraryImport(NativeMethods.DllName, StringMarshalling = StringMarshalling.Utf8)]
    [SuppressGCTransition]
    private static partial nint NewCallback(Guid assemblyId, string delegateName, nint delegateHandle);

    [LibraryImport(NativeMethods.DllName)]
    [SuppressGCTransition]
    private static partial void DeleteCallback(nint callback);

    [LibraryImport(NativeMethods.DllName)]
    [SuppressGCTransition]
    private static partial nint GetCallbackFunction(nint callback);

    [LibraryImport(NativeMethods.DllName)]
    [SuppressGCTransition]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static partial string GetCallbackError(nint callback);
}
