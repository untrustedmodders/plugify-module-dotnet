using System.Runtime.InteropServices;

namespace Plugify;

[Flags]
internal enum MessageLevel
{
	Info = 1 << 0, 
	Warning = 1 << 1, 
	Error = 1 << 2,
	All = Info | Warning | Error
}

internal static class ManagedHost
{
	private static unsafe delegate* unmanaged[Cdecl]<NativeString, void> ExceptionCallback;
	private static unsafe delegate* unmanaged[Cdecl]<NativeString, MessageLevel, void> MessageCallback;

	[UnmanagedCallersOnly]
	private static unsafe void Initialize(delegate* unmanaged[Cdecl]<NativeString, MessageLevel, void> messageCallback, delegate* unmanaged[Cdecl]<NativeString, void> exceptionCallback)
	{
		MessageCallback = messageCallback;
		ExceptionCallback = exceptionCallback;
	}

	[UnmanagedCallersOnly]
	private static void Shutdown()
	{
		//ManagedObject.CachedMethods.Clear();

		TypeInterface.CachedTypes.Clear();
		TypeInterface.CachedMethods.Clear();
		TypeInterface.CachedFields.Clear();
		TypeInterface.CachedProperties.Clear();
		TypeInterface.CachedAttributes.Clear();
		
		Marshalling.CachedDelegates.Clear();
		Marshalling.CachedFunctions.Clear();

		if (AssemblyLoader.AllocatedHandles.Count > 0)
		{
			LogMessage("Handles were not unloaded correctly. Please file a bug report at 'https://github.com/untrustedmodders/plugify-module-dotnet/issues'.", MessageLevel.Error);
			AssemblyLoader.AllocatedHandles.Clear();
		}
		
		if (AssemblyLoader.LoadedAssemblies.Count > 0)
		{
			LogMessage("Assemblies were not unloaded correctly. Please file a bug report at 'https://github.com/untrustedmodders/plugify-module-dotnet/issues'.", MessageLevel.Error);
			AssemblyLoader.LoadedAssemblies.Clear();
		}
	}

	public static void LogMessage(string message, MessageLevel messageLevel)
	{
		unsafe
		{
			using NativeString msg = message;
			MessageCallback(msg, messageLevel);
		}
	}

	public static void HandleException(Exception exception)
	{
		unsafe
		{
			if (ExceptionCallback == null)
				return;

			using NativeString msg = exception.ToString();
			ExceptionCallback(msg);
		}
	}
}
