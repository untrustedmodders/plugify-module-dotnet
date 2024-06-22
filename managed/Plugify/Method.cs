﻿using System.Runtime.InteropServices;

namespace Plugify;

internal enum ValueType : byte {
	Invalid,

	// C types
	Void,
	Bool,
	Char8,
	Char16,
	Int8,
	Int16,
	Int32,
	Int64,
	UInt8,
	UInt16,
	UInt32,
	UInt64,
	Pointer,
	Float,
	Double,
	Function,

	// std::string
	String,

	// std::vector
	ArrayBool,
	ArrayChar8,
	ArrayChar16,
	ArrayInt8,
	ArrayInt16,
	ArrayInt32,
	ArrayInt64,
	ArrayUInt8,
	ArrayUInt16,
	ArrayUInt32,
	ArrayUInt64,
	ArrayPointer,
	ArrayFloat,
	ArrayDouble,
	ArrayString,

	// glm:vec
	Vector2,
	Vector3,
	Vector4,

	// glm:mat
	Matrix4x4,
	//Matrix2x2,
	//Matrix2x3,
	//Matrix2x4,
	//Matrix3x2,
	//Matrix3x3,
	//Matrix3x4,
	//Matrix4x2,
	//Matrix4x3,
};

[StructLayout(LayoutKind.Sequential, Size = 16)]
internal struct Property 
{
	private byte type;
	private bool reference;
	private nint prototype;
	
	public ValueType Type => (ValueType) type;
	public bool Reference => reference;
	public Method? Prototype => prototype != nint.Zero ? Marshal.PtrToStructure<Method>(prototype) : null;
};
	
[StructLayout(LayoutKind.Sequential, Size = 48)]
internal struct Method
{
	private nint nameStringPtr;
	private nint funcNameStringPtr;
	private nint callConvStringPtr;
	private nint paramTypesArrayPtr;
	private nint retType;
	private byte varIndex; 
	private int paramCount;
	
	public string Name => Marshal.PtrToStringAnsi(nameStringPtr);
	public string FunctionName => Marshal.PtrToStringAnsi(funcNameStringPtr);
	public string CallingConvention => Marshal.PtrToStringAnsi(callConvStringPtr);
	public Property[] ParameterTypes
	{
		get
		{
			var paramTypes = new Property[paramCount];
			unsafe
			{
				var paramPropertyPtr = (Property*) paramTypesArrayPtr.ToPointer();
				for (int i = 0; i < paramCount; i++)
				{
					paramTypes[i] = paramPropertyPtr[i];
				}
			}
			return paramTypes;
		}
	}
	public int ParamCount => paramCount;
	public Property ReturnType => Marshal.PtrToStructure<Property>(retType);
	public int VarIndex => varIndex;
}

internal static class TypeMapper
{
    internal static ValueType MonoTypeToValueType(string typeName)
    {
        switch (typeName)
        {
            case "System.Void": return ValueType.Void;
            case "System.Boolean": return ValueType.Bool;
            case "System.Char": return ValueType.Char16;
            case "System.SByte": return ValueType.Int8;
            case "System.Int16": return ValueType.Int16;
            case "System.Int32": return ValueType.Int32;
            case "System.Int64": return ValueType.Int64;
            case "System.Byte": return ValueType.UInt8;
            case "System.UInt16": return ValueType.UInt16;
            case "System.UInt32": return ValueType.UInt32;
            case "System.UInt64": return ValueType.UInt64;
            case "System.IntPtr": return ValueType.Pointer;
            case "System.UIntPtr": return ValueType.Pointer;
            case "System.Single": return ValueType.Float;
            case "System.Double": return ValueType.Double;
            case "System.String": return ValueType.String;
            case "System.Boolean[]": return ValueType.ArrayBool;
            case "System.Char[]": return ValueType.ArrayChar16;
            case "System.SByte[]": return ValueType.ArrayInt8;
            case "System.Int16[]": return ValueType.ArrayInt16;
            case "System.Int32[]": return ValueType.ArrayInt32;
            case "System.Int64[]": return ValueType.ArrayInt64;
            case "System.Byte[]": return ValueType.ArrayUInt8;
            case "System.UInt16[]": return ValueType.ArrayUInt16;
            case "System.UInt32[]": return ValueType.ArrayUInt32;
            case "System.UInt64[]": return ValueType.ArrayUInt64;
            case "System.IntPtr[]": return ValueType.ArrayPointer;
            case "System.UIntPtr[]": return ValueType.ArrayPointer;
            case "System.Single[]": return ValueType.ArrayFloat;
            case "System.Double[]": return ValueType.ArrayDouble;
            case "System.String[]": return ValueType.ArrayString;

            case "System.Boolean&": return ValueType.Bool;
            case "System.Char&": return ValueType.Char16;
            case "System.SByte&": return ValueType.Int8;
            case "System.Int16&": return ValueType.Int16;
            case "System.Int32&": return ValueType.Int32;
            case "System.Int64&": return ValueType.Int64;
            case "System.Byte&": return ValueType.UInt8;
            case "System.UInt16&": return ValueType.UInt16;
            case "System.UInt32&": return ValueType.UInt32;
            case "System.UInt64&": return ValueType.UInt64;
            case "System.IntPtr&": return ValueType.Pointer;
            case "System.UIntPtr&": return ValueType.Pointer;
            case "System.Single&": return ValueType.Float;
            case "System.Double&": return ValueType.Double;
            case "System.String&": return ValueType.String;
            case "System.Boolean[]&": return ValueType.ArrayBool;
            case "System.Char[]&": return ValueType.ArrayChar16;
            case "System.SByte[]&": return ValueType.ArrayInt8;
            case "System.Int16[]&": return ValueType.ArrayInt16;
            case "System.Int32[]&": return ValueType.ArrayInt32;
            case "System.Int64[]&": return ValueType.ArrayInt64;
            case "System.Byte[]&": return ValueType.ArrayUInt8;
            case "System.UInt16[]&": return ValueType.ArrayUInt16;
            case "System.UInt32[]&": return ValueType.ArrayUInt32;
            case "System.UInt64[]&": return ValueType.ArrayUInt64;
            case "System.IntPtr[]&": return ValueType.ArrayPointer;
            case "System.UIntPtr[]&": return ValueType.ArrayPointer;
            case "System.Single[]&": return ValueType.ArrayFloat;
            case "System.Double[]&": return ValueType.ArrayDouble;
            case "System.String[]&": return ValueType.ArrayString;

            case "System.Delegate": return ValueType.Function;
            case "System.Func`1": return ValueType.Function;
            case "System.Func`2": return ValueType.Function;
            case "System.Func`3": return ValueType.Function;
            case "System.Func`4": return ValueType.Function;
            case "System.Func`5": return ValueType.Function;
            case "System.Func`6": return ValueType.Function;
            case "System.Func`7": return ValueType.Function;
            case "System.Func`8": return ValueType.Function;
            case "System.Func`9": return ValueType.Function;
            case "System.Func`10": return ValueType.Function;
            case "System.Func`11": return ValueType.Function;
            case "System.Func`12": return ValueType.Function;
            case "System.Func`13": return ValueType.Function;
            case "System.Func`14": return ValueType.Function;
            case "System.Func`15": return ValueType.Function;
            case "System.Func`16": return ValueType.Function;
            case "System.Func`17": return ValueType.Function;
            case "System.Action": return ValueType.Function;
            case "System.Action`1": return ValueType.Function;
            case "System.Action`2": return ValueType.Function;
            case "System.Action`3": return ValueType.Function;
            case "System.Action`4": return ValueType.Function;
            case "System.Action`5": return ValueType.Function;
            case "System.Action`6": return ValueType.Function;
            case "System.Action`7": return ValueType.Function;
            case "System.Action`8": return ValueType.Function;
            case "System.Action`9": return ValueType.Function;
            case "System.Action`10": return ValueType.Function;
            case "System.Action`11": return ValueType.Function;
            case "System.Action`12": return ValueType.Function;
            case "System.Action`13": return ValueType.Function;
            case "System.Action`14": return ValueType.Function;
            case "System.Action`15": return ValueType.Function;
            case "System.Action`16": return ValueType.Function;

            case "Plugify.Vector2": return ValueType.Vector2;
            case "Plugify.Vector3": return ValueType.Vector3;
            case "Plugify.Vector4": return ValueType.Vector4;
            case "Plugify.Matrix4x4": return ValueType.Matrix4x4;

            case "Plugify.Vector2&": return ValueType.Vector2;
            case "Plugify.Vector3&": return ValueType.Vector3;
            case "Plugify.Vector4&": return ValueType.Vector4;
            case "Plugify.Matrix4x4&": return ValueType.Matrix4x4;

            default: return ValueType.Invalid;
        }
    }
}