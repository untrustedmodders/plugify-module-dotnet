using System.Runtime.InteropServices;

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
	
	#endregion
}
