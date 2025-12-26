using System.Runtime.InteropServices;

namespace Plugify;

internal partial class JitCallback : SafeHandle
{
    public JitCallback(Delegate target) : base(nint.Zero, ownsHandle: true)
    {
        var targetType = target.GetType();
        var delegateHandle = GCHandle.Alloc(target, GCHandleType.Normal);
        AssemblyLoader.RegisterHandle(targetType.Assembly, delegateHandle);
        handle = NewCallback(targetType.FullName!, GCHandle.ToIntPtr(delegateHandle));
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
    private static partial nint NewCallback(string delegateName, nint delegateHandle);

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
