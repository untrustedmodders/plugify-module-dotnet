using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Plugify;

// Generated from cross_call_master.pplugin (group: core)

namespace cross_call_master {
#pragma warning disable CS0649

	internal static unsafe partial class cross_call_master {

#region ReverseReturn
		internal static delegate*<string, void> _ReverseReturn = &___ReverseReturn;
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
#endregion ReverseReturn
		/// <summary>
		/// ReverseReturn
		/// </summary>
		/// <param name="returnString">returnString</param>
		internal static void ReverseReturn(string returnString, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ReverseReturn", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_ReverseReturn(returnString);
		}

#region NoParamReturnVoidCallback
		internal static delegate*<void> _NoParamReturnVoidCallback = &___NoParamReturnVoidCallback;
		internal static delegate* unmanaged[Cdecl]<void> __NoParamReturnVoidCallback;
		private static void ___NoParamReturnVoidCallback()
		{
			__NoParamReturnVoidCallback();
		}
#endregion NoParamReturnVoidCallback
		/// <summary>
		/// NoParamReturnVoidCallback
		/// </summary>
		internal static void NoParamReturnVoidCallback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnVoidCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_NoParamReturnVoidCallback();
		}

#region NoParamReturnBoolCallback
		internal static delegate*<Bool8> _NoParamReturnBoolCallback = &___NoParamReturnBoolCallback;
		internal static delegate* unmanaged[Cdecl]<Bool8> __NoParamReturnBoolCallback;
		private static Bool8 ___NoParamReturnBoolCallback()
		{
			Bool8 __retVal = __NoParamReturnBoolCallback();
			return __retVal;
		}
#endregion NoParamReturnBoolCallback
		/// <summary>
		/// NoParamReturnBoolCallback
		/// </summary>
		internal static Bool8 NoParamReturnBoolCallback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnBoolCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnBoolCallback();
		}

#region NoParamReturnChar8Callback
		internal static delegate*<Char8> _NoParamReturnChar8Callback = &___NoParamReturnChar8Callback;
		internal static delegate* unmanaged[Cdecl]<Char8> __NoParamReturnChar8Callback;
		private static Char8 ___NoParamReturnChar8Callback()
		{
			Char8 __retVal = __NoParamReturnChar8Callback();
			return __retVal;
		}
#endregion NoParamReturnChar8Callback
		/// <summary>
		/// NoParamReturnChar8Callback
		/// </summary>
		internal static Char8 NoParamReturnChar8Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnChar8Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnChar8Callback();
		}

#region NoParamReturnChar16Callback
		internal static delegate*<Char16> _NoParamReturnChar16Callback = &___NoParamReturnChar16Callback;
		internal static delegate* unmanaged[Cdecl]<Char16> __NoParamReturnChar16Callback;
		private static Char16 ___NoParamReturnChar16Callback()
		{
			Char16 __retVal = __NoParamReturnChar16Callback();
			return __retVal;
		}
#endregion NoParamReturnChar16Callback
		/// <summary>
		/// NoParamReturnChar16Callback
		/// </summary>
		internal static Char16 NoParamReturnChar16Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnChar16Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnChar16Callback();
		}

#region NoParamReturnInt8Callback
		internal static delegate*<sbyte> _NoParamReturnInt8Callback = &___NoParamReturnInt8Callback;
		internal static delegate* unmanaged[Cdecl]<sbyte> __NoParamReturnInt8Callback;
		private static sbyte ___NoParamReturnInt8Callback()
		{
			sbyte __retVal = __NoParamReturnInt8Callback();
			return __retVal;
		}
#endregion NoParamReturnInt8Callback
		/// <summary>
		/// NoParamReturnInt8Callback
		/// </summary>
		internal static sbyte NoParamReturnInt8Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnInt8Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnInt8Callback();
		}

#region NoParamReturnInt16Callback
		internal static delegate*<short> _NoParamReturnInt16Callback = &___NoParamReturnInt16Callback;
		internal static delegate* unmanaged[Cdecl]<short> __NoParamReturnInt16Callback;
		private static short ___NoParamReturnInt16Callback()
		{
			short __retVal = __NoParamReturnInt16Callback();
			return __retVal;
		}
#endregion NoParamReturnInt16Callback
		/// <summary>
		/// NoParamReturnInt16Callback
		/// </summary>
		internal static short NoParamReturnInt16Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnInt16Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnInt16Callback();
		}

#region NoParamReturnInt32Callback
		internal static delegate*<int> _NoParamReturnInt32Callback = &___NoParamReturnInt32Callback;
		internal static delegate* unmanaged[Cdecl]<int> __NoParamReturnInt32Callback;
		private static int ___NoParamReturnInt32Callback()
		{
			int __retVal = __NoParamReturnInt32Callback();
			return __retVal;
		}
#endregion NoParamReturnInt32Callback
		/// <summary>
		/// NoParamReturnInt32Callback
		/// </summary>
		internal static int NoParamReturnInt32Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnInt32Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnInt32Callback();
		}

#region NoParamReturnInt64Callback
		internal static delegate*<long> _NoParamReturnInt64Callback = &___NoParamReturnInt64Callback;
		internal static delegate* unmanaged[Cdecl]<long> __NoParamReturnInt64Callback;
		private static long ___NoParamReturnInt64Callback()
		{
			long __retVal = __NoParamReturnInt64Callback();
			return __retVal;
		}
#endregion NoParamReturnInt64Callback
		/// <summary>
		/// NoParamReturnInt64Callback
		/// </summary>
		internal static long NoParamReturnInt64Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnInt64Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnInt64Callback();
		}

#region NoParamReturnUInt8Callback
		internal static delegate*<byte> _NoParamReturnUInt8Callback = &___NoParamReturnUInt8Callback;
		internal static delegate* unmanaged[Cdecl]<byte> __NoParamReturnUInt8Callback;
		private static byte ___NoParamReturnUInt8Callback()
		{
			byte __retVal = __NoParamReturnUInt8Callback();
			return __retVal;
		}
#endregion NoParamReturnUInt8Callback
		/// <summary>
		/// NoParamReturnUInt8Callback
		/// </summary>
		internal static byte NoParamReturnUInt8Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnUInt8Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnUInt8Callback();
		}

#region NoParamReturnUInt16Callback
		internal static delegate*<ushort> _NoParamReturnUInt16Callback = &___NoParamReturnUInt16Callback;
		internal static delegate* unmanaged[Cdecl]<ushort> __NoParamReturnUInt16Callback;
		private static ushort ___NoParamReturnUInt16Callback()
		{
			ushort __retVal = __NoParamReturnUInt16Callback();
			return __retVal;
		}
#endregion NoParamReturnUInt16Callback
		/// <summary>
		/// NoParamReturnUInt16Callback
		/// </summary>
		internal static ushort NoParamReturnUInt16Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnUInt16Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnUInt16Callback();
		}

#region NoParamReturnUInt32Callback
		internal static delegate*<uint> _NoParamReturnUInt32Callback = &___NoParamReturnUInt32Callback;
		internal static delegate* unmanaged[Cdecl]<uint> __NoParamReturnUInt32Callback;
		private static uint ___NoParamReturnUInt32Callback()
		{
			uint __retVal = __NoParamReturnUInt32Callback();
			return __retVal;
		}
#endregion NoParamReturnUInt32Callback
		/// <summary>
		/// NoParamReturnUInt32Callback
		/// </summary>
		internal static uint NoParamReturnUInt32Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnUInt32Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnUInt32Callback();
		}

#region NoParamReturnUInt64Callback
		internal static delegate*<ulong> _NoParamReturnUInt64Callback = &___NoParamReturnUInt64Callback;
		internal static delegate* unmanaged[Cdecl]<ulong> __NoParamReturnUInt64Callback;
		private static ulong ___NoParamReturnUInt64Callback()
		{
			ulong __retVal = __NoParamReturnUInt64Callback();
			return __retVal;
		}
#endregion NoParamReturnUInt64Callback
		/// <summary>
		/// NoParamReturnUInt64Callback
		/// </summary>
		internal static ulong NoParamReturnUInt64Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnUInt64Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnUInt64Callback();
		}

#region NoParamReturnPointerCallback
		internal static delegate*<nint> _NoParamReturnPointerCallback = &___NoParamReturnPointerCallback;
		internal static delegate* unmanaged[Cdecl]<nint> __NoParamReturnPointerCallback;
		private static nint ___NoParamReturnPointerCallback()
		{
			nint __retVal = __NoParamReturnPointerCallback();
			return __retVal;
		}
#endregion NoParamReturnPointerCallback
		/// <summary>
		/// NoParamReturnPointerCallback
		/// </summary>
		internal static nint NoParamReturnPointerCallback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnPointerCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnPointerCallback();
		}

#region NoParamReturnFloatCallback
		internal static delegate*<float> _NoParamReturnFloatCallback = &___NoParamReturnFloatCallback;
		internal static delegate* unmanaged[Cdecl]<float> __NoParamReturnFloatCallback;
		private static float ___NoParamReturnFloatCallback()
		{
			float __retVal = __NoParamReturnFloatCallback();
			return __retVal;
		}
#endregion NoParamReturnFloatCallback
		/// <summary>
		/// NoParamReturnFloatCallback
		/// </summary>
		internal static float NoParamReturnFloatCallback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnFloatCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnFloatCallback();
		}

#region NoParamReturnDoubleCallback
		internal static delegate*<double> _NoParamReturnDoubleCallback = &___NoParamReturnDoubleCallback;
		internal static delegate* unmanaged[Cdecl]<double> __NoParamReturnDoubleCallback;
		private static double ___NoParamReturnDoubleCallback()
		{
			double __retVal = __NoParamReturnDoubleCallback();
			return __retVal;
		}
#endregion NoParamReturnDoubleCallback
		/// <summary>
		/// NoParamReturnDoubleCallback
		/// </summary>
		internal static double NoParamReturnDoubleCallback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnDoubleCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnDoubleCallback();
		}

#region NoParamReturnFunctionCallback
		internal static delegate*<NoParamReturnFunctionCallbackFunc> _NoParamReturnFunctionCallback = &___NoParamReturnFunctionCallback;
		internal static delegate* unmanaged[Cdecl]<nint> __NoParamReturnFunctionCallback;
		private static NoParamReturnFunctionCallbackFunc ___NoParamReturnFunctionCallback()
		{
			nint __retVal = __NoParamReturnFunctionCallback();
			return Marshalling.GetDelegateForFunctionPointer<NoParamReturnFunctionCallbackFunc>(__retVal);
		}
#endregion NoParamReturnFunctionCallback
		/// <summary>
		/// NoParamReturnFunctionCallback
		/// </summary>
		internal static NoParamReturnFunctionCallbackFunc NoParamReturnFunctionCallback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnFunctionCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnFunctionCallback();
		}

#region NoParamReturnStringCallback
		internal static delegate*<string> _NoParamReturnStringCallback = &___NoParamReturnStringCallback;
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
#endregion NoParamReturnStringCallback
		/// <summary>
		/// NoParamReturnStringCallback
		/// </summary>
		internal static string NoParamReturnStringCallback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnStringCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnStringCallback();
		}

#region NoParamReturnAnyCallback
		internal static delegate*<object> _NoParamReturnAnyCallback = &___NoParamReturnAnyCallback;
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
#endregion NoParamReturnAnyCallback
		/// <summary>
		/// NoParamReturnAnyCallback
		/// </summary>
		internal static object NoParamReturnAnyCallback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnAnyCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnAnyCallback();
		}

#region NoParamReturnArrayBoolCallback
		internal static delegate*<Bool8[]> _NoParamReturnArrayBoolCallback = &___NoParamReturnArrayBoolCallback;
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
#endregion NoParamReturnArrayBoolCallback
		/// <summary>
		/// NoParamReturnArrayBoolCallback
		/// </summary>
		internal static Bool8[] NoParamReturnArrayBoolCallback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayBoolCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayBoolCallback();
		}

#region NoParamReturnArrayChar8Callback
		internal static delegate*<Char8[]> _NoParamReturnArrayChar8Callback = &___NoParamReturnArrayChar8Callback;
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
#endregion NoParamReturnArrayChar8Callback
		/// <summary>
		/// NoParamReturnArrayChar8Callback
		/// </summary>
		internal static Char8[] NoParamReturnArrayChar8Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayChar8Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayChar8Callback();
		}

#region NoParamReturnArrayChar16Callback
		internal static delegate*<Char16[]> _NoParamReturnArrayChar16Callback = &___NoParamReturnArrayChar16Callback;
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
#endregion NoParamReturnArrayChar16Callback
		/// <summary>
		/// NoParamReturnArrayChar16Callback
		/// </summary>
		internal static Char16[] NoParamReturnArrayChar16Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayChar16Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayChar16Callback();
		}

#region NoParamReturnArrayInt8Callback
		internal static delegate*<sbyte[]> _NoParamReturnArrayInt8Callback = &___NoParamReturnArrayInt8Callback;
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
#endregion NoParamReturnArrayInt8Callback
		/// <summary>
		/// NoParamReturnArrayInt8Callback
		/// </summary>
		internal static sbyte[] NoParamReturnArrayInt8Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayInt8Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayInt8Callback();
		}

#region NoParamReturnArrayInt16Callback
		internal static delegate*<short[]> _NoParamReturnArrayInt16Callback = &___NoParamReturnArrayInt16Callback;
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
#endregion NoParamReturnArrayInt16Callback
		/// <summary>
		/// NoParamReturnArrayInt16Callback
		/// </summary>
		internal static short[] NoParamReturnArrayInt16Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayInt16Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayInt16Callback();
		}

#region NoParamReturnArrayInt32Callback
		internal static delegate*<int[]> _NoParamReturnArrayInt32Callback = &___NoParamReturnArrayInt32Callback;
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
#endregion NoParamReturnArrayInt32Callback
		/// <summary>
		/// NoParamReturnArrayInt32Callback
		/// </summary>
		internal static int[] NoParamReturnArrayInt32Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayInt32Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayInt32Callback();
		}

#region NoParamReturnArrayInt64Callback
		internal static delegate*<long[]> _NoParamReturnArrayInt64Callback = &___NoParamReturnArrayInt64Callback;
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
#endregion NoParamReturnArrayInt64Callback
		/// <summary>
		/// NoParamReturnArrayInt64Callback
		/// </summary>
		internal static long[] NoParamReturnArrayInt64Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayInt64Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayInt64Callback();
		}

#region NoParamReturnArrayUInt8Callback
		internal static delegate*<byte[]> _NoParamReturnArrayUInt8Callback = &___NoParamReturnArrayUInt8Callback;
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
#endregion NoParamReturnArrayUInt8Callback
		/// <summary>
		/// NoParamReturnArrayUInt8Callback
		/// </summary>
		internal static byte[] NoParamReturnArrayUInt8Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayUInt8Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayUInt8Callback();
		}

#region NoParamReturnArrayUInt16Callback
		internal static delegate*<ushort[]> _NoParamReturnArrayUInt16Callback = &___NoParamReturnArrayUInt16Callback;
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
#endregion NoParamReturnArrayUInt16Callback
		/// <summary>
		/// NoParamReturnArrayUInt16Callback
		/// </summary>
		internal static ushort[] NoParamReturnArrayUInt16Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayUInt16Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayUInt16Callback();
		}

#region NoParamReturnArrayUInt32Callback
		internal static delegate*<uint[]> _NoParamReturnArrayUInt32Callback = &___NoParamReturnArrayUInt32Callback;
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
#endregion NoParamReturnArrayUInt32Callback
		/// <summary>
		/// NoParamReturnArrayUInt32Callback
		/// </summary>
		internal static uint[] NoParamReturnArrayUInt32Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayUInt32Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayUInt32Callback();
		}

#region NoParamReturnArrayUInt64Callback
		internal static delegate*<ulong[]> _NoParamReturnArrayUInt64Callback = &___NoParamReturnArrayUInt64Callback;
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
#endregion NoParamReturnArrayUInt64Callback
		/// <summary>
		/// NoParamReturnArrayUInt64Callback
		/// </summary>
		internal static ulong[] NoParamReturnArrayUInt64Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayUInt64Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayUInt64Callback();
		}

#region NoParamReturnArrayPointerCallback
		internal static delegate*<nint[]> _NoParamReturnArrayPointerCallback = &___NoParamReturnArrayPointerCallback;
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
#endregion NoParamReturnArrayPointerCallback
		/// <summary>
		/// NoParamReturnArrayPointerCallback
		/// </summary>
		internal static nint[] NoParamReturnArrayPointerCallback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayPointerCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayPointerCallback();
		}

#region NoParamReturnArrayFloatCallback
		internal static delegate*<float[]> _NoParamReturnArrayFloatCallback = &___NoParamReturnArrayFloatCallback;
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
#endregion NoParamReturnArrayFloatCallback
		/// <summary>
		/// NoParamReturnArrayFloatCallback
		/// </summary>
		internal static float[] NoParamReturnArrayFloatCallback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayFloatCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayFloatCallback();
		}

#region NoParamReturnArrayDoubleCallback
		internal static delegate*<double[]> _NoParamReturnArrayDoubleCallback = &___NoParamReturnArrayDoubleCallback;
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
#endregion NoParamReturnArrayDoubleCallback
		/// <summary>
		/// NoParamReturnArrayDoubleCallback
		/// </summary>
		internal static double[] NoParamReturnArrayDoubleCallback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayDoubleCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayDoubleCallback();
		}

#region NoParamReturnArrayStringCallback
		internal static delegate*<string[]> _NoParamReturnArrayStringCallback = &___NoParamReturnArrayStringCallback;
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
#endregion NoParamReturnArrayStringCallback
		/// <summary>
		/// NoParamReturnArrayStringCallback
		/// </summary>
		internal static string[] NoParamReturnArrayStringCallback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayStringCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayStringCallback();
		}

#region NoParamReturnArrayAnyCallback
		internal static delegate*<object[]> _NoParamReturnArrayAnyCallback = &___NoParamReturnArrayAnyCallback;
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
#endregion NoParamReturnArrayAnyCallback
		/// <summary>
		/// NoParamReturnArrayAnyCallback
		/// </summary>
		internal static object[] NoParamReturnArrayAnyCallback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayAnyCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayAnyCallback();
		}

#region NoParamReturnArrayVector2Callback
		internal static delegate*<Vector2[]> _NoParamReturnArrayVector2Callback = &___NoParamReturnArrayVector2Callback;
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
#endregion NoParamReturnArrayVector2Callback
		/// <summary>
		/// NoParamReturnArrayVector2Callback
		/// </summary>
		internal static Vector2[] NoParamReturnArrayVector2Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayVector2Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayVector2Callback();
		}

#region NoParamReturnArrayVector3Callback
		internal static delegate*<Vector3[]> _NoParamReturnArrayVector3Callback = &___NoParamReturnArrayVector3Callback;
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
#endregion NoParamReturnArrayVector3Callback
		/// <summary>
		/// NoParamReturnArrayVector3Callback
		/// </summary>
		internal static Vector3[] NoParamReturnArrayVector3Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayVector3Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayVector3Callback();
		}

#region NoParamReturnArrayVector4Callback
		internal static delegate*<Vector4[]> _NoParamReturnArrayVector4Callback = &___NoParamReturnArrayVector4Callback;
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
#endregion NoParamReturnArrayVector4Callback
		/// <summary>
		/// NoParamReturnArrayVector4Callback
		/// </summary>
		internal static Vector4[] NoParamReturnArrayVector4Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayVector4Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayVector4Callback();
		}

#region NoParamReturnArrayMatrix4x4Callback
		internal static delegate*<Matrix4x4[]> _NoParamReturnArrayMatrix4x4Callback = &___NoParamReturnArrayMatrix4x4Callback;
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
#endregion NoParamReturnArrayMatrix4x4Callback
		/// <summary>
		/// NoParamReturnArrayMatrix4x4Callback
		/// </summary>
		internal static Matrix4x4[] NoParamReturnArrayMatrix4x4Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnArrayMatrix4x4Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnArrayMatrix4x4Callback();
		}

#region NoParamReturnVector2Callback
		internal static delegate*<Vector2> _NoParamReturnVector2Callback = &___NoParamReturnVector2Callback;
		internal static delegate* unmanaged[Cdecl]<Vector2> __NoParamReturnVector2Callback;
		private static Vector2 ___NoParamReturnVector2Callback()
		{
			Vector2 __retVal = __NoParamReturnVector2Callback();
			return __retVal;
		}
#endregion NoParamReturnVector2Callback
		/// <summary>
		/// NoParamReturnVector2Callback
		/// </summary>
		internal static Vector2 NoParamReturnVector2Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnVector2Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnVector2Callback();
		}

#region NoParamReturnVector3Callback
		internal static delegate*<Vector3> _NoParamReturnVector3Callback = &___NoParamReturnVector3Callback;
		internal static delegate* unmanaged[Cdecl]<Vector3> __NoParamReturnVector3Callback;
		private static Vector3 ___NoParamReturnVector3Callback()
		{
			Vector3 __retVal = __NoParamReturnVector3Callback();
			return __retVal;
		}
#endregion NoParamReturnVector3Callback
		/// <summary>
		/// NoParamReturnVector3Callback
		/// </summary>
		internal static Vector3 NoParamReturnVector3Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnVector3Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnVector3Callback();
		}

#region NoParamReturnVector4Callback
		internal static delegate*<Vector4> _NoParamReturnVector4Callback = &___NoParamReturnVector4Callback;
		internal static delegate* unmanaged[Cdecl]<Vector4> __NoParamReturnVector4Callback;
		private static Vector4 ___NoParamReturnVector4Callback()
		{
			Vector4 __retVal = __NoParamReturnVector4Callback();
			return __retVal;
		}
#endregion NoParamReturnVector4Callback
		/// <summary>
		/// NoParamReturnVector4Callback
		/// </summary>
		internal static Vector4 NoParamReturnVector4Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnVector4Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnVector4Callback();
		}

#region NoParamReturnMatrix4x4Callback
		internal static delegate*<Matrix4x4> _NoParamReturnMatrix4x4Callback = &___NoParamReturnMatrix4x4Callback;
		internal static delegate* unmanaged[Cdecl]<Matrix4x4> __NoParamReturnMatrix4x4Callback;
		private static Matrix4x4 ___NoParamReturnMatrix4x4Callback()
		{
			Matrix4x4 __retVal = __NoParamReturnMatrix4x4Callback();
			return __retVal;
		}
#endregion NoParamReturnMatrix4x4Callback
		/// <summary>
		/// NoParamReturnMatrix4x4Callback
		/// </summary>
		internal static Matrix4x4 NoParamReturnMatrix4x4Callback([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::NoParamReturnMatrix4x4Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _NoParamReturnMatrix4x4Callback();
		}

#region Param1Callback
		internal static delegate*<int, void> _Param1Callback = &___Param1Callback;
		internal static delegate* unmanaged[Cdecl]<int, void> __Param1Callback;
		private static void ___Param1Callback(int a)
		{
			__Param1Callback(a);
		}
#endregion Param1Callback
		/// <summary>
		/// Param1Callback
		/// </summary>
		/// <param name="a">a</param>
		internal static void Param1Callback(int a, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::Param1Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_Param1Callback(a);
		}

#region Param2Callback
		internal static delegate*<int, float, void> _Param2Callback = &___Param2Callback;
		internal static delegate* unmanaged[Cdecl]<int, float, void> __Param2Callback;
		private static void ___Param2Callback(int a, float b)
		{
			__Param2Callback(a, b);
		}
#endregion Param2Callback
		/// <summary>
		/// Param2Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		internal static void Param2Callback(int a, float b, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::Param2Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_Param2Callback(a, b);
		}

#region Param3Callback
		internal static delegate*<int, float, double, void> _Param3Callback = &___Param3Callback;
		internal static delegate* unmanaged[Cdecl]<int, float, double, void> __Param3Callback;
		private static void ___Param3Callback(int a, float b, double c)
		{
			__Param3Callback(a, b, c);
		}
#endregion Param3Callback
		/// <summary>
		/// Param3Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		internal static void Param3Callback(int a, float b, double c, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::Param3Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_Param3Callback(a, b, c);
		}

#region Param4Callback
		internal static delegate*<int, float, double, Vector4, void> _Param4Callback = &___Param4Callback;
		internal static delegate* unmanaged[Cdecl]<int, float, double, Vector4*, void> __Param4Callback;
		private static void ___Param4Callback(int a, float b, double c, Vector4 d)
		{
			__Param4Callback(a, b, c, &d);
		}
#endregion Param4Callback
		/// <summary>
		/// Param4Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		internal static void Param4Callback(int a, float b, double c, Vector4 d, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::Param4Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_Param4Callback(a, b, c, d);
		}

#region Param5Callback
		internal static delegate*<int, float, double, Vector4, long[], void> _Param5Callback = &___Param5Callback;
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
#endregion Param5Callback
		/// <summary>
		/// Param5Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		/// <param name="e">e</param>
		internal static void Param5Callback(int a, float b, double c, Vector4 d, long[] e, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::Param5Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_Param5Callback(a, b, c, d, e);
		}

#region Param6Callback
		internal static delegate*<int, float, double, Vector4, long[], Char8, void> _Param6Callback = &___Param6Callback;
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
#endregion Param6Callback
		/// <summary>
		/// Param6Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		/// <param name="e">e</param>
		/// <param name="f">f</param>
		internal static void Param6Callback(int a, float b, double c, Vector4 d, long[] e, Char8 f, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::Param6Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_Param6Callback(a, b, c, d, e, f);
		}

#region Param7Callback
		internal static delegate*<int, float, double, Vector4, long[], Char8, string, void> _Param7Callback = &___Param7Callback;
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
#endregion Param7Callback
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
		internal static void Param7Callback(int a, float b, double c, Vector4 d, long[] e, Char8 f, string g, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::Param7Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_Param7Callback(a, b, c, d, e, f, g);
		}

#region Param8Callback
		internal static delegate*<int, float, double, Vector4, long[], Char8, string, Char16, void> _Param8Callback = &___Param8Callback;
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
#endregion Param8Callback
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
		internal static void Param8Callback(int a, float b, double c, Vector4 d, long[] e, Char8 f, string g, Char16 h, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::Param8Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_Param8Callback(a, b, c, d, e, f, g, h);
		}

#region Param9Callback
		internal static delegate*<int, float, double, Vector4, long[], Char8, string, Char16, short, void> _Param9Callback = &___Param9Callback;
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
#endregion Param9Callback
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
		internal static void Param9Callback(int a, float b, double c, Vector4 d, long[] e, Char8 f, string g, Char16 h, short k, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::Param9Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_Param9Callback(a, b, c, d, e, f, g, h, k);
		}

#region Param10Callback
		internal static delegate*<int, float, double, Vector4, long[], Char8, string, Char16, short, nint, void> _Param10Callback = &___Param10Callback;
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
#endregion Param10Callback
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
		internal static void Param10Callback(int a, float b, double c, Vector4 d, long[] e, Char8 f, string g, Char16 h, short k, nint l, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::Param10Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_Param10Callback(a, b, c, d, e, f, g, h, k, l);
		}

#region ParamRef1Callback
		internal static delegate*<ref int, void> _ParamRef1Callback = &___ParamRef1Callback;
		internal static delegate* unmanaged[Cdecl]<int*, void> __ParamRef1Callback;
		private static void ___ParamRef1Callback(ref int a)
		{
			fixed(int* __a = &a) {
			__ParamRef1Callback(__a);
			}
		}
#endregion ParamRef1Callback
		/// <summary>
		/// ParamRef1Callback
		/// </summary>
		/// <param name="a">a</param>
		internal static void ParamRef1Callback(ref int a, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ParamRef1Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_ParamRef1Callback(ref a);
		}

#region ParamRef2Callback
		internal static delegate*<ref int, ref float, void> _ParamRef2Callback = &___ParamRef2Callback;
		internal static delegate* unmanaged[Cdecl]<int*, float*, void> __ParamRef2Callback;
		private static void ___ParamRef2Callback(ref int a, ref float b)
		{
			fixed(int* __a = &a) {
			fixed(float* __b = &b) {
			__ParamRef2Callback(__a, __b);
			}
			}
		}
#endregion ParamRef2Callback
		/// <summary>
		/// ParamRef2Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		internal static void ParamRef2Callback(ref int a, ref float b, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ParamRef2Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_ParamRef2Callback(ref a, ref b);
		}

#region ParamRef3Callback
		internal static delegate*<ref int, ref float, ref double, void> _ParamRef3Callback = &___ParamRef3Callback;
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
#endregion ParamRef3Callback
		/// <summary>
		/// ParamRef3Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		internal static void ParamRef3Callback(ref int a, ref float b, ref double c, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ParamRef3Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_ParamRef3Callback(ref a, ref b, ref c);
		}

#region ParamRef4Callback
		internal static delegate*<ref int, ref float, ref double, ref Vector4, void> _ParamRef4Callback = &___ParamRef4Callback;
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
#endregion ParamRef4Callback
		/// <summary>
		/// ParamRef4Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		internal static void ParamRef4Callback(ref int a, ref float b, ref double c, ref Vector4 d, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ParamRef4Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_ParamRef4Callback(ref a, ref b, ref c, ref d);
		}

#region ParamRef5Callback
		internal static delegate*<ref int, ref float, ref double, ref Vector4, ref long[], void> _ParamRef5Callback = &___ParamRef5Callback;
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
#endregion ParamRef5Callback
		/// <summary>
		/// ParamRef5Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		/// <param name="e">e</param>
		internal static void ParamRef5Callback(ref int a, ref float b, ref double c, ref Vector4 d, ref long[] e, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ParamRef5Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_ParamRef5Callback(ref a, ref b, ref c, ref d, ref e);
		}

#region ParamRef6Callback
		internal static delegate*<ref int, ref float, ref double, ref Vector4, ref long[], ref Char8, void> _ParamRef6Callback = &___ParamRef6Callback;
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
#endregion ParamRef6Callback
		/// <summary>
		/// ParamRef6Callback
		/// </summary>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <param name="c">c</param>
		/// <param name="d">d</param>
		/// <param name="e">e</param>
		/// <param name="f">f</param>
		internal static void ParamRef6Callback(ref int a, ref float b, ref double c, ref Vector4 d, ref long[] e, ref Char8 f, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ParamRef6Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_ParamRef6Callback(ref a, ref b, ref c, ref d, ref e, ref f);
		}

#region ParamRef7Callback
		internal static delegate*<ref int, ref float, ref double, ref Vector4, ref long[], ref Char8, ref string, void> _ParamRef7Callback = &___ParamRef7Callback;
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
#endregion ParamRef7Callback
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
		internal static void ParamRef7Callback(ref int a, ref float b, ref double c, ref Vector4 d, ref long[] e, ref Char8 f, ref string g, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ParamRef7Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_ParamRef7Callback(ref a, ref b, ref c, ref d, ref e, ref f, ref g);
		}

#region ParamRef8Callback
		internal static delegate*<ref int, ref float, ref double, ref Vector4, ref long[], ref Char8, ref string, ref Char16, void> _ParamRef8Callback = &___ParamRef8Callback;
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
#endregion ParamRef8Callback
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
		internal static void ParamRef8Callback(ref int a, ref float b, ref double c, ref Vector4 d, ref long[] e, ref Char8 f, ref string g, ref Char16 h, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ParamRef8Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_ParamRef8Callback(ref a, ref b, ref c, ref d, ref e, ref f, ref g, ref h);
		}

#region ParamRef9Callback
		internal static delegate*<ref int, ref float, ref double, ref Vector4, ref long[], ref Char8, ref string, ref Char16, ref short, void> _ParamRef9Callback = &___ParamRef9Callback;
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
#endregion ParamRef9Callback
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
		internal static void ParamRef9Callback(ref int a, ref float b, ref double c, ref Vector4 d, ref long[] e, ref Char8 f, ref string g, ref Char16 h, ref short k, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ParamRef9Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_ParamRef9Callback(ref a, ref b, ref c, ref d, ref e, ref f, ref g, ref h, ref k);
		}

#region ParamRef10Callback
		internal static delegate*<ref int, ref float, ref double, ref Vector4, ref long[], ref Char8, ref string, ref Char16, ref short, ref nint, void> _ParamRef10Callback = &___ParamRef10Callback;
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
#endregion ParamRef10Callback
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
		internal static void ParamRef10Callback(ref int a, ref float b, ref double c, ref Vector4 d, ref long[] e, ref Char8 f, ref string g, ref Char16 h, ref short k, ref nint l, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ParamRef10Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_ParamRef10Callback(ref a, ref b, ref c, ref d, ref e, ref f, ref g, ref h, ref k, ref l);
		}

#region ParamRefVectorsCallback
		internal static delegate*<ref Bool8[], ref Char8[], ref Char16[], ref sbyte[], ref short[], ref int[], ref long[], ref byte[], ref ushort[], ref uint[], ref ulong[], ref nint[], ref float[], ref double[], ref string[], void> _ParamRefVectorsCallback = &___ParamRefVectorsCallback;
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
#endregion ParamRefVectorsCallback
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
		internal static void ParamRefVectorsCallback(ref Bool8[] p1, ref Char8[] p2, ref Char16[] p3, ref sbyte[] p4, ref short[] p5, ref int[] p6, ref long[] p7, ref byte[] p8, ref ushort[] p9, ref uint[] p10, ref ulong[] p11, ref nint[] p12, ref float[] p13, ref double[] p14, ref string[] p15, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ParamRefVectorsCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_ParamRefVectorsCallback(ref p1, ref p2, ref p3, ref p4, ref p5, ref p6, ref p7, ref p8, ref p9, ref p10, ref p11, ref p12, ref p13, ref p14, ref p15);
		}

#region ParamAllPrimitivesCallback
		internal static delegate*<Bool8, Char8, Char16, sbyte, short, int, long, byte, ushort, uint, ulong, nint, float, double, long> _ParamAllPrimitivesCallback = &___ParamAllPrimitivesCallback;
		internal static delegate* unmanaged[Cdecl]<Bool8, Char8, Char16, sbyte, short, int, long, byte, ushort, uint, ulong, nint, float, double, long> __ParamAllPrimitivesCallback;
		private static long ___ParamAllPrimitivesCallback(Bool8 p1, Char8 p2, Char16 p3, sbyte p4, short p5, int p6, long p7, byte p8, ushort p9, uint p10, ulong p11, nint p12, float p13, double p14)
		{
			long __retVal = __ParamAllPrimitivesCallback(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14);
			return __retVal;
		}
#endregion ParamAllPrimitivesCallback
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
		internal static long ParamAllPrimitivesCallback(Bool8 p1, Char8 p2, Char16 p3, sbyte p4, short p5, int p6, long p7, byte p8, ushort p9, uint p10, ulong p11, nint p12, float p13, double p14, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ParamAllPrimitivesCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _ParamAllPrimitivesCallback(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14);
		}

#region ParamAllAliasesCallback
		internal static delegate*<AliasBool, AliasChar8, AliasChar16, AliasInt8, AliasInt16, AliasInt32, AliasInt64, AliasPtr, AliasFloat, AliasDouble, AliasString, AliasAny, AliasVec2, AliasVec3, AliasVec4, AliasMat4x4, AliasBoolVector, AliasChar8Vector, AliasChar16Vector, AliasInt8Vector, AliasInt16Vector, AliasInt32Vector, AliasInt64Vector, AliasPtrVector, AliasFloatVector, AliasDoubleVector, AliasStringVector, AliasAnyVector, AliasVec2Vector, AliasVec3Vector, AliasVec4Vector, int> _ParamAllAliasesCallback = &___ParamAllAliasesCallback;
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
#endregion ParamAllAliasesCallback
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
		internal static int ParamAllAliasesCallback(AliasBool aBool, AliasChar8 aChar8, AliasChar16 aChar16, AliasInt8 aInt8, AliasInt16 aInt16, AliasInt32 aInt32, AliasInt64 aInt64, AliasPtr aPtr, AliasFloat aFloat, AliasDouble aDouble, AliasString aString, AliasAny aAny, AliasVec2 aVec2, AliasVec3 aVec3, AliasVec4 aVec4, AliasMat4x4 aMat4x4, AliasBoolVector aBoolVec, AliasChar8Vector aChar8Vec, AliasChar16Vector aChar16Vec, AliasInt8Vector aInt8Vec, AliasInt16Vector aInt16Vec, AliasInt32Vector aInt32Vec, AliasInt64Vector aInt64Vec, AliasPtrVector aPtrVec, AliasFloatVector aFloatVec, AliasDoubleVector aDoubleVec, AliasStringVector aStringVec, AliasAnyVector aAnyVec, AliasVec2Vector aVec2Vec, AliasVec3Vector aVec3Vec, AliasVec4Vector aVec4Vec, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ParamAllAliasesCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _ParamAllAliasesCallback(aBool, aChar8, aChar16, aInt8, aInt16, aInt32, aInt64, aPtr, aFloat, aDouble, aString, aAny, aVec2, aVec3, aVec4, aMat4x4, aBoolVec, aChar8Vec, aChar16Vec, aInt8Vec, aInt16Vec, aInt32Vec, aInt64Vec, aPtrVec, aFloatVec, aDoubleVec, aStringVec, aAnyVec, aVec2Vec, aVec3Vec, aVec4Vec);
		}

#region ParamAllRefAliasesCallback
		internal static delegate*<ref AliasBool, ref AliasChar8, ref AliasChar16, ref AliasInt8, ref AliasInt16, ref AliasInt32, ref AliasInt64, ref AliasPtr, ref AliasFloat, ref AliasDouble, ref AliasString, ref AliasAny, ref AliasVec2, ref AliasVec3, ref AliasVec4, ref AliasMat4x4, ref AliasBoolVector, ref AliasChar8Vector, ref AliasChar16Vector, ref AliasInt8Vector, ref AliasInt16Vector, ref AliasInt32Vector, ref AliasInt64Vector, ref AliasPtrVector, ref AliasFloatVector, ref AliasDoubleVector, ref AliasStringVector, ref AliasAnyVector, ref AliasVec2Vector, ref AliasVec3Vector, ref AliasVec4Vector, long> _ParamAllRefAliasesCallback = &___ParamAllRefAliasesCallback;
		internal static delegate* unmanaged[Cdecl]<Bool8*, Char8*, Char16*, sbyte*, short*, int*, long*, nint*, float*, double*, String192*, Variant256*, Vector2*, Vector3*, Vector4*, Matrix4x4*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, Vector192*, long> __ParamAllRefAliasesCallback;
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
#endregion ParamAllRefAliasesCallback
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
		internal static long ParamAllRefAliasesCallback(ref AliasBool aBool, ref AliasChar8 aChar8, ref AliasChar16 aChar16, ref AliasInt8 aInt8, ref AliasInt16 aInt16, ref AliasInt32 aInt32, ref AliasInt64 aInt64, ref AliasPtr aPtr, ref AliasFloat aFloat, ref AliasDouble aDouble, ref AliasString aString, ref AliasAny aAny, ref AliasVec2 aVec2, ref AliasVec3 aVec3, ref AliasVec4 aVec4, ref AliasMat4x4 aMat4x4, ref AliasBoolVector aBoolVec, ref AliasChar8Vector aChar8Vec, ref AliasChar16Vector aChar16Vec, ref AliasInt8Vector aInt8Vec, ref AliasInt16Vector aInt16Vec, ref AliasInt32Vector aInt32Vec, ref AliasInt64Vector aInt64Vec, ref AliasPtrVector aPtrVec, ref AliasFloatVector aFloatVec, ref AliasDoubleVector aDoubleVec, ref AliasStringVector aStringVec, ref AliasAnyVector aAnyVec, ref AliasVec2Vector aVec2Vec, ref AliasVec3Vector aVec3Vec, ref AliasVec4Vector aVec4Vec, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ParamAllRefAliasesCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _ParamAllRefAliasesCallback(ref aBool, ref aChar8, ref aChar16, ref aInt8, ref aInt16, ref aInt32, ref aInt64, ref aPtr, ref aFloat, ref aDouble, ref aString, ref aAny, ref aVec2, ref aVec3, ref aVec4, ref aMat4x4, ref aBoolVec, ref aChar8Vec, ref aChar16Vec, ref aInt8Vec, ref aInt16Vec, ref aInt32Vec, ref aInt64Vec, ref aPtrVec, ref aFloatVec, ref aDoubleVec, ref aStringVec, ref aAnyVec, ref aVec2Vec, ref aVec3Vec, ref aVec4Vec);
		}

#region ParamEnumCallback
		internal static delegate*<Example, Example[], int> _ParamEnumCallback = &___ParamEnumCallback;
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
#endregion ParamEnumCallback
		/// <summary>
		/// ParamEnumCallback
		/// </summary>
		/// <param name="p1">p1</param>
		/// <param name="p2">p2</param>
		internal static int ParamEnumCallback(Example p1, Example[] p2, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ParamEnumCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _ParamEnumCallback(p1, p2);
		}

#region ParamEnumRefCallback
		internal static delegate*<ref Example, ref Example[], int> _ParamEnumRefCallback = &___ParamEnumRefCallback;
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
#endregion ParamEnumRefCallback
		/// <summary>
		/// ParamEnumRefCallback
		/// </summary>
		/// <param name="p1">p1</param>
		/// <param name="p2">p2</param>
		internal static int ParamEnumRefCallback(ref Example p1, ref Example[] p2, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ParamEnumRefCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _ParamEnumRefCallback(ref p1, ref p2);
		}

#region ParamVariantCallback
		internal static delegate*<object, object[], void> _ParamVariantCallback = &___ParamVariantCallback;
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
#endregion ParamVariantCallback
		/// <summary>
		/// ParamVariantCallback
		/// </summary>
		/// <param name="p1">p1</param>
		/// <param name="p2">p2</param>
		internal static void ParamVariantCallback(object p1, object[] p2, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ParamVariantCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_ParamVariantCallback(p1, p2);
		}

#region ParamVariantRefCallback
		internal static delegate*<ref object, ref object[], void> _ParamVariantRefCallback = &___ParamVariantRefCallback;
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
#endregion ParamVariantRefCallback
		/// <summary>
		/// ParamVariantRefCallback
		/// </summary>
		/// <param name="p1">p1</param>
		/// <param name="p2">p2</param>
		internal static void ParamVariantRefCallback(ref object p1, ref object[] p2, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ParamVariantRefCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_ParamVariantRefCallback(ref p1, ref p2);
		}

#region CallFuncVoidCallback
		internal static delegate*<FuncVoid, void> _CallFuncVoidCallback = &___CallFuncVoidCallback;
		internal static delegate* unmanaged[Cdecl]<nint, void> __CallFuncVoidCallback;
		private static void ___CallFuncVoidCallback(FuncVoid func)
		{
			__CallFuncVoidCallback(Marshalling.GetFunctionPointerForDelegate(func));
		}
#endregion CallFuncVoidCallback
		/// <summary>
		/// CallFuncVoidCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static void CallFuncVoidCallback(FuncVoid func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncVoidCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_CallFuncVoidCallback(func);
		}

#region CallFuncBoolCallback
		internal static delegate*<FuncBool, Bool8> _CallFuncBoolCallback = &___CallFuncBoolCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Bool8> __CallFuncBoolCallback;
		private static Bool8 ___CallFuncBoolCallback(FuncBool func)
		{
			Bool8 __retVal = __CallFuncBoolCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncBoolCallback
		/// <summary>
		/// CallFuncBoolCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static Bool8 CallFuncBoolCallback(FuncBool func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncBoolCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncBoolCallback(func);
		}

#region CallFuncChar8Callback
		internal static delegate*<FuncChar8, Char8> _CallFuncChar8Callback = &___CallFuncChar8Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Char8> __CallFuncChar8Callback;
		private static Char8 ___CallFuncChar8Callback(FuncChar8 func)
		{
			Char8 __retVal = __CallFuncChar8Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncChar8Callback
		/// <summary>
		/// CallFuncChar8Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static Char8 CallFuncChar8Callback(FuncChar8 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncChar8Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncChar8Callback(func);
		}

#region CallFuncChar16Callback
		internal static delegate*<FuncChar16, Char16> _CallFuncChar16Callback = &___CallFuncChar16Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Char16> __CallFuncChar16Callback;
		private static Char16 ___CallFuncChar16Callback(FuncChar16 func)
		{
			Char16 __retVal = __CallFuncChar16Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncChar16Callback
		/// <summary>
		/// CallFuncChar16Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static Char16 CallFuncChar16Callback(FuncChar16 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncChar16Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncChar16Callback(func);
		}

#region CallFuncInt8Callback
		internal static delegate*<FuncInt8, sbyte> _CallFuncInt8Callback = &___CallFuncInt8Callback;
		internal static delegate* unmanaged[Cdecl]<nint, sbyte> __CallFuncInt8Callback;
		private static sbyte ___CallFuncInt8Callback(FuncInt8 func)
		{
			sbyte __retVal = __CallFuncInt8Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncInt8Callback
		/// <summary>
		/// CallFuncInt8Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static sbyte CallFuncInt8Callback(FuncInt8 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncInt8Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncInt8Callback(func);
		}

#region CallFuncInt16Callback
		internal static delegate*<FuncInt16, short> _CallFuncInt16Callback = &___CallFuncInt16Callback;
		internal static delegate* unmanaged[Cdecl]<nint, short> __CallFuncInt16Callback;
		private static short ___CallFuncInt16Callback(FuncInt16 func)
		{
			short __retVal = __CallFuncInt16Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncInt16Callback
		/// <summary>
		/// CallFuncInt16Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static short CallFuncInt16Callback(FuncInt16 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncInt16Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncInt16Callback(func);
		}

#region CallFuncInt32Callback
		internal static delegate*<FuncInt32, int> _CallFuncInt32Callback = &___CallFuncInt32Callback;
		internal static delegate* unmanaged[Cdecl]<nint, int> __CallFuncInt32Callback;
		private static int ___CallFuncInt32Callback(FuncInt32 func)
		{
			int __retVal = __CallFuncInt32Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncInt32Callback
		/// <summary>
		/// CallFuncInt32Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static int CallFuncInt32Callback(FuncInt32 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncInt32Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncInt32Callback(func);
		}

#region CallFuncInt64Callback
		internal static delegate*<FuncInt64, long> _CallFuncInt64Callback = &___CallFuncInt64Callback;
		internal static delegate* unmanaged[Cdecl]<nint, long> __CallFuncInt64Callback;
		private static long ___CallFuncInt64Callback(FuncInt64 func)
		{
			long __retVal = __CallFuncInt64Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncInt64Callback
		/// <summary>
		/// CallFuncInt64Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static long CallFuncInt64Callback(FuncInt64 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncInt64Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncInt64Callback(func);
		}

#region CallFuncUInt8Callback
		internal static delegate*<FuncUInt8, byte> _CallFuncUInt8Callback = &___CallFuncUInt8Callback;
		internal static delegate* unmanaged[Cdecl]<nint, byte> __CallFuncUInt8Callback;
		private static byte ___CallFuncUInt8Callback(FuncUInt8 func)
		{
			byte __retVal = __CallFuncUInt8Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncUInt8Callback
		/// <summary>
		/// CallFuncUInt8Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static byte CallFuncUInt8Callback(FuncUInt8 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncUInt8Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncUInt8Callback(func);
		}

#region CallFuncUInt16Callback
		internal static delegate*<FuncUInt16, ushort> _CallFuncUInt16Callback = &___CallFuncUInt16Callback;
		internal static delegate* unmanaged[Cdecl]<nint, ushort> __CallFuncUInt16Callback;
		private static ushort ___CallFuncUInt16Callback(FuncUInt16 func)
		{
			ushort __retVal = __CallFuncUInt16Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncUInt16Callback
		/// <summary>
		/// CallFuncUInt16Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static ushort CallFuncUInt16Callback(FuncUInt16 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncUInt16Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncUInt16Callback(func);
		}

#region CallFuncUInt32Callback
		internal static delegate*<FuncUInt32, uint> _CallFuncUInt32Callback = &___CallFuncUInt32Callback;
		internal static delegate* unmanaged[Cdecl]<nint, uint> __CallFuncUInt32Callback;
		private static uint ___CallFuncUInt32Callback(FuncUInt32 func)
		{
			uint __retVal = __CallFuncUInt32Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncUInt32Callback
		/// <summary>
		/// CallFuncUInt32Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static uint CallFuncUInt32Callback(FuncUInt32 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncUInt32Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncUInt32Callback(func);
		}

#region CallFuncUInt64Callback
		internal static delegate*<FuncUInt64, ulong> _CallFuncUInt64Callback = &___CallFuncUInt64Callback;
		internal static delegate* unmanaged[Cdecl]<nint, ulong> __CallFuncUInt64Callback;
		private static ulong ___CallFuncUInt64Callback(FuncUInt64 func)
		{
			ulong __retVal = __CallFuncUInt64Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncUInt64Callback
		/// <summary>
		/// CallFuncUInt64Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static ulong CallFuncUInt64Callback(FuncUInt64 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncUInt64Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncUInt64Callback(func);
		}

#region CallFuncPtrCallback
		internal static delegate*<FuncPtr, nint> _CallFuncPtrCallback = &___CallFuncPtrCallback;
		internal static delegate* unmanaged[Cdecl]<nint, nint> __CallFuncPtrCallback;
		private static nint ___CallFuncPtrCallback(FuncPtr func)
		{
			nint __retVal = __CallFuncPtrCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncPtrCallback
		/// <summary>
		/// CallFuncPtrCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static nint CallFuncPtrCallback(FuncPtr func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncPtrCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncPtrCallback(func);
		}

#region CallFuncFloatCallback
		internal static delegate*<FuncFloat, float> _CallFuncFloatCallback = &___CallFuncFloatCallback;
		internal static delegate* unmanaged[Cdecl]<nint, float> __CallFuncFloatCallback;
		private static float ___CallFuncFloatCallback(FuncFloat func)
		{
			float __retVal = __CallFuncFloatCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncFloatCallback
		/// <summary>
		/// CallFuncFloatCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static float CallFuncFloatCallback(FuncFloat func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncFloatCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncFloatCallback(func);
		}

#region CallFuncDoubleCallback
		internal static delegate*<FuncDouble, double> _CallFuncDoubleCallback = &___CallFuncDoubleCallback;
		internal static delegate* unmanaged[Cdecl]<nint, double> __CallFuncDoubleCallback;
		private static double ___CallFuncDoubleCallback(FuncDouble func)
		{
			double __retVal = __CallFuncDoubleCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncDoubleCallback
		/// <summary>
		/// CallFuncDoubleCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static double CallFuncDoubleCallback(FuncDouble func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncDoubleCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncDoubleCallback(func);
		}

#region CallFuncStringCallback
		internal static delegate*<FuncString, string> _CallFuncStringCallback = &___CallFuncStringCallback;
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
#endregion CallFuncStringCallback
		/// <summary>
		/// CallFuncStringCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFuncStringCallback(FuncString func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncStringCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncStringCallback(func);
		}

#region CallFuncAnyCallback
		internal static delegate*<FuncAny, object> _CallFuncAnyCallback = &___CallFuncAnyCallback;
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
#endregion CallFuncAnyCallback
		/// <summary>
		/// CallFuncAnyCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static object CallFuncAnyCallback(FuncAny func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAnyCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAnyCallback(func);
		}

#region CallFuncFunctionCallback
		internal static delegate*<FuncFunction, nint> _CallFuncFunctionCallback = &___CallFuncFunctionCallback;
		internal static delegate* unmanaged[Cdecl]<nint, nint> __CallFuncFunctionCallback;
		private static nint ___CallFuncFunctionCallback(FuncFunction func)
		{
			nint __retVal = __CallFuncFunctionCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncFunctionCallback
		/// <summary>
		/// CallFuncFunctionCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static nint CallFuncFunctionCallback(FuncFunction func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncFunctionCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncFunctionCallback(func);
		}

#region CallFuncBoolVectorCallback
		internal static delegate*<FuncBoolVector, Bool8[]> _CallFuncBoolVectorCallback = &___CallFuncBoolVectorCallback;
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
#endregion CallFuncBoolVectorCallback
		/// <summary>
		/// CallFuncBoolVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static Bool8[] CallFuncBoolVectorCallback(FuncBoolVector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncBoolVectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncBoolVectorCallback(func);
		}

#region CallFuncChar8VectorCallback
		internal static delegate*<FuncChar8Vector, Char8[]> _CallFuncChar8VectorCallback = &___CallFuncChar8VectorCallback;
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
#endregion CallFuncChar8VectorCallback
		/// <summary>
		/// CallFuncChar8VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static Char8[] CallFuncChar8VectorCallback(FuncChar8Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncChar8VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncChar8VectorCallback(func);
		}

#region CallFuncChar16VectorCallback
		internal static delegate*<FuncChar16Vector, Char16[]> _CallFuncChar16VectorCallback = &___CallFuncChar16VectorCallback;
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
#endregion CallFuncChar16VectorCallback
		/// <summary>
		/// CallFuncChar16VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static Char16[] CallFuncChar16VectorCallback(FuncChar16Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncChar16VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncChar16VectorCallback(func);
		}

#region CallFuncInt8VectorCallback
		internal static delegate*<FuncInt8Vector, sbyte[]> _CallFuncInt8VectorCallback = &___CallFuncInt8VectorCallback;
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
#endregion CallFuncInt8VectorCallback
		/// <summary>
		/// CallFuncInt8VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static sbyte[] CallFuncInt8VectorCallback(FuncInt8Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncInt8VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncInt8VectorCallback(func);
		}

#region CallFuncInt16VectorCallback
		internal static delegate*<FuncInt16Vector, short[]> _CallFuncInt16VectorCallback = &___CallFuncInt16VectorCallback;
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
#endregion CallFuncInt16VectorCallback
		/// <summary>
		/// CallFuncInt16VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static short[] CallFuncInt16VectorCallback(FuncInt16Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncInt16VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncInt16VectorCallback(func);
		}

#region CallFuncInt32VectorCallback
		internal static delegate*<FuncInt32Vector, int[]> _CallFuncInt32VectorCallback = &___CallFuncInt32VectorCallback;
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
#endregion CallFuncInt32VectorCallback
		/// <summary>
		/// CallFuncInt32VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static int[] CallFuncInt32VectorCallback(FuncInt32Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncInt32VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncInt32VectorCallback(func);
		}

#region CallFuncInt64VectorCallback
		internal static delegate*<FuncInt64Vector, long[]> _CallFuncInt64VectorCallback = &___CallFuncInt64VectorCallback;
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
#endregion CallFuncInt64VectorCallback
		/// <summary>
		/// CallFuncInt64VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static long[] CallFuncInt64VectorCallback(FuncInt64Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncInt64VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncInt64VectorCallback(func);
		}

#region CallFuncUInt8VectorCallback
		internal static delegate*<FuncUInt8Vector, byte[]> _CallFuncUInt8VectorCallback = &___CallFuncUInt8VectorCallback;
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
#endregion CallFuncUInt8VectorCallback
		/// <summary>
		/// CallFuncUInt8VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static byte[] CallFuncUInt8VectorCallback(FuncUInt8Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncUInt8VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncUInt8VectorCallback(func);
		}

#region CallFuncUInt16VectorCallback
		internal static delegate*<FuncUInt16Vector, ushort[]> _CallFuncUInt16VectorCallback = &___CallFuncUInt16VectorCallback;
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
#endregion CallFuncUInt16VectorCallback
		/// <summary>
		/// CallFuncUInt16VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static ushort[] CallFuncUInt16VectorCallback(FuncUInt16Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncUInt16VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncUInt16VectorCallback(func);
		}

#region CallFuncUInt32VectorCallback
		internal static delegate*<FuncUInt32Vector, uint[]> _CallFuncUInt32VectorCallback = &___CallFuncUInt32VectorCallback;
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
#endregion CallFuncUInt32VectorCallback
		/// <summary>
		/// CallFuncUInt32VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static uint[] CallFuncUInt32VectorCallback(FuncUInt32Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncUInt32VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncUInt32VectorCallback(func);
		}

#region CallFuncUInt64VectorCallback
		internal static delegate*<FuncUInt64Vector, ulong[]> _CallFuncUInt64VectorCallback = &___CallFuncUInt64VectorCallback;
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
#endregion CallFuncUInt64VectorCallback
		/// <summary>
		/// CallFuncUInt64VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static ulong[] CallFuncUInt64VectorCallback(FuncUInt64Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncUInt64VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncUInt64VectorCallback(func);
		}

#region CallFuncPtrVectorCallback
		internal static delegate*<FuncPtrVector, nint[]> _CallFuncPtrVectorCallback = &___CallFuncPtrVectorCallback;
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
#endregion CallFuncPtrVectorCallback
		/// <summary>
		/// CallFuncPtrVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static nint[] CallFuncPtrVectorCallback(FuncPtrVector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncPtrVectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncPtrVectorCallback(func);
		}

#region CallFuncFloatVectorCallback
		internal static delegate*<FuncFloatVector, float[]> _CallFuncFloatVectorCallback = &___CallFuncFloatVectorCallback;
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
#endregion CallFuncFloatVectorCallback
		/// <summary>
		/// CallFuncFloatVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static float[] CallFuncFloatVectorCallback(FuncFloatVector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncFloatVectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncFloatVectorCallback(func);
		}

#region CallFuncDoubleVectorCallback
		internal static delegate*<FuncDoubleVector, double[]> _CallFuncDoubleVectorCallback = &___CallFuncDoubleVectorCallback;
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
#endregion CallFuncDoubleVectorCallback
		/// <summary>
		/// CallFuncDoubleVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static double[] CallFuncDoubleVectorCallback(FuncDoubleVector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncDoubleVectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncDoubleVectorCallback(func);
		}

#region CallFuncStringVectorCallback
		internal static delegate*<FuncStringVector, string[]> _CallFuncStringVectorCallback = &___CallFuncStringVectorCallback;
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
#endregion CallFuncStringVectorCallback
		/// <summary>
		/// CallFuncStringVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static string[] CallFuncStringVectorCallback(FuncStringVector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncStringVectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncStringVectorCallback(func);
		}

#region CallFuncAnyVectorCallback
		internal static delegate*<FuncAnyVector, object[]> _CallFuncAnyVectorCallback = &___CallFuncAnyVectorCallback;
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
#endregion CallFuncAnyVectorCallback
		/// <summary>
		/// CallFuncAnyVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static object[] CallFuncAnyVectorCallback(FuncAnyVector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAnyVectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAnyVectorCallback(func);
		}

#region CallFuncVec2VectorCallback
		internal static delegate*<FuncVec2Vector, Vector2[]> _CallFuncVec2VectorCallback = &___CallFuncVec2VectorCallback;
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
#endregion CallFuncVec2VectorCallback
		/// <summary>
		/// CallFuncVec2VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static Vector2[] CallFuncVec2VectorCallback(FuncVec2Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncVec2VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncVec2VectorCallback(func);
		}

#region CallFuncVec3VectorCallback
		internal static delegate*<FuncVec3Vector, Vector3[]> _CallFuncVec3VectorCallback = &___CallFuncVec3VectorCallback;
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
#endregion CallFuncVec3VectorCallback
		/// <summary>
		/// CallFuncVec3VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static Vector3[] CallFuncVec3VectorCallback(FuncVec3Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncVec3VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncVec3VectorCallback(func);
		}

#region CallFuncVec4VectorCallback
		internal static delegate*<FuncVec4Vector, Vector4[]> _CallFuncVec4VectorCallback = &___CallFuncVec4VectorCallback;
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
#endregion CallFuncVec4VectorCallback
		/// <summary>
		/// CallFuncVec4VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static Vector4[] CallFuncVec4VectorCallback(FuncVec4Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncVec4VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncVec4VectorCallback(func);
		}

#region CallFuncMat4x4VectorCallback
		internal static delegate*<FuncMat4x4Vector, Matrix4x4[]> _CallFuncMat4x4VectorCallback = &___CallFuncMat4x4VectorCallback;
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
#endregion CallFuncMat4x4VectorCallback
		/// <summary>
		/// CallFuncMat4x4VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static Matrix4x4[] CallFuncMat4x4VectorCallback(FuncMat4x4Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncMat4x4VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncMat4x4VectorCallback(func);
		}

#region CallFuncVec2Callback
		internal static delegate*<FuncVec2, Vector2> _CallFuncVec2Callback = &___CallFuncVec2Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector2> __CallFuncVec2Callback;
		private static Vector2 ___CallFuncVec2Callback(FuncVec2 func)
		{
			Vector2 __retVal = __CallFuncVec2Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncVec2Callback
		/// <summary>
		/// CallFuncVec2Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static Vector2 CallFuncVec2Callback(FuncVec2 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncVec2Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncVec2Callback(func);
		}

#region CallFuncVec3Callback
		internal static delegate*<FuncVec3, Vector3> _CallFuncVec3Callback = &___CallFuncVec3Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector3> __CallFuncVec3Callback;
		private static Vector3 ___CallFuncVec3Callback(FuncVec3 func)
		{
			Vector3 __retVal = __CallFuncVec3Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncVec3Callback
		/// <summary>
		/// CallFuncVec3Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static Vector3 CallFuncVec3Callback(FuncVec3 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncVec3Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncVec3Callback(func);
		}

#region CallFuncVec4Callback
		internal static delegate*<FuncVec4, Vector4> _CallFuncVec4Callback = &___CallFuncVec4Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector4> __CallFuncVec4Callback;
		private static Vector4 ___CallFuncVec4Callback(FuncVec4 func)
		{
			Vector4 __retVal = __CallFuncVec4Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncVec4Callback
		/// <summary>
		/// CallFuncVec4Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static Vector4 CallFuncVec4Callback(FuncVec4 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncVec4Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncVec4Callback(func);
		}

#region CallFuncMat4x4Callback
		internal static delegate*<FuncMat4x4, Matrix4x4> _CallFuncMat4x4Callback = &___CallFuncMat4x4Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Matrix4x4> __CallFuncMat4x4Callback;
		private static Matrix4x4 ___CallFuncMat4x4Callback(FuncMat4x4 func)
		{
			Matrix4x4 __retVal = __CallFuncMat4x4Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncMat4x4Callback
		/// <summary>
		/// CallFuncMat4x4Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static Matrix4x4 CallFuncMat4x4Callback(FuncMat4x4 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncMat4x4Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncMat4x4Callback(func);
		}

#region CallFuncAliasBoolCallback
		internal static delegate*<FuncAliasBool, AliasBool> _CallFuncAliasBoolCallback = &___CallFuncAliasBoolCallback;
		internal static delegate* unmanaged[Cdecl]<nint, Bool8> __CallFuncAliasBoolCallback;
		private static AliasBool ___CallFuncAliasBoolCallback(FuncAliasBool func)
		{
			AliasBool __retVal = __CallFuncAliasBoolCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncAliasBoolCallback
		/// <summary>
		/// CallFuncAliasBoolCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasBool CallFuncAliasBoolCallback(FuncAliasBool func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasBoolCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasBoolCallback(func);
		}

#region CallFuncAliasChar8Callback
		internal static delegate*<FuncAliasChar8, AliasChar8> _CallFuncAliasChar8Callback = &___CallFuncAliasChar8Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Char8> __CallFuncAliasChar8Callback;
		private static AliasChar8 ___CallFuncAliasChar8Callback(FuncAliasChar8 func)
		{
			AliasChar8 __retVal = __CallFuncAliasChar8Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncAliasChar8Callback
		/// <summary>
		/// CallFuncAliasChar8Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasChar8 CallFuncAliasChar8Callback(FuncAliasChar8 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasChar8Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasChar8Callback(func);
		}

#region CallFuncAliasChar16Callback
		internal static delegate*<FuncAliasChar16, AliasChar16> _CallFuncAliasChar16Callback = &___CallFuncAliasChar16Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Char16> __CallFuncAliasChar16Callback;
		private static AliasChar16 ___CallFuncAliasChar16Callback(FuncAliasChar16 func)
		{
			AliasChar16 __retVal = __CallFuncAliasChar16Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncAliasChar16Callback
		/// <summary>
		/// CallFuncAliasChar16Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasChar16 CallFuncAliasChar16Callback(FuncAliasChar16 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasChar16Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasChar16Callback(func);
		}

#region CallFuncAliasInt8Callback
		internal static delegate*<FuncAliasInt8, AliasInt8> _CallFuncAliasInt8Callback = &___CallFuncAliasInt8Callback;
		internal static delegate* unmanaged[Cdecl]<nint, sbyte> __CallFuncAliasInt8Callback;
		private static AliasInt8 ___CallFuncAliasInt8Callback(FuncAliasInt8 func)
		{
			AliasInt8 __retVal = __CallFuncAliasInt8Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncAliasInt8Callback
		/// <summary>
		/// CallFuncAliasInt8Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasInt8 CallFuncAliasInt8Callback(FuncAliasInt8 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasInt8Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasInt8Callback(func);
		}

#region CallFuncAliasInt16Callback
		internal static delegate*<FuncAliasInt16, AliasInt16> _CallFuncAliasInt16Callback = &___CallFuncAliasInt16Callback;
		internal static delegate* unmanaged[Cdecl]<nint, short> __CallFuncAliasInt16Callback;
		private static AliasInt16 ___CallFuncAliasInt16Callback(FuncAliasInt16 func)
		{
			AliasInt16 __retVal = __CallFuncAliasInt16Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncAliasInt16Callback
		/// <summary>
		/// CallFuncAliasInt16Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasInt16 CallFuncAliasInt16Callback(FuncAliasInt16 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasInt16Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasInt16Callback(func);
		}

#region CallFuncAliasInt32Callback
		internal static delegate*<FuncAliasInt32, AliasInt32> _CallFuncAliasInt32Callback = &___CallFuncAliasInt32Callback;
		internal static delegate* unmanaged[Cdecl]<nint, int> __CallFuncAliasInt32Callback;
		private static AliasInt32 ___CallFuncAliasInt32Callback(FuncAliasInt32 func)
		{
			AliasInt32 __retVal = __CallFuncAliasInt32Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncAliasInt32Callback
		/// <summary>
		/// CallFuncAliasInt32Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasInt32 CallFuncAliasInt32Callback(FuncAliasInt32 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasInt32Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasInt32Callback(func);
		}

#region CallFuncAliasInt64Callback
		internal static delegate*<FuncAliasInt64, AliasInt64> _CallFuncAliasInt64Callback = &___CallFuncAliasInt64Callback;
		internal static delegate* unmanaged[Cdecl]<nint, long> __CallFuncAliasInt64Callback;
		private static AliasInt64 ___CallFuncAliasInt64Callback(FuncAliasInt64 func)
		{
			AliasInt64 __retVal = __CallFuncAliasInt64Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncAliasInt64Callback
		/// <summary>
		/// CallFuncAliasInt64Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasInt64 CallFuncAliasInt64Callback(FuncAliasInt64 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasInt64Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasInt64Callback(func);
		}

#region CallFuncAliasUInt8Callback
		internal static delegate*<FuncAliasUInt8, AliasUInt8> _CallFuncAliasUInt8Callback = &___CallFuncAliasUInt8Callback;
		internal static delegate* unmanaged[Cdecl]<nint, byte> __CallFuncAliasUInt8Callback;
		private static AliasUInt8 ___CallFuncAliasUInt8Callback(FuncAliasUInt8 func)
		{
			AliasUInt8 __retVal = __CallFuncAliasUInt8Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncAliasUInt8Callback
		/// <summary>
		/// CallFuncAliasUInt8Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasUInt8 CallFuncAliasUInt8Callback(FuncAliasUInt8 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasUInt8Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasUInt8Callback(func);
		}

#region CallFuncAliasUInt16Callback
		internal static delegate*<FuncAliasUInt16, AliasUInt16> _CallFuncAliasUInt16Callback = &___CallFuncAliasUInt16Callback;
		internal static delegate* unmanaged[Cdecl]<nint, ushort> __CallFuncAliasUInt16Callback;
		private static AliasUInt16 ___CallFuncAliasUInt16Callback(FuncAliasUInt16 func)
		{
			AliasUInt16 __retVal = __CallFuncAliasUInt16Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncAliasUInt16Callback
		/// <summary>
		/// CallFuncAliasUInt16Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasUInt16 CallFuncAliasUInt16Callback(FuncAliasUInt16 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasUInt16Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasUInt16Callback(func);
		}

#region CallFuncAliasUInt32Callback
		internal static delegate*<FuncAliasUInt32, AliasUInt32> _CallFuncAliasUInt32Callback = &___CallFuncAliasUInt32Callback;
		internal static delegate* unmanaged[Cdecl]<nint, uint> __CallFuncAliasUInt32Callback;
		private static AliasUInt32 ___CallFuncAliasUInt32Callback(FuncAliasUInt32 func)
		{
			AliasUInt32 __retVal = __CallFuncAliasUInt32Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncAliasUInt32Callback
		/// <summary>
		/// CallFuncAliasUInt32Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasUInt32 CallFuncAliasUInt32Callback(FuncAliasUInt32 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasUInt32Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasUInt32Callback(func);
		}

#region CallFuncAliasUInt64Callback
		internal static delegate*<FuncAliasUInt64, AliasUInt64> _CallFuncAliasUInt64Callback = &___CallFuncAliasUInt64Callback;
		internal static delegate* unmanaged[Cdecl]<nint, ulong> __CallFuncAliasUInt64Callback;
		private static AliasUInt64 ___CallFuncAliasUInt64Callback(FuncAliasUInt64 func)
		{
			AliasUInt64 __retVal = __CallFuncAliasUInt64Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncAliasUInt64Callback
		/// <summary>
		/// CallFuncAliasUInt64Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasUInt64 CallFuncAliasUInt64Callback(FuncAliasUInt64 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasUInt64Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasUInt64Callback(func);
		}

#region CallFuncAliasPtrCallback
		internal static delegate*<FuncAliasPtr, AliasPtr> _CallFuncAliasPtrCallback = &___CallFuncAliasPtrCallback;
		internal static delegate* unmanaged[Cdecl]<nint, nint> __CallFuncAliasPtrCallback;
		private static AliasPtr ___CallFuncAliasPtrCallback(FuncAliasPtr func)
		{
			AliasPtr __retVal = __CallFuncAliasPtrCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncAliasPtrCallback
		/// <summary>
		/// CallFuncAliasPtrCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasPtr CallFuncAliasPtrCallback(FuncAliasPtr func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasPtrCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasPtrCallback(func);
		}

#region CallFuncAliasFloatCallback
		internal static delegate*<FuncAliasFloat, AliasFloat> _CallFuncAliasFloatCallback = &___CallFuncAliasFloatCallback;
		internal static delegate* unmanaged[Cdecl]<nint, float> __CallFuncAliasFloatCallback;
		private static AliasFloat ___CallFuncAliasFloatCallback(FuncAliasFloat func)
		{
			AliasFloat __retVal = __CallFuncAliasFloatCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncAliasFloatCallback
		/// <summary>
		/// CallFuncAliasFloatCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasFloat CallFuncAliasFloatCallback(FuncAliasFloat func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasFloatCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasFloatCallback(func);
		}

#region CallFuncAliasDoubleCallback
		internal static delegate*<FuncAliasDouble, AliasDouble> _CallFuncAliasDoubleCallback = &___CallFuncAliasDoubleCallback;
		internal static delegate* unmanaged[Cdecl]<nint, double> __CallFuncAliasDoubleCallback;
		private static AliasDouble ___CallFuncAliasDoubleCallback(FuncAliasDouble func)
		{
			AliasDouble __retVal = __CallFuncAliasDoubleCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncAliasDoubleCallback
		/// <summary>
		/// CallFuncAliasDoubleCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasDouble CallFuncAliasDoubleCallback(FuncAliasDouble func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasDoubleCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasDoubleCallback(func);
		}

#region CallFuncAliasStringCallback
		internal static delegate*<FuncAliasString, AliasString> _CallFuncAliasStringCallback = &___CallFuncAliasStringCallback;
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
#endregion CallFuncAliasStringCallback
		/// <summary>
		/// CallFuncAliasStringCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasString CallFuncAliasStringCallback(FuncAliasString func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasStringCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasStringCallback(func);
		}

#region CallFuncAliasAnyCallback
		internal static delegate*<FuncAliasAny, AliasAny> _CallFuncAliasAnyCallback = &___CallFuncAliasAnyCallback;
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
#endregion CallFuncAliasAnyCallback
		/// <summary>
		/// CallFuncAliasAnyCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasAny CallFuncAliasAnyCallback(FuncAliasAny func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasAnyCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasAnyCallback(func);
		}

#region CallFuncAliasFunctionCallback
		internal static delegate*<FuncAliasFunction, AliasFunction> _CallFuncAliasFunctionCallback = &___CallFuncAliasFunctionCallback;
		internal static delegate* unmanaged[Cdecl]<nint, nint> __CallFuncAliasFunctionCallback;
		private static AliasFunction ___CallFuncAliasFunctionCallback(FuncAliasFunction func)
		{
			AliasFunction __retVal = __CallFuncAliasFunctionCallback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncAliasFunctionCallback
		/// <summary>
		/// CallFuncAliasFunctionCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasFunction CallFuncAliasFunctionCallback(FuncAliasFunction func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasFunctionCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasFunctionCallback(func);
		}

#region CallFuncAliasBoolVectorCallback
		internal static delegate*<FuncAliasBoolVector, AliasBoolVector> _CallFuncAliasBoolVectorCallback = &___CallFuncAliasBoolVectorCallback;
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
#endregion CallFuncAliasBoolVectorCallback
		/// <summary>
		/// CallFuncAliasBoolVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasBoolVector CallFuncAliasBoolVectorCallback(FuncAliasBoolVector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasBoolVectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasBoolVectorCallback(func);
		}

#region CallFuncAliasChar8VectorCallback
		internal static delegate*<FuncAliasChar8Vector, AliasChar8Vector> _CallFuncAliasChar8VectorCallback = &___CallFuncAliasChar8VectorCallback;
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
#endregion CallFuncAliasChar8VectorCallback
		/// <summary>
		/// CallFuncAliasChar8VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasChar8Vector CallFuncAliasChar8VectorCallback(FuncAliasChar8Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasChar8VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasChar8VectorCallback(func);
		}

#region CallFuncAliasChar16VectorCallback
		internal static delegate*<FuncAliasChar16Vector, AliasChar16Vector> _CallFuncAliasChar16VectorCallback = &___CallFuncAliasChar16VectorCallback;
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
#endregion CallFuncAliasChar16VectorCallback
		/// <summary>
		/// CallFuncAliasChar16VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasChar16Vector CallFuncAliasChar16VectorCallback(FuncAliasChar16Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasChar16VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasChar16VectorCallback(func);
		}

#region CallFuncAliasInt8VectorCallback
		internal static delegate*<FuncAliasInt8Vector, AliasInt8Vector> _CallFuncAliasInt8VectorCallback = &___CallFuncAliasInt8VectorCallback;
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
#endregion CallFuncAliasInt8VectorCallback
		/// <summary>
		/// CallFuncAliasInt8VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasInt8Vector CallFuncAliasInt8VectorCallback(FuncAliasInt8Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasInt8VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasInt8VectorCallback(func);
		}

#region CallFuncAliasInt16VectorCallback
		internal static delegate*<FuncAliasInt16Vector, AliasInt16Vector> _CallFuncAliasInt16VectorCallback = &___CallFuncAliasInt16VectorCallback;
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
#endregion CallFuncAliasInt16VectorCallback
		/// <summary>
		/// CallFuncAliasInt16VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasInt16Vector CallFuncAliasInt16VectorCallback(FuncAliasInt16Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasInt16VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasInt16VectorCallback(func);
		}

#region CallFuncAliasInt32VectorCallback
		internal static delegate*<FuncAliasInt32Vector, AliasInt32Vector> _CallFuncAliasInt32VectorCallback = &___CallFuncAliasInt32VectorCallback;
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
#endregion CallFuncAliasInt32VectorCallback
		/// <summary>
		/// CallFuncAliasInt32VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasInt32Vector CallFuncAliasInt32VectorCallback(FuncAliasInt32Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasInt32VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasInt32VectorCallback(func);
		}

#region CallFuncAliasInt64VectorCallback
		internal static delegate*<FuncAliasInt64Vector, AliasInt64Vector> _CallFuncAliasInt64VectorCallback = &___CallFuncAliasInt64VectorCallback;
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
#endregion CallFuncAliasInt64VectorCallback
		/// <summary>
		/// CallFuncAliasInt64VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasInt64Vector CallFuncAliasInt64VectorCallback(FuncAliasInt64Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasInt64VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasInt64VectorCallback(func);
		}

#region CallFuncAliasUInt8VectorCallback
		internal static delegate*<FuncAliasUInt8Vector, AliasUInt8Vector> _CallFuncAliasUInt8VectorCallback = &___CallFuncAliasUInt8VectorCallback;
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
#endregion CallFuncAliasUInt8VectorCallback
		/// <summary>
		/// CallFuncAliasUInt8VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasUInt8Vector CallFuncAliasUInt8VectorCallback(FuncAliasUInt8Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasUInt8VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasUInt8VectorCallback(func);
		}

#region CallFuncAliasUInt16VectorCallback
		internal static delegate*<FuncAliasUInt16Vector, AliasUInt16Vector> _CallFuncAliasUInt16VectorCallback = &___CallFuncAliasUInt16VectorCallback;
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
#endregion CallFuncAliasUInt16VectorCallback
		/// <summary>
		/// CallFuncAliasUInt16VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasUInt16Vector CallFuncAliasUInt16VectorCallback(FuncAliasUInt16Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasUInt16VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasUInt16VectorCallback(func);
		}

#region CallFuncAliasUInt32VectorCallback
		internal static delegate*<FuncAliasUInt32Vector, AliasUInt32Vector> _CallFuncAliasUInt32VectorCallback = &___CallFuncAliasUInt32VectorCallback;
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
#endregion CallFuncAliasUInt32VectorCallback
		/// <summary>
		/// CallFuncAliasUInt32VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasUInt32Vector CallFuncAliasUInt32VectorCallback(FuncAliasUInt32Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasUInt32VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasUInt32VectorCallback(func);
		}

#region CallFuncAliasUInt64VectorCallback
		internal static delegate*<FuncAliasUInt64Vector, AliasUInt64Vector> _CallFuncAliasUInt64VectorCallback = &___CallFuncAliasUInt64VectorCallback;
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
#endregion CallFuncAliasUInt64VectorCallback
		/// <summary>
		/// CallFuncAliasUInt64VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasUInt64Vector CallFuncAliasUInt64VectorCallback(FuncAliasUInt64Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasUInt64VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasUInt64VectorCallback(func);
		}

#region CallFuncAliasPtrVectorCallback
		internal static delegate*<FuncAliasPtrVector, AliasPtrVector> _CallFuncAliasPtrVectorCallback = &___CallFuncAliasPtrVectorCallback;
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
#endregion CallFuncAliasPtrVectorCallback
		/// <summary>
		/// CallFuncAliasPtrVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasPtrVector CallFuncAliasPtrVectorCallback(FuncAliasPtrVector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasPtrVectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasPtrVectorCallback(func);
		}

#region CallFuncAliasFloatVectorCallback
		internal static delegate*<FuncAliasFloatVector, AliasFloatVector> _CallFuncAliasFloatVectorCallback = &___CallFuncAliasFloatVectorCallback;
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
#endregion CallFuncAliasFloatVectorCallback
		/// <summary>
		/// CallFuncAliasFloatVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasFloatVector CallFuncAliasFloatVectorCallback(FuncAliasFloatVector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasFloatVectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasFloatVectorCallback(func);
		}

#region CallFuncAliasDoubleVectorCallback
		internal static delegate*<FuncAliasDoubleVector, AliasDoubleVector> _CallFuncAliasDoubleVectorCallback = &___CallFuncAliasDoubleVectorCallback;
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
#endregion CallFuncAliasDoubleVectorCallback
		/// <summary>
		/// CallFuncAliasDoubleVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasDoubleVector CallFuncAliasDoubleVectorCallback(FuncAliasDoubleVector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasDoubleVectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasDoubleVectorCallback(func);
		}

#region CallFuncAliasStringVectorCallback
		internal static delegate*<FuncAliasStringVector, AliasStringVector> _CallFuncAliasStringVectorCallback = &___CallFuncAliasStringVectorCallback;
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
#endregion CallFuncAliasStringVectorCallback
		/// <summary>
		/// CallFuncAliasStringVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasStringVector CallFuncAliasStringVectorCallback(FuncAliasStringVector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasStringVectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasStringVectorCallback(func);
		}

#region CallFuncAliasAnyVectorCallback
		internal static delegate*<FuncAliasAnyVector, AliasAnyVector> _CallFuncAliasAnyVectorCallback = &___CallFuncAliasAnyVectorCallback;
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
#endregion CallFuncAliasAnyVectorCallback
		/// <summary>
		/// CallFuncAliasAnyVectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasAnyVector CallFuncAliasAnyVectorCallback(FuncAliasAnyVector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasAnyVectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasAnyVectorCallback(func);
		}

#region CallFuncAliasVec2VectorCallback
		internal static delegate*<FuncAliasVec2Vector, AliasVec2Vector> _CallFuncAliasVec2VectorCallback = &___CallFuncAliasVec2VectorCallback;
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
#endregion CallFuncAliasVec2VectorCallback
		/// <summary>
		/// CallFuncAliasVec2VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasVec2Vector CallFuncAliasVec2VectorCallback(FuncAliasVec2Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasVec2VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasVec2VectorCallback(func);
		}

#region CallFuncAliasVec3VectorCallback
		internal static delegate*<FuncAliasVec3Vector, AliasVec3Vector> _CallFuncAliasVec3VectorCallback = &___CallFuncAliasVec3VectorCallback;
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
#endregion CallFuncAliasVec3VectorCallback
		/// <summary>
		/// CallFuncAliasVec3VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasVec3Vector CallFuncAliasVec3VectorCallback(FuncAliasVec3Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasVec3VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasVec3VectorCallback(func);
		}

#region CallFuncAliasVec4VectorCallback
		internal static delegate*<FuncAliasVec4Vector, AliasVec4Vector> _CallFuncAliasVec4VectorCallback = &___CallFuncAliasVec4VectorCallback;
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
#endregion CallFuncAliasVec4VectorCallback
		/// <summary>
		/// CallFuncAliasVec4VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasVec4Vector CallFuncAliasVec4VectorCallback(FuncAliasVec4Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasVec4VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasVec4VectorCallback(func);
		}

#region CallFuncAliasMat4x4VectorCallback
		internal static delegate*<FuncAliasMat4x4Vector, AliasMat4x4Vector> _CallFuncAliasMat4x4VectorCallback = &___CallFuncAliasMat4x4VectorCallback;
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
#endregion CallFuncAliasMat4x4VectorCallback
		/// <summary>
		/// CallFuncAliasMat4x4VectorCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasMat4x4Vector CallFuncAliasMat4x4VectorCallback(FuncAliasMat4x4Vector func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasMat4x4VectorCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasMat4x4VectorCallback(func);
		}

#region CallFuncAliasVec2Callback
		internal static delegate*<FuncAliasVec2, AliasVec2> _CallFuncAliasVec2Callback = &___CallFuncAliasVec2Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector2> __CallFuncAliasVec2Callback;
		private static AliasVec2 ___CallFuncAliasVec2Callback(FuncAliasVec2 func)
		{
			AliasVec2 __retVal = __CallFuncAliasVec2Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncAliasVec2Callback
		/// <summary>
		/// CallFuncAliasVec2Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasVec2 CallFuncAliasVec2Callback(FuncAliasVec2 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasVec2Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasVec2Callback(func);
		}

#region CallFuncAliasVec3Callback
		internal static delegate*<FuncAliasVec3, AliasVec3> _CallFuncAliasVec3Callback = &___CallFuncAliasVec3Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector3> __CallFuncAliasVec3Callback;
		private static AliasVec3 ___CallFuncAliasVec3Callback(FuncAliasVec3 func)
		{
			AliasVec3 __retVal = __CallFuncAliasVec3Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncAliasVec3Callback
		/// <summary>
		/// CallFuncAliasVec3Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasVec3 CallFuncAliasVec3Callback(FuncAliasVec3 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasVec3Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasVec3Callback(func);
		}

#region CallFuncAliasVec4Callback
		internal static delegate*<FuncAliasVec4, AliasVec4> _CallFuncAliasVec4Callback = &___CallFuncAliasVec4Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector4> __CallFuncAliasVec4Callback;
		private static AliasVec4 ___CallFuncAliasVec4Callback(FuncAliasVec4 func)
		{
			AliasVec4 __retVal = __CallFuncAliasVec4Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncAliasVec4Callback
		/// <summary>
		/// CallFuncAliasVec4Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasVec4 CallFuncAliasVec4Callback(FuncAliasVec4 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasVec4Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasVec4Callback(func);
		}

#region CallFuncAliasMat4x4Callback
		internal static delegate*<FuncAliasMat4x4, AliasMat4x4> _CallFuncAliasMat4x4Callback = &___CallFuncAliasMat4x4Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Matrix4x4> __CallFuncAliasMat4x4Callback;
		private static AliasMat4x4 ___CallFuncAliasMat4x4Callback(FuncAliasMat4x4 func)
		{
			AliasMat4x4 __retVal = __CallFuncAliasMat4x4Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFuncAliasMat4x4Callback
		/// <summary>
		/// CallFuncAliasMat4x4Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static AliasMat4x4 CallFuncAliasMat4x4Callback(FuncAliasMat4x4 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasMat4x4Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasMat4x4Callback(func);
		}

#region CallFuncAliasAllCallback
		internal static delegate*<FuncAliasAll, string> _CallFuncAliasAllCallback = &___CallFuncAliasAllCallback;
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
#endregion CallFuncAliasAllCallback
		/// <summary>
		/// CallFuncAliasAllCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFuncAliasAllCallback(FuncAliasAll func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncAliasAllCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncAliasAllCallback(func);
		}

#region CallFunc1Callback
		internal static delegate*<Func1, int> _CallFunc1Callback = &___CallFunc1Callback;
		internal static delegate* unmanaged[Cdecl]<nint, int> __CallFunc1Callback;
		private static int ___CallFunc1Callback(Func1 func)
		{
			int __retVal = __CallFunc1Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFunc1Callback
		/// <summary>
		/// CallFunc1Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static int CallFunc1Callback(Func1 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc1Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc1Callback(func);
		}

#region CallFunc2Callback
		internal static delegate*<Func2, Char8> _CallFunc2Callback = &___CallFunc2Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Char8> __CallFunc2Callback;
		private static Char8 ___CallFunc2Callback(Func2 func)
		{
			Char8 __retVal = __CallFunc2Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFunc2Callback
		/// <summary>
		/// CallFunc2Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static Char8 CallFunc2Callback(Func2 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc2Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc2Callback(func);
		}

#region CallFunc3Callback
		internal static delegate*<Func3, void> _CallFunc3Callback = &___CallFunc3Callback;
		internal static delegate* unmanaged[Cdecl]<nint, void> __CallFunc3Callback;
		private static void ___CallFunc3Callback(Func3 func)
		{
			__CallFunc3Callback(Marshalling.GetFunctionPointerForDelegate(func));
		}
#endregion CallFunc3Callback
		/// <summary>
		/// CallFunc3Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static void CallFunc3Callback(Func3 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc3Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_CallFunc3Callback(func);
		}

#region CallFunc4Callback
		internal static delegate*<Func4, Vector4> _CallFunc4Callback = &___CallFunc4Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Vector4> __CallFunc4Callback;
		private static Vector4 ___CallFunc4Callback(Func4 func)
		{
			Vector4 __retVal = __CallFunc4Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFunc4Callback
		/// <summary>
		/// CallFunc4Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static Vector4 CallFunc4Callback(Func4 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc4Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc4Callback(func);
		}

#region CallFunc5Callback
		internal static delegate*<Func5, Bool8> _CallFunc5Callback = &___CallFunc5Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Bool8> __CallFunc5Callback;
		private static Bool8 ___CallFunc5Callback(Func5 func)
		{
			Bool8 __retVal = __CallFunc5Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFunc5Callback
		/// <summary>
		/// CallFunc5Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static Bool8 CallFunc5Callback(Func5 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc5Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc5Callback(func);
		}

#region CallFunc6Callback
		internal static delegate*<Func6, long> _CallFunc6Callback = &___CallFunc6Callback;
		internal static delegate* unmanaged[Cdecl]<nint, long> __CallFunc6Callback;
		private static long ___CallFunc6Callback(Func6 func)
		{
			long __retVal = __CallFunc6Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFunc6Callback
		/// <summary>
		/// CallFunc6Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static long CallFunc6Callback(Func6 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc6Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc6Callback(func);
		}

#region CallFunc7Callback
		internal static delegate*<Func7, double> _CallFunc7Callback = &___CallFunc7Callback;
		internal static delegate* unmanaged[Cdecl]<nint, double> __CallFunc7Callback;
		private static double ___CallFunc7Callback(Func7 func)
		{
			double __retVal = __CallFunc7Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFunc7Callback
		/// <summary>
		/// CallFunc7Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static double CallFunc7Callback(Func7 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc7Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc7Callback(func);
		}

#region CallFunc8Callback
		internal static delegate*<Func8, Matrix4x4> _CallFunc8Callback = &___CallFunc8Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Matrix4x4> __CallFunc8Callback;
		private static Matrix4x4 ___CallFunc8Callback(Func8 func)
		{
			Matrix4x4 __retVal = __CallFunc8Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFunc8Callback
		/// <summary>
		/// CallFunc8Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static Matrix4x4 CallFunc8Callback(Func8 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc8Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc8Callback(func);
		}

#region CallFunc9Callback
		internal static delegate*<Func9, void> _CallFunc9Callback = &___CallFunc9Callback;
		internal static delegate* unmanaged[Cdecl]<nint, void> __CallFunc9Callback;
		private static void ___CallFunc9Callback(Func9 func)
		{
			__CallFunc9Callback(Marshalling.GetFunctionPointerForDelegate(func));
		}
#endregion CallFunc9Callback
		/// <summary>
		/// CallFunc9Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static void CallFunc9Callback(Func9 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc9Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_CallFunc9Callback(func);
		}

#region CallFunc10Callback
		internal static delegate*<Func10, uint> _CallFunc10Callback = &___CallFunc10Callback;
		internal static delegate* unmanaged[Cdecl]<nint, uint> __CallFunc10Callback;
		private static uint ___CallFunc10Callback(Func10 func)
		{
			uint __retVal = __CallFunc10Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFunc10Callback
		/// <summary>
		/// CallFunc10Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static uint CallFunc10Callback(Func10 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc10Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc10Callback(func);
		}

#region CallFunc11Callback
		internal static delegate*<Func11, nint> _CallFunc11Callback = &___CallFunc11Callback;
		internal static delegate* unmanaged[Cdecl]<nint, nint> __CallFunc11Callback;
		private static nint ___CallFunc11Callback(Func11 func)
		{
			nint __retVal = __CallFunc11Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFunc11Callback
		/// <summary>
		/// CallFunc11Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static nint CallFunc11Callback(Func11 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc11Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc11Callback(func);
		}

#region CallFunc12Callback
		internal static delegate*<Func12, Bool8> _CallFunc12Callback = &___CallFunc12Callback;
		internal static delegate* unmanaged[Cdecl]<nint, Bool8> __CallFunc12Callback;
		private static Bool8 ___CallFunc12Callback(Func12 func)
		{
			Bool8 __retVal = __CallFunc12Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFunc12Callback
		/// <summary>
		/// CallFunc12Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static Bool8 CallFunc12Callback(Func12 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc12Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc12Callback(func);
		}

#region CallFunc13Callback
		internal static delegate*<Func13, string> _CallFunc13Callback = &___CallFunc13Callback;
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
#endregion CallFunc13Callback
		/// <summary>
		/// CallFunc13Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFunc13Callback(Func13 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc13Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc13Callback(func);
		}

#region CallFunc14Callback
		internal static delegate*<Func14, string[]> _CallFunc14Callback = &___CallFunc14Callback;
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
#endregion CallFunc14Callback
		/// <summary>
		/// CallFunc14Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static string[] CallFunc14Callback(Func14 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc14Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc14Callback(func);
		}

#region CallFunc15Callback
		internal static delegate*<Func15, short> _CallFunc15Callback = &___CallFunc15Callback;
		internal static delegate* unmanaged[Cdecl]<nint, short> __CallFunc15Callback;
		private static short ___CallFunc15Callback(Func15 func)
		{
			short __retVal = __CallFunc15Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFunc15Callback
		/// <summary>
		/// CallFunc15Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static short CallFunc15Callback(Func15 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc15Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc15Callback(func);
		}

#region CallFunc16Callback
		internal static delegate*<Func16, nint> _CallFunc16Callback = &___CallFunc16Callback;
		internal static delegate* unmanaged[Cdecl]<nint, nint> __CallFunc16Callback;
		private static nint ___CallFunc16Callback(Func16 func)
		{
			nint __retVal = __CallFunc16Callback(Marshalling.GetFunctionPointerForDelegate(func));
			return __retVal;
		}
#endregion CallFunc16Callback
		/// <summary>
		/// CallFunc16Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static nint CallFunc16Callback(Func16 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc16Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc16Callback(func);
		}

#region CallFunc17Callback
		internal static delegate*<Func17, string> _CallFunc17Callback = &___CallFunc17Callback;
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
#endregion CallFunc17Callback
		/// <summary>
		/// CallFunc17Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFunc17Callback(Func17 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc17Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc17Callback(func);
		}

#region CallFunc18Callback
		internal static delegate*<Func18, string> _CallFunc18Callback = &___CallFunc18Callback;
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
#endregion CallFunc18Callback
		/// <summary>
		/// CallFunc18Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFunc18Callback(Func18 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc18Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc18Callback(func);
		}

#region CallFunc19Callback
		internal static delegate*<Func19, string> _CallFunc19Callback = &___CallFunc19Callback;
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
#endregion CallFunc19Callback
		/// <summary>
		/// CallFunc19Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFunc19Callback(Func19 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc19Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc19Callback(func);
		}

#region CallFunc20Callback
		internal static delegate*<Func20, string> _CallFunc20Callback = &___CallFunc20Callback;
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
#endregion CallFunc20Callback
		/// <summary>
		/// CallFunc20Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFunc20Callback(Func20 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc20Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc20Callback(func);
		}

#region CallFunc21Callback
		internal static delegate*<Func21, string> _CallFunc21Callback = &___CallFunc21Callback;
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
#endregion CallFunc21Callback
		/// <summary>
		/// CallFunc21Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFunc21Callback(Func21 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc21Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc21Callback(func);
		}

#region CallFunc22Callback
		internal static delegate*<Func22, string> _CallFunc22Callback = &___CallFunc22Callback;
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
#endregion CallFunc22Callback
		/// <summary>
		/// CallFunc22Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFunc22Callback(Func22 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc22Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc22Callback(func);
		}

#region CallFunc23Callback
		internal static delegate*<Func23, string> _CallFunc23Callback = &___CallFunc23Callback;
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
#endregion CallFunc23Callback
		/// <summary>
		/// CallFunc23Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFunc23Callback(Func23 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc23Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc23Callback(func);
		}

#region CallFunc24Callback
		internal static delegate*<Func24, string> _CallFunc24Callback = &___CallFunc24Callback;
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
#endregion CallFunc24Callback
		/// <summary>
		/// CallFunc24Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFunc24Callback(Func24 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc24Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc24Callback(func);
		}

#region CallFunc25Callback
		internal static delegate*<Func25, string> _CallFunc25Callback = &___CallFunc25Callback;
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
#endregion CallFunc25Callback
		/// <summary>
		/// CallFunc25Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFunc25Callback(Func25 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc25Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc25Callback(func);
		}

#region CallFunc26Callback
		internal static delegate*<Func26, string> _CallFunc26Callback = &___CallFunc26Callback;
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
#endregion CallFunc26Callback
		/// <summary>
		/// CallFunc26Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFunc26Callback(Func26 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc26Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc26Callback(func);
		}

#region CallFunc27Callback
		internal static delegate*<Func27, string> _CallFunc27Callback = &___CallFunc27Callback;
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
#endregion CallFunc27Callback
		/// <summary>
		/// CallFunc27Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFunc27Callback(Func27 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc27Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc27Callback(func);
		}

#region CallFunc28Callback
		internal static delegate*<Func28, string> _CallFunc28Callback = &___CallFunc28Callback;
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
#endregion CallFunc28Callback
		/// <summary>
		/// CallFunc28Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFunc28Callback(Func28 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc28Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc28Callback(func);
		}

#region CallFunc29Callback
		internal static delegate*<Func29, string> _CallFunc29Callback = &___CallFunc29Callback;
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
#endregion CallFunc29Callback
		/// <summary>
		/// CallFunc29Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFunc29Callback(Func29 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc29Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc29Callback(func);
		}

#region CallFunc30Callback
		internal static delegate*<Func30, string> _CallFunc30Callback = &___CallFunc30Callback;
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
#endregion CallFunc30Callback
		/// <summary>
		/// CallFunc30Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFunc30Callback(Func30 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc30Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc30Callback(func);
		}

#region CallFunc31Callback
		internal static delegate*<Func31, string> _CallFunc31Callback = &___CallFunc31Callback;
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
#endregion CallFunc31Callback
		/// <summary>
		/// CallFunc31Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFunc31Callback(Func31 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc31Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc31Callback(func);
		}

#region CallFunc32Callback
		internal static delegate*<Func32, string> _CallFunc32Callback = &___CallFunc32Callback;
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
#endregion CallFunc32Callback
		/// <summary>
		/// CallFunc32Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFunc32Callback(Func32 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc32Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc32Callback(func);
		}

#region CallFunc33Callback
		internal static delegate*<Func33, string> _CallFunc33Callback = &___CallFunc33Callback;
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
#endregion CallFunc33Callback
		/// <summary>
		/// CallFunc33Callback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFunc33Callback(Func33 func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFunc33Callback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFunc33Callback(func);
		}

#region CallFuncEnumCallback
		internal static delegate*<FuncEnum, string> _CallFuncEnumCallback = &___CallFuncEnumCallback;
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
#endregion CallFuncEnumCallback
		/// <summary>
		/// CallFuncEnumCallback
		/// </summary>
		/// <param name="func">func</param>
		internal static string CallFuncEnumCallback(FuncEnum func, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CallFuncEnumCallback", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CallFuncEnumCallback(func);
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
		public void Reset()
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
		public void Reset2()
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
