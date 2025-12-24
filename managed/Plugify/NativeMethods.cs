using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Plugify;

public static unsafe partial class NativeMethods
{
	public const string DllName = "plugify-module-dotnet";

	#region Core functions

	[LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
	[SuppressGCTransition]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool IsExtensionLoaded(string name, string? constraint);

	#endregion
	
	#region String functions

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetStringLength(String192* str);

	[LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
	[SuppressGCTransition]
	public static partial string GetStringData(String192* str);

	[LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
	[SuppressGCTransition]
	public static partial String192 ConstructString(string? source);
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyString(String192* str);

	[LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
	[SuppressGCTransition]
	public static partial void AssignString(String192* str, string? source);

	#endregion
	
	#region Variant functions

	public static object? GetVariantData(Variant256* var)
	{
		switch ((ValueType)var->currect)
		{
			case ValueType.Invalid:
			case ValueType.Void:
				return null;
			case ValueType.Bool:
				return var->boolean;
			case ValueType.Char8:
				return var->char8;
			case ValueType.Char16:
				return var->char16;
			case ValueType.Int8:
				return var->int8;
			case ValueType.Int16:
				return var->int16;
			case ValueType.Int32:
				return var->int32;
			case ValueType.Int64:
				return var->int64;
			case ValueType.UInt8:
				return var->uint8;
			case ValueType.UInt16:
				return var->uint16;
			case ValueType.UInt32:
				return var->uint32;
			case ValueType.UInt64:
				return var->uint64;
			case ValueType.Pointer:
				return var->ptr;
			case ValueType.Float:
				return var->flt;
			case ValueType.Double:
				return var->dbl;
			case ValueType.String:
				return GetStringData(&var->str);
			case ValueType.Any:
				throw new TypeNotFoundException("Any recursion is not supported");
			case ValueType.ArrayBool:
				return GetVectorDataBool(&var->vec);
			case ValueType.ArrayChar8:
				return GetVectorDataChar8(&var->vec);
			case ValueType.ArrayChar16:
				return GetVectorDataChar16(&var->vec);
			case ValueType.ArrayInt8:
				return GetVectorDataInt8(&var->vec);
			case ValueType.ArrayInt16:
				return GetVectorDataInt16(&var->vec);
			case ValueType.ArrayInt32:
				return GetVectorDataInt32(&var->vec);
			case ValueType.ArrayInt64:
				return GetVectorDataInt64(&var->vec);
			case ValueType.ArrayUInt8:
				return GetVectorDataUInt8(&var->vec);
			case ValueType.ArrayUInt16:
				return GetVectorDataUInt16(&var->vec);
			case ValueType.ArrayUInt32:
				return GetVectorDataUInt32(&var->vec);
			case ValueType.ArrayUInt64:
				return GetVectorDataUInt64(&var->vec);
			case ValueType.ArrayPointer:
				return GetVectorDataIntPtr(&var->vec);
			case ValueType.ArrayFloat:
				return GetVectorDataFloat(&var->vec);
			case ValueType.ArrayDouble:
				return GetVectorDataDouble(&var->vec);
			case ValueType.ArrayString:
				return GetVectorDataString(&var->vec);
			case ValueType.ArrayAny:
				throw new TypeNotFoundException("Any[] recursion is not supported");
			case ValueType.ArrayVector2:
				return GetVectorDataVector2(&var->vec);
			case ValueType.ArrayVector3:
				return GetVectorDataVector3(&var->vec);
			case ValueType.ArrayVector4:
				return GetVectorDataVector4(&var->vec);
			case ValueType.ArrayMatrix4x4:
				return GetVectorDataMatrix4x4(&var->vec);;
			case ValueType.Vector2:
				return var->vec2;
			case ValueType.Vector3:
				return var->vec3;
			case ValueType.Vector4:
				return var->vec4;
			default:
				throw new TypeNotFoundException();
		}
	}

	public static void SetVariantData(Variant256* var, object? paramValue)
	{
		if (paramValue == null)
		{
			return;
		}
		
		ValueType valueType = TypeUtils.ConvertToValueType(paramValue.GetType());
		switch (valueType)
		{
			case ValueType.Bool:
				var->boolean = (Bool8)paramValue;
				break;
			case ValueType.Char8:
				var->char8 = (Char8)paramValue;
				break;
			case ValueType.Char16:
				var->char16 = (Char16)paramValue;
				break;
			case ValueType.Int8:
				var->int8 = (sbyte)paramValue;
				break;
			case ValueType.Int16:
				var->int16 = (short)paramValue;
				break;
			case ValueType.Int32:
				var->int32 = (int)paramValue;
				break;
			case ValueType.Int64:
				var->int64 = (long)paramValue;
				break;
			case ValueType.UInt8:
				var->uint8 = (byte)paramValue;
				break;
			case ValueType.UInt16:
				var->uint16 = (ushort)paramValue;
				break;
			case ValueType.UInt32:
				var->uint32 = (uint)paramValue;
				break;
			case ValueType.UInt64:
				var->uint64 = (ulong)paramValue;
				break;
			case ValueType.Pointer:
				var->ptr = (nint)paramValue;
				break;
			case ValueType.Float:
				var->flt = (float)paramValue;
				break;
			case ValueType.Double:
				var->dbl = (double)paramValue;
				break;
			case ValueType.String:
				var->str = ConstructString((string)paramValue);
				break;
			case ValueType.Any:
				throw new TypeNotFoundException("Any recursion is not supported");
			case ValueType.ArrayBool:
			{
				var arr = (Bool8[])paramValue;
				var->vec = ConstructVectorBool(arr, arr.Length);
				break;
			}
			case ValueType.ArrayChar8:
			{
				var arr = (Char8[])paramValue;
				var->vec = ConstructVectorChar8(arr, arr.Length);
				break;
			}
			case ValueType.ArrayChar16:
			{
				var arr = (Char16[])paramValue;
				var->vec = ConstructVectorChar16(arr, arr.Length);
				break;
			}
			case ValueType.ArrayInt8:
			{
				var arr = (sbyte[])paramValue;
				var->vec = ConstructVectorInt8(arr, arr.Length);
				break;
			}
			case ValueType.ArrayInt16:
			{
				var arr = (short[])paramValue;
				var->vec = ConstructVectorInt16(arr, arr.Length);
				break;
			}
			case ValueType.ArrayInt32:
			{
				var arr = (int[])paramValue;
				var->vec = ConstructVectorInt32(arr, arr.Length);
				break;
			}
			case ValueType.ArrayInt64:
			{
				var arr = (long[])paramValue;
				var->vec = ConstructVectorInt64(arr, arr.Length);
				break;
			}
			case ValueType.ArrayUInt8:
			{
				var arr = (byte[])paramValue;
				var->vec = ConstructVectorUInt8(arr, arr.Length);
				break;
			}
			case ValueType.ArrayUInt16:
			{
				var arr = (ushort[])paramValue;
				var->vec = ConstructVectorUInt16(arr, arr.Length);
				break;
			}
			case ValueType.ArrayUInt32:
			{
				var arr = (uint[])paramValue;
				var->vec = ConstructVectorUInt32(arr, arr.Length);
				break;
			}
			case ValueType.ArrayUInt64:
			{
				var arr = (ulong[])paramValue;
				var->vec = ConstructVectorUInt64(arr, arr.Length);
				break;
			}
			case ValueType.ArrayPointer:
			{
				var arr = (nint[])paramValue;
				var->vec = ConstructVectorIntPtr(arr, arr.Length);
				break;
			}
			case ValueType.ArrayFloat:
			{
				var arr = (float[])paramValue;
				var->vec = ConstructVectorFloat(arr, arr.Length);
				break;
			}
			case ValueType.ArrayDouble:
			{
				var arr = (double[])paramValue;
				var->vec = ConstructVectorDouble(arr, arr.Length);
				break;
			}
			case ValueType.ArrayString:
			{
				var arr = (string[])paramValue;
				var->vec = ConstructVectorString(arr, arr.Length);
				break;
			}
			case ValueType.ArrayAny:
			{
				throw new TypeNotFoundException("Any[] recursion is not supported");
			}
			case ValueType.ArrayVector2:
			{
				var arr = (Vector2[])paramValue;
				var->vec = ConstructVectorVector2(arr, arr.Length);
				break;
			}
			case ValueType.ArrayVector3:
			{
				var arr = (Vector3[])paramValue;
				var->vec = ConstructVectorVector3(arr, arr.Length);
				break;
			}
			case ValueType.ArrayVector4:
			{
				var arr = (Vector4[])paramValue;
				var->vec = ConstructVectorVector4(arr, arr.Length);
				break;
			}
			case ValueType.ArrayMatrix4x4:
			{
				var arr = (Matrix4x4[])paramValue;
				var->vec = ConstructVectorMatrix4x4(arr, arr.Length);
				break;
			}
			case ValueType.Vector2:
				var->vec2 = (Vector2)paramValue;
				break;
			case ValueType.Vector3:
				var->vec3 = (Vector3)paramValue;
				break;
			case ValueType.Vector4:
				var->vec4 = (Vector4)paramValue;
				break;
			default:
				throw new TypeNotFoundException();
		}
		var->currect = (int)valueType;
	}
	
	public static void AssignVariant(Variant256* var, object? paramValue)
	{
		DestroyVariant(var);
		SetVariantData(var, paramValue);
	}

	public static Variant256 ConstructVariant(object? source)
	{
		Variant256 var;
		SetVariantData(&var, source);
		return var;
	}
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVariant(Variant256* var);

	#endregion

	#region GetVectorSize functions

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeBool(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeChar8(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeChar16(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeInt8(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeInt16(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeInt32(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeInt64(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeUInt8(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeUInt16(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeUInt32(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeUInt64(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeIntPtr(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeFloat(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeDouble(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeString(Vector192* vec);
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeVariant(Vector192* vec);
		
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeVector2(Vector192* vec);
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeVector3(Vector192* vec);
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeVector4(Vector192* vec);
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeMatrix4x4(Vector192* vec);
	
	#endregion

	#region GetVectorData functions

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataBool(Vector192* vec, [In, Out] Bool8[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataChar8(Vector192* vec, [In, Out] Char8[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataChar16(Vector192* vec, [In, Out] Char16[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataInt8(Vector192* vec, [In, Out] sbyte[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataInt16(Vector192* vec, [In, Out] short[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataInt32(Vector192* vec, [In, Out] int[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataInt64(Vector192* vec, [In, Out] long[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataUInt8(Vector192* vec, [In, Out] byte[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataUInt16(Vector192* vec, [In, Out] ushort[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataUInt32(Vector192* vec, [In, Out] uint[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataUInt64(Vector192* vec, [In, Out] ulong[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataIntPtr(Vector192* vec, [In, Out] nint[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataFloat(Vector192* vec, [In, Out] float[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataDouble(Vector192* vec, [In, Out] double[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataString(Vector192* vec, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] [In, Out] string[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Variant256* GetVectorDataVariant(Vector192* vec, int index);

	public static void GetVectorDataVariant(Vector192* vec, [In, Out] object?[] arr)
	{
		int len = GetVectorSizeVariant(vec);
		for (int i = 0; i < len; i++)
		{
			Variant256* var = GetVectorDataVariant(vec, i);
			arr[i] = GetVariantData(var);
		}
	}
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataVector2(Vector192* vec, [In, Out] Vector2[] arr);
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataVector3(Vector192* vec, [In, Out] Vector3[] arr);
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataVector4(Vector192* vec, [In, Out] Vector4[] arr);
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataMatrix4x4(Vector192* vec, [In, Out] Matrix4x4[] arr);

	public static Bool8[] GetVectorDataBool(Vector192* vec)
	{
	    var arr = new Bool8[GetVectorSizeBool(vec)];
	    GetVectorDataBool(vec, arr);
	    return arr;
	}

	public static Char8[] GetVectorDataChar8(Vector192* vec)
	{
	    var arr = new Char8[GetVectorSizeChar8(vec)];
	    GetVectorDataChar8(vec, arr);
	    return arr;
	}

	public static Char16[] GetVectorDataChar16(Vector192* vec)
	{
	    var arr = new Char16[GetVectorSizeChar16(vec)];
	    GetVectorDataChar16(vec, arr);
	    return arr;
	}

	public static sbyte[] GetVectorDataInt8(Vector192* vec)
	{
	    var arr = new sbyte[GetVectorSizeInt8(vec)];
	    GetVectorDataInt8(vec, arr);
	    return arr;
	}

	public static short[] GetVectorDataInt16(Vector192* vec)
	{
	    var arr = new short[GetVectorSizeInt16(vec)];
	    GetVectorDataInt16(vec, arr);
	    return arr;
	}

	public static int[] GetVectorDataInt32(Vector192* vec)
	{
	    var arr = new int[GetVectorSizeInt32(vec)];
	    GetVectorDataInt32(vec, arr);
	    return arr;
	}

	public static long[] GetVectorDataInt64(Vector192* vec)
	{
	    var arr = new long[GetVectorSizeInt64(vec)];
	    GetVectorDataInt64(vec, arr);
	    return arr;
	}

	public static byte[] GetVectorDataUInt8(Vector192* vec)
	{
	    var arr = new byte[GetVectorSizeUInt8(vec)];
	    GetVectorDataUInt8(vec, arr);
	    return arr;
	}

	public static ushort[] GetVectorDataUInt16(Vector192* vec)
	{
	    var arr = new ushort[GetVectorSizeUInt16(vec)];
	    GetVectorDataUInt16(vec, arr);
	    return arr;
	}

	public static uint[] GetVectorDataUInt32(Vector192* vec)
	{
	    var arr = new uint[GetVectorSizeUInt32(vec)];
	    GetVectorDataUInt32(vec, arr);
	    return arr;
	}

	public static ulong[] GetVectorDataUInt64(Vector192* vec)
	{
	    var arr = new ulong[GetVectorSizeUInt64(vec)];
	    GetVectorDataUInt64(vec, arr);
	    return arr;
	}

	public static nint[] GetVectorDataIntPtr(Vector192* vec)
	{
	    var arr = new nint[GetVectorSizeIntPtr(vec)];
	    GetVectorDataIntPtr(vec, arr);
	    return arr;
	}

	public static float[] GetVectorDataFloat(Vector192* vec)
	{
	    var arr = new float[GetVectorSizeFloat(vec)];
	    GetVectorDataFloat(vec, arr);
	    return arr;
	}

	public static double[] GetVectorDataDouble(Vector192* vec)
	{
	    var arr = new double[GetVectorSizeDouble(vec)];
	    GetVectorDataDouble(vec, arr);
	    return arr;
	}

	public static string[] GetVectorDataString(Vector192* vec)
	{
	    var arr = new string[GetVectorSizeString(vec)];
	    GetVectorDataString(vec, arr);
	    return arr;
	}

	public static object[] GetVectorDataVariant(Vector192* vec)
	{
	    var arr = new object[GetVectorSizeVariant(vec)];
	    GetVectorDataVariant(vec, arr);
	    return arr;
	}

	public static Vector2[] GetVectorDataVector2(Vector192* vec)
	{
	    var arr = new Vector2[GetVectorSizeVector2(vec)];
	    GetVectorDataVector2(vec, arr);
	    return arr;
	}

	public static Vector3[] GetVectorDataVector3(Vector192* vec)
	{
	    var arr = new Vector3[GetVectorSizeVector3(vec)];
	    GetVectorDataVector3(vec, arr);
	    return arr;
	}

	public static Vector4[] GetVectorDataVector4(Vector192* vec)
	{
	    var arr = new Vector4[GetVectorSizeVector4(vec)];
	    GetVectorDataVector4(vec, arr);
	    return arr;
	}

	public static Matrix4x4[] GetVectorDataMatrix4x4(Vector192* vec)
	{
	    var arr = new Matrix4x4[GetVectorSizeMatrix4x4(vec)];
	    GetVectorDataMatrix4x4(vec, arr);
	    return arr;
	}
	
	#endregion

	#region ConstructVector Functions

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorBool([In] Bool8[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorChar8([In] Char8[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorChar16([In] Char16[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorInt8([In] sbyte[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorInt16([In] short[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorInt32([In] int[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorInt64([In] long[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorUInt8([In] byte[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorUInt16([In] ushort[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorUInt32([In] uint[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorUInt64([In] ulong[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorIntPtr([In] nint[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorFloat([In] float[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorDouble([In] double[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorString([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] [In] string[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorVariant(int len);
	
	public static Vector192 ConstructVectorVariant([In] object?[] arr, int len)
	{
		Vector192 vec = ConstructVectorVariant(len);
		AssignVectorVariant(&vec, arr, len);
		return vec;
	}

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorVector2([In] Vector2[] arr, int len);
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorVector3([In] Vector3[] arr, int len);
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorVector4([In] Vector4[] arr, int len);
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial Vector192 ConstructVectorMatrix4x4([In] Matrix4x4[] arr, int len);
	
	#endregion
	
	#region DestroyVector functions

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorBool(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorChar8(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorChar16(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorInt8(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorInt16(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorInt32(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorInt64(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorUInt8(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorUInt16(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorUInt32(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorUInt64(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorIntPtr(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorFloat(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorDouble(Vector192* vec);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorString(Vector192* vec);
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorVariant(Vector192* vec);
		
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorVector2(Vector192* vec);
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorVector3(Vector192* vec);
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorVector4(Vector192* vec);
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DestroyVectorMatrix4x4(Vector192* vec);
	
	#endregion
	
	#region AssignVector Functions
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorBool(Vector192* vec, [In] Bool8[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorChar8(Vector192* vec, [In] Char8[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorChar16(Vector192* vec, [In] Char16[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorInt8(Vector192* vec, [In] sbyte[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorInt16(Vector192* vec, [In] short[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorInt32(Vector192* vec, [In] int[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorInt64(Vector192* vec, [In] long[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorUInt8(Vector192* vec, [In] byte[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorUInt16(Vector192* vec, [In] ushort[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorUInt32(Vector192* vec, [In] uint[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorUInt64(Vector192* vec, [In] ulong[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorIntPtr(Vector192* vec, [In] nint[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorFloat(Vector192* vec, [In] float[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorDouble(Vector192* vec, [In] double[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorString(Vector192* vec, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] [In] string[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorVariant(Vector192* vec, int len);

	public static void AssignVectorVariant(Vector192* vec, [In] object?[] arr, int len)
	{
		AssignVectorVariant(vec, len);
		for (int i = 0; i < len; i++)
		{
			AssignVariant(GetVectorDataVariant(vec, i), arr[i]);
		}
	}
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorVector2(Vector192* vec, [In] Vector2[] arr, int len);
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorVector3(Vector192* vec, [In] Vector3[] arr, int len);
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorVector4(Vector192* vec, [In] Vector4[] arr, int len);
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorMatrix4x4(Vector192* vec, [In] Matrix4x4[] arr, int len);
	
	#endregion
	
	#region ConstructHelpers Functions
	
	public static Vector192 ConstructVectorBool([In] Bool8[] arr) 
		=> ConstructVectorBool(arr, arr.Length);

	public static Vector192 ConstructVectorChar8([In] Char8[] arr) 
	    => ConstructVectorChar8(arr, arr.Length);

	public static Vector192 ConstructVectorChar16([In] Char16[] arr) 
	    => ConstructVectorChar16(arr, arr.Length);

	public static Vector192 ConstructVectorInt8([In] sbyte[] arr) 
	    => ConstructVectorInt8(arr, arr.Length);

	public static Vector192 ConstructVectorInt16([In] short[] arr) 
	    => ConstructVectorInt16(arr, arr.Length);

	public static Vector192 ConstructVectorInt32([In] int[] arr) 
	    => ConstructVectorInt32(arr, arr.Length);

	public static Vector192 ConstructVectorInt64([In] long[] arr) 
	    => ConstructVectorInt64(arr, arr.Length);

	public static Vector192 ConstructVectorUInt8([In] byte[] arr) 
	    => ConstructVectorUInt8(arr, arr.Length);

	public static Vector192 ConstructVectorUInt16([In] ushort[] arr) 
	    => ConstructVectorUInt16(arr, arr.Length);

	public static Vector192 ConstructVectorUInt32([In] uint[] arr) 
	    => ConstructVectorUInt32(arr, arr.Length);

	public static Vector192 ConstructVectorUInt64([In] ulong[] arr) 
	    => ConstructVectorUInt64(arr, arr.Length);

	public static Vector192 ConstructVectorIntPtr([In] nint[] arr) 
	    => ConstructVectorIntPtr(arr, arr.Length);

	public static Vector192 ConstructVectorFloat([In] float[] arr) 
	    => ConstructVectorFloat(arr, arr.Length);

	public static Vector192 ConstructVectorDouble([In] double[] arr) 
	    => ConstructVectorDouble(arr, arr.Length);

	public static Vector192 ConstructVectorString([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] [In] string[] arr) 
	    => ConstructVectorString(arr, arr.Length);

	public static Vector192 ConstructVectorVariant([In] object?[] arr) 
	    => ConstructVectorVariant(arr, arr.Length);

	public static Vector192 ConstructVectorVector2([In] Vector2[] arr) 
	    => ConstructVectorVector2(arr, arr.Length);

	public static Vector192 ConstructVectorVector3([In] Vector3[] arr) 
	    => ConstructVectorVector3(arr, arr.Length);

	public static Vector192 ConstructVectorVector4([In] Vector4[] arr) 
	    => ConstructVectorVector4(arr, arr.Length);

	public static Vector192 ConstructVectorMatrix4x4([In] Matrix4x4[] arr) 
	    => ConstructVectorMatrix4x4(arr, arr.Length);
	
	#endregion ConstructHelpers Functions
}

public static unsafe partial class NativeMethodsT
{
	[LibraryImport("plugify-module-dotnet", EntryPoint = "GetVectorDataInt8")]
	[SuppressGCTransition]
	private static partial void __GetVectorDataInt8(Vector192* vec, sbyte* arrNative);
	
	public static void GetVectorDataInt8<T>(Vector192* vec, [In, Out] T[] arr) where T : unmanaged
	{
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			__GetVectorDataInt8(vec, (sbyte*)arrNative);
		}
	}
	
	[LibraryImport("plugify-module-dotnet", EntryPoint = "GetVectorDataInt16")]
	[SuppressGCTransition]
	private static partial void __GetVectorDataInt16(Vector192* vec, sbyte* arrNative);

	public static void GetVectorDataInt16<T>(Vector192* vec, [In, Out] T[] arr) where T : unmanaged
	{
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			__GetVectorDataInt16(vec, (sbyte*)arrNative);
		}
	}
	
	[LibraryImport("plugify-module-dotnet", EntryPoint = "GetVectorDataInt32")]
	[SuppressGCTransition]
	private static partial void __GetVectorDataInt32(Vector192* vec, sbyte* arrNative);
	
	public static void GetVectorDataInt32<T>(Vector192* vec, [In, Out] T[] arr) where T : unmanaged
	{
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			__GetVectorDataInt32(vec, (sbyte*)arrNative);
		}
	}
	
	[LibraryImport("plugify-module-dotnet", EntryPoint = "GetVectorDataInt64")]
	[SuppressGCTransition]
	private static partial void __GetVectorDataInt64(Vector192* vec, sbyte* arrNative);

	public static void GetVectorDataInt64<T>(Vector192* vec, [In, Out] T[] arr) where T : unmanaged
	{
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			__GetVectorDataInt64(vec, (sbyte*)arrNative);
		}
	}
	
	[LibraryImport("plugify-module-dotnet", EntryPoint = "GetVectorDataUInt8")]
	[SuppressGCTransition]
	private static partial void __GetVectorDataUInt8(Vector192* vec, sbyte* arrNative);

	public static void GetVectorDataUInt8<T>(Vector192* vec, [In, Out] T[] arr) where T : unmanaged
	{
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			__GetVectorDataUInt8(vec, (sbyte*)arrNative);
		}
	}
	
	[LibraryImport("plugify-module-dotnet", EntryPoint = "GetVectorDataUInt16")]
	[SuppressGCTransition]
	private static partial void __GetVectorDataUInt16(Vector192* vec, sbyte* arrNative);

	public static void GetVectorDataUInt16<T>(Vector192* vec, [In, Out] T[] arr) where T : unmanaged
	{
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			__GetVectorDataUInt16(vec, (sbyte*)arrNative);
		}
	}
	
	[LibraryImport("plugify-module-dotnet", EntryPoint = "GetVectorDataUInt32")]
	[SuppressGCTransition]
	private static partial void __GetVectorDataUInt32(Vector192* vec, sbyte* arrNative);

	public static void GetVectorDataUInt32<T>(Vector192* vec, [In, Out] T[] arr) where T : unmanaged
	{
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			__GetVectorDataUInt32(vec, (sbyte*)arrNative);
		}
	}
	
	[LibraryImport("plugify-module-dotnet", EntryPoint = "GetVectorDataUInt64")]
	[SuppressGCTransition]
	private static partial void __GetVectorDataUInt64(Vector192* vec, sbyte* arrNative);

	public static void GetVectorDataUInt64<T>(Vector192* vec, [In, Out] T[] arr) where T : unmanaged
	{
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			__GetVectorDataUInt64(vec, (sbyte*)arrNative);
		}
	}
	
	
	[LibraryImport(NativeMethods.DllName, EntryPoint = "ConstructVectorInt8")]
	[SuppressGCTransition]
	private static partial Vector192 __ConstructVectorInt8(sbyte* arrNative, int lenNative);

	public static Vector192 ConstructVectorInt8<T>([In] T[] arr, int len) where T : unmanaged
	{
		Vector192 retVal;
		fixed (T* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			retVal = __ConstructVectorInt8((sbyte*)arrNative, len);
		}
		return retVal;
	}

	[LibraryImport(NativeMethods.DllName, EntryPoint = "ConstructVectorInt16")]
	[SuppressGCTransition]
	private static partial Vector192 __ConstructVectorInt16(short* arrNative, int lenNative);

	public static Vector192 ConstructVectorInt16<T>([In] T[] arr, int len) where T : unmanaged
	{
		Vector192 retVal;
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			retVal = __ConstructVectorInt16((short*)arrNative, len);
		}
		return retVal;
	}

	[LibraryImport(NativeMethods.DllName, EntryPoint = "ConstructVectorInt32")]
	[SuppressGCTransition]
	private static partial Vector192 __ConstructVectorInt32(int* arrNative, int lenNative);

	public static Vector192 ConstructVectorInt32<T>([In] T[] arr, int len) where T : unmanaged
	{
		Vector192 retVal;
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			retVal = __ConstructVectorInt32((int*)arrNative, len);
		}
		return retVal;
	}

	[LibraryImport(NativeMethods.DllName, EntryPoint = "ConstructVectorInt64")]
	[SuppressGCTransition]
	private static partial Vector192 __ConstructVectorInt64(long* arrNative, int lenNative);

	public static Vector192 ConstructVectorInt64<T>([In] T[] arr, int len) where T : unmanaged
	{
		Vector192 retVal;
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			retVal = __ConstructVectorInt64((long*)arrNative, len);
		}
		return retVal;
	}

	[LibraryImport(NativeMethods.DllName, EntryPoint = "ConstructVectorUInt8")]
	[SuppressGCTransition]
	private static partial Vector192 __ConstructVectorUInt8(byte* arrNative, int lenNative);

	public static Vector192 ConstructVectorUInt8<T>([In] T[] arr, int len) where T : unmanaged
	{
		Vector192 retVal;
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			retVal = __ConstructVectorUInt8((byte*)arrNative, len);
		}
		return retVal;
	}

	[LibraryImport(NativeMethods.DllName, EntryPoint = "ConstructVectorUInt16")]
	[SuppressGCTransition]
	private static partial Vector192 __ConstructVectorUInt16(ushort* arrNative, int lenNative);

	public static Vector192 ConstructVectorUInt16<T>([In] T[] arr, int len) where T : unmanaged
	{
		Vector192 retVal;
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			retVal = __ConstructVectorUInt16((ushort*)arrNative, len);
		}
		return retVal;
	}

	[LibraryImport(NativeMethods.DllName, EntryPoint = "ConstructVectorUInt32")]
	[SuppressGCTransition]
	private static partial Vector192 __ConstructVectorUInt32(uint* arrNative, int lenNative);

	public static Vector192 ConstructVectorUInt32<T>([In] T[] arr, int len) where T : unmanaged
	{
		Vector192 retVal;
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			retVal = __ConstructVectorUInt32((uint*)arrNative, len);
		}
		return retVal;
	}

	[LibraryImport(NativeMethods.DllName, EntryPoint = "ConstructVectorUInt64")]
	[SuppressGCTransition]
	private static partial Vector192 __ConstructVectorUInt64(ulong* arrNative, int lenNative);

	public static Vector192 ConstructVectorUInt64<T>([In] T[] arr, int len) where T : unmanaged
	{
		Vector192 retVal;
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			retVal = __ConstructVectorUInt64((ulong*)arrNative, len);
		}
		return retVal;
	}
}