using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Plugify;

// Generated from cross_call_master.pplugin (group: core)

namespace cross_call_master {
#pragma warning disable CS0649

	internal static unsafe partial class cross_call_master {

		/// <summary>
		/// ReverseReturn
		/// </summary>
		/// <param name="returnString">returnString</param>
		internal static delegate*<string, void> ReverseReturn = &___ReverseReturn;
		internal static delegate* unmanaged[Cdecl]<String192*, void> __ReverseReturn;
		private static void ___ReverseReturn(string returnString)
		{
			var __returnString = NativeMethods.ConstructString(returnString);
			try {
				__ReverseReturn(&__returnString);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__returnString);
			}
		}

		/// <summary>
		/// NoParamReturnVoidCallback
		/// </summary>
		internal static delegate*<void> NoParamReturnVoidCallback = &___NoParamReturnVoidCallback;
		internal static delegate* unmanaged[Cdecl]<void> __NoParamReturnVoidCallback;
		private static void ___NoParamReturnVoidCallback()
		{
			__NoParamReturnVoidCallback();
		}

		/// <summary>
		/// NoParamReturnBoolCallback
		/// </summary>
		internal static delegate*<Bool8> NoParamReturnBoolCallback = &___NoParamReturnBoolCallback;
		internal static delegate* unmanaged[Cdecl]<Bool8> __NoParamReturnBoolCallback;
		private static Bool8 ___NoParamReturnBoolCallback()
		{
			Bool8 __retVal = __NoParamReturnBoolCallback();
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnChar8Callback
		/// </summary>
		internal static delegate*<Char8> NoParamReturnChar8Callback = &___NoParamReturnChar8Callback;
		internal static delegate* unmanaged[Cdecl]<Char8> __NoParamReturnChar8Callback;
		private static Char8 ___NoParamReturnChar8Callback()
		{
			Char8 __retVal = __NoParamReturnChar8Callback();
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnChar16Callback
		/// </summary>
		internal static delegate*<Char16> NoParamReturnChar16Callback = &___NoParamReturnChar16Callback;
		internal static delegate* unmanaged[Cdecl]<Char16> __NoParamReturnChar16Callback;
		private static Char16 ___NoParamReturnChar16Callback()
		{
			Char16 __retVal = __NoParamReturnChar16Callback();
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnInt8Callback
		/// </summary>
		internal static delegate*<sbyte> NoParamReturnInt8Callback = &___NoParamReturnInt8Callback;
		internal static delegate* unmanaged[Cdecl]<sbyte> __NoParamReturnInt8Callback;
		private static sbyte ___NoParamReturnInt8Callback()
		{
			sbyte __retVal = __NoParamReturnInt8Callback();
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnInt16Callback
		/// </summary>
		internal static delegate*<short> NoParamReturnInt16Callback = &___NoParamReturnInt16Callback;
		internal static delegate* unmanaged[Cdecl]<short> __NoParamReturnInt16Callback;
		private static short ___NoParamReturnInt16Callback()
		{
			short __retVal = __NoParamReturnInt16Callback();
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnInt32Callback
		/// </summary>
		internal static delegate*<int> NoParamReturnInt32Callback = &___NoParamReturnInt32Callback;
		internal static delegate* unmanaged[Cdecl]<int> __NoParamReturnInt32Callback;
		private static int ___NoParamReturnInt32Callback()
		{
			int __retVal = __NoParamReturnInt32Callback();
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnInt64Callback
		/// </summary>
		internal static delegate*<long> NoParamReturnInt64Callback = &___NoParamReturnInt64Callback;
		internal static delegate* unmanaged[Cdecl]<long> __NoParamReturnInt64Callback;
		private static long ___NoParamReturnInt64Callback()
		{
			long __retVal = __NoParamReturnInt64Callback();
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnUInt8Callback
		/// </summary>
		internal static delegate*<byte> NoParamReturnUInt8Callback = &___NoParamReturnUInt8Callback;
		internal static delegate* unmanaged[Cdecl]<byte> __NoParamReturnUInt8Callback;
		private static byte ___NoParamReturnUInt8Callback()
		{
			byte __retVal = __NoParamReturnUInt8Callback();
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnUInt16Callback
		/// </summary>
		internal static delegate*<ushort> NoParamReturnUInt16Callback = &___NoParamReturnUInt16Callback;
		internal static delegate* unmanaged[Cdecl]<ushort> __NoParamReturnUInt16Callback;
		private static ushort ___NoParamReturnUInt16Callback()
		{
			ushort __retVal = __NoParamReturnUInt16Callback();
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnUInt32Callback
		/// </summary>
		internal static delegate*<uint> NoParamReturnUInt32Callback = &___NoParamReturnUInt32Callback;
		internal static delegate* unmanaged[Cdecl]<uint> __NoParamReturnUInt32Callback;
		private static uint ___NoParamReturnUInt32Callback()
		{
			uint __retVal = __NoParamReturnUInt32Callback();
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnUInt64Callback
		/// </summary>
		internal static delegate*<ulong> NoParamReturnUInt64Callback = &___NoParamReturnUInt64Callback;
		internal static delegate* unmanaged[Cdecl]<ulong> __NoParamReturnUInt64Callback;
		private static ulong ___NoParamReturnUInt64Callback()
		{
			ulong __retVal = __NoParamReturnUInt64Callback();
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnPointerCallback
		/// </summary>
		internal static delegate*<nint> NoParamReturnPointerCallback = &___NoParamReturnPointerCallback;
		internal static delegate* unmanaged[Cdecl]<nint> __NoParamReturnPointerCallback;
		private static nint ___NoParamReturnPointerCallback()
		{
			nint __retVal = __NoParamReturnPointerCallback();
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnFloatCallback
		/// </summary>
		internal static delegate*<float> NoParamReturnFloatCallback = &___NoParamReturnFloatCallback;
		internal static delegate* unmanaged[Cdecl]<float> __NoParamReturnFloatCallback;
		private static float ___NoParamReturnFloatCallback()
		{
			float __retVal = __NoParamReturnFloatCallback();
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnDoubleCallback
		/// </summary>
		internal static delegate*<double> NoParamReturnDoubleCallback = &___NoParamReturnDoubleCallback;
		internal static delegate* unmanaged[Cdecl]<double> __NoParamReturnDoubleCallback;
		private static double ___NoParamReturnDoubleCallback()
		{
			double __retVal = __NoParamReturnDoubleCallback();
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnFunctionCallback
		/// </summary>
		internal static delegate*<NoParamReturnFunctionCallbackFunc> NoParamReturnFunctionCallback = &___NoParamReturnFunctionCallback;
		internal static delegate* unmanaged[Cdecl]<nint> __NoParamReturnFunctionCallback;
		private static NoParamReturnFunctionCallbackFunc ___NoParamReturnFunctionCallback()
		{
			nint __retVal = __NoParamReturnFunctionCallback();
			return Marshalling.GetDelegateForFunctionPointer<NoParamReturnFunctionCallbackFunc>(__retVal);
		}

		/// <summary>
		/// NoParamReturnStringCallback
		/// </summary>
		internal static delegate*<string> NoParamReturnStringCallback = &___NoParamReturnStringCallback;
		internal static delegate* unmanaged[Cdecl]<String192> __NoParamReturnStringCallback;
		private static string ___NoParamReturnStringCallback()
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnStringCallback();
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnAnyCallback
		/// </summary>
		internal static delegate*<object> NoParamReturnAnyCallback = &___NoParamReturnAnyCallback;
		internal static delegate* unmanaged[Cdecl]<Variant256> __NoParamReturnAnyCallback;
		private static object ___NoParamReturnAnyCallback()
		{
			object __retVal;
			Variant256 __retVal_native;
			try {
				__retVal_native = __NoParamReturnAnyCallback();
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetVariantData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVariant(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayBoolCallback
		/// </summary>
		internal static delegate*<Bool8[]> NoParamReturnArrayBoolCallback = &___NoParamReturnArrayBoolCallback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayBoolCallback;
		private static Bool8[] ___NoParamReturnArrayBoolCallback()
		{
			Bool8[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayBoolCallback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new Bool8[NativeMethods.GetVectorSizeBool(&__retVal_native)];
				NativeMethods.GetVectorDataBool(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorBool(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayChar8Callback
		/// </summary>
		internal static delegate*<Char8[]> NoParamReturnArrayChar8Callback = &___NoParamReturnArrayChar8Callback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayChar8Callback;
		private static Char8[] ___NoParamReturnArrayChar8Callback()
		{
			Char8[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayChar8Callback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new Char8[NativeMethods.GetVectorSizeChar8(&__retVal_native)];
				NativeMethods.GetVectorDataChar8(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorChar8(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayChar16Callback
		/// </summary>
		internal static delegate*<Char16[]> NoParamReturnArrayChar16Callback = &___NoParamReturnArrayChar16Callback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayChar16Callback;
		private static Char16[] ___NoParamReturnArrayChar16Callback()
		{
			Char16[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayChar16Callback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new Char16[NativeMethods.GetVectorSizeChar16(&__retVal_native)];
				NativeMethods.GetVectorDataChar16(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorChar16(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayInt8Callback
		/// </summary>
		internal static delegate*<sbyte[]> NoParamReturnArrayInt8Callback = &___NoParamReturnArrayInt8Callback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayInt8Callback;
		private static sbyte[] ___NoParamReturnArrayInt8Callback()
		{
			sbyte[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayInt8Callback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new sbyte[NativeMethods.GetVectorSizeInt8(&__retVal_native)];
				NativeMethods.GetVectorDataInt8(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt8(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayInt16Callback
		/// </summary>
		internal static delegate*<short[]> NoParamReturnArrayInt16Callback = &___NoParamReturnArrayInt16Callback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayInt16Callback;
		private static short[] ___NoParamReturnArrayInt16Callback()
		{
			short[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayInt16Callback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new short[NativeMethods.GetVectorSizeInt16(&__retVal_native)];
				NativeMethods.GetVectorDataInt16(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt16(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayInt32Callback
		/// </summary>
		internal static delegate*<int[]> NoParamReturnArrayInt32Callback = &___NoParamReturnArrayInt32Callback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayInt32Callback;
		private static int[] ___NoParamReturnArrayInt32Callback()
		{
			int[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayInt32Callback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new int[NativeMethods.GetVectorSizeInt32(&__retVal_native)];
				NativeMethods.GetVectorDataInt32(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt32(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayInt64Callback
		/// </summary>
		internal static delegate*<long[]> NoParamReturnArrayInt64Callback = &___NoParamReturnArrayInt64Callback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayInt64Callback;
		private static long[] ___NoParamReturnArrayInt64Callback()
		{
			long[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayInt64Callback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new long[NativeMethods.GetVectorSizeInt64(&__retVal_native)];
				NativeMethods.GetVectorDataInt64(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt64(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayUInt8Callback
		/// </summary>
		internal static delegate*<byte[]> NoParamReturnArrayUInt8Callback = &___NoParamReturnArrayUInt8Callback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayUInt8Callback;
		private static byte[] ___NoParamReturnArrayUInt8Callback()
		{
			byte[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayUInt8Callback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new byte[NativeMethods.GetVectorSizeUInt8(&__retVal_native)];
				NativeMethods.GetVectorDataUInt8(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorUInt8(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayUInt16Callback
		/// </summary>
		internal static delegate*<ushort[]> NoParamReturnArrayUInt16Callback = &___NoParamReturnArrayUInt16Callback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayUInt16Callback;
		private static ushort[] ___NoParamReturnArrayUInt16Callback()
		{
			ushort[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayUInt16Callback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new ushort[NativeMethods.GetVectorSizeUInt16(&__retVal_native)];
				NativeMethods.GetVectorDataUInt16(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorUInt16(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayUInt32Callback
		/// </summary>
		internal static delegate*<uint[]> NoParamReturnArrayUInt32Callback = &___NoParamReturnArrayUInt32Callback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayUInt32Callback;
		private static uint[] ___NoParamReturnArrayUInt32Callback()
		{
			uint[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayUInt32Callback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new uint[NativeMethods.GetVectorSizeUInt32(&__retVal_native)];
				NativeMethods.GetVectorDataUInt32(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorUInt32(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayUInt64Callback
		/// </summary>
		internal static delegate*<ulong[]> NoParamReturnArrayUInt64Callback = &___NoParamReturnArrayUInt64Callback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayUInt64Callback;
		private static ulong[] ___NoParamReturnArrayUInt64Callback()
		{
			ulong[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayUInt64Callback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new ulong[NativeMethods.GetVectorSizeUInt64(&__retVal_native)];
				NativeMethods.GetVectorDataUInt64(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorUInt64(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayPointerCallback
		/// </summary>
		internal static delegate*<nint[]> NoParamReturnArrayPointerCallback = &___NoParamReturnArrayPointerCallback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayPointerCallback;
		private static nint[] ___NoParamReturnArrayPointerCallback()
		{
			nint[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayPointerCallback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new nint[NativeMethods.GetVectorSizeIntPtr(&__retVal_native)];
				NativeMethods.GetVectorDataIntPtr(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorIntPtr(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayFloatCallback
		/// </summary>
		internal static delegate*<float[]> NoParamReturnArrayFloatCallback = &___NoParamReturnArrayFloatCallback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayFloatCallback;
		private static float[] ___NoParamReturnArrayFloatCallback()
		{
			float[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayFloatCallback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new float[NativeMethods.GetVectorSizeFloat(&__retVal_native)];
				NativeMethods.GetVectorDataFloat(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorFloat(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayDoubleCallback
		/// </summary>
		internal static delegate*<double[]> NoParamReturnArrayDoubleCallback = &___NoParamReturnArrayDoubleCallback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayDoubleCallback;
		private static double[] ___NoParamReturnArrayDoubleCallback()
		{
			double[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayDoubleCallback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new double[NativeMethods.GetVectorSizeDouble(&__retVal_native)];
				NativeMethods.GetVectorDataDouble(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorDouble(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayStringCallback
		/// </summary>
		internal static delegate*<string[]> NoParamReturnArrayStringCallback = &___NoParamReturnArrayStringCallback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayStringCallback;
		private static string[] ___NoParamReturnArrayStringCallback()
		{
			string[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayStringCallback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new string[NativeMethods.GetVectorSizeString(&__retVal_native)];
				NativeMethods.GetVectorDataString(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayAnyCallback
		/// </summary>
		internal static delegate*<object[]> NoParamReturnArrayAnyCallback = &___NoParamReturnArrayAnyCallback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayAnyCallback;
		private static object[] ___NoParamReturnArrayAnyCallback()
		{
			object[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayAnyCallback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new object[NativeMethods.GetVectorSizeVariant(&__retVal_native)];
				NativeMethods.GetVectorDataVariant(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorVariant(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayVector2Callback
		/// </summary>
		internal static delegate*<Vector2[]> NoParamReturnArrayVector2Callback = &___NoParamReturnArrayVector2Callback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayVector2Callback;
		private static Vector2[] ___NoParamReturnArrayVector2Callback()
		{
			Vector2[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayVector2Callback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new Vector2[NativeMethods.GetVectorSizeVector2(&__retVal_native)];
				NativeMethods.GetVectorDataVector2(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorVector2(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayVector3Callback
		/// </summary>
		internal static delegate*<Vector3[]> NoParamReturnArrayVector3Callback = &___NoParamReturnArrayVector3Callback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayVector3Callback;
		private static Vector3[] ___NoParamReturnArrayVector3Callback()
		{
			Vector3[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayVector3Callback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new Vector3[NativeMethods.GetVectorSizeVector3(&__retVal_native)];
				NativeMethods.GetVectorDataVector3(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorVector3(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayVector4Callback
		/// </summary>
		internal static delegate*<Vector4[]> NoParamReturnArrayVector4Callback = &___NoParamReturnArrayVector4Callback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayVector4Callback;
		private static Vector4[] ___NoParamReturnArrayVector4Callback()
		{
			Vector4[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayVector4Callback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new Vector4[NativeMethods.GetVectorSizeVector4(&__retVal_native)];
				NativeMethods.GetVectorDataVector4(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorVector4(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnArrayMatrix4x4Callback
		/// </summary>
		internal static delegate*<Matrix4x4[]> NoParamReturnArrayMatrix4x4Callback = &___NoParamReturnArrayMatrix4x4Callback;
		internal static delegate* unmanaged[Cdecl]<Vector192> __NoParamReturnArrayMatrix4x4Callback;
		private static Matrix4x4[] ___NoParamReturnArrayMatrix4x4Callback()
		{
			Matrix4x4[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __NoParamReturnArrayMatrix4x4Callback();
				// Unmarshal - Convert native data to managed data.
				__retVal = new Matrix4x4[NativeMethods.GetVectorSizeMatrix4x4(&__retVal_native)];
				NativeMethods.GetVectorDataMatrix4x4(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorMatrix4x4(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnVector2Callback
		/// </summary>
		internal static delegate*<Vector2> NoParamReturnVector2Callback = &___NoParamReturnVector2Callback;
		internal static delegate* unmanaged[Cdecl]<Vector2> __NoParamReturnVector2Callback;
		private static Vector2 ___NoParamReturnVector2Callback()
		{
			Vector2 __retVal = __NoParamReturnVector2Callback();
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnVector3Callback
		/// </summary>
		internal static delegate*<Vector3> NoParamReturnVector3Callback = &___NoParamReturnVector3Callback;
		internal static delegate* unmanaged[Cdecl]<Vector3> __NoParamReturnVector3Callback;
		private static Vector3 ___NoParamReturnVector3Callback()
		{
			Vector3 __retVal = __NoParamReturnVector3Callback();
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnVector4Callback
		/// </summary>
		internal static delegate*<Vector4> NoParamReturnVector4Callback = &___NoParamReturnVector4Callback;
		internal static delegate* unmanaged[Cdecl]<Vector4> __NoParamReturnVector4Callback;
		private static Vector4 ___NoParamReturnVector4Callback()
		{
			Vector4 __retVal = __NoParamReturnVector4Callback();
			return __retVal;
		}

		/// <summary>
		/// NoParamReturnMatrix4x4Callback
		/// </summary>
		internal static delegate*<Matrix4x4> NoParamReturnMatrix4x4Callback = &___NoParamReturnMatrix4x4Callback;
		internal static delegate* unmanaged[Cdecl]<Matrix4x4> __NoParamReturnMatrix4x4Callback;
		private static Matrix4x4 ___NoParamReturnMatrix4x4Callback()
		{
			Matrix4x4 __retVal = __NoParamReturnMatrix4x4Callback();
			return __retVal;
		}

		/// <summary>
		/// Param1Callback
		/// </summary>
		/// <param name="a">a</param>
		internal static delegate*<int, void> Param1Callback = &___Param1Callback;
		internal static delegate* unmanaged[Cdecl]<int, void> __Param1Callback;
		private static void ___Param1Callback(int a)
		{
			__Param1Callback(a);
		}

		/// <summary>
		/// Param2Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		internal static delegate*<int, float, void> Param2Callback = &___Param2Callback;
		internal static delegate* unmanaged[Cdecl]<int, float, void> __Param2Callback;
		private static void ___Param2Callback(int a, float b)
		{
			__Param2Callback(a, b);
		}

		/// <summary>
		/// Param3Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		internal static delegate*<int, float, double, void> Param3Callback = &___Param3Callback;
		internal static delegate* unmanaged[Cdecl]<int, float, double, void> __Param3Callback;
		private static void ___Param3Callback(int a, float b, double c)
		{
			__Param3Callback(a, b, c);
		}

		/// <summary>
		/// Param4Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		internal static delegate*<int, float, double, Vector4, void> Param4Callback = &___Param4Callback;
		internal static delegate* unmanaged[Cdecl]<int, float, double, Vector4*, void> __Param4Callback;
		private static void ___Param4Callback(int a, float b, double c, Vector4 d)
		{
			__Param4Callback(a, b, c, &d);
		}

		/// <summary>
		/// Param5Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		/// <param name="e">e</param>
		internal static delegate*<int, float, double, Vector4, long[], void> Param5Callback = &___Param5Callback;
		internal static delegate* unmanaged[Cdecl]<int, float, double, Vector4*, Vector192*, void> __Param5Callback;
		private static void ___Param5Callback(int a, float b, double c, Vector4 d, long[] e)
		{
			var __e = NativeMethods.ConstructVectorInt64(e, e.Length);
			try {
				__Param5Callback(a, b, c, &d, &__e);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt64(&__e);
			}
		}

		/// <summary>
		/// Param6Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		/// <param name="e">e</param>
		/// <param name="f">f</param>
		internal static delegate*<int, float, double, Vector4, long[], Char8, void> Param6Callback = &___Param6Callback;
		internal static delegate* unmanaged[Cdecl]<int, float, double, Vector4*, Vector192*, Char8, void> __Param6Callback;
		private static void ___Param6Callback(int a, float b, double c, Vector4 d, long[] e, Char8 f)
		{
			var __e = NativeMethods.ConstructVectorInt64(e, e.Length);
			try {
				__Param6Callback(a, b, c, &d, &__e, f);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt64(&__e);
			}
		}

		/// <summary>
		/// Param7Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		/// <param name="e">e</param>
		/// <param name="f">f</param>
		/// <param name="g">g</param>
		internal static delegate*<int, float, double, Vector4, long[], Char8, string, void> Param7Callback = &___Param7Callback;
		internal static delegate* unmanaged[Cdecl]<int, float, double, Vector4*, Vector192*, Char8, String192*, void> __Param7Callback;
		private static void ___Param7Callback(int a, float b, double c, Vector4 d, long[] e, Char8 f, string g)
		{
			var __e = NativeMethods.ConstructVectorInt64(e, e.Length);
			var __g = NativeMethods.ConstructString(g);
			try {
				__Param7Callback(a, b, c, &d, &__e, f, &__g);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt64(&__e);
				NativeMethods.DestroyString(&__g);
			}
		}

		/// <summary>
		/// Param8Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		/// <param name="e">e</param>
		/// <param name="f">f</param>
		/// <param name="g">g</param>
		/// <param name="h">h</param>
		internal static delegate*<int, float, double, Vector4, long[], Char8, string, Char16, void> Param8Callback = &___Param8Callback;
		internal static delegate* unmanaged[Cdecl]<int, float, double, Vector4*, Vector192*, Char8, String192*, Char16, void> __Param8Callback;
		private static void ___Param8Callback(int a, float b, double c, Vector4 d, long[] e, Char8 f, string g, Char16 h)
		{
			var __e = NativeMethods.ConstructVectorInt64(e, e.Length);
			var __g = NativeMethods.ConstructString(g);
			try {
				__Param8Callback(a, b, c, &d, &__e, f, &__g, h);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt64(&__e);
				NativeMethods.DestroyString(&__g);
			}
		}

		/// <summary>
		/// Param9Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		/// <param name="e">e</param>
		/// <param name="f">f</param>
		/// <param name="g">g</param>
		/// <param name="h">h</param>
		/// <param name="k">k</param>
		internal static delegate*<int, float, double, Vector4, long[], Char8, string, Char16, short, void> Param9Callback = &___Param9Callback;
		internal static delegate* unmanaged[Cdecl]<int, float, double, Vector4*, Vector192*, Char8, String192*, Char16, short, void> __Param9Callback;
		private static void ___Param9Callback(int a, float b, double c, Vector4 d, long[] e, Char8 f, string g, Char16 h, short k)
		{
			var __e = NativeMethods.ConstructVectorInt64(e, e.Length);
			var __g = NativeMethods.ConstructString(g);
			try {
				__Param9Callback(a, b, c, &d, &__e, f, &__g, h, k);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt64(&__e);
				NativeMethods.DestroyString(&__g);
			}
		}

		/// <summary>
		/// Param10Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		/// <param name="e">e</param>
		/// <param name="f">f</param>
		/// <param name="g">g</param>
		/// <param name="h">h</param>
		/// <param name="k">k</param>
		/// <param name="l">l</param>
		internal static delegate*<int, float, double, Vector4, long[], Char8, string, Char16, short, nint, void> Param10Callback = &___Param10Callback;
		internal static delegate* unmanaged[Cdecl]<int, float, double, Vector4*, Vector192*, Char8, String192*, Char16, short, nint, void> __Param10Callback;
		private static void ___Param10Callback(int a, float b, double c, Vector4 d, long[] e, Char8 f, string g, Char16 h, short k, nint l)
		{
			var __e = NativeMethods.ConstructVectorInt64(e, e.Length);
			var __g = NativeMethods.ConstructString(g);
			try {
				__Param10Callback(a, b, c, &d, &__e, f, &__g, h, k, l);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt64(&__e);
				NativeMethods.DestroyString(&__g);
			}
		}

		/// <summary>
		/// ParamRef1Callback
		/// </summary>
		/// <param name="a">a</param>
		internal static delegate*<ref int, void> ParamRef1Callback = &___ParamRef1Callback;
		internal static delegate* unmanaged[Cdecl]<int*, void> __ParamRef1Callback;
		private static void ___ParamRef1Callback(ref int a)
		{
			fixed(int* __a = &a) {
			__ParamRef1Callback(__a);
			}
		}

		/// <summary>
		/// ParamRef2Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		internal static delegate*<ref int, ref float, void> ParamRef2Callback = &___ParamRef2Callback;
		internal static delegate* unmanaged[Cdecl]<int*, float*, void> __ParamRef2Callback;
		private static void ___ParamRef2Callback(ref int a, ref float b)
		{
			fixed(int* __a = &a) {
			fixed(float* __b = &b) {
			__ParamRef2Callback(__a, __b);
			}
			}
		}

		/// <summary>
		/// ParamRef3Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		internal static delegate*<ref int, ref float, ref double, void> ParamRef3Callback = &___ParamRef3Callback;
		internal static delegate* unmanaged[Cdecl]<int*, float*, double*, void> __ParamRef3Callback;
		private static void ___ParamRef3Callback(ref int a, ref float b, ref double c)
		{
			fixed(int* __a = &a) {
			fixed(float* __b = &b) {
			fixed(double* __c = &c) {
			__ParamRef3Callback(__a, __b, __c);
			}
			}
			}
		}

		/// <summary>
		/// ParamRef4Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		internal static delegate*<ref int, ref float, ref double, ref Vector4, void> ParamRef4Callback = &___ParamRef4Callback;
		internal static delegate* unmanaged[Cdecl]<int*, float*, double*, Vector4*, void> __ParamRef4Callback;
		private static void ___ParamRef4Callback(ref int a, ref float b, ref double c, ref Vector4 d)
		{
			fixed(int* __a = &a) {
			fixed(float* __b = &b) {
			fixed(double* __c = &c) {
			fixed(Vector4* __d = &d) {
			__ParamRef4Callback(__a, __b, __c, __d);
			}
			}
			}
			}
		}

		/// <summary>
		/// ParamRef5Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		/// <param name="e">e</param>
		internal static delegate*<ref int, ref float, ref double, ref Vector4, ref long[], void> ParamRef5Callback = &___ParamRef5Callback;
		internal static delegate* unmanaged[Cdecl]<int*, float*, double*, Vector4*, Vector192*, void> __ParamRef5Callback;
		private static void ___ParamRef5Callback(ref int a, ref float b, ref double c, ref Vector4 d, ref long[] e)
		{
			fixed(int* __a = &a) {
			fixed(float* __b = &b) {
			fixed(double* __c = &c) {
			fixed(Vector4* __d = &d) {
			var __e = NativeMethods.ConstructVectorInt64(e, e.Length);
			try {
				__ParamRef5Callback(__a, __b, __c, __d, &__e);
				// Unmarshal - Convert native data to managed data.
				Array.Resize(ref e, NativeMethods.GetVectorSizeInt64(&__e));
				NativeMethods.GetVectorDataInt64(&__e, e);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt64(&__e);
			}
			}
			}
			}
			}
		}

		/// <summary>
		/// ParamRef6Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		/// <param name="e">e</param>
		/// <param name="f">f</param>
		internal static delegate*<ref int, ref float, ref double, ref Vector4, ref long[], ref Char8, void> ParamRef6Callback = &___ParamRef6Callback;
		internal static delegate* unmanaged[Cdecl]<int*, float*, double*, Vector4*, Vector192*, Char8*, void> __ParamRef6Callback;
		private static void ___ParamRef6Callback(ref int a, ref float b, ref double c, ref Vector4 d, ref long[] e, ref Char8 f)
		{
			fixed(int* __a = &a) {
			fixed(float* __b = &b) {
			fixed(double* __c = &c) {
			fixed(Vector4* __d = &d) {
			fixed(Char8* __f = &f) {
			var __e = NativeMethods.ConstructVectorInt64(e, e.Length);
			try {
				__ParamRef6Callback(__a, __b, __c, __d, &__e, __f);
				// Unmarshal - Convert native data to managed data.
				Array.Resize(ref e, NativeMethods.GetVectorSizeInt64(&__e));
				NativeMethods.GetVectorDataInt64(&__e, e);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt64(&__e);
			}
			}
			}
			}
			}
			}
		}

		/// <summary>
		/// ParamRef7Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		/// <param name="e">e</param>
		/// <param name="f">f</param>
		/// <param name="g">g</param>
		internal static delegate*<ref int, ref float, ref double, ref Vector4, ref long[], ref Char8, ref string, void> ParamRef7Callback = &___ParamRef7Callback;
		internal static delegate* unmanaged[Cdecl]<int*, float*, double*, Vector4*, Vector192*, Char8*, String192*, void> __ParamRef7Callback;
		private static void ___ParamRef7Callback(ref int a, ref float b, ref double c, ref Vector4 d, ref long[] e, ref Char8 f, ref string g)
		{
			fixed(int* __a = &a) {
			fixed(float* __b = &b) {
			fixed(double* __c = &c) {
			fixed(Vector4* __d = &d) {
			fixed(Char8* __f = &f) {
			var __e = NativeMethods.ConstructVectorInt64(e, e.Length);
			var __g = NativeMethods.ConstructString(g);
			try {
				__ParamRef7Callback(__a, __b, __c, __d, &__e, __f, &__g);
				// Unmarshal - Convert native data to managed data.
				Array.Resize(ref e, NativeMethods.GetVectorSizeInt64(&__e));
				NativeMethods.GetVectorDataInt64(&__e, e);
				g = NativeMethods.GetStringData(&__g);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt64(&__e);
				NativeMethods.DestroyString(&__g);
			}
			}
			}
			}
			}
			}
		}

		/// <summary>
		/// ParamRef8Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		/// <param name="e">e</param>
		/// <param name="f">f</param>
		/// <param name="g">g</param>
		/// <param name="h">h</param>
		internal static delegate*<ref int, ref float, ref double, ref Vector4, ref long[], ref Char8, ref string, ref Char16, void> ParamRef8Callback = &___ParamRef8Callback;
		internal static delegate* unmanaged[Cdecl]<int*, float*, double*, Vector4*, Vector192*, Char8*, String192*, Char16*, void> __ParamRef8Callback;
		private static void ___ParamRef8Callback(ref int a, ref float b, ref double c, ref Vector4 d, ref long[] e, ref Char8 f, ref string g, ref Char16 h)
		{
			fixed(int* __a = &a) {
			fixed(float* __b = &b) {
			fixed(double* __c = &c) {
			fixed(Vector4* __d = &d) {
			fixed(Char8* __f = &f) {
			fixed(Char16* __h = &h) {
			var __e = NativeMethods.ConstructVectorInt64(e, e.Length);
			var __g = NativeMethods.ConstructString(g);
			try {
				__ParamRef8Callback(__a, __b, __c, __d, &__e, __f, &__g, __h);
				// Unmarshal - Convert native data to managed data.
				Array.Resize(ref e, NativeMethods.GetVectorSizeInt64(&__e));
				NativeMethods.GetVectorDataInt64(&__e, e);
				g = NativeMethods.GetStringData(&__g);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt64(&__e);
				NativeMethods.DestroyString(&__g);
			}
			}
			}
			}
			}
			}
			}
		}

		/// <summary>
		/// ParamRef9Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		/// <param name="e">e</param>
		/// <param name="f">f</param>
		/// <param name="g">g</param>
		/// <param name="h">h</param>
		/// <param name="k">k</param>
		internal static delegate*<ref int, ref float, ref double, ref Vector4, ref long[], ref Char8, ref string, ref Char16, ref short, void> ParamRef9Callback = &___ParamRef9Callback;
		internal static delegate* unmanaged[Cdecl]<int*, float*, double*, Vector4*, Vector192*, Char8*, String192*, Char16*, short*, void> __ParamRef9Callback;
		private static void ___ParamRef9Callback(ref int a, ref float b, ref double c, ref Vector4 d, ref long[] e, ref Char8 f, ref string g, ref Char16 h, ref short k)
		{
			fixed(int* __a = &a) {
			fixed(float* __b = &b) {
			fixed(double* __c = &c) {
			fixed(Vector4* __d = &d) {
			fixed(Char8* __f = &f) {
			fixed(Char16* __h = &h) {
			fixed(short* __k = &k) {
			var __e = NativeMethods.ConstructVectorInt64(e, e.Length);
			var __g = NativeMethods.ConstructString(g);
			try {
				__ParamRef9Callback(__a, __b, __c, __d, &__e, __f, &__g, __h, __k);
				// Unmarshal - Convert native data to managed data.
				Array.Resize(ref e, NativeMethods.GetVectorSizeInt64(&__e));
				NativeMethods.GetVectorDataInt64(&__e, e);
				g = NativeMethods.GetStringData(&__g);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt64(&__e);
				NativeMethods.DestroyString(&__g);
			}
			}
			}
			}
			}
			}
			}
			}
		}

		/// <summary>
		/// ParamRef10Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		/// <param name="e">e</param>
		/// <param name="f">f</param>
		/// <param name="g">g</param>
		/// <param name="h">h</param>
		/// <param name="k">k</param>
		/// <param name="l">l</param>
		internal static delegate*<ref int, ref float, ref double, ref Vector4, ref long[], ref Char8, ref string, ref Char16, ref short, ref nint, void> ParamRef10Callback = &___ParamRef10Callback;
		internal static delegate* unmanaged[Cdecl]<int*, float*, double*, Vector4*, Vector192*, Char8*, String192*, Char16*, short*, nint*, void> __ParamRef10Callback;
		private static void ___ParamRef10Callback(ref int a, ref float b, ref double c, ref Vector4 d, ref long[] e, ref Char8 f, ref string g, ref Char16 h, ref short k, ref nint l)
		{
			fixed(int* __a = &a) {
			fixed(float* __b = &b) {
			fixed(double* __c = &c) {
			fixed(Vector4* __d = &d) {
			fixed(Char8* __f = &f) {
			fixed(Char16* __h = &h) {
			fixed(short* __k = &k) {
			fixed(nint* __l = &l) {
			var __e = NativeMethods.ConstructVectorInt64(e, e.Length);
			var __g = NativeMethods.ConstructString(g);
			try {
				__ParamRef10Callback(__a, __b, __c, __d, &__e, __f, &__g, __h, __k, __l);
				// Unmarshal - Convert native data to managed data.
				Array.Resize(ref e, NativeMethods.GetVectorSizeInt64(&__e));
				NativeMethods.GetVectorDataInt64(&__e, e);
				g = NativeMethods.GetStringData(&__g);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt64(&__e);
				NativeMethods.DestroyString(&__g);
			}
			}
			}
			}
			}
			}
			}
			}
			}
		}

		/// <summary>
		/// ParamRefVectorsCallback
		/// </summary>
		/// <param name="p1">p1</param>
		/// <param name="p2">p2</param>
		/// <param name="p3">p3</param>
		/// <param name="p4">p4</param>
		/// <param name="p5">p5</param>
		/// <param name="p6">p6</param>
		/// <param name="p7">p7</param>
		/// <param name="p8">p8</param>
		/// <param name="p9">p9</param>
		/// <param name="p10">p10</param>
		/// <param name="p11">p11</param>
		/// <param name="p12">p12</param>
		/// <param name="p13">p13</param>
		/// <param name="p14">p14</param>
		/// <param name="p15">p15</param>
		internal static delegate*<ref Bool8[], ref Char8[], ref Char16[], ref sbyte[], ref short[], ref int[], ref long[], ref byte[], ref ushort[], ref uint[], ref ulong[], ref nint[], ref float[], ref double[], ref string[], void> ParamRefVectorsCallback = &___ParamRefVectorsCallback;
		internal static delegate* unmanaged[Cdecl]<Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, void> __ParamRefVectorsCallback;
		private static void ___ParamRefVectorsCallback(ref Bool8[] p1, ref Char8[] p2, ref Char16[] p3, ref sbyte[] p4, ref short[] p5, ref int[] p6, ref long[] p7, ref byte[] p8, ref ushort[] p9, ref uint[] p10, ref ulong[] p11, ref nint[] p12, ref float[] p13, ref double[] p14, ref string[] p15)
		{
			var __p1 = NativeMethods.ConstructVectorBool(p1, p1.Length);
			var __p2 = NativeMethods.ConstructVectorChar8(p2, p2.Length);
			var __p3 = NativeMethods.ConstructVectorChar16(p3, p3.Length);
			var __p4 = NativeMethods.ConstructVectorInt8(p4, p4.Length);
			var __p5 = NativeMethods.ConstructVectorInt16(p5, p5.Length);
			var __p6 = NativeMethods.ConstructVectorInt32(p6, p6.Length);
			var __p7 = NativeMethods.ConstructVectorInt64(p7, p7.Length);
			var __p8 = NativeMethods.ConstructVectorUInt8(p8, p8.Length);
			var __p9 = NativeMethods.ConstructVectorUInt16(p9, p9.Length);
			var __p10 = NativeMethods.ConstructVectorUInt32(p10, p10.Length);
			var __p11 = NativeMethods.ConstructVectorUInt64(p11, p11.Length);
			var __p12 = NativeMethods.ConstructVectorIntPtr(p12, p12.Length);
			var __p13 = NativeMethods.ConstructVectorFloat(p13, p13.Length);
			var __p14 = NativeMethods.ConstructVectorDouble(p14, p14.Length);
			var __p15 = NativeMethods.ConstructVectorString(p15, p15.Length);
			try {
				__ParamRefVectorsCallback(&__p1, &__p2, &__p3, &__p4, &__p5, &__p6, &__p7, &__p8, &__p9, &__p10, &__p11, &__p12, &__p13, &__p14, &__p15);
				// Unmarshal - Convert native data to managed data.
				Array.Resize(ref p1, NativeMethods.GetVectorSizeBool(&__p1));
				NativeMethods.GetVectorDataBool(&__p1, p1);
				Array.Resize(ref p2, NativeMethods.GetVectorSizeChar8(&__p2));
				NativeMethods.GetVectorDataChar8(&__p2, p2);
				Array.Resize(ref p3, NativeMethods.GetVectorSizeChar16(&__p3));
				NativeMethods.GetVectorDataChar16(&__p3, p3);
				Array.Resize(ref p4, NativeMethods.GetVectorSizeInt8(&__p4));
				NativeMethods.GetVectorDataInt8(&__p4, p4);
				Array.Resize(ref p5, NativeMethods.GetVectorSizeInt16(&__p5));
				NativeMethods.GetVectorDataInt16(&__p5, p5);
				Array.Resize(ref p6, NativeMethods.GetVectorSizeInt32(&__p6));
				NativeMethods.GetVectorDataInt32(&__p6, p6);
				Array.Resize(ref p7, NativeMethods.GetVectorSizeInt64(&__p7));
				NativeMethods.GetVectorDataInt64(&__p7, p7);
				Array.Resize(ref p8, NativeMethods.GetVectorSizeUInt8(&__p8));
				NativeMethods.GetVectorDataUInt8(&__p8, p8);
				Array.Resize(ref p9, NativeMethods.GetVectorSizeUInt16(&__p9));
				NativeMethods.GetVectorDataUInt16(&__p9, p9);
				Array.Resize(ref p10, NativeMethods.GetVectorSizeUInt32(&__p10));
				NativeMethods.GetVectorDataUInt32(&__p10, p10);
				Array.Resize(ref p11, NativeMethods.GetVectorSizeUInt64(&__p11));
				NativeMethods.GetVectorDataUInt64(&__p11, p11);
				Array.Resize(ref p12, NativeMethods.GetVectorSizeIntPtr(&__p12));
				NativeMethods.GetVectorDataIntPtr(&__p12, p12);
				Array.Resize(ref p13, NativeMethods.GetVectorSizeFloat(&__p13));
				NativeMethods.GetVectorDataFloat(&__p13, p13);
				Array.Resize(ref p14, NativeMethods.GetVectorSizeDouble(&__p14));
				NativeMethods.GetVectorDataDouble(&__p14, p14);
				Array.Resize(ref p15, NativeMethods.GetVectorSizeString(&__p15));
				NativeMethods.GetVectorDataString(&__p15, p15);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorBool(&__p1);
				NativeMethods.DestroyVectorChar8(&__p2);
				NativeMethods.DestroyVectorChar16(&__p3);
				NativeMethods.DestroyVectorInt8(&__p4);
				NativeMethods.DestroyVectorInt16(&__p5);
				NativeMethods.DestroyVectorInt32(&__p6);
				NativeMethods.DestroyVectorInt64(&__p7);
				NativeMethods.DestroyVectorUInt8(&__p8);
				NativeMethods.DestroyVectorUInt16(&__p9);
				NativeMethods.DestroyVectorUInt32(&__p10);
				NativeMethods.DestroyVectorUInt64(&__p11);
				NativeMethods.DestroyVectorIntPtr(&__p12);
				NativeMethods.DestroyVectorFloat(&__p13);
				NativeMethods.DestroyVectorDouble(&__p14);
				NativeMethods.DestroyVectorString(&__p15);
			}
		}

		/// <summary>
		/// ParamAllPrimitivesCallback
		/// </summary>
		/// <param name="p1">p1</param>
		/// <param name="p2">p2</param>
		/// <param name="p3">p3</param>
		/// <param name="p4">p4</param>
		/// <param name="p5">p5</param>
		/// <param name="p6">p6</param>
		/// <param name="p7">p7</param>
		/// <param name="p8">p8</param>
		/// <param name="p9">p9</param>
		/// <param name="p10">p10</param>
		/// <param name="p11">p11</param>
		/// <param name="p12">p12</param>
		/// <param name="p13">p13</param>
		/// <param name="p14">p14</param>
		internal static delegate*<Bool8, Char8, Char16, sbyte, short, int, long, byte, ushort, uint, ulong, nint, float, double, long> ParamAllPrimitivesCallback = &___ParamAllPrimitivesCallback;
		internal static delegate* unmanaged[Cdecl]<Bool8, Char8, Char16, sbyte, short, int, long, byte, ushort, uint, ulong, nint, float, double, long> __ParamAllPrimitivesCallback;
		private static long ___ParamAllPrimitivesCallback(Bool8 p1, Char8 p2, Char16 p3, sbyte p4, short p5, int p6, long p7, byte p8, ushort p9, uint p10, ulong p11, nint p12, float p13, double p14)
		{
			long __retVal = __ParamAllPrimitivesCallback(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14);
			return __retVal;
		}

		/// <summary>
		/// ParamAllAliasesCallback
		/// </summary>
		/// <param name="aBool">aBool</param>
		/// <param name="aChar8">aChar8</param>
		/// <param name="aChar16">aChar16</param>
		/// <param name="aInt8">aInt8</param>
		/// <param name="aInt16">aInt16</param>
		/// <param name="aInt32">aInt32</param>
		/// <param name="aInt64">aInt64</param>
		/// <param name="aPtr">aPtr</param>
		/// <param name="aFloat">aFloat</param>
		/// <param name="aDouble">aDouble</param>
		/// <param name="aString">aString</param>
		/// <param name="aAny">aAny</param>
		/// <param name="aVec2">aVec2</param>
		/// <param name="aVec3">aVec3</param>
		/// <param name="aVec4">aVec4</param>
		/// <param name="aMat4x4">aMat4x4</param>
		/// <param name="aBoolVec">aBoolVec</param>
		/// <param name="aChar8Vec">aChar8Vec</param>
		/// <param name="aChar16Vec">aChar16Vec</param>
		/// <param name="aInt8Vec">aInt8Vec</param>
		/// <param name="aInt16Vec">aInt16Vec</param>
		/// <param name="aInt32Vec">aInt32Vec</param>
		/// <param name="aInt64Vec">aInt64Vec</param>
		/// <param name="aPtrVec">aPtrVec</param>
		/// <param name="aFloatVec">aFloatVec</param>
		/// <param name="aDoubleVec">aDoubleVec</param>
		/// <param name="aStringVec">aStringVec</param>
		/// <param name="aAnyVec">aAnyVec</param>
		/// <param name="aVec2Vec">aVec2Vec</param>
		/// <param name="aVec3Vec">aVec3Vec</param>
		/// <param name="aVec4Vec">aVec4Vec</param>
		internal static delegate*<AliasBool, AliasChar8, AliasChar16, AliasInt8, AliasInt16, AliasInt32, AliasInt64, AliasPtr, AliasFloat, AliasDouble, AliasString, AliasAny, AliasVec2, AliasVec3, AliasVec4, AliasMat4x4, AliasBoolVector, AliasChar8Vector, AliasChar16Vector, AliasInt8Vector, AliasInt16Vector, AliasInt32Vector, AliasInt64Vector, AliasPtrVector, AliasFloatVector, AliasDoubleVector, AliasStringVector, AliasAnyVector, AliasVec2Vector, AliasVec3Vector, AliasVec4Vector, int> ParamAllAliasesCallback = &___ParamAllAliasesCallback;
		internal static delegate* unmanaged[Cdecl]<Bool8, Char8, Char16, sbyte, short, int, long, nint, float, double, String192*, Variant256*, Vector2*, Vector3*, Vector4*, Matrix4x4*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, int> __ParamAllAliasesCallback;
		private static int ___ParamAllAliasesCallback(AliasBool aBool, AliasChar8 aChar8, AliasChar16 aChar16, AliasInt8 aInt8, AliasInt16 aInt16, AliasInt32 aInt32, AliasInt64 aInt64, AliasPtr aPtr, AliasFloat aFloat, AliasDouble aDouble, AliasString aString, AliasAny aAny, AliasVec2 aVec2, AliasVec3 aVec3, AliasVec4 aVec4, AliasMat4x4 aMat4x4, AliasBoolVector aBoolVec, AliasChar8Vector aChar8Vec, AliasChar16Vector aChar16Vec, AliasInt8Vector aInt8Vec, AliasInt16Vector aInt16Vec, AliasInt32Vector aInt32Vec, AliasInt64Vector aInt64Vec, AliasPtrVector aPtrVec, AliasFloatVector aFloatVec, AliasDoubleVector aDoubleVec, AliasStringVector aStringVec, AliasAnyVector aAnyVec, AliasVec2Vector aVec2Vec, AliasVec3Vector aVec3Vec, AliasVec4Vector aVec4Vec)
		{
			int __retVal;
			var __aString = NativeMethods.ConstructString(aString);
			var __aAny = NativeMethods.ConstructVariant(aAny);
			var __aBoolVec = NativeMethods.ConstructVectorBool(aBoolVec, aBoolVec.Length);
			var __aChar8Vec = NativeMethods.ConstructVectorChar8(aChar8Vec, aChar8Vec.Length);
			var __aChar16Vec = NativeMethods.ConstructVectorChar16(aChar16Vec, aChar16Vec.Length);
			var __aInt8Vec = NativeMethods.ConstructVectorInt8(aInt8Vec, aInt8Vec.Length);
			var __aInt16Vec = NativeMethods.ConstructVectorInt16(aInt16Vec, aInt16Vec.Length);
			var __aInt32Vec = NativeMethods.ConstructVectorInt32(aInt32Vec, aInt32Vec.Length);
			var __aInt64Vec = NativeMethods.ConstructVectorInt64(aInt64Vec, aInt64Vec.Length);
			var __aPtrVec = NativeMethods.ConstructVectorIntPtr(aPtrVec, aPtrVec.Length);
			var __aFloatVec = NativeMethods.ConstructVectorFloat(aFloatVec, aFloatVec.Length);
			var __aDoubleVec = NativeMethods.ConstructVectorDouble(aDoubleVec, aDoubleVec.Length);
			var __aStringVec = NativeMethods.ConstructVectorString(aStringVec, aStringVec.Length);
			var __aAnyVec = NativeMethods.ConstructVectorVariant(aAnyVec, aAnyVec.Length);
			var __aVec2Vec = NativeMethods.ConstructVectorVector2(aVec2Vec, aVec2Vec.Length);
			var __aVec3Vec = NativeMethods.ConstructVectorVector3(aVec3Vec, aVec3Vec.Length);
			var __aVec4Vec = NativeMethods.ConstructVectorVector4(aVec4Vec, aVec4Vec.Length);
			try {
				__retVal = __ParamAllAliasesCallback(aBool, aChar8, aChar16, aInt8, aInt16, aInt32, aInt64, aPtr, aFloat, aDouble, &__aString, &__aAny, &aVec2, &aVec3, &aVec4, &aMat4x4, &__aBoolVec, &__aChar8Vec, &__aChar16Vec, &__aInt8Vec, &__aInt16Vec, &__aInt32Vec, &__aInt64Vec, &__aPtrVec, &__aFloatVec, &__aDoubleVec, &__aStringVec, &__aAnyVec, &__aVec2Vec, &__aVec3Vec, &__aVec4Vec);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__aString);
				NativeMethods.DestroyVariant(&__aAny);
				NativeMethods.DestroyVectorBool(&__aBoolVec);
				NativeMethods.DestroyVectorChar8(&__aChar8Vec);
				NativeMethods.DestroyVectorChar16(&__aChar16Vec);
				NativeMethods.DestroyVectorInt8(&__aInt8Vec);
				NativeMethods.DestroyVectorInt16(&__aInt16Vec);
				NativeMethods.DestroyVectorInt32(&__aInt32Vec);
				NativeMethods.DestroyVectorInt64(&__aInt64Vec);
				NativeMethods.DestroyVectorIntPtr(&__aPtrVec);
				NativeMethods.DestroyVectorFloat(&__aFloatVec);
				NativeMethods.DestroyVectorDouble(&__aDoubleVec);
				NativeMethods.DestroyVectorString(&__aStringVec);
				NativeMethods.DestroyVectorVariant(&__aAnyVec);
				NativeMethods.DestroyVectorVector2(&__aVec2Vec);
				NativeMethods.DestroyVectorVector3(&__aVec3Vec);
				NativeMethods.DestroyVectorVector4(&__aVec4Vec);
			}
			return __retVal;
		}

		/// <summary>
		/// ParamAllRefAliasesCallback
		/// </summary>
		/// <param name="aBool">aBool</param>
		/// <param name="aChar8">aChar8</param>
		/// <param name="aChar16">aChar16</param>
		/// <param name="aInt8">aInt8</param>
		/// <param name="aInt16">aInt16</param>
		/// <param name="aInt32">aInt32</param>
		/// <param name="aInt64">aInt64</param>
		/// <param name="aPtr">aPtr</param>
		/// <param name="aFloat">aFloat</param>
		/// <param name="aDouble">aDouble</param>
		/// <param name="aString">aString</param>
		/// <param name="aAny">aAny</param>
		/// <param name="aVec2">aVec2</param>
		/// <param name="aVec3">aVec3</param>
		/// <param name="aVec4">aVec4</param>
		/// <param name="aMat4x4">aMat4x4</param>
		/// <param name="aBoolVec">aBoolVec</param>
		/// <param name="aChar8Vec">aChar8Vec</param>
		/// <param name="aChar16Vec">aChar16Vec</param>
		/// <param name="aInt8Vec">aInt8Vec</param>
		/// <param name="aInt16Vec">aInt16Vec</param>
		/// <param name="aInt32Vec">aInt32Vec</param>
		/// <param name="aInt64Vec">aInt64Vec</param>
		/// <param name="aPtrVec">aPtrVec</param>
		/// <param name="aFloatVec">aFloatVec</param>
		/// <param name="aDoubleVec">aDoubleVec</param>
		/// <param name="aStringVec">aStringVec</param>
		/// <param name="aAnyVec">aAnyVec</param>
		/// <param name="aVec2Vec">aVec2Vec</param>
		/// <param name="aVec3Vec">aVec3Vec</param>
		/// <param name="aVec4Vec">aVec4Vec</param>
		internal static delegate*<ref AliasBool, ref AliasChar8, ref AliasChar16, ref AliasInt8, ref AliasInt16, ref AliasInt32, ref AliasInt64, ref AliasPtr, ref AliasFloat, ref AliasDouble, ref AliasString, ref AliasAny, ref AliasVec2, ref AliasVec3, ref AliasVec4, ref AliasMat4x4, ref AliasBoolVector, ref AliasChar8Vector, ref AliasChar16Vector, ref AliasInt8Vector, ref AliasInt16Vector, ref AliasInt32Vector, ref AliasInt64Vector, ref AliasPtrVector, ref AliasFloatVector, ref AliasDoubleVector, ref AliasStringVector, ref AliasAnyVector, ref AliasVec2Vector, ref AliasVec3Vector, ref AliasVec4Vector, long> ParamAllRefAliasesCallback = &___ParamAllRefAliasesCallback;
		internal static delegate* unmanaged[Cdecl]<Bool8*, Char8*, Char16*, sbyte*, short*, int*, long*, nint*, float*, double*, String192*, Variant256*, Vector2*, Vector3*, Vector4*, Matrix4x4*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*,  long> __ParamAllRefAliasesCallback;
		private static long ___ParamAllRefAliasesCallback(ref AliasBool aBool, ref AliasChar8 aChar8, ref AliasChar16 aChar16, ref AliasInt8 aInt8, ref AliasInt16 aInt16, ref AliasInt32 aInt32, ref AliasInt64 aInt64, ref AliasPtr aPtr, ref AliasFloat aFloat, ref AliasDouble aDouble, ref AliasString aString, ref AliasAny aAny, ref AliasVec2 aVec2, ref AliasVec3 aVec3, ref AliasVec4 aVec4, ref AliasMat4x4 aMat4x4, ref AliasBoolVector aBoolVec, ref AliasChar8Vector aChar8Vec, ref AliasChar16Vector aChar16Vec, ref AliasInt8Vector aInt8Vec, ref AliasInt16Vector aInt16Vec, ref AliasInt32Vector aInt32Vec, ref AliasInt64Vector aInt64Vec, ref AliasPtrVector aPtrVec, ref AliasFloatVector aFloatVec, ref AliasDoubleVector aDoubleVec, ref AliasStringVector aStringVec, ref AliasAnyVector aAnyVec, ref AliasVec2Vector aVec2Vec, ref AliasVec3Vector aVec3Vec, ref AliasVec4Vector aVec4Vec)
		{
			long __retVal;
			fixed(Bool8* __aBool = &aBool) {
			fixed(Char8* __aChar8 = &aChar8) {
			fixed(Char16* __aChar16 = &aChar16) {
			fixed(sbyte* __aInt8 = &aInt8) {
			fixed(short* __aInt16 = &aInt16) {
			fixed(int* __aInt32 = &aInt32) {
			fixed(long* __aInt64 = &aInt64) {
			fixed(nint* __aPtr = &aPtr) {
			fixed(float* __aFloat = &aFloat) {
			fixed(double* __aDouble = &aDouble) {
			fixed(Vector2* __aVec2 = &aVec2) {
			fixed(Vector3* __aVec3 = &aVec3) {
			fixed(Vector4* __aVec4 = &aVec4) {
			fixed(Matrix4x4* __aMat4x4 = &aMat4x4) {
			var __aString = NativeMethods.ConstructString(aString);
			var __aAny = NativeMethods.ConstructVariant(aAny);
			var __aBoolVec = NativeMethods.ConstructVectorBool(aBoolVec, aBoolVec.Length);
			var __aChar8Vec = NativeMethods.ConstructVectorChar8(aChar8Vec, aChar8Vec.Length);
			var __aChar16Vec = NativeMethods.ConstructVectorChar16(aChar16Vec, aChar16Vec.Length);
			var __aInt8Vec = NativeMethods.ConstructVectorInt8(aInt8Vec, aInt8Vec.Length);
			var __aInt16Vec = NativeMethods.ConstructVectorInt16(aInt16Vec, aInt16Vec.Length);
			var __aInt32Vec = NativeMethods.ConstructVectorInt32(aInt32Vec, aInt32Vec.Length);
			var __aInt64Vec = NativeMethods.ConstructVectorInt64(aInt64Vec, aInt64Vec.Length);
			var __aPtrVec = NativeMethods.ConstructVectorIntPtr(aPtrVec, aPtrVec.Length);
			var __aFloatVec = NativeMethods.ConstructVectorFloat(aFloatVec, aFloatVec.Length);
			var __aDoubleVec = NativeMethods.ConstructVectorDouble(aDoubleVec, aDoubleVec.Length);
			var __aStringVec = NativeMethods.ConstructVectorString(aStringVec, aStringVec.Length);
			var __aAnyVec = NativeMethods.ConstructVectorVariant(aAnyVec, aAnyVec.Length);
			var __aVec2Vec = NativeMethods.ConstructVectorVector2(aVec2Vec, aVec2Vec.Length);
			var __aVec3Vec = NativeMethods.ConstructVectorVector3(aVec3Vec, aVec3Vec.Length);
			var __aVec4Vec = NativeMethods.ConstructVectorVector4(aVec4Vec, aVec4Vec.Length);
			try {
				__retVal = __ParamAllRefAliasesCallback(__aBool, __aChar8, __aChar16, __aInt8, __aInt16, __aInt32, __aInt64, __aPtr, __aFloat, __aDouble, &__aString, &__aAny, __aVec2, __aVec3, __aVec4, __aMat4x4, &__aBoolVec, &__aChar8Vec, &__aChar16Vec, &__aInt8Vec, &__aInt16Vec, &__aInt32Vec, &__aInt64Vec, &__aPtrVec, &__aFloatVec, &__aDoubleVec, &__aStringVec, &__aAnyVec, &__aVec2Vec, &__aVec3Vec, &__aVec4Vec);
				// Unmarshal - Convert native data to managed data.
				aString = NativeMethods.GetStringData(&__aString);
				aAny = NativeMethods.GetVariantData(&__aAny);
				Array.Resize(ref aBoolVec, NativeMethods.GetVectorSizeBool(&__aBoolVec));
				NativeMethods.GetVectorDataBool(&__aBoolVec, aBoolVec);
				Array.Resize(ref aChar8Vec, NativeMethods.GetVectorSizeChar8(&__aChar8Vec));
				NativeMethods.GetVectorDataChar8(&__aChar8Vec, aChar8Vec);
				Array.Resize(ref aChar16Vec, NativeMethods.GetVectorSizeChar16(&__aChar16Vec));
				NativeMethods.GetVectorDataChar16(&__aChar16Vec, aChar16Vec);
				Array.Resize(ref aInt8Vec, NativeMethods.GetVectorSizeInt8(&__aInt8Vec));
				NativeMethods.GetVectorDataInt8(&__aInt8Vec, aInt8Vec);
				Array.Resize(ref aInt16Vec, NativeMethods.GetVectorSizeInt16(&__aInt16Vec));
				NativeMethods.GetVectorDataInt16(&__aInt16Vec, aInt16Vec);
				Array.Resize(ref aInt32Vec, NativeMethods.GetVectorSizeInt32(&__aInt32Vec));
				NativeMethods.GetVectorDataInt32(&__aInt32Vec, aInt32Vec);
				Array.Resize(ref aInt64Vec, NativeMethods.GetVectorSizeInt64(&__aInt64Vec));
				NativeMethods.GetVectorDataInt64(&__aInt64Vec, aInt64Vec);
				Array.Resize(ref aPtrVec, NativeMethods.GetVectorSizeIntPtr(&__aPtrVec));
				NativeMethods.GetVectorDataIntPtr(&__aPtrVec, aPtrVec);
				Array.Resize(ref aFloatVec, NativeMethods.GetVectorSizeFloat(&__aFloatVec));
				NativeMethods.GetVectorDataFloat(&__aFloatVec, aFloatVec);
				Array.Resize(ref aDoubleVec, NativeMethods.GetVectorSizeDouble(&__aDoubleVec));
				NativeMethods.GetVectorDataDouble(&__aDoubleVec, aDoubleVec);
				Array.Resize(ref aStringVec, NativeMethods.GetVectorSizeString(&__aStringVec));
				NativeMethods.GetVectorDataString(&__aStringVec, aStringVec);
				Array.Resize(ref aAnyVec, NativeMethods.GetVectorSizeVariant(&__aAnyVec));
				NativeMethods.GetVectorDataVariant(&__aAnyVec, aAnyVec);
				Array.Resize(ref aVec2Vec, NativeMethods.GetVectorSizeVector2(&__aVec2Vec));
				NativeMethods.GetVectorDataVector2(&__aVec2Vec, aVec2Vec);
				Array.Resize(ref aVec3Vec, NativeMethods.GetVectorSizeVector3(&__aVec3Vec));
				NativeMethods.GetVectorDataVector3(&__aVec3Vec, aVec3Vec);
				Array.Resize(ref aVec4Vec, NativeMethods.GetVectorSizeVector4(&__aVec4Vec));
				NativeMethods.GetVectorDataVector4(&__aVec4Vec, aVec4Vec);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__aString);
				NativeMethods.DestroyVariant(&__aAny);
				NativeMethods.DestroyVectorBool(&__aBoolVec);
				NativeMethods.DestroyVectorChar8(&__aChar8Vec);
				NativeMethods.DestroyVectorChar16(&__aChar16Vec);
				NativeMethods.DestroyVectorInt8(&__aInt8Vec);
				NativeMethods.DestroyVectorInt16(&__aInt16Vec);
				NativeMethods.DestroyVectorInt32(&__aInt32Vec);
				NativeMethods.DestroyVectorInt64(&__aInt64Vec);
				NativeMethods.DestroyVectorIntPtr(&__aPtrVec);
				NativeMethods.DestroyVectorFloat(&__aFloatVec);
				NativeMethods.DestroyVectorDouble(&__aDoubleVec);
				NativeMethods.DestroyVectorString(&__aStringVec);
				NativeMethods.DestroyVectorVariant(&__aAnyVec);
				NativeMethods.DestroyVectorVector2(&__aVec2Vec);
				NativeMethods.DestroyVectorVector3(&__aVec3Vec);
				NativeMethods.DestroyVectorVector4(&__aVec4Vec);
			}
			}
			}
			}
			}
			}
			}
			}
			}
			}
			}
			}
			}
			}
			}
			return __retVal;
		}

		/// <summary>
		/// ParamEnumCallback
		/// </summary>
		/// <param name="p1">p1</param>
		/// <param name="p2">p2</param>
		internal static delegate*<Example, Example[], int> ParamEnumCallback = &___ParamEnumCallback;
		internal static delegate* unmanaged[Cdecl]<Example, Vector192*, int> __ParamEnumCallback;
		private static int ___ParamEnumCallback(Example p1, Example[] p2)
		{
			int __retVal;
			var __p2 = NativeMethodsT.ConstructVectorInt32(p2, p2.Length);
			try {
				__retVal = __ParamEnumCallback(p1, &__p2);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt32(&__p2);
			}
			return __retVal;
		}

		/// <summary>
		/// ParamEnumRefCallback
		/// </summary>
		/// <param name="p1">p1</param>
		/// <param name="p2">p2</param>
		internal static delegate*<ref Example, ref Example[], int> ParamEnumRefCallback = &___ParamEnumRefCallback;
		internal static delegate* unmanaged[Cdecl]<Example*, Vector192*, int> __ParamEnumRefCallback;
		private static int ___ParamEnumRefCallback(ref Example p1, ref Example[] p2)
		{
			int __retVal;
			fixed(Example* __p1 = &p1) {
			var __p2 = NativeMethodsT.ConstructVectorInt32(p2, p2.Length);
			try {
				__retVal = __ParamEnumRefCallback(__p1, &__p2);
				// Unmarshal - Convert native data to managed data.
				Array.Resize(ref p2, NativeMethods.GetVectorSizeInt32(&__p2));
				NativeMethodsT.GetVectorDataInt32(&__p2, p2);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt32(&__p2);
			}
			}
			return __retVal;
		}

		/// <summary>
		/// ParamVariantCallback
		/// </summary>
		/// <param name="p1">p1</param>
		/// <param name="p2">p2</param>
		internal static delegate*<object, object[], void> ParamVariantCallback = &___ParamVariantCallback;
		internal static delegate* unmanaged[Cdecl]<Variant256*, Vector192*, void> __ParamVariantCallback;
		private static void ___ParamVariantCallback(object p1, object[] p2)
		{
			var __p1 = NativeMethods.ConstructVariant(p1);
			var __p2 = NativeMethods.ConstructVectorVariant(p2, p2.Length);
			try {
				__ParamVariantCallback(&__p1, &__p2);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVariant(&__p1);
				NativeMethods.DestroyVectorVariant(&__p2);
			}
		}

		/// <summary>
		/// ParamVariantRefCallback
		/// </summary>
		/// <param name="p1">p1</param>
		/// <param name="p2">p2</param>
		internal static delegate*<ref object, ref object[], void> ParamVariantRefCallback = &___ParamVariantRefCallback;
		internal static delegate* unmanaged[Cdecl]<Variant256*, Vector192*, void> __ParamVariantRefCallback;
		private static void ___ParamVariantRefCallback(ref object p1, ref object[] p2)
		{
			var __p1 = NativeMethods.ConstructVariant(p1);
			var __p2 = NativeMethods.ConstructVectorVariant(p2, p2.Length);
			try {
				__ParamVariantRefCallback(&__p1, &__p2);
				// Unmarshal - Convert native data to managed data.
				p1 = NativeMethods.GetVariantData(&__p1);
				Array.Resize(ref p2, NativeMethods.GetVectorSizeVariant(&__p2));
				NativeMethods.GetVectorDataVariant(&__p2, p2);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVariant(&__p1);
				NativeMethods.DestroyVectorVariant(&__p2);
			}
		}

		/// <summary>
		/// CallFuncVoidCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncVoid, void> CallFuncVoidCallback = &___CallFuncVoidCallback;
		internal static delegate* unmanaged[Cdecl]<nint, void> __CallFuncVoidCallback;
		private static void ___CallFuncVoidCallback(FuncVoid func)
		{
			__CallFuncVoidCallback(Marshalling.GetFunctionPointerForDelegate(func));
		}

		/// <summary>
		/// CallFuncBoolCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncBool, Bool8> CallFuncBoolCallback = &___CallFuncBoolCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Bool8> __CallFuncBoolCallback;
		private static Bool8 ___CallFuncBoolCallback(FuncBool func)
		{
			Bool8 __retVal = __CallFuncBoolCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncChar8Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncChar8, Char8> CallFuncChar8Callback = &___CallFuncChar8Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Char8> __CallFuncChar8Callback;
		private static Char8 ___CallFuncChar8Callback(FuncChar8 func)
		{
			Char8 __retVal = __CallFuncChar8Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncChar16Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncChar16, Char16> CallFuncChar16Callback = &___CallFuncChar16Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Char16> __CallFuncChar16Callback;
		private static Char16 ___CallFuncChar16Callback(FuncChar16 func)
		{
			Char16 __retVal = __CallFuncChar16Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncInt8Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncInt8, sbyte> CallFuncInt8Callback = &___CallFuncInt8Callback;
		internal static delegate* unmanaged[Cdecl]<nint, sbyte> __CallFuncInt8Callback;
		private static sbyte ___CallFuncInt8Callback(FuncInt8 func)
		{
			sbyte __retVal = __CallFuncInt8Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncInt16Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncInt16, short> CallFuncInt16Callback = &___CallFuncInt16Callback;
		internal static delegate* unmanaged[Cdecl]<nint, short> __CallFuncInt16Callback;
		private static short ___CallFuncInt16Callback(FuncInt16 func)
		{
			short __retVal = __CallFuncInt16Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncInt32Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncInt32, int> CallFuncInt32Callback = &___CallFuncInt32Callback;
		internal static delegate* unmanaged[Cdecl]<nint, int> __CallFuncInt32Callback;
		private static int ___CallFuncInt32Callback(FuncInt32 func)
		{
			int __retVal = __CallFuncInt32Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncInt64Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncInt64, long> CallFuncInt64Callback = &___CallFuncInt64Callback;
		internal static delegate* unmanaged[Cdecl]<nint, long> __CallFuncInt64Callback;
		private static long ___CallFuncInt64Callback(FuncInt64 func)
		{
			long __retVal = __CallFuncInt64Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncUInt8Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncUInt8, byte> CallFuncUInt8Callback = &___CallFuncUInt8Callback;
		internal static delegate* unmanaged[Cdecl]<nint, byte> __CallFuncUInt8Callback;
		private static byte ___CallFuncUInt8Callback(FuncUInt8 func)
		{
			byte __retVal = __CallFuncUInt8Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncUInt16Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncUInt16, ushort> CallFuncUInt16Callback = &___CallFuncUInt16Callback;
		internal static delegate* unmanaged[Cdecl]<nint, ushort> __CallFuncUInt16Callback;
		private static ushort ___CallFuncUInt16Callback(FuncUInt16 func)
		{
			ushort __retVal = __CallFuncUInt16Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncUInt32Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncUInt32, uint> CallFuncUInt32Callback = &___CallFuncUInt32Callback;
		internal static delegate* unmanaged[Cdecl]<nint, uint> __CallFuncUInt32Callback;
		private static uint ___CallFuncUInt32Callback(FuncUInt32 func)
		{
			uint __retVal = __CallFuncUInt32Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncUInt64Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncUInt64, ulong> CallFuncUInt64Callback = &___CallFuncUInt64Callback;
		internal static delegate* unmanaged[Cdecl]<nint, ulong> __CallFuncUInt64Callback;
		private static ulong ___CallFuncUInt64Callback(FuncUInt64 func)
		{
			ulong __retVal = __CallFuncUInt64Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncPtrCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncPtr, nint> CallFuncPtrCallback = &___CallFuncPtrCallback;
		internal static delegate* unmanaged[Cdecl]<nint, nint> __CallFuncPtrCallback;
		private static nint ___CallFuncPtrCallback(FuncPtr func)
		{
			nint __retVal = __CallFuncPtrCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncFloatCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncFloat, float> CallFuncFloatCallback = &___CallFuncFloatCallback;
		internal static delegate* unmanaged[Cdecl]<nint, float> __CallFuncFloatCallback;
		private static float ___CallFuncFloatCallback(FuncFloat func)
		{
			float __retVal = __CallFuncFloatCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncDoubleCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncDouble, double> CallFuncDoubleCallback = &___CallFuncDoubleCallback;
		internal static delegate* unmanaged[Cdecl]<nint, double> __CallFuncDoubleCallback;
		private static double ___CallFuncDoubleCallback(FuncDouble func)
		{
			double __retVal = __CallFuncDoubleCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncStringCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncString, string> CallFuncStringCallback = &___CallFuncStringCallback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFuncStringCallback;
		private static string ___CallFuncStringCallback(FuncString func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFuncStringCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAnyCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAny, object> CallFuncAnyCallback = &___CallFuncAnyCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Variant256> __CallFuncAnyCallback;
		private static object ___CallFuncAnyCallback(FuncAny func)
		{
			object __retVal;
			Variant256 __retVal_native;
			try {
				__retVal_native = __CallFuncAnyCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetVariantData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVariant(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncFunctionCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncFunction, nint> CallFuncFunctionCallback = &___CallFuncFunctionCallback;
		internal static delegate* unmanaged[Cdecl]<nint, nint> __CallFuncFunctionCallback;
		private static nint ___CallFuncFunctionCallback(FuncFunction func)
		{
			nint __retVal = __CallFuncFunctionCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncBoolVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncBoolVector, Bool8[]> CallFuncBoolVectorCallback = &___CallFuncBoolVectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncBoolVectorCallback;
		private static Bool8[] ___CallFuncBoolVectorCallback(FuncBoolVector func)
		{
			Bool8[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncBoolVectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new Bool8[NativeMethods.GetVectorSizeBool(&__retVal_native)];
				NativeMethods.GetVectorDataBool(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorBool(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncChar8VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncChar8Vector, Char8[]> CallFuncChar8VectorCallback = &___CallFuncChar8VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncChar8VectorCallback;
		private static Char8[] ___CallFuncChar8VectorCallback(FuncChar8Vector func)
		{
			Char8[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncChar8VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new Char8[NativeMethods.GetVectorSizeChar8(&__retVal_native)];
				NativeMethods.GetVectorDataChar8(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorChar8(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncChar16VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncChar16Vector, Char16[]> CallFuncChar16VectorCallback = &___CallFuncChar16VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncChar16VectorCallback;
		private static Char16[] ___CallFuncChar16VectorCallback(FuncChar16Vector func)
		{
			Char16[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncChar16VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new Char16[NativeMethods.GetVectorSizeChar16(&__retVal_native)];
				NativeMethods.GetVectorDataChar16(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorChar16(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncInt8VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncInt8Vector, sbyte[]> CallFuncInt8VectorCallback = &___CallFuncInt8VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncInt8VectorCallback;
		private static sbyte[] ___CallFuncInt8VectorCallback(FuncInt8Vector func)
		{
			sbyte[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncInt8VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new sbyte[NativeMethods.GetVectorSizeInt8(&__retVal_native)];
				NativeMethods.GetVectorDataInt8(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt8(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncInt16VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncInt16Vector, short[]> CallFuncInt16VectorCallback = &___CallFuncInt16VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncInt16VectorCallback;
		private static short[] ___CallFuncInt16VectorCallback(FuncInt16Vector func)
		{
			short[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncInt16VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new short[NativeMethods.GetVectorSizeInt16(&__retVal_native)];
				NativeMethods.GetVectorDataInt16(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt16(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncInt32VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncInt32Vector, int[]> CallFuncInt32VectorCallback = &___CallFuncInt32VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncInt32VectorCallback;
		private static int[] ___CallFuncInt32VectorCallback(FuncInt32Vector func)
		{
			int[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncInt32VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new int[NativeMethods.GetVectorSizeInt32(&__retVal_native)];
				NativeMethods.GetVectorDataInt32(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt32(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncInt64VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncInt64Vector, long[]> CallFuncInt64VectorCallback = &___CallFuncInt64VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncInt64VectorCallback;
		private static long[] ___CallFuncInt64VectorCallback(FuncInt64Vector func)
		{
			long[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncInt64VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new long[NativeMethods.GetVectorSizeInt64(&__retVal_native)];
				NativeMethods.GetVectorDataInt64(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt64(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncUInt8VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncUInt8Vector, byte[]> CallFuncUInt8VectorCallback = &___CallFuncUInt8VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncUInt8VectorCallback;
		private static byte[] ___CallFuncUInt8VectorCallback(FuncUInt8Vector func)
		{
			byte[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncUInt8VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new byte[NativeMethods.GetVectorSizeUInt8(&__retVal_native)];
				NativeMethods.GetVectorDataUInt8(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorUInt8(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncUInt16VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncUInt16Vector, ushort[]> CallFuncUInt16VectorCallback = &___CallFuncUInt16VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncUInt16VectorCallback;
		private static ushort[] ___CallFuncUInt16VectorCallback(FuncUInt16Vector func)
		{
			ushort[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncUInt16VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new ushort[NativeMethods.GetVectorSizeUInt16(&__retVal_native)];
				NativeMethods.GetVectorDataUInt16(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorUInt16(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncUInt32VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncUInt32Vector, uint[]> CallFuncUInt32VectorCallback = &___CallFuncUInt32VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncUInt32VectorCallback;
		private static uint[] ___CallFuncUInt32VectorCallback(FuncUInt32Vector func)
		{
			uint[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncUInt32VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new uint[NativeMethods.GetVectorSizeUInt32(&__retVal_native)];
				NativeMethods.GetVectorDataUInt32(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorUInt32(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncUInt64VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncUInt64Vector, ulong[]> CallFuncUInt64VectorCallback = &___CallFuncUInt64VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncUInt64VectorCallback;
		private static ulong[] ___CallFuncUInt64VectorCallback(FuncUInt64Vector func)
		{
			ulong[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncUInt64VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new ulong[NativeMethods.GetVectorSizeUInt64(&__retVal_native)];
				NativeMethods.GetVectorDataUInt64(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorUInt64(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncPtrVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncPtrVector, nint[]> CallFuncPtrVectorCallback = &___CallFuncPtrVectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncPtrVectorCallback;
		private static nint[] ___CallFuncPtrVectorCallback(FuncPtrVector func)
		{
			nint[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncPtrVectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new nint[NativeMethods.GetVectorSizeIntPtr(&__retVal_native)];
				NativeMethods.GetVectorDataIntPtr(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorIntPtr(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncFloatVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncFloatVector, float[]> CallFuncFloatVectorCallback = &___CallFuncFloatVectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncFloatVectorCallback;
		private static float[] ___CallFuncFloatVectorCallback(FuncFloatVector func)
		{
			float[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncFloatVectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new float[NativeMethods.GetVectorSizeFloat(&__retVal_native)];
				NativeMethods.GetVectorDataFloat(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorFloat(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncDoubleVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncDoubleVector, double[]> CallFuncDoubleVectorCallback = &___CallFuncDoubleVectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncDoubleVectorCallback;
		private static double[] ___CallFuncDoubleVectorCallback(FuncDoubleVector func)
		{
			double[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncDoubleVectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new double[NativeMethods.GetVectorSizeDouble(&__retVal_native)];
				NativeMethods.GetVectorDataDouble(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorDouble(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncStringVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncStringVector, string[]> CallFuncStringVectorCallback = &___CallFuncStringVectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncStringVectorCallback;
		private static string[] ___CallFuncStringVectorCallback(FuncStringVector func)
		{
			string[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncStringVectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new string[NativeMethods.GetVectorSizeString(&__retVal_native)];
				NativeMethods.GetVectorDataString(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAnyVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAnyVector, object[]> CallFuncAnyVectorCallback = &___CallFuncAnyVectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAnyVectorCallback;
		private static object[] ___CallFuncAnyVectorCallback(FuncAnyVector func)
		{
			object[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAnyVectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new object[NativeMethods.GetVectorSizeVariant(&__retVal_native)];
				NativeMethods.GetVectorDataVariant(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorVariant(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncVec2VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncVec2Vector, Vector2[]> CallFuncVec2VectorCallback = &___CallFuncVec2VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncVec2VectorCallback;
		private static Vector2[] ___CallFuncVec2VectorCallback(FuncVec2Vector func)
		{
			Vector2[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncVec2VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new Vector2[NativeMethods.GetVectorSizeVector2(&__retVal_native)];
				NativeMethods.GetVectorDataVector2(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorVector2(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncVec3VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncVec3Vector, Vector3[]> CallFuncVec3VectorCallback = &___CallFuncVec3VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncVec3VectorCallback;
		private static Vector3[] ___CallFuncVec3VectorCallback(FuncVec3Vector func)
		{
			Vector3[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncVec3VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new Vector3[NativeMethods.GetVectorSizeVector3(&__retVal_native)];
				NativeMethods.GetVectorDataVector3(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorVector3(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncVec4VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncVec4Vector, Vector4[]> CallFuncVec4VectorCallback = &___CallFuncVec4VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncVec4VectorCallback;
		private static Vector4[] ___CallFuncVec4VectorCallback(FuncVec4Vector func)
		{
			Vector4[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncVec4VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new Vector4[NativeMethods.GetVectorSizeVector4(&__retVal_native)];
				NativeMethods.GetVectorDataVector4(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorVector4(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncMat4x4VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncMat4x4Vector, Matrix4x4[]> CallFuncMat4x4VectorCallback = &___CallFuncMat4x4VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncMat4x4VectorCallback;
		private static Matrix4x4[] ___CallFuncMat4x4VectorCallback(FuncMat4x4Vector func)
		{
			Matrix4x4[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncMat4x4VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new Matrix4x4[NativeMethods.GetVectorSizeMatrix4x4(&__retVal_native)];
				NativeMethods.GetVectorDataMatrix4x4(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorMatrix4x4(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncVec2Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncVec2, Vector2> CallFuncVec2Callback = &___CallFuncVec2Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector2> __CallFuncVec2Callback;
		private static Vector2 ___CallFuncVec2Callback(FuncVec2 func)
		{
			Vector2 __retVal = __CallFuncVec2Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncVec3Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncVec3, Vector3> CallFuncVec3Callback = &___CallFuncVec3Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector3> __CallFuncVec3Callback;
		private static Vector3 ___CallFuncVec3Callback(FuncVec3 func)
		{
			Vector3 __retVal = __CallFuncVec3Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncVec4Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncVec4, Vector4> CallFuncVec4Callback = &___CallFuncVec4Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector4> __CallFuncVec4Callback;
		private static Vector4 ___CallFuncVec4Callback(FuncVec4 func)
		{
			Vector4 __retVal = __CallFuncVec4Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncMat4x4Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncMat4x4, Matrix4x4> CallFuncMat4x4Callback = &___CallFuncMat4x4Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Matrix4x4> __CallFuncMat4x4Callback;
		private static Matrix4x4 ___CallFuncMat4x4Callback(FuncMat4x4 func)
		{
			Matrix4x4 __retVal = __CallFuncMat4x4Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasBoolCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasBool, AliasBool> CallFuncAliasBoolCallback = &___CallFuncAliasBoolCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Bool8> __CallFuncAliasBoolCallback;
		private static AliasBool ___CallFuncAliasBoolCallback(FuncAliasBool func)
		{
			AliasBool __retVal = __CallFuncAliasBoolCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasChar8Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasChar8, AliasChar8> CallFuncAliasChar8Callback = &___CallFuncAliasChar8Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Char8> __CallFuncAliasChar8Callback;
		private static AliasChar8 ___CallFuncAliasChar8Callback(FuncAliasChar8 func)
		{
			AliasChar8 __retVal = __CallFuncAliasChar8Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasChar16Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasChar16, AliasChar16> CallFuncAliasChar16Callback = &___CallFuncAliasChar16Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Char16> __CallFuncAliasChar16Callback;
		private static AliasChar16 ___CallFuncAliasChar16Callback(FuncAliasChar16 func)
		{
			AliasChar16 __retVal = __CallFuncAliasChar16Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasInt8Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasInt8, AliasInt8> CallFuncAliasInt8Callback = &___CallFuncAliasInt8Callback;
		internal static delegate* unmanaged[Cdecl]<nint, sbyte> __CallFuncAliasInt8Callback;
		private static AliasInt8 ___CallFuncAliasInt8Callback(FuncAliasInt8 func)
		{
			AliasInt8 __retVal = __CallFuncAliasInt8Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasInt16Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasInt16, AliasInt16> CallFuncAliasInt16Callback = &___CallFuncAliasInt16Callback;
		internal static delegate* unmanaged[Cdecl]<nint, short> __CallFuncAliasInt16Callback;
		private static AliasInt16 ___CallFuncAliasInt16Callback(FuncAliasInt16 func)
		{
			AliasInt16 __retVal = __CallFuncAliasInt16Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasInt32Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasInt32, AliasInt32> CallFuncAliasInt32Callback = &___CallFuncAliasInt32Callback;
		internal static delegate* unmanaged[Cdecl]<nint, int> __CallFuncAliasInt32Callback;
		private static AliasInt32 ___CallFuncAliasInt32Callback(FuncAliasInt32 func)
		{
			AliasInt32 __retVal = __CallFuncAliasInt32Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasInt64Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasInt64, AliasInt64> CallFuncAliasInt64Callback = &___CallFuncAliasInt64Callback;
		internal static delegate* unmanaged[Cdecl]<nint, long> __CallFuncAliasInt64Callback;
		private static AliasInt64 ___CallFuncAliasInt64Callback(FuncAliasInt64 func)
		{
			AliasInt64 __retVal = __CallFuncAliasInt64Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasUInt8Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasUInt8, AliasUInt8> CallFuncAliasUInt8Callback = &___CallFuncAliasUInt8Callback;
		internal static delegate* unmanaged[Cdecl]<nint, byte> __CallFuncAliasUInt8Callback;
		private static AliasUInt8 ___CallFuncAliasUInt8Callback(FuncAliasUInt8 func)
		{
			AliasUInt8 __retVal = __CallFuncAliasUInt8Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasUInt16Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasUInt16, AliasUInt16> CallFuncAliasUInt16Callback = &___CallFuncAliasUInt16Callback;
		internal static delegate* unmanaged[Cdecl]<nint, ushort> __CallFuncAliasUInt16Callback;
		private static AliasUInt16 ___CallFuncAliasUInt16Callback(FuncAliasUInt16 func)
		{
			AliasUInt16 __retVal = __CallFuncAliasUInt16Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasUInt32Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasUInt32, AliasUInt32> CallFuncAliasUInt32Callback = &___CallFuncAliasUInt32Callback;
		internal static delegate* unmanaged[Cdecl]<nint, uint> __CallFuncAliasUInt32Callback;
		private static AliasUInt32 ___CallFuncAliasUInt32Callback(FuncAliasUInt32 func)
		{
			AliasUInt32 __retVal = __CallFuncAliasUInt32Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasUInt64Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasUInt64, AliasUInt64> CallFuncAliasUInt64Callback = &___CallFuncAliasUInt64Callback;
		internal static delegate* unmanaged[Cdecl]<nint, ulong> __CallFuncAliasUInt64Callback;
		private static AliasUInt64 ___CallFuncAliasUInt64Callback(FuncAliasUInt64 func)
		{
			AliasUInt64 __retVal = __CallFuncAliasUInt64Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasPtrCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasPtr, AliasPtr> CallFuncAliasPtrCallback = &___CallFuncAliasPtrCallback;
		internal static delegate* unmanaged[Cdecl]<nint, nint> __CallFuncAliasPtrCallback;
		private static AliasPtr ___CallFuncAliasPtrCallback(FuncAliasPtr func)
		{
			AliasPtr __retVal = __CallFuncAliasPtrCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasFloatCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasFloat, AliasFloat> CallFuncAliasFloatCallback = &___CallFuncAliasFloatCallback;
		internal static delegate* unmanaged[Cdecl]<nint, float> __CallFuncAliasFloatCallback;
		private static AliasFloat ___CallFuncAliasFloatCallback(FuncAliasFloat func)
		{
			AliasFloat __retVal = __CallFuncAliasFloatCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasDoubleCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasDouble, AliasDouble> CallFuncAliasDoubleCallback = &___CallFuncAliasDoubleCallback;
		internal static delegate* unmanaged[Cdecl]<nint, double> __CallFuncAliasDoubleCallback;
		private static AliasDouble ___CallFuncAliasDoubleCallback(FuncAliasDouble func)
		{
			AliasDouble __retVal = __CallFuncAliasDoubleCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasStringCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasString, AliasString> CallFuncAliasStringCallback = &___CallFuncAliasStringCallback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFuncAliasStringCallback;
		private static AliasString ___CallFuncAliasStringCallback(FuncAliasString func)
		{
			AliasString __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasStringCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasAnyCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasAny, AliasAny> CallFuncAliasAnyCallback = &___CallFuncAliasAnyCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Variant256> __CallFuncAliasAnyCallback;
		private static AliasAny ___CallFuncAliasAnyCallback(FuncAliasAny func)
		{
			AliasAny __retVal;
			Variant256 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasAnyCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetVariantData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVariant(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasFunctionCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasFunction, AliasFunction> CallFuncAliasFunctionCallback = &___CallFuncAliasFunctionCallback;
		internal static delegate* unmanaged[Cdecl]<nint, nint> __CallFuncAliasFunctionCallback;
		private static AliasFunction ___CallFuncAliasFunctionCallback(FuncAliasFunction func)
		{
			AliasFunction __retVal = __CallFuncAliasFunctionCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasBoolVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasBoolVector, AliasBoolVector> CallFuncAliasBoolVectorCallback = &___CallFuncAliasBoolVectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasBoolVectorCallback;
		private static AliasBoolVector ___CallFuncAliasBoolVectorCallback(FuncAliasBoolVector func)
		{
			AliasBoolVector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasBoolVectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new Bool8[NativeMethods.GetVectorSizeBool(&__retVal_native)];
				NativeMethods.GetVectorDataBool(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorBool(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasChar8VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasChar8Vector, AliasChar8Vector> CallFuncAliasChar8VectorCallback = &___CallFuncAliasChar8VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasChar8VectorCallback;
		private static AliasChar8Vector ___CallFuncAliasChar8VectorCallback(FuncAliasChar8Vector func)
		{
			AliasChar8Vector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasChar8VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new Char8[NativeMethods.GetVectorSizeChar8(&__retVal_native)];
				NativeMethods.GetVectorDataChar8(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorChar8(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasChar16VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasChar16Vector, AliasChar16Vector> CallFuncAliasChar16VectorCallback = &___CallFuncAliasChar16VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasChar16VectorCallback;
		private static AliasChar16Vector ___CallFuncAliasChar16VectorCallback(FuncAliasChar16Vector func)
		{
			AliasChar16Vector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasChar16VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new Char16[NativeMethods.GetVectorSizeChar16(&__retVal_native)];
				NativeMethods.GetVectorDataChar16(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorChar16(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasInt8VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasInt8Vector, AliasInt8Vector> CallFuncAliasInt8VectorCallback = &___CallFuncAliasInt8VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasInt8VectorCallback;
		private static AliasInt8Vector ___CallFuncAliasInt8VectorCallback(FuncAliasInt8Vector func)
		{
			AliasInt8Vector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasInt8VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new sbyte[NativeMethods.GetVectorSizeInt8(&__retVal_native)];
				NativeMethods.GetVectorDataInt8(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt8(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasInt16VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasInt16Vector, AliasInt16Vector> CallFuncAliasInt16VectorCallback = &___CallFuncAliasInt16VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasInt16VectorCallback;
		private static AliasInt16Vector ___CallFuncAliasInt16VectorCallback(FuncAliasInt16Vector func)
		{
			AliasInt16Vector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasInt16VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new short[NativeMethods.GetVectorSizeInt16(&__retVal_native)];
				NativeMethods.GetVectorDataInt16(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt16(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasInt32VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasInt32Vector, AliasInt32Vector> CallFuncAliasInt32VectorCallback = &___CallFuncAliasInt32VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasInt32VectorCallback;
		private static AliasInt32Vector ___CallFuncAliasInt32VectorCallback(FuncAliasInt32Vector func)
		{
			AliasInt32Vector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasInt32VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new int[NativeMethods.GetVectorSizeInt32(&__retVal_native)];
				NativeMethods.GetVectorDataInt32(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt32(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasInt64VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasInt64Vector, AliasInt64Vector> CallFuncAliasInt64VectorCallback = &___CallFuncAliasInt64VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasInt64VectorCallback;
		private static AliasInt64Vector ___CallFuncAliasInt64VectorCallback(FuncAliasInt64Vector func)
		{
			AliasInt64Vector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasInt64VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new long[NativeMethods.GetVectorSizeInt64(&__retVal_native)];
				NativeMethods.GetVectorDataInt64(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt64(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasUInt8VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasUInt8Vector, AliasUInt8Vector> CallFuncAliasUInt8VectorCallback = &___CallFuncAliasUInt8VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasUInt8VectorCallback;
		private static AliasUInt8Vector ___CallFuncAliasUInt8VectorCallback(FuncAliasUInt8Vector func)
		{
			AliasUInt8Vector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasUInt8VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new byte[NativeMethods.GetVectorSizeUInt8(&__retVal_native)];
				NativeMethods.GetVectorDataUInt8(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorUInt8(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasUInt16VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasUInt16Vector, AliasUInt16Vector> CallFuncAliasUInt16VectorCallback = &___CallFuncAliasUInt16VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasUInt16VectorCallback;
		private static AliasUInt16Vector ___CallFuncAliasUInt16VectorCallback(FuncAliasUInt16Vector func)
		{
			AliasUInt16Vector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasUInt16VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new ushort[NativeMethods.GetVectorSizeUInt16(&__retVal_native)];
				NativeMethods.GetVectorDataUInt16(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorUInt16(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasUInt32VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasUInt32Vector, AliasUInt32Vector> CallFuncAliasUInt32VectorCallback = &___CallFuncAliasUInt32VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasUInt32VectorCallback;
		private static AliasUInt32Vector ___CallFuncAliasUInt32VectorCallback(FuncAliasUInt32Vector func)
		{
			AliasUInt32Vector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasUInt32VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new uint[NativeMethods.GetVectorSizeUInt32(&__retVal_native)];
				NativeMethods.GetVectorDataUInt32(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorUInt32(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasUInt64VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasUInt64Vector, AliasUInt64Vector> CallFuncAliasUInt64VectorCallback = &___CallFuncAliasUInt64VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasUInt64VectorCallback;
		private static AliasUInt64Vector ___CallFuncAliasUInt64VectorCallback(FuncAliasUInt64Vector func)
		{
			AliasUInt64Vector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasUInt64VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new ulong[NativeMethods.GetVectorSizeUInt64(&__retVal_native)];
				NativeMethods.GetVectorDataUInt64(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorUInt64(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasPtrVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasPtrVector, AliasPtrVector> CallFuncAliasPtrVectorCallback = &___CallFuncAliasPtrVectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasPtrVectorCallback;
		private static AliasPtrVector ___CallFuncAliasPtrVectorCallback(FuncAliasPtrVector func)
		{
			AliasPtrVector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasPtrVectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new nint[NativeMethods.GetVectorSizeIntPtr(&__retVal_native)];
				NativeMethods.GetVectorDataIntPtr(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorIntPtr(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasFloatVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasFloatVector, AliasFloatVector> CallFuncAliasFloatVectorCallback = &___CallFuncAliasFloatVectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasFloatVectorCallback;
		private static AliasFloatVector ___CallFuncAliasFloatVectorCallback(FuncAliasFloatVector func)
		{
			AliasFloatVector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasFloatVectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new float[NativeMethods.GetVectorSizeFloat(&__retVal_native)];
				NativeMethods.GetVectorDataFloat(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorFloat(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasDoubleVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasDoubleVector, AliasDoubleVector> CallFuncAliasDoubleVectorCallback = &___CallFuncAliasDoubleVectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasDoubleVectorCallback;
		private static AliasDoubleVector ___CallFuncAliasDoubleVectorCallback(FuncAliasDoubleVector func)
		{
			AliasDoubleVector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasDoubleVectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new double[NativeMethods.GetVectorSizeDouble(&__retVal_native)];
				NativeMethods.GetVectorDataDouble(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorDouble(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasStringVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasStringVector, AliasStringVector> CallFuncAliasStringVectorCallback = &___CallFuncAliasStringVectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasStringVectorCallback;
		private static AliasStringVector ___CallFuncAliasStringVectorCallback(FuncAliasStringVector func)
		{
			AliasStringVector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasStringVectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new string[NativeMethods.GetVectorSizeString(&__retVal_native)];
				NativeMethods.GetVectorDataString(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasAnyVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasAnyVector, AliasAnyVector> CallFuncAliasAnyVectorCallback = &___CallFuncAliasAnyVectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasAnyVectorCallback;
		private static AliasAnyVector ___CallFuncAliasAnyVectorCallback(FuncAliasAnyVector func)
		{
			AliasAnyVector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasAnyVectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new object[NativeMethods.GetVectorSizeVariant(&__retVal_native)];
				NativeMethods.GetVectorDataVariant(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorVariant(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasVec2VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasVec2Vector, AliasVec2Vector> CallFuncAliasVec2VectorCallback = &___CallFuncAliasVec2VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasVec2VectorCallback;
		private static AliasVec2Vector ___CallFuncAliasVec2VectorCallback(FuncAliasVec2Vector func)
		{
			AliasVec2Vector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasVec2VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new Vector2[NativeMethods.GetVectorSizeVector2(&__retVal_native)];
				NativeMethods.GetVectorDataVector2(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorVector2(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasVec3VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasVec3Vector, AliasVec3Vector> CallFuncAliasVec3VectorCallback = &___CallFuncAliasVec3VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasVec3VectorCallback;
		private static AliasVec3Vector ___CallFuncAliasVec3VectorCallback(FuncAliasVec3Vector func)
		{
			AliasVec3Vector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasVec3VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new Vector3[NativeMethods.GetVectorSizeVector3(&__retVal_native)];
				NativeMethods.GetVectorDataVector3(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorVector3(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasVec4VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasVec4Vector, AliasVec4Vector> CallFuncAliasVec4VectorCallback = &___CallFuncAliasVec4VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasVec4VectorCallback;
		private static AliasVec4Vector ___CallFuncAliasVec4VectorCallback(FuncAliasVec4Vector func)
		{
			AliasVec4Vector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasVec4VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new Vector4[NativeMethods.GetVectorSizeVector4(&__retVal_native)];
				NativeMethods.GetVectorDataVector4(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorVector4(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasMat4x4VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasMat4x4Vector, AliasMat4x4Vector> CallFuncAliasMat4x4VectorCallback = &___CallFuncAliasMat4x4VectorCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFuncAliasMat4x4VectorCallback;
		private static AliasMat4x4Vector ___CallFuncAliasMat4x4VectorCallback(FuncAliasMat4x4Vector func)
		{
			AliasMat4x4Vector __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasMat4x4VectorCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new Matrix4x4[NativeMethods.GetVectorSizeMatrix4x4(&__retVal_native)];
				NativeMethods.GetVectorDataMatrix4x4(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorMatrix4x4(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasVec2Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasVec2, AliasVec2> CallFuncAliasVec2Callback = &___CallFuncAliasVec2Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector2> __CallFuncAliasVec2Callback;
		private static AliasVec2 ___CallFuncAliasVec2Callback(FuncAliasVec2 func)
		{
			AliasVec2 __retVal = __CallFuncAliasVec2Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasVec3Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasVec3, AliasVec3> CallFuncAliasVec3Callback = &___CallFuncAliasVec3Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector3> __CallFuncAliasVec3Callback;
		private static AliasVec3 ___CallFuncAliasVec3Callback(FuncAliasVec3 func)
		{
			AliasVec3 __retVal = __CallFuncAliasVec3Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasVec4Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasVec4, AliasVec4> CallFuncAliasVec4Callback = &___CallFuncAliasVec4Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector4> __CallFuncAliasVec4Callback;
		private static AliasVec4 ___CallFuncAliasVec4Callback(FuncAliasVec4 func)
		{
			AliasVec4 __retVal = __CallFuncAliasVec4Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasMat4x4Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasMat4x4, AliasMat4x4> CallFuncAliasMat4x4Callback = &___CallFuncAliasMat4x4Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Matrix4x4> __CallFuncAliasMat4x4Callback;
		private static AliasMat4x4 ___CallFuncAliasMat4x4Callback(FuncAliasMat4x4 func)
		{
			AliasMat4x4 __retVal = __CallFuncAliasMat4x4Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFuncAliasAllCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncAliasAll, string> CallFuncAliasAllCallback = &___CallFuncAliasAllCallback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFuncAliasAllCallback;
		private static string ___CallFuncAliasAllCallback(FuncAliasAll func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFuncAliasAllCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFunc1Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func1, int> CallFunc1Callback = &___CallFunc1Callback;
		internal static delegate* unmanaged[Cdecl]<nint, int> __CallFunc1Callback;
		private static int ___CallFunc1Callback(Func1 func)
		{
			int __retVal = __CallFunc1Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFunc2Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func2, Char8> CallFunc2Callback = &___CallFunc2Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Char8> __CallFunc2Callback;
		private static Char8 ___CallFunc2Callback(Func2 func)
		{
			Char8 __retVal = __CallFunc2Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFunc3Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func3, void> CallFunc3Callback = &___CallFunc3Callback;
		internal static delegate* unmanaged[Cdecl]<nint, void> __CallFunc3Callback;
		private static void ___CallFunc3Callback(Func3 func)
		{
			__CallFunc3Callback(Marshalling.GetFunctionPointerForDelegate(func));
		}

		/// <summary>
		/// CallFunc4Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func4, Vector4> CallFunc4Callback = &___CallFunc4Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector4> __CallFunc4Callback;
		private static Vector4 ___CallFunc4Callback(Func4 func)
		{
			Vector4 __retVal = __CallFunc4Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFunc5Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func5, Bool8> CallFunc5Callback = &___CallFunc5Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Bool8> __CallFunc5Callback;
		private static Bool8 ___CallFunc5Callback(Func5 func)
		{
			Bool8 __retVal = __CallFunc5Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFunc6Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func6, long> CallFunc6Callback = &___CallFunc6Callback;
		internal static delegate* unmanaged[Cdecl]<nint, long> __CallFunc6Callback;
		private static long ___CallFunc6Callback(Func6 func)
		{
			long __retVal = __CallFunc6Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFunc7Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func7, double> CallFunc7Callback = &___CallFunc7Callback;
		internal static delegate* unmanaged[Cdecl]<nint, double> __CallFunc7Callback;
		private static double ___CallFunc7Callback(Func7 func)
		{
			double __retVal = __CallFunc7Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFunc8Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func8, Matrix4x4> CallFunc8Callback = &___CallFunc8Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Matrix4x4> __CallFunc8Callback;
		private static Matrix4x4 ___CallFunc8Callback(Func8 func)
		{
			Matrix4x4 __retVal = __CallFunc8Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFunc9Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func9, void> CallFunc9Callback = &___CallFunc9Callback;
		internal static delegate* unmanaged[Cdecl]<nint, void> __CallFunc9Callback;
		private static void ___CallFunc9Callback(Func9 func)
		{
			__CallFunc9Callback(Marshalling.GetFunctionPointerForDelegate(func));
		}

		/// <summary>
		/// CallFunc10Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func10, uint> CallFunc10Callback = &___CallFunc10Callback;
		internal static delegate* unmanaged[Cdecl]<nint, uint> __CallFunc10Callback;
		private static uint ___CallFunc10Callback(Func10 func)
		{
			uint __retVal = __CallFunc10Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFunc11Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func11, nint> CallFunc11Callback = &___CallFunc11Callback;
		internal static delegate* unmanaged[Cdecl]<nint, nint> __CallFunc11Callback;
		private static nint ___CallFunc11Callback(Func11 func)
		{
			nint __retVal = __CallFunc11Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFunc12Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func12, Bool8> CallFunc12Callback = &___CallFunc12Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Bool8> __CallFunc12Callback;
		private static Bool8 ___CallFunc12Callback(Func12 func)
		{
			Bool8 __retVal = __CallFunc12Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFunc13Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func13, string> CallFunc13Callback = &___CallFunc13Callback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFunc13Callback;
		private static string ___CallFunc13Callback(Func13 func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFunc13Callback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFunc14Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func14, string[]> CallFunc14Callback = &___CallFunc14Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __CallFunc14Callback;
		private static string[] ___CallFunc14Callback(Func14 func)
		{
			string[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __CallFunc14Callback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = new string[NativeMethods.GetVectorSizeString(&__retVal_native)];
				NativeMethods.GetVectorDataString(&__retVal_native, __retVal);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFunc15Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func15, short> CallFunc15Callback = &___CallFunc15Callback;
		internal static delegate* unmanaged[Cdecl]<nint, short> __CallFunc15Callback;
		private static short ___CallFunc15Callback(Func15 func)
		{
			short __retVal = __CallFunc15Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFunc16Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func16, nint> CallFunc16Callback = &___CallFunc16Callback;
		internal static delegate* unmanaged[Cdecl]<nint, nint> __CallFunc16Callback;
		private static nint ___CallFunc16Callback(Func16 func)
		{
			nint __retVal = __CallFunc16Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}

		/// <summary>
		/// CallFunc17Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func17, string> CallFunc17Callback = &___CallFunc17Callback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFunc17Callback;
		private static string ___CallFunc17Callback(Func17 func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFunc17Callback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFunc18Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func18, string> CallFunc18Callback = &___CallFunc18Callback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFunc18Callback;
		private static string ___CallFunc18Callback(Func18 func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFunc18Callback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFunc19Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func19, string> CallFunc19Callback = &___CallFunc19Callback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFunc19Callback;
		private static string ___CallFunc19Callback(Func19 func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFunc19Callback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFunc20Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func20, string> CallFunc20Callback = &___CallFunc20Callback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFunc20Callback;
		private static string ___CallFunc20Callback(Func20 func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFunc20Callback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFunc21Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func21, string> CallFunc21Callback = &___CallFunc21Callback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFunc21Callback;
		private static string ___CallFunc21Callback(Func21 func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFunc21Callback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFunc22Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func22, string> CallFunc22Callback = &___CallFunc22Callback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFunc22Callback;
		private static string ___CallFunc22Callback(Func22 func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFunc22Callback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFunc23Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func23, string> CallFunc23Callback = &___CallFunc23Callback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFunc23Callback;
		private static string ___CallFunc23Callback(Func23 func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFunc23Callback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFunc24Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func24, string> CallFunc24Callback = &___CallFunc24Callback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFunc24Callback;
		private static string ___CallFunc24Callback(Func24 func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFunc24Callback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFunc25Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func25, string> CallFunc25Callback = &___CallFunc25Callback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFunc25Callback;
		private static string ___CallFunc25Callback(Func25 func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFunc25Callback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFunc26Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func26, string> CallFunc26Callback = &___CallFunc26Callback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFunc26Callback;
		private static string ___CallFunc26Callback(Func26 func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFunc26Callback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFunc27Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func27, string> CallFunc27Callback = &___CallFunc27Callback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFunc27Callback;
		private static string ___CallFunc27Callback(Func27 func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFunc27Callback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFunc28Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func28, string> CallFunc28Callback = &___CallFunc28Callback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFunc28Callback;
		private static string ___CallFunc28Callback(Func28 func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFunc28Callback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFunc29Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func29, string> CallFunc29Callback = &___CallFunc29Callback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFunc29Callback;
		private static string ___CallFunc29Callback(Func29 func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFunc29Callback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFunc30Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func30, string> CallFunc30Callback = &___CallFunc30Callback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFunc30Callback;
		private static string ___CallFunc30Callback(Func30 func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFunc30Callback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFunc31Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func31, string> CallFunc31Callback = &___CallFunc31Callback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFunc31Callback;
		private static string ___CallFunc31Callback(Func31 func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFunc31Callback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFunc32Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func32, string> CallFunc32Callback = &___CallFunc32Callback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFunc32Callback;
		private static string ___CallFunc32Callback(Func32 func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFunc32Callback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFunc33Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<Func33, string> CallFunc33Callback = &___CallFunc33Callback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFunc33Callback;
		private static string ___CallFunc33Callback(Func33 func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFunc33Callback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

		/// <summary>
		/// CallFuncEnumCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static delegate*<FuncEnum, string> CallFuncEnumCallback = &___CallFuncEnumCallback;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __CallFuncEnumCallback;
		private static string ___CallFuncEnumCallback(FuncEnum func)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __CallFuncEnumCallback(Marshalling.GetFunctionPointerForDelegate(func));
				// Unmarshal - Convert native data to managed data.
				__retVal = NativeMethods.GetStringData(&__retVal_native);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__retVal_native);
			}
			return __retVal;
		}

	}

	/// <summary>
	/// RAII wrapper for ResourceHandle pointer
	/// </summary>
	internal sealed unsafe class ResourceHandle : SafeHandle
	{
		/// <summary>
		/// Creates a new ResourceHandle instance
		/// </summary>
		/// <param name="id">id</param>
		/// <param name="name">name</param>
		public ResourceHandle(int id, string name) : this(cross_call_master.ResourceHandleCreate(id, name), Ownership.Owned)
		{
		}

		/// <summary>
		/// Creates a new ResourceHandle instance
		/// </summary>
		public ResourceHandle() : this(cross_call_master.ResourceHandleCreateDefault(), Ownership.Owned)
		{
		}

		/// <summary>
		/// Internal constructor for creating ResourceHandle from existing handle
		/// </summary>
		public ResourceHandle(nint handle, Ownership ownership = Ownership.Borrowed) : base((nint)handle, ownsHandle: ownership == Ownership.Owned)
		{
		}

		/// <summary>
		/// Releases the handle (called automatically by SafeHandle)
		/// </summary>
		protected override bool ReleaseHandle()
		{
			cross_call_master.ResourceHandleDestroy((nint)handle);
			return true;
		}

		/// <summary>
		/// Checks if the ResourceHandle has a valid handle
		/// </summary>
		public override bool IsInvalid => handle == nint.Zero;

		/// <summary>
		/// Gets the underlying handle
		/// </summary>
		public nint Handle => (nint)handle;

		/// <summary>
		/// Checks if the handle is valid
		/// </summary>
		public bool IsValid => handle != nint.Zero;

		/// <summary>
		/// Gets the underlying handle
		/// </summary>
		public nint Get() => (nint)handle;

		/// <summary>
		/// Releases ownership of the handle and returns it
		/// </summary>
		public nint Release()
		{
			var h = handle;
			SetHandleAsInvalid();
			return (nint)h;
		}

		/// <summary>
		/// Disposes the handle
		/// </summary>
		public void Reset()
		{
			Dispose();
		}

		/// <summary>
		/// GetId
		/// </summary>
		public int GetId()
		{
			ObjectDisposedException.ThrowIf(!IsValid, this);
			bool success = false;
			DangerousAddRef(ref success);
			try
			{
				return cross_call_master.ResourceHandleGetId(Handle);
			}
			finally
			{
				if (success) DangerousRelease();
			}
		}

		/// <summary>
		/// GetName
		/// </summary>
		public string GetName()
		{
			ObjectDisposedException.ThrowIf(!IsValid, this);
			bool success = false;
			DangerousAddRef(ref success);
			try
			{
				return cross_call_master.ResourceHandleGetName(Handle);
			}
			finally
			{
				if (success) DangerousRelease();
			}
		}

		/// <summary>
		/// SetName
		/// </summary>
		/// <param name="name">name</param>
		public void SetName(string name)
		{
			ObjectDisposedException.ThrowIf(!IsValid, this);
			bool success = false;
			DangerousAddRef(ref success);
			try
			{
				cross_call_master.ResourceHandleSetName(Handle, name);
			}
			finally
			{
				if (success) DangerousRelease();
			}
		}

		/// <summary>
		/// IncrementCounter
		/// </summary>
		public void IncrementCounter()
		{
			ObjectDisposedException.ThrowIf(!IsValid, this);
			bool success = false;
			DangerousAddRef(ref success);
			try
			{
				cross_call_master.ResourceHandleIncrementCounter(Handle);
			}
			finally
			{
				if (success) DangerousRelease();
			}
		}

		/// <summary>
		/// GetCounter
		/// </summary>
		public int GetCounter()
		{
			ObjectDisposedException.ThrowIf(!IsValid, this);
			bool success = false;
			DangerousAddRef(ref success);
			try
			{
				return cross_call_master.ResourceHandleGetCounter(Handle);
			}
			finally
			{
				if (success) DangerousRelease();
			}
		}

		/// <summary>
		/// AddData
		/// </summary>
		/// <param name="value">value</param>
		public void AddData(float value)
		{
			ObjectDisposedException.ThrowIf(!IsValid, this);
			bool success = false;
			DangerousAddRef(ref success);
			try
			{
				cross_call_master.ResourceHandleAddData(Handle, value);
			}
			finally
			{
				if (success) DangerousRelease();
			}
		}

		/// <summary>
		/// GetData
		/// </summary>
		public float[] GetData()
		{
			ObjectDisposedException.ThrowIf(!IsValid, this);
			bool success = false;
			DangerousAddRef(ref success);
			try
			{
				return cross_call_master.ResourceHandleGetData(Handle);
			}
			finally
			{
				if (success) DangerousRelease();
			}
		}

		/// <summary>
		/// GetAliveCount
		/// </summary>
		public static int GetAliveCount()
		{
			return cross_call_master.ResourceHandleGetAliveCount();
		}

		/// <summary>
		/// GetTotalCreated
		/// </summary>
		public static int GetTotalCreated()
		{
			return cross_call_master.ResourceHandleGetTotalCreated();
		}

		/// <summary>
		/// GetTotalDestroyed
		/// </summary>
		public static int GetTotalDestroyed()
		{
			return cross_call_master.ResourceHandleGetTotalDestroyed();
		}

	}

	/// <summary>
	/// Counter wrapper
	/// </summary>
	internal sealed unsafe class Counter
	{
		private nint handle;

		/// <summary>
		/// Creates a new Counter instance
		/// </summary>
		/// <param name="initialValue">initialValue</param>
		public Counter(long initialValue)
		{
			this.handle = cross_call_master.CounterCreate(initialValue);
		}

		/// <summary>
		/// Creates a new Counter instance
		/// </summary>
		public Counter()
		{
			this.handle = cross_call_master.CounterCreateZero();
		}

		/// <summary>
		/// Internal constructor for creating Counter from existing handle
		/// </summary>
		public Counter(nint handle, Ownership ownership = Ownership.Borrowed)
		{
			this.handle = handle;
		}

		/// <summary>
		/// Gets the underlying handle
		/// </summary>
		public nint Handle => handle;

		/// <summary>
		/// Checks if the handle is valid
		/// </summary>
		public bool IsValid => handle != nint.Zero;

		/// <summary>
		/// Gets the underlying handle
		/// </summary>
		public nint Get() => handle;

		/// <summary>
		/// Releases ownership of the handle and returns it
		/// </summary>
		public nint Release()
		{
			var h = handle;
			handle = nint.Zero;
			return h;
		}

		/// <summary>
		/// Resets the handle to invalid
		/// </summary>
		public void Reset2()
		{
			handle = nint.Zero;
		}

		/// <summary>
		/// GetValue
		/// </summary>
		public long GetValue()
		{
			ObjectDisposedException.ThrowIf(!IsValid, this);
			return cross_call_master.CounterGetValue(handle);
		}

		/// <summary>
		/// SetValue
		/// </summary>
		/// <param name="value">value</param>
		public void SetValue(long value)
		{
			ObjectDisposedException.ThrowIf(!IsValid, this);
			cross_call_master.CounterSetValue(handle, value);
		}

		/// <summary>
		/// Increment
		/// </summary>
		public void Increment()
		{
			ObjectDisposedException.ThrowIf(!IsValid, this);
			cross_call_master.CounterIncrement(handle);
		}

		/// <summary>
		/// Decrement
		/// </summary>
		public void Decrement()
		{
			ObjectDisposedException.ThrowIf(!IsValid, this);
			cross_call_master.CounterDecrement(handle);
		}

		/// <summary>
		/// Add
		/// </summary>
		/// <param name="amount">amount</param>
		public void Add(long amount)
		{
			ObjectDisposedException.ThrowIf(!IsValid, this);
			cross_call_master.CounterAdd(handle, amount);
		}

		/// <summary>
		/// Reset
		/// </summary>
		public void Reset()
		{
			ObjectDisposedException.ThrowIf(!IsValid, this);
			cross_call_master.CounterReset(handle);
		}

		/// <summary>
		/// IsPositive
		/// </summary>
		public Bool8 IsPositive()
		{
			ObjectDisposedException.ThrowIf(!IsValid, this);
			return cross_call_master.CounterIsPositive(handle);
		}

		/// <summary>
		/// Compare
		/// </summary>
		/// <param name="value1">value1</param>
		/// <param name="value2">value2</param>
		public static int Compare(long value1, long value2)
		{
			return cross_call_master.CounterCompare(value1, value2);
		}

		/// <summary>
		/// Sum
		/// </summary>
		/// <param name="values">values</param>
		public static long Sum(long[] values)
		{
			return cross_call_master.CounterSum(values);
		}

	}

#pragma warning restore CS0649
}
