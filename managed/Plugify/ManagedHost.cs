using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

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
        Marshalling.CachedMethods.Clear();

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

            var sb = new StringBuilder();

            Exception? current = exception;
            while (current != null)
            {
                sb.AppendLine(current.GetType().FullName);
                sb.AppendLine(current.Message);

                if (!string.IsNullOrEmpty(current.StackTrace))
                    sb.AppendLine(current.StackTrace);

                current = current.InnerException;
                if (current != null)
                    sb.AppendLine("Inner Exception:");
            }

            if (exception.StackTrace == null)
            {
                sb.AppendLine("Captured Stack:");
                sb.AppendLine(new StackTrace(true).ToString());
            }

            using NativeString msg = sb.ToString();
            ExceptionCallback(msg);
        }
    }
}
