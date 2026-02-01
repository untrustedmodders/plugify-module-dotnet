using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Plugify;

// Generated from cross_call_master.pplugin (group: counter)

namespace cross_call_master {
#pragma warning disable CS0649

	internal static unsafe partial class cross_call_master {

		/// <summary>
		/// CounterCreate
		/// </summary>
		/// <param name="initialValue">initialValue</param>
		internal static delegate*<long, nint> CounterCreate = &___CounterCreate;
		internal static delegate* unmanaged[Cdecl]<long, nint> __CounterCreate;
		private static nint ___CounterCreate(long initialValue)
		{
			nint __retVal = __CounterCreate(initialValue);
			return __retVal;
		}

		/// <summary>
		/// CounterCreateZero
		/// </summary>
		internal static delegate*<nint> CounterCreateZero = &___CounterCreateZero;
		internal static delegate* unmanaged[Cdecl]<nint> __CounterCreateZero;
		private static nint ___CounterCreateZero()
		{
			nint __retVal = __CounterCreateZero();
			return __retVal;
		}

		/// <summary>
		/// CounterGetValue
		/// </summary>
		/// <param name="counter">counter</param>
		internal static delegate*<nint, long> CounterGetValue = &___CounterGetValue;
		internal static delegate* unmanaged[Cdecl]<nint, long> __CounterGetValue;
		private static long ___CounterGetValue(nint counter)
		{
			long __retVal = __CounterGetValue(counter);
			return __retVal;
		}

		/// <summary>
		/// CounterSetValue
		/// </summary>
		/// <param name="counter">counter</param>
		/// <param name="value">value</param>
		internal static delegate*<nint, long, void> CounterSetValue = &___CounterSetValue;
		internal static delegate* unmanaged[Cdecl]<nint, long, void> __CounterSetValue;
		private static void ___CounterSetValue(nint counter, long value)
		{
			__CounterSetValue(counter, value);
		}

		/// <summary>
		/// CounterIncrement
		/// </summary>
		/// <param name="counter">counter</param>
		internal static delegate*<nint, void> CounterIncrement = &___CounterIncrement;
		internal static delegate* unmanaged[Cdecl]<nint, void> __CounterIncrement;
		private static void ___CounterIncrement(nint counter)
		{
			__CounterIncrement(counter);
		}

		/// <summary>
		/// CounterDecrement
		/// </summary>
		/// <param name="counter">counter</param>
		internal static delegate*<nint, void> CounterDecrement = &___CounterDecrement;
		internal static delegate* unmanaged[Cdecl]<nint, void> __CounterDecrement;
		private static void ___CounterDecrement(nint counter)
		{
			__CounterDecrement(counter);
		}

		/// <summary>
		/// CounterAdd
		/// </summary>
		/// <param name="counter">counter</param>
		/// <param name="amount">amount</param>
		internal static delegate*<nint, long, void> CounterAdd = &___CounterAdd;
		internal static delegate* unmanaged[Cdecl]<nint, long, void> __CounterAdd;
		private static void ___CounterAdd(nint counter, long amount)
		{
			__CounterAdd(counter, amount);
		}

		/// <summary>
		/// CounterReset
		/// </summary>
		/// <param name="counter">counter</param>
		internal static delegate*<nint, void> CounterReset = &___CounterReset;
		internal static delegate* unmanaged[Cdecl]<nint, void> __CounterReset;
		private static void ___CounterReset(nint counter)
		{
			__CounterReset(counter);
		}

		/// <summary>
		/// CounterIsPositive
		/// </summary>
		/// <param name="counter">counter</param>
		internal static delegate*<nint, Bool8> CounterIsPositive = &___CounterIsPositive;
		internal static delegate* unmanaged[Cdecl]<nint, Bool8> __CounterIsPositive;
		private static Bool8 ___CounterIsPositive(nint counter)
		{
			Bool8 __retVal = __CounterIsPositive(counter);
			return __retVal;
		}

		/// <summary>
		/// CounterCompare
		/// </summary>
		/// <param name="value1">value1</param>
		/// <param name="value2">value2</param>
		internal static delegate*<long, long, int> CounterCompare = &___CounterCompare;
		internal static delegate* unmanaged[Cdecl]<long, long, int> __CounterCompare;
		private static int ___CounterCompare(long value1, long value2)
		{
			int __retVal = __CounterCompare(value1, value2);
			return __retVal;
		}

		/// <summary>
		/// CounterSum
		/// </summary>
		/// <param name="values">values</param>
		internal static delegate*<long[], long> CounterSum = &___CounterSum;
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

	}

#pragma warning restore CS0649
}
