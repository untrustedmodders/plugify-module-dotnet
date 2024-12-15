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
	public static partial string GetBaseDir();
	[LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
	[SuppressGCTransition]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool IsModuleLoaded(string moduleName, int version, [MarshalAs(UnmanagedType.I1)] bool minimum);
	[LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
	[SuppressGCTransition]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool IsPluginLoaded(string pluginName, int version, [MarshalAs(UnmanagedType.I1)] bool minimum);
	[LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
	[SuppressGCTransition]
	public static partial string? FindPluginResource(long pluginId, string path);

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

	public static object? GetVariantData(Variant256* var, Type? type = null)
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
			case ValueType.Function:
				return Marshalling.GetDelegateForFunctionPointer(var->ptr, type);
			case ValueType.String:
				return NativeMethods.GetStringData(&var->str);
			case ValueType.ArrayBool:
				var ptrBool = &var->vec;
				var arrBool = new Bool8[NativeMethods.GetVectorSizeBool(ptrBool)];
				NativeMethods.GetVectorDataBool(ptrBool, arrBool);
				return arrBool;
			case ValueType.ArrayChar8:
				var ptrChar8 = &var->vec;
				var arrChar8 = new Char8[NativeMethods.GetVectorSizeChar8(ptrChar8)];
				NativeMethods.GetVectorDataChar8(ptrChar8, arrChar8);
				return arrChar8;
			case ValueType.ArrayChar16:
				var ptrChar16 = &var->vec;
				var arrChar16 = new Char16[NativeMethods.GetVectorSizeChar16(ptrChar16)];
				NativeMethods.GetVectorDataChar16(ptrChar16, arrChar16);
				return arrChar16;
			case ValueType.ArrayInt8:
				var ptrInt8 = &var->vec;
				var arrInt8 = new sbyte[NativeMethods.GetVectorSizeInt8(ptrInt8)];
				NativeMethods.GetVectorDataInt8(ptrInt8, arrInt8);
				return arrInt8;
			case ValueType.ArrayInt16:
				var ptrInt16 = &var->vec;
				var arrInt16 = new short[NativeMethods.GetVectorSizeInt16(ptrInt16)];
				NativeMethods.GetVectorDataInt16(ptrInt16, arrInt16);
				return arrInt16;
			case ValueType.ArrayInt32:
				var ptrInt32 = &var->vec;
				var arrInt32 = new int[NativeMethods.GetVectorSizeInt32(ptrInt32)];
				NativeMethods.GetVectorDataInt32(ptrInt32, arrInt32);
				return arrInt32;
			case ValueType.ArrayInt64:
				var ptrInt64 = &var->vec;
				var arrInt64 = new long[NativeMethods.GetVectorSizeInt64(ptrInt64)];
				NativeMethods.GetVectorDataInt64(ptrInt64, arrInt64);
				return arrInt64;
			case ValueType.ArrayUInt8:
				var ptrUInt8 = &var->vec;
				var arrUInt8 = new byte[NativeMethods.GetVectorSizeUInt8(ptrUInt8)];
				NativeMethods.GetVectorDataUInt8(ptrUInt8, arrUInt8);
				return arrUInt8;
			case ValueType.ArrayUInt16:
				var ptrUInt16 = &var->vec;
				var arrUInt16 = new ushort[NativeMethods.GetVectorSizeUInt16(ptrUInt16)];
				NativeMethods.GetVectorDataUInt16(ptrUInt16, arrUInt16);
				return arrUInt16;
			case ValueType.ArrayUInt32:
				var ptrUInt32 = &var->vec;
				var arrUInt32 = new uint[NativeMethods.GetVectorSizeUInt32(ptrUInt32)];
				NativeMethods.GetVectorDataUInt32(ptrUInt32, arrUInt32);
				return arrUInt32;
			case ValueType.ArrayUInt64:
				var ptrUInt64 = &var->vec;
				var arrUInt64 = new ulong[NativeMethods.GetVectorSizeUInt64(ptrUInt64)];
				NativeMethods.GetVectorDataUInt64(ptrUInt64, arrUInt64);
				return arrUInt64;
			case ValueType.ArrayPointer:
				var ptrIntPtr = &var->vec;
				var arrIntPtr = new nint[NativeMethods.GetVectorSizeIntPtr(ptrIntPtr)];
				NativeMethods.GetVectorDataIntPtr(ptrIntPtr, arrIntPtr);
				return arrIntPtr;
			case ValueType.ArrayFloat:
				var ptrFloat = &var->vec;
				var arrFloat = new float[NativeMethods.GetVectorSizeFloat(ptrFloat)];
				NativeMethods.GetVectorDataFloat(ptrFloat, arrFloat);
				return arrFloat;
			case ValueType.ArrayDouble:
				var ptrDouble = &var->vec;
				var arrDouble = new double[NativeMethods.GetVectorSizeDouble(ptrDouble)];
				NativeMethods.GetVectorDataDouble(ptrDouble, arrDouble);
				return arrDouble;
			case ValueType.ArrayString:
				var ptrString = &var->vec;
				var arrString = new string[NativeMethods.GetVectorSizeString(ptrString)];
				NativeMethods.GetVectorDataString(ptrString, arrString);
				return arrString;
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
			return;
		
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
			case ValueType.Function:
				var->ptr = Marshalling.GetFunctionPointerForDelegate((Delegate)paramValue);
				break;
			case ValueType.String:
				var->str = NativeMethods.ConstructString((string)paramValue);
				break;
			case ValueType.ArrayBool:
				var arrBool = (Bool8[])paramValue;
				var->vec = NativeMethods.ConstructVectorBool(arrBool, arrBool.Length);
				break;
			case ValueType.ArrayChar8:
				var arrChar8 = (Char8[])paramValue;
				var->vec = NativeMethods.ConstructVectorChar8(arrChar8, arrChar8.Length);
				break;
			case ValueType.ArrayChar16:
				var arrChar16 = (Char16[])paramValue;
				var->vec = NativeMethods.ConstructVectorChar16(arrChar16, arrChar16.Length);
				break;
			case ValueType.ArrayInt8:
				var arrInt8 = (sbyte[])paramValue;
				var->vec = NativeMethods.ConstructVectorInt8(arrInt8, arrInt8.Length);
				break;
			case ValueType.ArrayInt16:
				var arrInt16 = (short[])paramValue;
				var->vec = NativeMethods.ConstructVectorInt16(arrInt16, arrInt16.Length);
				break;
			case ValueType.ArrayInt32:
				var arrInt32 = (int[])paramValue;
				var->vec = NativeMethods.ConstructVectorInt32(arrInt32, arrInt32.Length);
				break;
			case ValueType.ArrayInt64:
				var arrInt64 = (long[])paramValue;
				var->vec = NativeMethods.ConstructVectorInt64(arrInt64, arrInt64.Length);
				break;
			case ValueType.ArrayUInt8:
				var arrUInt8 = (byte[])paramValue;
				var->vec = NativeMethods.ConstructVectorUInt8(arrUInt8, arrUInt8.Length);
				break;
			case ValueType.ArrayUInt16:
				var arrUInt16 = (ushort[])paramValue;
				var->vec = NativeMethods.ConstructVectorUInt16(arrUInt16, arrUInt16.Length);
				break;
			case ValueType.ArrayUInt32:
				var arrUInt32 = (uint[])paramValue;
				var->vec = NativeMethods.ConstructVectorUInt32(arrUInt32, arrUInt32.Length);
				break;
			case ValueType.ArrayUInt64:
				var arrUInt64 = (ulong[])paramValue;
				var->vec = NativeMethods.ConstructVectorUInt64(arrUInt64, arrUInt64.Length);
				break;
			case ValueType.ArrayPointer:
				var arrIntPtr = (nint[])paramValue;
				var->vec = NativeMethods.ConstructVectorIntPtr(arrIntPtr, arrIntPtr.Length);
				break;
			case ValueType.ArrayFloat:
				var arrFloat = (float[])paramValue;
				var->vec = NativeMethods.ConstructVectorFloat(arrFloat, arrFloat.Length);
				break;
			case ValueType.ArrayDouble:
				var arrDouble = (double[])paramValue;
				var->vec = NativeMethods.ConstructVectorDouble(arrDouble, arrDouble.Length);
				break;
			case ValueType.ArrayString:
				var arrString = (string[])paramValue;
				var->vec = NativeMethods.ConstructVectorString(arrString, arrString.Length);
				break;
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
			arr[i] = GetVariantData(var, arr[i]?.GetType());
		}
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
			Variant256* var = GetVectorDataVariant(vec, i);
			SetVariantData(var, arr[i]);
		}
	}
	
	#endregion
}

public static unsafe class NativeMethodsT
{
	[DllImport("plugify-module-dotnet", EntryPoint = "GetVectorDataInt8", ExactSpelling = true)]
	[SuppressGCTransition]
	private static extern void __GetVectorDataInt8(Vector192* vec, sbyte* arrNative);
	
	public static void GetVectorDataInt8<T>(Vector192* vec, [In, Out] T[] arr) where T : unmanaged
	{
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			__GetVectorDataInt8(vec, (sbyte*)arrNative);
		}
	}
	
	[DllImport("plugify-module-dotnet", EntryPoint = "GetVectorDataInt16", ExactSpelling = true)]
	[SuppressGCTransition]
	private static extern void __GetVectorDataInt16(Vector192* vec, sbyte* arrNative);

	public static void GetVectorDataInt16<T>(Vector192* vec, [In, Out] T[] arr) where T : unmanaged
	{
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			__GetVectorDataInt16(vec, (sbyte*)arrNative);
		}
	}
	
	[DllImport("plugify-module-dotnet", EntryPoint = "GetVectorDataInt32", ExactSpelling = true)]
	[SuppressGCTransition]
	private static extern void __GetVectorDataInt32(Vector192* vec, sbyte* arrNative);
	
	public static void GetVectorDataInt32<T>(Vector192* vec, [In, Out] T[] arr) where T : unmanaged
	{
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			__GetVectorDataInt32(vec, (sbyte*)arrNative);
		}
	}
	
	[DllImport("plugify-module-dotnet", EntryPoint = "GetVectorDataInt64", ExactSpelling = true)]
	[SuppressGCTransition]
	private static extern void __GetVectorDataInt64(Vector192* vec, sbyte* arrNative);

	public static void GetVectorDataInt64<T>(Vector192* vec, [In, Out] T[] arr) where T : unmanaged
	{
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			__GetVectorDataInt64(vec, (sbyte*)arrNative);
		}
	}
	
	[DllImport("plugify-module-dotnet", EntryPoint = "GetVectorDataUInt8", ExactSpelling = true)]
	[SuppressGCTransition]
	private static extern void __GetVectorDataUInt8(Vector192* vec, sbyte* arrNative);

	public static void GetVectorDataUInt8<T>(Vector192* vec, [In, Out] T[] arr) where T : unmanaged
	{
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			__GetVectorDataUInt8(vec, (sbyte*)arrNative);
		}
	}
	
	[DllImport("plugify-module-dotnet", EntryPoint = "GetVectorDataUInt16", ExactSpelling = true)]
	[SuppressGCTransition]
	private static extern void __GetVectorDataUInt16(Vector192* vec, sbyte* arrNative);

	public static void GetVectorDataUInt16<T>(Vector192* vec, [In, Out] T[] arr) where T : unmanaged
	{
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			__GetVectorDataUInt16(vec, (sbyte*)arrNative);
		}
	}
	
	[DllImport("plugify-module-dotnet", EntryPoint = "GetVectorDataUInt32", ExactSpelling = true)]
	[SuppressGCTransition]
	private static extern void __GetVectorDataUInt32(Vector192* vec, sbyte* arrNative);

	public static void GetVectorDataUInt32<T>(Vector192* vec, [In, Out] T[] arr) where T : unmanaged
	{
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			__GetVectorDataUInt32(vec, (sbyte*)arrNative);
		}
	}
	
	[DllImport("plugify-module-dotnet", EntryPoint = "GetVectorDataUInt64", ExactSpelling = true)]
	[SuppressGCTransition]
	private static extern void __GetVectorDataUInt64(Vector192* vec, sbyte* arrNative);

	public static void GetVectorDataUInt64<T>(Vector192* vec, [In, Out] T[] arr) where T : unmanaged
	{
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			__GetVectorDataUInt64(vec, (sbyte*)arrNative);
		}
	}
	
	
	[DllImport(NativeMethods.DllName, EntryPoint = "ConstructVectorInt8", ExactSpelling = true)]
	[SuppressGCTransition]
	private static extern Vector192 __ConstructVectorInt8(sbyte* arrNative, int lenNative);

	public static Vector192 ConstructVectorInt8<T>([In] T[] arr, int len) where T : unmanaged
	{
		Vector192 retVal;
		fixed (T* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			retVal = __ConstructVectorInt8((sbyte*)arrNative, len);
		}
		return retVal;
	}

	[DllImport(NativeMethods.DllName, EntryPoint = "ConstructVectorInt16", ExactSpelling = true)]
	[SuppressGCTransition]
	private static extern Vector192 __ConstructVectorInt16(short* arrNative, int lenNative);

	public static Vector192 ConstructVectorInt16<T>([In] T[] arr, int len) where T : unmanaged
	{
		Vector192 retVal;
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			retVal = __ConstructVectorInt16((short*)arrNative, len);
		}
		return retVal;
	}

	[DllImport(NativeMethods.DllName, EntryPoint = "ConstructVectorInt32", ExactSpelling = true)]
	[SuppressGCTransition]
	private static extern Vector192 __ConstructVectorInt32(int* arrNative, int lenNative);

	public static Vector192 ConstructVectorInt32<T>([In] T[] arr, int len) where T : unmanaged
	{
		Vector192 retVal;
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			retVal = __ConstructVectorInt32((int*)arrNative, len);
		}
		return retVal;
	}

	[DllImport(NativeMethods.DllName, EntryPoint = "ConstructVectorInt64", ExactSpelling = true)]
	[SuppressGCTransition]
	private static extern Vector192 __ConstructVectorInt64(long* arrNative, int lenNative);

	public static Vector192 ConstructVectorInt64<T>([In] T[] arr, int len) where T : unmanaged
	{
		Vector192 retVal;
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			retVal = __ConstructVectorInt64((long*)arrNative, len);
		}
		return retVal;
	}

	[DllImport(NativeMethods.DllName, EntryPoint = "ConstructVectorUInt8", ExactSpelling = true)]
	[SuppressGCTransition]
	private static extern Vector192 __ConstructVectorUInt8(byte* arrNative, int lenNative);

	public static Vector192 ConstructVectorUInt8<T>([In] T[] arr, int len) where T : unmanaged
	{
		Vector192 retVal;
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			retVal = __ConstructVectorUInt8((byte*)arrNative, len);
		}
		return retVal;
	}

	[DllImport(NativeMethods.DllName, EntryPoint = "ConstructVectorUInt16", ExactSpelling = true)]
	[SuppressGCTransition]
	private static extern Vector192 __ConstructVectorUInt16(ushort* arrNative, int lenNative);

	public static Vector192 ConstructVectorUInt16<T>([In] T[] arr, int len) where T : unmanaged
	{
		Vector192 retVal;
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			retVal = __ConstructVectorUInt16((ushort*)arrNative, len);
		}
		return retVal;
	}

	[DllImport(NativeMethods.DllName, EntryPoint = "ConstructVectorUInt32", ExactSpelling = true)]
	[SuppressGCTransition]
	private static extern Vector192 __ConstructVectorUInt32(uint* arrNative, int lenNative);

	public static Vector192 ConstructVectorUInt32<T>([In] T[] arr, int len) where T : unmanaged
	{
		Vector192 retVal;
		fixed (void* arrNative = &ArrayMarshaller<T, T>.ManagedToUnmanagedIn.GetPinnableReference(arr))
		{
			retVal = __ConstructVectorUInt32((uint*)arrNative, len);
		}
		return retVal;
	}

	[DllImport(NativeMethods.DllName, EntryPoint = "ConstructVectorUInt64", ExactSpelling = true)]
	[SuppressGCTransition]
	private static extern Vector192 __ConstructVectorUInt64(ulong* arrNative, int lenNative);

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