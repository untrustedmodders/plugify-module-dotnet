using System.Numerics;
using System.Runtime.InteropServices;

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
	
	// std::any
	Any,

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
	ArrayAny,
	ArrayVector2,
	ArrayVector3,
	ArrayVector4,
	ArrayMatrix4x4,

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
	
	//! Helpers

	_BaseStart = Void,
	_BaseEnd = Function,

	_FloatStart = Float,
	_FloatEnd = Double,

	_ObjectStart = String,
	_ObjectEnd = ArrayMatrix4x4,

	_ArrayStart = ArrayBool,
	_ArrayEnd = ArrayMatrix4x4,

	_StructStart = Vector2,
	_StructEnd = Matrix4x4,

	// First struct which return as hidden parameter
/*#if _WIN32 && !_M_ARM64
	_HiddenParamStart = Vector3,
#else
	_HiddenParamStart = Matrix4x4,
#endif*/
	_LastAssigned = Matrix4x4,
};

internal static class TypeUtils
{
	private static readonly Dictionary<Type, ValueType> TypeSwitcher = new()
	{
		[typeof(void)] = ValueType.Void,
		[typeof(Bool8)] = ValueType.Bool,
		[typeof(Char8)] = ValueType.Char8,
		[typeof(Char16)] = ValueType.Char16,
		[typeof(sbyte)] = ValueType.Int8,
		[typeof(short)] = ValueType.Int16,
		[typeof(int)] = ValueType.Int32,
		[typeof(long)] = ValueType.Int64,
		[typeof(byte)] = ValueType.UInt8,
		[typeof(ushort)] = ValueType.UInt16,
		[typeof(uint)] = ValueType.UInt32,
		[typeof(ulong)] = ValueType.UInt64,
		[typeof(nuint)] = ValueType.Pointer,
		[typeof(nint)] = ValueType.Pointer,
		[typeof(float)] = ValueType.Float,
		[typeof(double)] = ValueType.Double,
		[typeof(Delegate)] = ValueType.Function,
		[typeof(MulticastDelegate)] = ValueType.Function,
		// plg::string
		[typeof(string)] = ValueType.String,
		[typeof(object)] = ValueType.Any,
		// plg::vector
		[typeof(Bool8[])] = ValueType.ArrayBool,
		[typeof(Char8[])] = ValueType.ArrayChar8,
		[typeof(Char16[])] = ValueType.ArrayChar16,
		[typeof(sbyte[])] = ValueType.ArrayInt8,
		[typeof(short[])] = ValueType.ArrayInt16,
		[typeof(int[])] = ValueType.ArrayInt32,
		[typeof(long[])] = ValueType.ArrayInt64,
		[typeof(byte[])] = ValueType.ArrayUInt8,
		[typeof(ushort[])] = ValueType.ArrayUInt16,
		[typeof(uint[])] = ValueType.ArrayUInt32,
		[typeof(ulong[])] = ValueType.ArrayUInt64,
		[typeof(nuint[])] = ValueType.ArrayPointer,
		[typeof(nint[])] = ValueType.ArrayPointer,
		[typeof(float[])] = ValueType.ArrayFloat,
		[typeof(double[])] = ValueType.ArrayDouble,
		[typeof(string[])] = ValueType.ArrayString,
		[typeof(object[])] = ValueType.ArrayAny,
		[typeof(Vector2[])] = ValueType.ArrayVector2,
		[typeof(Vector3[])] = ValueType.ArrayVector3,
		[typeof(Vector4[])] = ValueType.ArrayVector4,
		[typeof(Matrix4x4[])] = ValueType.ArrayMatrix4x4,
		// plg:vec
		[typeof(Vector2)] = ValueType.Vector2,
		[typeof(Vector3)] = ValueType.Vector3,
		[typeof(Vector4)] = ValueType.Vector4,
		// plg:mat
		[typeof(Matrix4x4)] = ValueType.Matrix4x4
	};

	public static ValueType ConvertToValueType(Type type)
	{
		var baseType = type.IsByRef ? type.GetElementType()! : type;
		
		if (baseType.IsEnum)
		{
			baseType = baseType.GetEnumUnderlyingType();
		}
		else if (baseType.IsArray)
		{
			var elementType = baseType.GetElementType()!;
			if (elementType.IsEnum)
			{
				baseType = elementType.GetEnumUnderlyingType().MakeArrayType();
			}
		}

		if (TypeSwitcher.TryGetValue(baseType, out var valueType))
		{
			return valueType;
		}

		return type.IsDelegate() ? ValueType.Function : ValueType.Invalid;
	}
	
	public static Array ConvertToEnumArray<T>(Type enumType, T[] array) where T : unmanaged
	{
		if (!enumType.IsEnum)
		{
			throw new ArgumentException($"{enumType} is not an Enum type.");
		}

		Array enumArray = Array.CreateInstance(enumType, array.Length);

		for (int i = 0; i < enumArray.Length; i++)
		{
			object enumValue = Enum.ToObject(enumType, array[i]);
			enumArray.SetValue(enumValue, i);
		}

		return enumArray;
	}
	
	public static T[] ConvertFromEnumArray<T>(Type enumType, Array enumArray) where T : unmanaged
	{
		if (!enumType.IsEnum)
		{
			throw new ArgumentException($"{enumType.Name} is not an enum type.");
		}

		T[] array = new T[enumArray.Length];

		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (T) Enum.ToObject(enumType, enumArray.GetValue(i) ?? 0);
		}

		return array;
	}
	
	public static unsafe int SizeOf(ValueType valueType)
    {
        switch (valueType)
        {
            case ValueType.Void: return 0;
            case ValueType.Bool: return sizeof(Bool8);
            case ValueType.Char8: return sizeof(Char8);
            case ValueType.Char16: return sizeof(Char16);
            case ValueType.Int8: return sizeof(sbyte); 
            case ValueType.Int16: return sizeof(short); 
            case ValueType.Int32: return sizeof(int); 
            case ValueType.Int64: return sizeof(long);
            case ValueType.UInt8: return sizeof(byte);
            case ValueType.UInt16: return sizeof(ushort);
            case ValueType.UInt32: return sizeof(uint);
            case ValueType.UInt64: return sizeof(ulong);
            case ValueType.Float: return sizeof(float);
            case ValueType.Double: return sizeof(double);
            case ValueType.Function: return sizeof(void*);
            case ValueType.Pointer: return sizeof(nint);
            case ValueType.String: return sizeof(String192);
            case ValueType.Any: return sizeof(Variant256);

            case ValueType.ArrayBool:
            case ValueType.ArrayChar8:
            case ValueType.ArrayChar16:
            case ValueType.ArrayInt8:
            case ValueType.ArrayInt16:
            case ValueType.ArrayInt32:
            case ValueType.ArrayInt64:
            case ValueType.ArrayUInt8:
            case ValueType.ArrayUInt16:
            case ValueType.ArrayUInt32:
            case ValueType.ArrayUInt64:
            case ValueType.ArrayPointer:
            case ValueType.ArrayFloat:
            case ValueType.ArrayDouble:
            case ValueType.ArrayString:
            case ValueType.ArrayAny:
            case ValueType.ArrayVector2:
            case ValueType.ArrayVector3:
            case ValueType.ArrayVector4:
            case ValueType.ArrayMatrix4x4:
                return sizeof(Vector192);

            case ValueType.Vector2: return sizeof(Vector2);
            case ValueType.Vector3: return sizeof(Vector3);
            case ValueType.Vector4: return sizeof(Vector4);
            case ValueType.Matrix4x4: return sizeof(Matrix4x4);

            default: return 0;
        }
    }
}
