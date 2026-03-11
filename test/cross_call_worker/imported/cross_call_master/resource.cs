using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Plugify;

// Generated from cross_call_master.pplugin (group: resource)

namespace cross_call_master {
#pragma warning disable CS0649

	internal static unsafe partial class cross_call_master {

#region ResourceHandleCreate
		internal static delegate*<int, string, nint> _ResourceHandleCreate = &___ResourceHandleCreate;
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
#endregion ResourceHandleCreate
		/// <summary>
		/// ResourceHandleCreate
		/// </summary>
		/// <param name="id">id</param>
		/// <param name="name">name</param>
		internal static nint ResourceHandleCreate(int id, string name, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ResourceHandleCreate", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _ResourceHandleCreate(id, name);
		}

#region ResourceHandleCreateDefault
		internal static delegate*<nint> _ResourceHandleCreateDefault = &___ResourceHandleCreateDefault;
		internal static delegate* unmanaged[Cdecl]<nint> __ResourceHandleCreateDefault;
		private static nint ___ResourceHandleCreateDefault()
		{
			nint __retVal = __ResourceHandleCreateDefault();
			return __retVal;
		}
#endregion ResourceHandleCreateDefault
		/// <summary>
		/// ResourceHandleCreateDefault
		/// </summary>
		internal static nint ResourceHandleCreateDefault([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ResourceHandleCreateDefault", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _ResourceHandleCreateDefault();
		}

#region ResourceHandleDestroy
		internal static delegate*<nint, void> _ResourceHandleDestroy = &___ResourceHandleDestroy;
		internal static delegate* unmanaged[Cdecl]<nint, void> __ResourceHandleDestroy;
		private static void ___ResourceHandleDestroy(nint handle)
		{
			__ResourceHandleDestroy(handle);
		}
#endregion ResourceHandleDestroy
		/// <summary>
		/// ResourceHandleDestroy
		/// </summary>
		/// <param name="handle">handle</param>
		internal static void ResourceHandleDestroy(nint handle, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ResourceHandleDestroy", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_ResourceHandleDestroy(handle);
		}

#region ResourceHandleGetId
		internal static delegate*<nint, int> _ResourceHandleGetId = &___ResourceHandleGetId;
		internal static delegate* unmanaged[Cdecl]<nint, int> __ResourceHandleGetId;
		private static int ___ResourceHandleGetId(nint handle)
		{
			int __retVal = __ResourceHandleGetId(handle);
			return __retVal;
		}
#endregion ResourceHandleGetId
		/// <summary>
		/// ResourceHandleGetId
		/// </summary>
		/// <param name="handle">handle</param>
		internal static int ResourceHandleGetId(nint handle, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ResourceHandleGetId", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _ResourceHandleGetId(handle);
		}

#region ResourceHandleGetName
		internal static delegate*<nint, string> _ResourceHandleGetName = &___ResourceHandleGetName;
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
#endregion ResourceHandleGetName
		/// <summary>
		/// ResourceHandleGetName
		/// </summary>
		/// <param name="handle">handle</param>
		internal static string ResourceHandleGetName(nint handle, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ResourceHandleGetName", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _ResourceHandleGetName(handle);
		}

#region ResourceHandleSetName
		internal static delegate*<nint, string, void> _ResourceHandleSetName = &___ResourceHandleSetName;
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
#endregion ResourceHandleSetName
		/// <summary>
		/// ResourceHandleSetName
		/// </summary>
		/// <param name="handle">handle</param>
		/// <param name="name">name</param>
		internal static void ResourceHandleSetName(nint handle, string name, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ResourceHandleSetName", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_ResourceHandleSetName(handle, name);
		}

#region ResourceHandleIncrementCounter
		internal static delegate*<nint, void> _ResourceHandleIncrementCounter = &___ResourceHandleIncrementCounter;
		internal static delegate* unmanaged[Cdecl]<nint, void> __ResourceHandleIncrementCounter;
		private static void ___ResourceHandleIncrementCounter(nint handle)
		{
			__ResourceHandleIncrementCounter(handle);
		}
#endregion ResourceHandleIncrementCounter
		/// <summary>
		/// ResourceHandleIncrementCounter
		/// </summary>
		/// <param name="handle">handle</param>
		internal static void ResourceHandleIncrementCounter(nint handle, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ResourceHandleIncrementCounter", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_ResourceHandleIncrementCounter(handle);
		}

#region ResourceHandleGetCounter
		internal static delegate*<nint, int> _ResourceHandleGetCounter = &___ResourceHandleGetCounter;
		internal static delegate* unmanaged[Cdecl]<nint, int> __ResourceHandleGetCounter;
		private static int ___ResourceHandleGetCounter(nint handle)
		{
			int __retVal = __ResourceHandleGetCounter(handle);
			return __retVal;
		}
#endregion ResourceHandleGetCounter
		/// <summary>
		/// ResourceHandleGetCounter
		/// </summary>
		/// <param name="handle">handle</param>
		internal static int ResourceHandleGetCounter(nint handle, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ResourceHandleGetCounter", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _ResourceHandleGetCounter(handle);
		}

#region ResourceHandleAddData
		internal static delegate*<nint, float, void> _ResourceHandleAddData = &___ResourceHandleAddData;
		internal static delegate* unmanaged[Cdecl]<nint, float, void> __ResourceHandleAddData;
		private static void ___ResourceHandleAddData(nint handle, float value)
		{
			__ResourceHandleAddData(handle, value);
		}
#endregion ResourceHandleAddData
		/// <summary>
		/// ResourceHandleAddData
		/// </summary>
		/// <param name="handle">handle</param>
		/// <param name="value">value</param>
		internal static void ResourceHandleAddData(nint handle, float value, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ResourceHandleAddData", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			_ResourceHandleAddData(handle, value);
		}

#region ResourceHandleGetData
		internal static delegate*<nint, float[]> _ResourceHandleGetData = &___ResourceHandleGetData;
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
#endregion ResourceHandleGetData
		/// <summary>
		/// ResourceHandleGetData
		/// </summary>
		/// <param name="handle">handle</param>
		internal static float[] ResourceHandleGetData(nint handle, [CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ResourceHandleGetData", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _ResourceHandleGetData(handle);
		}

#region ResourceHandleGetAliveCount
		internal static delegate*<int> _ResourceHandleGetAliveCount = &___ResourceHandleGetAliveCount;
		internal static delegate* unmanaged[Cdecl]<int> __ResourceHandleGetAliveCount;
		private static int ___ResourceHandleGetAliveCount()
		{
			int __retVal = __ResourceHandleGetAliveCount();
			return __retVal;
		}
#endregion ResourceHandleGetAliveCount
		/// <summary>
		/// ResourceHandleGetAliveCount
		/// </summary>
		internal static int ResourceHandleGetAliveCount([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ResourceHandleGetAliveCount", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _ResourceHandleGetAliveCount();
		}

#region ResourceHandleGetTotalCreated
		internal static delegate*<int> _ResourceHandleGetTotalCreated = &___ResourceHandleGetTotalCreated;
		internal static delegate* unmanaged[Cdecl]<int> __ResourceHandleGetTotalCreated;
		private static int ___ResourceHandleGetTotalCreated()
		{
			int __retVal = __ResourceHandleGetTotalCreated();
			return __retVal;
		}
#endregion ResourceHandleGetTotalCreated
		/// <summary>
		/// ResourceHandleGetTotalCreated
		/// </summary>
		internal static int ResourceHandleGetTotalCreated([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ResourceHandleGetTotalCreated", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _ResourceHandleGetTotalCreated();
		}

#region ResourceHandleGetTotalDestroyed
		internal static delegate*<int> _ResourceHandleGetTotalDestroyed = &___ResourceHandleGetTotalDestroyed;
		internal static delegate* unmanaged[Cdecl]<int> __ResourceHandleGetTotalDestroyed;
		private static int ___ResourceHandleGetTotalDestroyed()
		{
			int __retVal = __ResourceHandleGetTotalDestroyed();
			return __retVal;
		}
#endregion ResourceHandleGetTotalDestroyed
		/// <summary>
		/// ResourceHandleGetTotalDestroyed
		/// </summary>
		internal static int ResourceHandleGetTotalDestroyed([CallerMemberName] string callerFunction = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0)
		{
			NativeMethods.Log("cross_call_master::ResourceHandleGetTotalDestroyed", Severity.Trace, callerLine, callerFile, callerFunction, callerModule);
			return _ResourceHandleGetTotalDestroyed();
		}

	}

#pragma warning restore CS0649
}
