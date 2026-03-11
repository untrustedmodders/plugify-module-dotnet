using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Plugify;

// Generated from cross_call_master.pplugin (group: counter)

namespace cross_call_master {
#pragma warning disable CS0649

	internal static unsafe partial class cross_call_master {

#region CounterCreate
		internal static delegate*<long, nint> _CounterCreate = &___CounterCreate;
		internal static delegate* unmanaged[Cdecl]<long, nint> __CounterCreate;
		private static nint ___CounterCreate(long initialValue)
		{
			nint __retVal = __CounterCreate(initialValue);
			return __retVal;
		}
#endregion CounterCreate
		/// <summary>
		/// CounterCreate
		/// </summary>
		/// <param name="initialValue">initialValue</param>
		internal static nint CounterCreate(long initialValue, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CounterCreate", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CounterCreate(initialValue);
		}

#region CounterCreateZero
		internal static delegate*<nint> _CounterCreateZero = &___CounterCreateZero;
		internal static delegate* unmanaged[Cdecl]<nint> __CounterCreateZero;
		private static nint ___CounterCreateZero()
		{
			nint __retVal = __CounterCreateZero();
			return __retVal;
		}
#endregion CounterCreateZero
		/// <summary>
		/// CounterCreateZero
		/// </summary>
		internal static nint CounterCreateZero([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CounterCreateZero", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CounterCreateZero();
		}

#region CounterGetValue
		internal static delegate*<nint, long> _CounterGetValue = &___CounterGetValue;
		internal static delegate* unmanaged[Cdecl]<nint, long> __CounterGetValue;
		private static long ___CounterGetValue(nint counter)
		{
			long __retVal = __CounterGetValue(counter);
			return __retVal;
		}
#endregion CounterGetValue
		/// <summary>
		/// CounterGetValue
		/// </summary>
		/// <param name="counter">counter</param>
		internal static long CounterGetValue(nint counter, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CounterGetValue", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CounterGetValue(counter);
		}

#region CounterSetValue
		internal static delegate*<nint, long, void> _CounterSetValue = &___CounterSetValue;
		internal static delegate* unmanaged[Cdecl]<nint, long, void> __CounterSetValue;
		private static void ___CounterSetValue(nint counter, long value)
		{
			__CounterSetValue(counter, value);
		}
#endregion CounterSetValue
		/// <summary>
		/// CounterSetValue
		/// </summary>
		/// <param name="counter">counter</param>
		/// <param name="value">value</param>
		internal static void CounterSetValue(nint counter, long value, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CounterSetValue", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_CounterSetValue(counter, value);
		}

#region CounterIncrement
		internal static delegate*<nint, void> _CounterIncrement = &___CounterIncrement;
		internal static delegate* unmanaged[Cdecl]<nint, void> __CounterIncrement;
		private static void ___CounterIncrement(nint counter)
		{
			__CounterIncrement(counter);
		}
#endregion CounterIncrement
		/// <summary>
		/// CounterIncrement
		/// </summary>
		/// <param name="counter">counter</param>
		internal static void CounterIncrement(nint counter, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CounterIncrement", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_CounterIncrement(counter);
		}

#region CounterDecrement
		internal static delegate*<nint, void> _CounterDecrement = &___CounterDecrement;
		internal static delegate* unmanaged[Cdecl]<nint, void> __CounterDecrement;
		private static void ___CounterDecrement(nint counter)
		{
			__CounterDecrement(counter);
		}
#endregion CounterDecrement
		/// <summary>
		/// CounterDecrement
		/// </summary>
		/// <param name="counter">counter</param>
		internal static void CounterDecrement(nint counter, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CounterDecrement", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_CounterDecrement(counter);
		}

#region CounterAdd
		internal static delegate*<nint, long, void> _CounterAdd = &___CounterAdd;
		internal static delegate* unmanaged[Cdecl]<nint, long, void> __CounterAdd;
		private static void ___CounterAdd(nint counter, long amount)
		{
			__CounterAdd(counter, amount);
		}
#endregion CounterAdd
		/// <summary>
		/// CounterAdd
		/// </summary>
		/// <param name="counter">counter</param>
		/// <param name="amount">amount</param>
		internal static void CounterAdd(nint counter, long amount, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CounterAdd", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_CounterAdd(counter, amount);
		}

#region CounterReset
		internal static delegate*<nint, void> _CounterReset = &___CounterReset;
		internal static delegate* unmanaged[Cdecl]<nint, void> __CounterReset;
		private static void ___CounterReset(nint counter)
		{
			__CounterReset(counter);
		}
#endregion CounterReset
		/// <summary>
		/// CounterReset
		/// </summary>
		/// <param name="counter">counter</param>
		internal static void CounterReset(nint counter, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CounterReset", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_CounterReset(counter);
		}

#region CounterIsPositive
		internal static delegate*<nint, Bool8> _CounterIsPositive = &___CounterIsPositive;
		internal static delegate* unmanaged[Cdecl]<nint, Bool8> __CounterIsPositive;
		private static Bool8 ___CounterIsPositive(nint counter)
		{
			Bool8 __retVal = __CounterIsPositive(counter);
			return __retVal;
		}
#endregion CounterIsPositive
		/// <summary>
		/// CounterIsPositive
		/// </summary>
		/// <param name="counter">counter</param>
		internal static Bool8 CounterIsPositive(nint counter, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CounterIsPositive", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CounterIsPositive(counter);
		}

#region CounterCompare
		internal static delegate*<long, long, int> _CounterCompare = &___CounterCompare;
		internal static delegate* unmanaged[Cdecl]<long, long, int> __CounterCompare;
		private static int ___CounterCompare(long value1, long value2)
		{
			int __retVal = __CounterCompare(value1, value2);
			return __retVal;
		}
#endregion CounterCompare
		/// <summary>
		/// CounterCompare
		/// </summary>
		/// <param name="value1">value1</param>
		/// <param name="value2">value2</param>
		internal static int CounterCompare(long value1, long value2, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CounterCompare", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CounterCompare(value1, value2);
		}

#region CounterSum
		internal static delegate*<long[], long> _CounterSum = &___CounterSum;
		internal static delegate* unmanaged[Cdecl]<Vector192*, long> __CounterSum;
		private static long ___CounterSum(long[] values)
		{
			long __retVal;
			var __values = NativeMethods.ConstructVectorInt64(values, values.Length);
			try {
				__retVal = __CounterSum(&__values);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyVectorInt64(&__values);
			}
			return __retVal;
		}
#endregion CounterSum
		/// <summary>
		/// CounterSum
		/// </summary>
		/// <param name="values">values</param>
		internal static long CounterSum(long[] values, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::CounterSum", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _CounterSum(values);
		}

	}

#pragma warning restore CS0649
}
