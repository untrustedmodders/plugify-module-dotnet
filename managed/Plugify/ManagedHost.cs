using System.Runtime.InteropServices;

namespace Plugify;

internal enum MessageLevel { Info = 1, Warning = 2, Error = 4 }

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

	internal static void LogMessage(string message, MessageLevel messageLevel)
	{
		unsafe
		{
			using NativeString msg = message;
			MessageCallback(msg, messageLevel);
		}
	}

	internal static void HandleException(Exception exception)
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
