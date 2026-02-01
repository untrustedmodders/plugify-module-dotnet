using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Plugify;

// Generated from cross_call_master.pplugin (group: resource)

namespace cross_call_master {
#pragma warning disable CS0649

	internal static unsafe partial class cross_call_master {

		/// <summary>
		/// ResourceHandleCreate
		/// </summary>
		/// <param name="id">id</param>
		/// <param name="name">name</param>
		internal static delegate*<int, string, nint> ResourceHandleCreate = &___ResourceHandleCreate;
		internal static delegate* unmanaged[Cdecl]<int, String192*, nint> __ResourceHandleCreate;
		private static nint ___ResourceHandleCreate(int id, string name)
		{
			nint __retVal;
			var __name = NativeMethods.ConstructString(name);
			try {
				__retVal = __ResourceHandleCreate(id, &__name);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__name);
			}
			return __retVal;
		}

		/// <summary>
		/// ResourceHandleCreateDefault
		/// </summary>
		internal static delegate*<nint> ResourceHandleCreateDefault = &___ResourceHandleCreateDefault;
		internal static delegate* unmanaged[Cdecl]<nint> __ResourceHandleCreateDefault;
		private static nint ___ResourceHandleCreateDefault()
		{
			nint __retVal = __ResourceHandleCreateDefault();
			return __retVal;
		}

		/// <summary>
		/// ResourceHandleDestroy
		/// </summary>
		/// <param name="handle">handle</param>
		internal static delegate*<nint, void> ResourceHandleDestroy = &___ResourceHandleDestroy;
		internal static delegate* unmanaged[Cdecl]<nint, void> __ResourceHandleDestroy;
		private static void ___ResourceHandleDestroy(nint handle)
		{
			__ResourceHandleDestroy(handle);
		}

		/// <summary>
		/// ResourceHandleGetId
		/// </summary>
		/// <param name="handle">handle</param>
		internal static delegate*<nint, int> ResourceHandleGetId = &___ResourceHandleGetId;
		internal static delegate* unmanaged[Cdecl]<nint, int> __ResourceHandleGetId;
		private static int ___ResourceHandleGetId(nint handle)
		{
			int __retVal = __ResourceHandleGetId(handle);
			return __retVal;
		}

		/// <summary>
		/// ResourceHandleGetName
		/// </summary>
		/// <param name="handle">handle</param>
		internal static delegate*<nint, string> ResourceHandleGetName = &___ResourceHandleGetName;
		internal static delegate* unmanaged[Cdecl]<nint, String192> __ResourceHandleGetName;
		private static string ___ResourceHandleGetName(nint handle)
		{
			string __retVal;
			String192 __retVal_native;
			try {
				__retVal_native = __ResourceHandleGetName(handle);
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
		/// ResourceHandleSetName
		/// </summary>
		/// <param name="handle">handle</param>
		/// <param name="name">name</param>
		internal static delegate*<nint, string, void> ResourceHandleSetName = &___ResourceHandleSetName;
		internal static delegate* unmanaged[Cdecl]<nint, String192*, void> __ResourceHandleSetName;
		private static void ___ResourceHandleSetName(nint handle, string name)
		{
			var __name = NativeMethods.ConstructString(name);
			try {
				__ResourceHandleSetName(handle, &__name);
			}
			finally {
				// Perform cleanup.
				NativeMethods.DestroyString(&__name);
			}
		}

		/// <summary>
		/// ResourceHandleIncrementCounter
		/// </summary>
		/// <param name="handle">handle</param>
		internal static delegate*<nint, void> ResourceHandleIncrementCounter = &___ResourceHandleIncrementCounter;
		internal static delegate* unmanaged[Cdecl]<nint, void> __ResourceHandleIncrementCounter;
		private static void ___ResourceHandleIncrementCounter(nint handle)
		{
			__ResourceHandleIncrementCounter(handle);
		}

		/// <summary>
		/// ResourceHandleGetCounter
		/// </summary>
		/// <param name="handle">handle</param>
		internal static delegate*<nint, int> ResourceHandleGetCounter = &___ResourceHandleGetCounter;
		internal static delegate* unmanaged[Cdecl]<nint, int> __ResourceHandleGetCounter;
		private static int ___ResourceHandleGetCounter(nint handle)
		{
			int __retVal = __ResourceHandleGetCounter(handle);
			return __retVal;
		}

		/// <summary>
		/// ResourceHandleAddData
		/// </summary>
		/// <param name="handle">handle</param>
		/// <param name="value">value</param>
		internal static delegate*<nint, float, void> ResourceHandleAddData = &___ResourceHandleAddData;
		internal static delegate* unmanaged[Cdecl]<nint, float, void> __ResourceHandleAddData;
		private static void ___ResourceHandleAddData(nint handle, float value)
		{
			__ResourceHandleAddData(handle, value);
		}

		/// <summary>
		/// ResourceHandleGetData
		/// </summary>
		/// <param name="handle">handle</param>
		internal static delegate*<nint, float[]> ResourceHandleGetData = &___ResourceHandleGetData;
		internal static delegate* unmanaged[Cdecl]<nint, Vector192> __ResourceHandleGetData;
		private static float[] ___ResourceHandleGetData(nint handle)
		{
			float[] __retVal;
			Vector192 __retVal_native;
			try {
				__retVal_native = __ResourceHandleGetData(handle);
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
		/// ResourceHandleGetAliveCount
		/// </summary>
		internal static delegate*<int> ResourceHandleGetAliveCount = &___ResourceHandleGetAliveCount;
		internal static delegate* unmanaged[Cdecl]<int> __ResourceHandleGetAliveCount;
		private static int ___ResourceHandleGetAliveCount()
		{
			int __retVal = __ResourceHandleGetAliveCount();
			return __retVal;
		}

		/// <summary>
		/// ResourceHandleGetTotalCreated
		/// </summary>
		internal static delegate*<int> ResourceHandleGetTotalCreated = &___ResourceHandleGetTotalCreated;
		internal static delegate* unmanaged[Cdecl]<int> __ResourceHandleGetTotalCreated;
		private static int ___ResourceHandleGetTotalCreated()
		{
			int __retVal = __ResourceHandleGetTotalCreated();
			return __retVal;
		}

		/// <summary>
		/// ResourceHandleGetTotalDestroyed
		/// </summary>
		internal static delegate*<int> ResourceHandleGetTotalDestroyed = &___ResourceHandleGetTotalDestroyed;
		internal static delegate* unmanaged[Cdecl]<int> __ResourceHandleGetTotalDestroyed;
		private static int ___ResourceHandleGetTotalDestroyed()
		{
			int __retVal = __ResourceHandleGetTotalDestroyed();
			return __retVal;
		}

	}

#pragma warning restore CS0649
}
