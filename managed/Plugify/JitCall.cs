using System.Runtime.InteropServices;

namespace Plugify;

public partial class JitCall : SafeHandle
{
	public JitCall(nint target, ManagedType[] parameters, ManagedType ret) : base(nint.Zero, true)
	{
		handle = NewCall(target, parameters, parameters.Length, ret);
	}

	public override bool IsInvalid => handle == nint.Zero;

	public unsafe delegate* unmanaged[Cdecl]<ulong*, ulong*, void> Function => (delegate* unmanaged[Cdecl]<ulong*, ulong*, void>) GetCallFunction(handle);
	
	public string Error => GetCallError(handle);

	protected override bool ReleaseHandle()
	{
		DeleteCall(handle);
		return true;
	}
	
	[LibraryImport(NativeMethods.DllName)]
	[SuppressGCTransition]
	private static partial nint NewCall(nint target, [In] ManagedType[] parameters, int count, ManagedType ret);

	[LibraryImport(NativeMethods.DllName)]
	[SuppressGCTransition]
	private static partial void DeleteCall(nint call);

	[LibraryImport(NativeMethods.DllName)]
	[SuppressGCTransition]
	private static partial nint GetCallFunction(nint call);

	[LibraryImport(NativeMethods.DllName)]
	[SuppressGCTransition]
	[return: MarshalAs(UnmanagedType.LPStr)]
	private static partial string GetCallError(nint call);
}
