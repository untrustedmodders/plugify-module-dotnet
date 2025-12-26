using System.Runtime.InteropServices;

namespace Plugify;

[StructLayout(LayoutKind.Sequential, Size = 2)]
internal struct ManagedType(Type type)
{
    private byte valueType = (byte)TypeUtils.ConvertToValueType(type);
    private byte reference = (byte)(type.IsByRef ? 1 : 0);

    public ValueType ValueType => (ValueType) valueType;
    public bool IsByRef => reference == 1;

    public static ManagedType Invalid => new();
}
