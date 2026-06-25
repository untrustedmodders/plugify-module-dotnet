using System;
using System.Reflection;

// Generated from cross_call_worker.pplugin

namespace cross_call_worker {
#pragma warning disable CS0649


	[Flags]	public enum Example : int
	{
		First = 1,
		Second = 2,
		Third = 3,
		Forth = 4
	}


	/// <summary>
	/// Ownership type for RAII wrappers
	/// </summary>
	internal enum Ownership { Borrowed, Owned }

	internal static unsafe partial class cross_call_worker {
		private static readonly string callerModule = Assembly.GetExecutingAssembly().GetName().Name!;
	}

#pragma warning restore CS0649
}
