using System.Runtime.CompilerServices;

namespace Plugify;

public sealed class Scope : IDisposable
{
    private readonly ulong _handle;

    private static readonly bool IsProfiling = NativeMethods.IsProfiling();
    private static readonly bool IsLogging = NativeMethods.IsLogging();
    
    public Scope(string name, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "", [CallerMemberName] string function = "", string module = "")
    {
        if (IsProfiling)
            _handle = NativeMethods.BeginZone(name, line, file, function, module);
        if (IsLogging)
            NativeMethods.Log(name, Severity.Trace, line, file, function, module);
    }

    public void Dispose()
    {
        if (_handle != 0)
            NativeMethods.EndZone(_handle);
    }
}