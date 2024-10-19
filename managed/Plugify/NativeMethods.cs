using System.Runtime.InteropServices;

namespace Plugify;

public static partial class NativeMethods
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
	public static partial nint AllocateString();

	[LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
	[SuppressGCTransition]
	public static partial nint CreateString(string? source);

	[LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
	[SuppressGCTransition]
	public static partial string GetStringData(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetStringLength(nint ptr);

	[LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
	[SuppressGCTransition]
	public static partial void ConstructString(nint ptr, string? source);

	[LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
	[SuppressGCTransition]
	public static partial void AssignString(nint ptr, string? source);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void FreeString(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DeleteString(nint ptr);

	#endregion

	#region CreateVector functions

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint CreateVectorBool([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1)] [In] bool[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint CreateVectorChar8([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2)] [In] char[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint CreateVectorChar16([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2)] [In] char[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint CreateVectorInt8([In] sbyte[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint CreateVectorInt16([In] short[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint CreateVectorInt32([In] int[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint CreateVectorInt64([In] long[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint CreateVectorUInt8([In] byte[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint CreateVectorUInt16([In] ushort[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint CreateVectorUInt32([In] uint[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint CreateVectorUInt64([In] ulong[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint CreateVectorIntPtr([In] nint[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint CreateVectorFloat([In] float[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint CreateVectorDouble([In] double[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint CreateVectorString([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] [In] string[] arr, int len);
	
	#endregion

	#region AllocateVector functions

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint AllocateVectorBool();

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint AllocateVectorChar8();

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint AllocateVectorChar16();

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint AllocateVectorInt8();

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint AllocateVectorInt16();

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint AllocateVectorInt32();

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint AllocateVectorInt64();

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint AllocateVectorUInt8();

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint AllocateVectorUInt16();

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint AllocateVectorUInt32();

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint AllocateVectorUInt64();

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint AllocateVectorIntPtr();

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint AllocateVectorFloat();

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint AllocateVectorDouble();

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial nint AllocateVectorString();
	
	#endregion
	
	#region GetVectorSize functions

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeBool(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeChar8(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeChar16(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeInt8(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeInt16(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeInt32(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeInt64(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeUInt8(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeUInt16(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeUInt32(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeUInt64(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeIntPtr(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeFloat(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeDouble(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial int GetVectorSizeString(nint ptr);
	
	#endregion

	#region GetVectorData functions

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataBool(nint ptr, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1)] [In, Out] bool[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataChar8(nint ptr, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2)] [In, Out] char[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataChar16(nint ptr, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2)] [In, Out] char[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataInt8(nint ptr, [In, Out] sbyte[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataInt16(nint ptr, [In, Out] short[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataInt32(nint ptr, [In, Out] int[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataInt64(nint ptr, [In, Out] long[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataUInt8(nint ptr, [In, Out] byte[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataUInt16(nint ptr, [In, Out] ushort[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataUInt32(nint ptr, [In, Out] uint[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataUInt64(nint ptr, [In, Out] ulong[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataIntPtr(nint ptr, [In, Out] nint[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataFloat(nint ptr, [In, Out] float[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataDouble(nint ptr, [In, Out] double[] arr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void GetVectorDataString(nint ptr, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] [In, Out] string[] arr);

	#endregion

	#region ConstructVector Functions
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void ConstructVectorBool(nint ptr, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1)] [In] bool[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void ConstructVectorChar8(nint ptr, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2)] [In] char[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void ConstructVectorChar16(nint ptr, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2)] [In] char[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void ConstructVectorInt8(nint ptr, [In] sbyte[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void ConstructVectorInt16(nint ptr, [In] short[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void ConstructVectorInt32(nint ptr, [In] int[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void ConstructVectorInt64(nint ptr, [In] long[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void ConstructVectorUInt8(nint ptr, [In] byte[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void ConstructVectorUInt16(nint ptr, [In] ushort[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void ConstructVectorUInt32(nint ptr, [In] uint[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void ConstructVectorUInt64(nint ptr, [In] ulong[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void ConstructVectorIntPtr(nint ptr, [In] nint[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void ConstructVectorFloat(nint ptr, [In] float[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void ConstructVectorDouble(nint ptr, [In] double[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void ConstructVectorString(nint ptr, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] [In] string[] arr, int len);

	#endregion

	#region AssignVector Functions
	
	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorBool(nint ptr, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1)] [In] bool[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorChar8(nint ptr, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2)] [In] char[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorChar16(nint ptr, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2)] [In] char[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorInt8(nint ptr, [In] sbyte[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorInt16(nint ptr, [In] short[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorInt32(nint ptr, [In] int[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorInt64(nint ptr, [In] long[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorUInt8(nint ptr, [In] byte[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorUInt16(nint ptr, [In] ushort[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorUInt32(nint ptr, [In] uint[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorUInt64(nint ptr, [In] ulong[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorIntPtr(nint ptr, [In] nint[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorFloat(nint ptr, [In] float[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorDouble(nint ptr, [In] double[] arr, int len);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void AssignVectorString(nint ptr, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] [In] string[] arr, int len);
	
	#endregion
	
	#region DeleteVector functions

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DeleteVectorBool(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DeleteVectorChar8(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DeleteVectorChar16(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DeleteVectorInt8(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DeleteVectorInt16(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DeleteVectorInt32(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DeleteVectorInt64(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DeleteVectorUInt8(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DeleteVectorUInt16(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DeleteVectorUInt32(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DeleteVectorUInt64(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DeleteVectorIntPtr(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DeleteVectorFloat(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DeleteVectorDouble(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void DeleteVectorString(nint ptr);

	#endregion

	#region FreeVectorData functions

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void FreeVectorBool(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void FreeVectorChar8(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void FreeVectorChar16(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void FreeVectorInt8(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void FreeVectorInt16(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void FreeVectorInt32(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void FreeVectorInt64(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void FreeVectorUInt8(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void FreeVectorUInt16(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void FreeVectorUInt32(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void FreeVectorUInt64(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void FreeVectorIntPtr(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void FreeVectorFloat(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void FreeVectorDouble(nint ptr);

	[LibraryImport(DllName)]
	[SuppressGCTransition]
	public static partial void FreeVectorString(nint ptr);

	#endregion
}
