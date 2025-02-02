using System.Runtime.InteropServices;

namespace Plugify;

[StructLayout(LayoutKind.Sequential, Size = 2)]
public struct ManagedType
{
	private byte valueType;
	private byte reference;

	public ValueType ValueType => (ValueType) valueType;
	public bool IsByRef => reference == 1;

	public static ManagedType Invalid => new();

	public ManagedType(Type type)
	{
		reference = (byte)(type.IsByRef ? 1 : 0);
		valueType = (byte)TypeUtils.ConvertToValueType(type);
	}
}
