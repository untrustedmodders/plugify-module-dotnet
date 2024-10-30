using System.Runtime.InteropServices;

namespace Plugify;

[StructLayout(LayoutKind.Sequential)]
public struct NativeString : IDisposable
{
	internal nint _string;
	private int _length;
	private Bool32 _disposed;

	public void Dispose()
	{
		if (!_disposed)
		{
			if (_string != nint.Zero)
			{
				Marshal.FreeCoTaskMem(_string);
				_string = nint.Zero;
				_length = 0;
			}

			_disposed = true;
		}

		GC.SuppressFinalize(this);
	}

	public override string? ToString() => this;

	public static NativeString Null() => new(){ _string = nint.Zero };

	public static implicit operator NativeString(string? value) => new(){ _string = Marshal.StringToCoTaskMemAuto(value), _length = value?.Length ?? 0 };
	public static implicit operator string?(NativeString value) => Marshal.PtrToStringAuto(value._string, value._length);
}

public readonly struct Bool8
{
	public byte Value { get; init; }

	public static implicit operator bool(Bool8 value) => value.Value != 0;
	public static implicit operator byte(Bool8 value) => value.Value;
	public static implicit operator Bool8(bool value) => new() { Value = value ? (byte)1 : (byte)0 };
	public static implicit operator Bool8(byte value) => new() { Value = value };
	
	public override string ToString() => (Value == 1).ToString();
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct Bool32
{
	public uint Value { get; init; }

	public static implicit operator Bool32(bool value) => new() { Value = value ? 1u : 0u };
	public static implicit operator Bool32(uint value) => new() { Value = value };
	public static implicit operator bool(Bool32 value) => value.Value > 0;
	public static implicit operator uint(Bool32 value) => value.Value;
	
	public override string ToString() => (Value == 1).ToString();
}

public readonly struct Char8
{
	public byte Value { get; init; }

	public static implicit operator Char8(char value) => new() { Value = (byte)value };
	public static implicit operator Char8(byte value) => new() { Value = value };
	public static implicit operator char(Char8 c) => (char)c.Value;
	public static implicit operator byte(Char8 c) => c.Value;
	
	public override string ToString() => ((char)Value).ToString();
}

public readonly struct Char16
{
	public ushort Value { get; init; }

	public static implicit operator Char16(char value) => new() { Value = (ushort)value };
	public static implicit operator Char16(ushort value) => new() { Value = value };
	public static implicit operator char(Char16 c) => (char)c.Value;
	public static implicit operator ushort(Char16 c) => c.Value;

	public override string ToString() => ((char)Value).ToString();
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct String192
{
	public readonly nint data;
	public readonly nint size;
	public readonly nint cap;
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct Vector192
{
	public readonly nint size;
	public readonly nint cap;
	public readonly nint data;
}