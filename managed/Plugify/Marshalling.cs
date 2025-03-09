using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Plugify;

public static class Marshalling
{
	internal static readonly Dictionary<Delegate, JitCallback?> CachedDelegates = new();
	internal static readonly Dictionary<nint, Delegate> CachedFunctions = new();

	internal static unsafe object?[]? MarshalParameterArray(nint paramsPtr, int parameterCount, MethodBase methodInfo)
	{
		ParameterInfo[] parameterInfos = methodInfo.GetParameters();
		int numParams = parameterInfos.Length;

		if (numParams == 0 || paramsPtr == nint.Zero || numParams != parameterCount)
		{
			return null;
		}

		var parameters = new object?[numParams];

		int paramsOffset = 0;

		for (int i = 0; i < numParams; i++)
		{
			// params is stored as void**
			nint paramPtr = *(nint*)((byte*)paramsPtr + paramsOffset);
			paramsOffset += nint.Size;

			ParameterInfo parameterInfo = parameterInfos[i];
			Type paramType = parameterInfo.ParameterType;

			parameters[i] = MarshalPointer(paramPtr, paramType);
		}

		return parameters;
	}

	internal static unsafe void MarshalParameterRefs(nint paramsPtr, int parameterCount, MethodInfo methodInfo, object?[]? parameters)
	{
		if (parameters == null)
		{
			return;
		}

		ParameterInfo[] parameterInfos = methodInfo.GetParameters();
		int numParams = parameterInfos.Length;

		if (numParams != parameterCount)
		{
			return;
		}

		int paramsOffset = 0;

		for (int i = 0; i < numParams; i++)
		{
			// params is stored as void**
			nint paramPtr = *(nint*)((byte*)paramsPtr + paramsOffset);
			paramsOffset += nint.Size;

			ParameterInfo parameterInfo = parameterInfos[i];
			Type paramType = parameterInfo.ParameterType;
			
			if (!paramType.IsByRef)
			{
				continue;
			}

			object? paramValue = parameters[i];
			if (paramValue == null)
			{
				throw new Exception("Reference cannot be null");
			}
			
			MarshalReturnValue(paramValue, paramType, paramPtr);
		}
	}

	internal static unsafe void MarshalReturnValue(object? paramValue, Type paramType, nint outValue)
	{
		ValueType valueType = TypeUtils.ConvertToValueType(paramType);
		Type? enumType = paramType.GetEnumType();
		if (enumType != null)
		{
			switch (valueType)
			{
				case ValueType.Int8:
					*(sbyte*)outValue = (sbyte?)Convert.ChangeType(paramValue, Enum.GetUnderlyingType(enumType)) ?? default;
					return;
				case ValueType.Int16:
					*(short*)outValue = (short?)Convert.ChangeType(paramValue, Enum.GetUnderlyingType(enumType)) ?? default;
					return;
				case ValueType.Int32:
					*(int*)outValue = (int?)Convert.ChangeType(paramValue, Enum.GetUnderlyingType(enumType)) ?? default;
					return;
				case ValueType.Int64:
					*(long*)outValue = (long?)Convert.ChangeType(paramValue, Enum.GetUnderlyingType(enumType)) ?? default;
					return;
				case ValueType.UInt8:
					*(byte*)outValue = (byte?)Convert.ChangeType(paramValue, Enum.GetUnderlyingType(enumType)) ?? default;
					return;
				case ValueType.UInt16:
					*(ushort*)outValue = (ushort?)Convert.ChangeType(paramValue, Enum.GetUnderlyingType(enumType)) ?? default;
					return;
				case ValueType.UInt32:
					*(uint*)outValue = (uint?)Convert.ChangeType(paramValue, Enum.GetUnderlyingType(enumType)) ?? default;
					return;
				case ValueType.UInt64:
					*(ulong*)outValue = (ulong?)Convert.ChangeType(paramValue, Enum.GetUnderlyingType(enumType)) ?? default;
					return;
				case ValueType.ArrayInt8:
				{
					if (paramValue is Array enumArray)
					{
						sbyte[] arr = TypeUtils.ConvertFromEnumArray<sbyte>(enumType, enumArray);
						NativeMethods.AssignVectorInt8((Vector192*)outValue, arr, arr.Length);
					}
					return;
				}
				case ValueType.ArrayInt16:
				{
					if (paramValue is Array enumArray)
					{
						short[] arr = TypeUtils.ConvertFromEnumArray<short>(enumType, enumArray);
						NativeMethods.AssignVectorInt16((Vector192*)outValue, arr, arr.Length);
					}
					return;
				}
				case ValueType.ArrayInt32:
				{
					if (paramValue is Array enumArray)
					{
						int[] arr = TypeUtils.ConvertFromEnumArray<int>(enumType, enumArray);
						NativeMethods.AssignVectorInt32((Vector192*)outValue, arr, arr.Length);
					}
					return;
				}
				case ValueType.ArrayInt64:
				{
					if (paramValue is Array enumArray)
					{
						long[] arr = TypeUtils.ConvertFromEnumArray<long>(enumType, enumArray);
						NativeMethods.AssignVectorInt64((Vector192*)outValue, arr, arr.Length);
					}
					return;
				}
				case ValueType.ArrayUInt8:
				{
					if (paramValue is Array enumArray)
					{
						byte[] arr = TypeUtils.ConvertFromEnumArray<byte>(enumType, enumArray);
						NativeMethods.AssignVectorUInt8((Vector192*)outValue, arr, arr.Length);
					}
					return;
				}
				case ValueType.ArrayUInt16:
				{
					if (paramValue is Array enumArray)
					{
						ushort[] arr = TypeUtils.ConvertFromEnumArray<ushort>(enumType, enumArray);
						NativeMethods.AssignVectorUInt16((Vector192*)outValue, arr, arr.Length);
					}
					return;
				}
				case ValueType.ArrayUInt32:
				{
					if (paramValue is Array enumArray)
					{
						uint[] arr = TypeUtils.ConvertFromEnumArray<uint>(enumType, enumArray);
						NativeMethods.AssignVectorUInt32((Vector192*)outValue, arr, arr.Length);
					}
					return;
				}
				case ValueType.ArrayUInt64:
				{
					if (paramValue is Array enumArray)
					{
						ulong[] arr = TypeUtils.ConvertFromEnumArray<ulong>(enumType, enumArray);
						NativeMethods.AssignVectorUInt64((Vector192*)outValue, arr, arr.Length);
					}
					return;
				}
			}
		}
		else
		{
			switch (valueType)
			{
				case ValueType.Bool:
					*(Bool8*)outValue = (Bool8?)paramValue ?? default;
					return;
				case ValueType.Char8:
					*(Char8*)outValue = (Char8?)paramValue ?? default;
					return;
				case ValueType.Char16:
					*(Char16*)outValue = (Char16?)paramValue ?? default;
					return;
				case ValueType.Int8:
					*(sbyte*)outValue = (sbyte?)paramValue ?? default;
					return;
				case ValueType.Int16:
					*(short*)outValue = (short?)paramValue ?? default;
					return;
				case ValueType.Int32:
					*(int*)outValue = (int?)paramValue ?? default;
					return;
				case ValueType.Int64:
					*(long*)outValue = (long?)paramValue ?? default;
					return;
				case ValueType.UInt8:
					*(byte*)outValue = (byte?)paramValue ?? default;
					return;
				case ValueType.UInt16:
					*(ushort*)outValue = (ushort?)paramValue ?? default;
					return;
				case ValueType.UInt32:
					*(uint*)outValue = (uint?)paramValue ?? default;
					return;
				case ValueType.UInt64:
					*(ulong*)outValue = (ulong?)paramValue ?? default;
					return;
				case ValueType.Pointer:
					*(nint*)outValue = (nint?)paramValue ?? default;
					return;
				case ValueType.Float:
					*(float*)outValue = (float?)paramValue ?? default;
					return;
				case ValueType.Double:
					*(double*)outValue = (double?)paramValue ?? default;
					return;
				case ValueType.Function:
				{
					*(nint*)outValue = paramValue != null ? GetFunctionPointerForDelegate((Delegate)paramValue) : nint.Zero;
					return;
				}
				case ValueType.String:
				{
					if (paramValue is string str) NativeMethods.AssignString((String192*)outValue, str);
					return;
				}
				case ValueType.Any:
				{
					NativeMethods.AssignVariant((Variant256*)outValue, paramValue);
					return;
				}
				case ValueType.ArrayBool:
				{
					if (paramValue is Bool8[] arr) NativeMethods.AssignVectorBool((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.ArrayChar8:
				{
					if (paramValue is Char8[] arr) NativeMethods.AssignVectorChar8((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.ArrayChar16:
				{
					if (paramValue is Char16[] arr) NativeMethods.AssignVectorChar16((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.ArrayInt8:
				{
					if (paramValue is sbyte[] arr) NativeMethods.AssignVectorInt8((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.ArrayInt16:
				{
					if (paramValue is short[] arr) NativeMethods.AssignVectorInt16((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.ArrayInt32:
				{
					if (paramValue is int[] arr) NativeMethods.AssignVectorInt32((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.ArrayInt64:
				{
					if (paramValue is long[] arr) NativeMethods.AssignVectorInt64((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.ArrayUInt8:
				{
					if (paramValue is byte[] arr) NativeMethods.AssignVectorUInt8((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.ArrayUInt16:
				{
					if (paramValue is ushort[] arr) NativeMethods.AssignVectorUInt16((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.ArrayUInt32:
				{
					if (paramValue is uint[] arr) NativeMethods.AssignVectorUInt32((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.ArrayUInt64:
				{
					if (paramValue is ulong[] arr) NativeMethods.AssignVectorUInt64((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.ArrayPointer:
				{
					if (paramValue is nint[] arr) NativeMethods.AssignVectorIntPtr((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.ArrayFloat:
				{
					if (paramValue is float[] arr) NativeMethods.AssignVectorFloat((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.ArrayDouble:
				{
					if (paramValue is double[] arr) NativeMethods.AssignVectorDouble((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.ArrayString:
				{
					if (paramValue is string[] arr) NativeMethods.AssignVectorString((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.ArrayAny:
				{
					if (paramValue is object[] arr) NativeMethods.AssignVectorVariant((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.ArrayVector2:
				{
					if (paramValue is Vector2[] arr) NativeMethods.AssignVectorVector2((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.ArrayVector3:
				{
					if (paramValue is Vector3[] arr) NativeMethods.AssignVectorVector3((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.ArrayVector4:
				{
					if (paramValue is Vector4[] arr) NativeMethods.AssignVectorVector4((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.ArrayMatrix4x4:
				{
					if (paramValue is Matrix4x4[] arr) NativeMethods.AssignVectorMatrix4x4((Vector192*)outValue, arr, arr.Length);
					return;
				}
				case ValueType.Vector2:
					Marshal.StructureToPtr(paramValue ?? Vector2.Zero, outValue, false);
					return;
				case ValueType.Vector3:
					Marshal.StructureToPtr(paramValue ?? Vector3.Zero, outValue, false);
					return;
				case ValueType.Vector4:
					Marshal.StructureToPtr(paramValue ?? Vector4.Zero, outValue, false);
					return;
				case ValueType.Matrix4x4:
					Marshal.StructureToPtr(paramValue ?? Matrix4x4.Identity, outValue, false);
					return;
			}

		}
		
		/*if (paramType.IsValueType)
		{
			Marshal.StructureToPtr(paramValue, outValue, false);
			return;
		}*/

		throw new NotImplementedException($"Parameter type {paramType.Name} not implemented");
	}

	internal static unsafe object MarshalPointer(nint inValue, Type paramType)
	{
		ValueType valueType = TypeUtils.ConvertToValueType(paramType);
		Type? enumType = paramType.GetEnumType();
		if (enumType != null)
		{
			switch (valueType)
			{
				case ValueType.Int8:
					return Enum.ToObject(enumType, *(sbyte*)inValue);
				case ValueType.Int16:
					return Enum.ToObject(enumType, *(short*)inValue);
				case ValueType.Int32:
					return Enum.ToObject(enumType, *(int*)inValue);
				case ValueType.Int64:
					return Enum.ToObject(enumType, *(long*)inValue);
				case ValueType.UInt8:
					return Enum.ToObject(enumType, *(byte*)inValue);
				case ValueType.UInt16:
					return Enum.ToObject(enumType, *(ushort*)inValue);
				case ValueType.UInt32:
					return Enum.ToObject(enumType, *(uint*)inValue);
				case ValueType.UInt64:
					return Enum.ToObject(enumType, *(ulong*)inValue);
				case ValueType.ArrayInt8:
				{
					var arr = NativeMethods.GetVectorDataInt8((Vector192*)inValue);
					return TypeUtils.ConvertToEnumArray(enumType, arr);
				}
				case ValueType.ArrayInt16:
				{
					var arr = NativeMethods.GetVectorDataInt16((Vector192*)inValue);
					return TypeUtils.ConvertToEnumArray(enumType, arr);
				}
				case ValueType.ArrayInt32:
				{
					var arr = NativeMethods.GetVectorDataInt32((Vector192*)inValue);
					return TypeUtils.ConvertToEnumArray(enumType, arr);
				}
				case ValueType.ArrayInt64:
				{
					var arr = NativeMethods.GetVectorDataInt64((Vector192*)inValue);
					return TypeUtils.ConvertToEnumArray(enumType, arr);
				}
				case ValueType.ArrayUInt8:
				{
					var arr = NativeMethods.GetVectorDataUInt8((Vector192*)inValue);
					return TypeUtils.ConvertToEnumArray(enumType, arr);
				}
				case ValueType.ArrayUInt16:
				{
					var arr = NativeMethods.GetVectorDataUInt16((Vector192*)inValue);
					return TypeUtils.ConvertToEnumArray(enumType, arr);
				}
				case ValueType.ArrayUInt32:
				{
					var arr = NativeMethods.GetVectorDataUInt32((Vector192*)inValue);
					return TypeUtils.ConvertToEnumArray(enumType, arr);
				}
				case ValueType.ArrayUInt64:
				{
					var arr = NativeMethods.GetVectorDataUInt64((Vector192*)inValue);
					return TypeUtils.ConvertToEnumArray(enumType, arr);
				}
			}
		}
		else
		{
			switch (valueType)
			{
				case ValueType.Bool:
					return *(Bool8*)inValue;
				case ValueType.Char8:
					return *(Char8*)inValue;
				case ValueType.Char16:
					return *(Char16*)inValue;
				case ValueType.Int8:
					return *(sbyte*)inValue;
				case ValueType.Int16:
					return *(short*)inValue;
				case ValueType.Int32:
					return *(int*)inValue;
				case ValueType.Int64:
					return *(long*)inValue;
				case ValueType.UInt8:
					return *(byte*)inValue;
				case ValueType.UInt16:
					return *(ushort*)inValue;
				case ValueType.UInt32:
					return *(uint*)inValue;
				case ValueType.UInt64:
					return *(ulong*)inValue;
				case ValueType.Pointer:
					return *(nint*)inValue;
				case ValueType.Float:
					return *(float*)inValue;
				case ValueType.Double:
					return *(double*)inValue;
				case ValueType.Function:
					return GetDelegateForFunctionPointer(inValue, paramType);
				case ValueType.String:
					return NativeMethods.GetStringData((String192*)inValue);
				case ValueType.Any:
					return NativeMethods.GetVariantData((Variant256*)inValue);
				case ValueType.ArrayBool:
					return NativeMethods.GetVectorDataBool((Vector192*)inValue);
				case ValueType.ArrayChar8:
					return NativeMethods.GetVectorDataChar8((Vector192*)inValue);
				case ValueType.ArrayChar16:
					return NativeMethods.GetVectorDataChar16((Vector192*)inValue);
				case ValueType.ArrayInt8:
					return NativeMethods.GetVectorDataInt8((Vector192*)inValue);
				case ValueType.ArrayInt16:
					return NativeMethods.GetVectorDataInt16((Vector192*)inValue);
				case ValueType.ArrayInt32:
					return NativeMethods.GetVectorDataInt32((Vector192*)inValue);
				case ValueType.ArrayInt64:
					return NativeMethods.GetVectorDataInt64((Vector192*)inValue);
				case ValueType.ArrayUInt8:
					return NativeMethods.GetVectorDataUInt8((Vector192*)inValue);
				case ValueType.ArrayUInt16:
					return NativeMethods.GetVectorDataUInt16((Vector192*)inValue);
				case ValueType.ArrayUInt32:
					return NativeMethods.GetVectorDataUInt32((Vector192*)inValue);
				case ValueType.ArrayUInt64:
					return NativeMethods.GetVectorDataUInt64((Vector192*)inValue);
				case ValueType.ArrayPointer:
					return NativeMethods.GetVectorDataIntPtr((Vector192*)inValue);
				case ValueType.ArrayFloat:
					return NativeMethods.GetVectorDataFloat((Vector192*)inValue);
				case ValueType.ArrayDouble:
					return NativeMethods.GetVectorDataDouble((Vector192*)inValue);
				case ValueType.ArrayString:
					return NativeMethods.GetVectorDataString((Vector192*)inValue);
				case ValueType.ArrayAny:
					return NativeMethods.GetVectorDataVariant((Vector192*)inValue);
				case ValueType.ArrayVector2:
					return NativeMethods.GetVectorDataVector2((Vector192*)inValue);
				case ValueType.ArrayVector3:
					return NativeMethods.GetVectorDataVector3((Vector192*)inValue);
				case ValueType.ArrayVector4:
					return NativeMethods.GetVectorDataVector4((Vector192*)inValue);
				case ValueType.ArrayMatrix4x4:
					return NativeMethods.GetVectorDataMatrix4x4((Vector192*)inValue);
				case ValueType.Vector2:
					return *(Vector2*)inValue;
				case ValueType.Vector3:
					return *(Vector3*)inValue;
				case ValueType.Vector4:
					return *(Vector4*)inValue;
				case ValueType.Matrix4x4:
					return *(Matrix4x4*)inValue;
			}
		}
		
		/*if (paramType.IsValueType)
		{
			return Marshal.PtrToStructure(inValue, paramType.IsByRef ? paramType.GetElementType() : paramType);
		}*/
		
		throw new NotImplementedException($"Parameter type {paramType.Name} not implemented");
	}

	internal static void MarshalFieldAddress(object obj, FieldInfo fieldInfo, nint outValue)
	{
		var ptr = Unsafe.As<object, nint>(ref obj) + IntPtr.Size + GetFieldOffset(fieldInfo);
		Marshal.WriteIntPtr(outValue, ptr);
	}
	
	private static int GetFieldOffset(this FieldInfo fieldInfo) => GetFieldOffset(fieldInfo.FieldHandle);

	private static int GetFieldOffset(RuntimeFieldHandle handle) => Marshal.ReadInt32(handle.Value + (4 + IntPtr.Size)) & 0xFFFFFF;
	
	public static Delegate GetDelegateForFunctionPointer(nint funcAddress, Type? delegateType)
	{
		if (CachedFunctions.TryGetValue(funcAddress, out var d))
		{
			return d;
		}

		if (delegateType == null)
		{
			throw new Exception("Type required to properly generate delegate at runtime");
		}

		MethodInfo methodInfo = delegateType.GetMethod("Invoke")!;
		if (IsNeedMarshal(methodInfo.ReturnType) || methodInfo.GetParameters().Any(p => IsNeedMarshal(p.ParameterType)))
		{
			d = MethodUtils.CreateObjectArrayDelegate(delegateType, ExternalInvoke(funcAddress, methodInfo));
		}
		else
		{
			d = Marshal.GetDelegateForFunctionPointer(funcAddress, delegateType);
		}
		
		CachedFunctions.Add(funcAddress, d);
		return d;
	}

	private static unsafe nint RCast<T>(T primitive) where T : struct
	{
		return *(nint*)&primitive;
	}
	
	private static readonly bool X86 = RuntimeInformation.ProcessArchitecture == Architecture.X64 || RuntimeInformation.ProcessArchitecture == Architecture.X86;
	private static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && X86;
	
	private static unsafe Func<object[], object> ExternalInvoke(nint funcAddress, MethodInfo methodInfo)
	{
		ManagedType returnType =  new ManagedType(methodInfo.ReturnParameter.ParameterType);
		ManagedType[] parameterTypes = methodInfo.GetParameters().Select(p => new ManagedType(p.ParameterType)).ToArray();
		
		bool hasRet = returnType.ValueType is >= ValueType._ObjectStart and <= ValueType._ObjectEnd;
		bool hasRefs = parameterTypes.Any(t => t.IsByRef);
		
		if (!hasRet)
		{
			ValueType firstHidden = IsWindows ? ValueType.Vector3 : ValueType.Matrix4x4;
			hasRet = returnType.ValueType >= firstHidden && returnType.ValueType <= ValueType.Matrix4x4;
		}
		
		int paramCount = parameterTypes.Length;
		int objectCount = parameterTypes.Select(t => t.ValueType is >= ValueType._ObjectStart and <= ValueType._ObjectEnd).Count();
		
		if (hasRet)
		{
			++objectCount;
			++paramCount;
		}

		JitCall call = new JitCall(funcAddress, parameterTypes, returnType);
		if (call.Function == null)
		{
			throw new InvalidOperationException($"{methodInfo.Name} (jit error: {call.Error})");
		}
		
		return parameters =>
		{
			int index = 0, pin = 0, handle = 0;
			GCHandle* pins = stackalloc GCHandle[paramCount];
			(nint, ValueType)* handlers = stackalloc (nint, ValueType)[objectCount];
			nint* @params = stackalloc nint[paramCount];
			nint* @return = stackalloc nint[2]{ 0, 0 }; // 128bits to fit Vector4

			object? ret = null;

			try
			{
				#region Allocate Memory for Return

				ValueType retType = returnType.ValueType;
				if (hasRet)
				{
					var obj = CreateReturnStorage(retType);
					nint ptr = Pin(ref obj, ref pins[pin++]);
					handlers[handle++] = (ptr, retType);
					@params[index++] = ptr;
				}

				#endregion

				#region Set Parameters

				for (int i = 0; i < parameters.Length; i++)
				{
					object paramValue = parameters[i];
					ManagedType paramType = parameterTypes[i];
					ValueType valueType = paramType.ValueType;

					if (paramType.IsByRef)
					{
						switch (valueType)
						{
							case ValueType.Bool:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Char8:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Char16:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Int8:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Int16:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Int32:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Int64:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.UInt8:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.UInt16:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.UInt32:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.UInt64:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Pointer:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Float:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Double:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Vector2:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Vector3:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Vector4:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Matrix4x4:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Function:
							{
								object ptr = GetFunctionPointerForDelegate((Delegate)paramValue);
								@params[index++] = Pin(ref ptr, ref pins[pin++]);
								break;
							}
							case ValueType.String:
							{
								object tmp = NativeMethods.ConstructString((string)paramValue);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.Any:
							{
								object tmp = NativeMethods.ConstructVariant(paramValue);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayBool:
							{
								var arr = (Bool8[])paramValue;
								object tmp = NativeMethods.ConstructVectorBool(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayChar8:
							{
								var arr = (Char8[])paramValue;
								object tmp = NativeMethods.ConstructVectorChar8(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayChar16:
							{
								var arr = (Char16[])paramValue;
								object tmp = NativeMethods.ConstructVectorChar16(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayInt8:
							{
								var arr = (sbyte[])paramValue;
								object tmp = NativeMethods.ConstructVectorInt8(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayInt16:
							{
								var arr = (short[])paramValue;
								object tmp = NativeMethods.ConstructVectorInt16(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayInt32:
							{
								var arr = (int[])paramValue;
								object tmp = NativeMethods.ConstructVectorInt32(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayInt64:
							{
								var arr = (long[])paramValue;
								object tmp = NativeMethods.ConstructVectorInt64(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayUInt8:
							{
								var arr = (byte[])paramValue;
								object tmp = NativeMethods.ConstructVectorUInt8(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayUInt16:
							{
								var arr = (ushort[])paramValue;
								object tmp = NativeMethods.ConstructVectorUInt16(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayUInt32:
							{
								var arr = (uint[])paramValue;
								object tmp = NativeMethods.ConstructVectorUInt32(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayUInt64:
							{
								var arr = (ulong[])paramValue;
								object tmp = NativeMethods.ConstructVectorUInt64(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayPointer:
							{
								var arr = (nint[])paramValue;
								object tmp = NativeMethods.ConstructVectorIntPtr(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayFloat:
							{
								var arr = (float[])paramValue;
								object tmp = NativeMethods.ConstructVectorFloat(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayDouble:
							{
								var arr = (double[])paramValue;
								object tmp = NativeMethods.ConstructVectorDouble(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayString:
							{
								var arr = (string[])paramValue;
								object tmp = NativeMethods.ConstructVectorString(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayAny:
							{
								var arr = (object?[])paramValue;
								object tmp = NativeMethods.ConstructVectorVariant(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayVector2:
							{
								var arr = (Vector2[])paramValue;
								object tmp = NativeMethods.ConstructVectorVector2(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayVector3:
							{
								var arr = (Vector3[])paramValue;
								object tmp = NativeMethods.ConstructVectorVector3(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayVector4:
							{
								var arr = (Vector4[])paramValue;
								object tmp = NativeMethods.ConstructVectorVector4(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayMatrix4x4:
							{
								var arr = (Matrix4x4[])paramValue;
								object tmp = NativeMethods.ConstructVectorMatrix4x4(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							default:
								throw new TypeNotFoundException($"Parameter '{parameterTypes[i].ValueType}' uses not supported type for marshalling!");
						}
					}
					else
					{
						switch (valueType)
						{
							case ValueType.Bool:
								@params[index++] = RCast((Bool8)paramValue);
								break;
							case ValueType.Char8:
								@params[index++] = RCast((Char8)paramValue);
								break;
							case ValueType.Char16:
								@params[index++] = RCast((Char16)paramValue);
								break;
							case ValueType.Int8:
								@params[index++] = RCast((sbyte)paramValue);
								break;
							case ValueType.Int16:
								@params[index++] = RCast((short)paramValue);
								break;
							case ValueType.Int32:
								@params[index++] = RCast((int)paramValue);
								break;
							case ValueType.Int64:
								@params[index++] = RCast((long)paramValue);
								break;
							case ValueType.UInt8:
								@params[index++] = RCast((byte)paramValue);
								break;
							case ValueType.UInt16:
								@params[index++] = RCast((ushort)paramValue);
								break;
							case ValueType.UInt32:
								@params[index++] = RCast((uint)paramValue);
								break;
							case ValueType.UInt64:
								@params[index++] = RCast((ulong)paramValue);
								break;
							case ValueType.Pointer:
								@params[index++] = (nint)paramValue;
								break;
							case ValueType.Float:
								@params[index++] = RCast((float)paramValue);
								break;
							case ValueType.Double:
								@params[index++] = RCast((double)paramValue);
								break;
							case ValueType.Vector2:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Vector3:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Vector4:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Matrix4x4:
								@params[index++] = Pin(ref paramValue, ref pins[pin++]);
								break;

							case ValueType.Function:
							{
								object ptr = GetFunctionPointerForDelegate((Delegate)paramValue);
								@params[index++] = Pin(ref ptr, ref pins[pin++]);
								break;
							}
							case ValueType.String:
							{
								object tmp = NativeMethods.ConstructString((string)paramValue);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.Any:
							{
								object tmp = NativeMethods.ConstructVariant(paramValue);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayBool:
							{
								var arr = (Bool8[])paramValue;
								object tmp = NativeMethods.ConstructVectorBool(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayChar8:
							{
								var arr = (Char8[])paramValue;
								object tmp = NativeMethods.ConstructVectorChar8(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayChar16:
							{
								var arr = (Char16[])paramValue;
								object tmp = NativeMethods.ConstructVectorChar16(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayInt8:
							{
								var arr = (sbyte[])paramValue;
								object tmp = NativeMethods.ConstructVectorInt8(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayInt16:
							{
								var arr = (short[])paramValue;
								object tmp = NativeMethods.ConstructVectorInt16(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayInt32:
							{
								var arr = (int[])paramValue;
								object tmp = NativeMethods.ConstructVectorInt32(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayInt64:
							{
								var arr = (long[])paramValue;
								object tmp = NativeMethods.ConstructVectorInt64(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayUInt8:
							{
								var arr = (byte[])paramValue;
								object tmp = NativeMethods.ConstructVectorUInt8(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayUInt16:
							{
								var arr = (ushort[])paramValue;
								object tmp = NativeMethods.ConstructVectorUInt16(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayUInt32:
							{
								var arr = (uint[])paramValue;
								object tmp = NativeMethods.ConstructVectorUInt32(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayUInt64:
							{
								var arr = (ulong[])paramValue;
								object tmp = NativeMethods.ConstructVectorUInt64(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayPointer:
							{
								var arr = (nint[])paramValue;
								object tmp = NativeMethods.ConstructVectorIntPtr(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayFloat:
							{
								var arr = (float[])paramValue;
								object tmp = NativeMethods.ConstructVectorFloat(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayDouble:
							{
								var arr = (double[])paramValue;
								object tmp = NativeMethods.ConstructVectorDouble(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayString:
							{
								var arr = (string[])paramValue;
								object tmp = NativeMethods.ConstructVectorString(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayAny:
							{
								var arr = (object?[])paramValue;
								object tmp = NativeMethods.ConstructVectorVariant(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayVector2:
							{
								var arr = (Vector2[])paramValue;
								object tmp = NativeMethods.ConstructVectorVector2(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayVector3:
							{
								var arr = (Vector3[])paramValue;
								object tmp = NativeMethods.ConstructVectorVector3(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayVector4:
							{
								var arr = (Vector4[])paramValue;
								object tmp = NativeMethods.ConstructVectorVector4(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							case ValueType.ArrayMatrix4x4:
							{
								var arr = (Matrix4x4[])paramValue;
								object tmp = NativeMethods.ConstructVectorMatrix4x4(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = ptr;
								break;
							}
							default:
								throw new TypeNotFoundException($"Parameter '{parameterTypes[i].ValueType}' uses not supported type for marshalling!");
						}
					}
				}

				#endregion

				#region Call Function

				call.Function(@params, @return);

				switch (retType)
				{
					case ValueType.Void:
						break;
					case ValueType.Bool:
						ret = *(Bool8*)@return;
						break;
					case ValueType.Char8:
						ret = *(Char8*)@return;
						break;
					case ValueType.Char16:
						ret = *(Char16*)@return;
						break;
					case ValueType.Int8:
						ret = *(sbyte*)@return;
						break;
					case ValueType.Int16:
						ret = *(short*)@return;
						break;
					case ValueType.Int32:
						ret = *(int*)@return;
						break;
					case ValueType.Int64:
						ret = *(long*)@return;
						break;
					case ValueType.UInt8:
						ret = *(byte*)@return;
						break;
					case ValueType.UInt16:
						ret = *(ushort*)@return;
						break;
					case ValueType.UInt32:
						ret = *(uint*)@return;
						break;
					case ValueType.UInt64:
						ret = *(ulong*)@return;
						break;
					case ValueType.Pointer:
						ret = *(nint*)@return;
						break;
					case ValueType.Float:
						ret = *(float*)@return;
						break;
					case ValueType.Double:
						ret = *(double*)@return;
						break;
					case ValueType.Function:
						ret = GetDelegateForFunctionPointer(*(nint*)@return, methodInfo.ReturnType);
						break;
					case ValueType.Vector2:
						ret = *(Vector2*)@return;
						break;
					case ValueType.Vector3:
						if (hasRet)
							ret = *(Vector3*)@return[0];
						else
							ret = *(Vector3*)@return;
						break;
					case ValueType.Vector4:
						if (hasRet)
							ret = *(Vector4*)@return[0];
						else
							ret = *(Vector4*)@return;
						break;
					case ValueType.Matrix4x4:
						ret = *(Matrix4x4*)@return[0];
						break;
					case ValueType.String:
						ret = NativeMethods.GetStringData((String192*)@return[0]);
						break;
					case ValueType.Any:
						ret = NativeMethods.GetVariantData((Variant256*)@return[0]);
						break;
					case ValueType.ArrayBool:
						ret = NativeMethods.GetVectorDataBool((Vector192*)@return[0]);
						break;
					case ValueType.ArrayChar8:
						ret = NativeMethods.GetVectorDataChar8((Vector192*)@return[0]);
						break;
					case ValueType.ArrayChar16:
						ret = NativeMethods.GetVectorDataChar16((Vector192*)@return[0]);
						break;
					case ValueType.ArrayInt8:
						ret = NativeMethods.GetVectorDataInt8((Vector192*)@return[0]);
						break;
					case ValueType.ArrayInt16:
						ret = NativeMethods.GetVectorDataInt16((Vector192*)@return[0]);
						break;
					case ValueType.ArrayInt32:
						ret = NativeMethods.GetVectorDataInt32((Vector192*)@return[0]);
						break;
					case ValueType.ArrayInt64:
						ret = NativeMethods.GetVectorDataInt64((Vector192*)@return[0]);
						break;
					case ValueType.ArrayUInt8:
						ret = NativeMethods.GetVectorDataUInt8((Vector192*)@return[0]);
						break;
					case ValueType.ArrayUInt16:
						ret = NativeMethods.GetVectorDataUInt16((Vector192*)@return[0]);
						break;
					case ValueType.ArrayUInt32:
						ret = NativeMethods.GetVectorDataUInt32((Vector192*)@return[0]);
						break;
					case ValueType.ArrayUInt64:
						ret = NativeMethods.GetVectorDataUInt64((Vector192*)@return[0]);
						break;
					case ValueType.ArrayPointer:
						ret = NativeMethods.GetVectorDataIntPtr((Vector192*)@return[0]);
						break;
					case ValueType.ArrayFloat:
						ret = NativeMethods.GetVectorDataFloat((Vector192*)@return[0]);
						break;
					case ValueType.ArrayDouble:
						ret = NativeMethods.GetVectorDataDouble((Vector192*)@return[0]);
						break;
					case ValueType.ArrayString:
						ret = NativeMethods.GetVectorDataString((Vector192*)@return[0]);
						break;
					case ValueType.ArrayAny:
						ret = NativeMethods.GetVectorDataVariant((Vector192*)@return[0]);
						break;
					case ValueType.ArrayVector2:
						ret = NativeMethods.GetVectorDataVector2((Vector192*)@return[0]);
						break;
					case ValueType.ArrayVector3:
						ret = NativeMethods.GetVectorDataVector3((Vector192*)@return[0]);
						break;
					case ValueType.ArrayVector4:
						ret = NativeMethods.GetVectorDataVector4((Vector192*)@return[0]);
						break;
					case ValueType.ArrayMatrix4x4:
						ret = NativeMethods.GetVectorDataMatrix4x4((Vector192*)@return[0]);
						break;
					default:
						throw new TypeNotFoundException($"Return '{returnType.ValueType}' uses not supported type for marshalling!");
				}

				#endregion

				#region Pull Refererences Back

				if (hasRefs)
				{
					int j = hasRet ? 1 : 0; // skip first param if has return

					if (j < handle)
					{
						for (int i = 0; i < parameters.Length; i++)
						{
							//object paramValue = parameters[i];
							ManagedType paramType = parameterTypes[i];
							if (paramType.IsByRef)
							{
								switch (paramType.ValueType)
								{
									case ValueType.String:
										parameters[i] = NativeMethods.GetStringData((String192*)handlers[j++].Item1);
										break;
									case ValueType.Any:
										parameters[i] = NativeMethods.GetVariantData((Variant256*)handlers[j++].Item1);
										break;
									case ValueType.ArrayBool:
										parameters[i] = NativeMethods.GetVectorDataBool((Vector192*)handlers[j++].Item1);
										break;
									case ValueType.ArrayChar8:
										parameters[i] = NativeMethods.GetVectorDataChar8((Vector192*)handlers[j++].Item1);
										break;
									case ValueType.ArrayChar16:
										parameters[i] = NativeMethods.GetVectorDataChar16((Vector192*)handlers[j++].Item1);
										break;
									case ValueType.ArrayInt8:
										parameters[i] = NativeMethods.GetVectorDataInt8((Vector192*)handlers[j++].Item1);
										break;
									case ValueType.ArrayInt16:
										parameters[i] = NativeMethods.GetVectorDataInt16((Vector192*)handlers[j++].Item1);
										break;
									case ValueType.ArrayInt32:
										parameters[i] = NativeMethods.GetVectorDataInt32((Vector192*)handlers[j++].Item1);
										break;
									case ValueType.ArrayInt64:
										parameters[i] = NativeMethods.GetVectorDataInt64((Vector192*)handlers[j++].Item1);
										break;
									case ValueType.ArrayUInt8:
										parameters[i] = NativeMethods.GetVectorDataUInt8((Vector192*)handlers[j++].Item1);
										break;
									case ValueType.ArrayUInt16:
										parameters[i] = NativeMethods.GetVectorDataUInt16((Vector192*)handlers[j++].Item1);
										break;
									case ValueType.ArrayUInt32:
										parameters[i] = NativeMethods.GetVectorDataUInt32((Vector192*)handlers[j++].Item1);
										break;
									case ValueType.ArrayUInt64:
										parameters[i] = NativeMethods.GetVectorDataUInt64((Vector192*)handlers[j++].Item1);
										break;
									case ValueType.ArrayPointer:
										parameters[i] = NativeMethods.GetVectorDataIntPtr((Vector192*)handlers[j++].Item1);
										break;
									case ValueType.ArrayFloat:
										parameters[i] = NativeMethods.GetVectorDataFloat((Vector192*)handlers[j++].Item1);
										break;
									case ValueType.ArrayDouble:
										parameters[i] =  NativeMethods.GetVectorDataDouble((Vector192*)handlers[j++].Item1);
										break;
									case ValueType.ArrayString:
										parameters[i] = NativeMethods.GetVectorDataString((Vector192*)handlers[j++].Item1);
										break;
									case ValueType.ArrayAny:
										parameters[i] = NativeMethods.GetVectorDataVariant((Vector192*)handlers[j++].Item1);
										break;
									case ValueType.ArrayVector2:
										parameters[i] = NativeMethods.GetVectorDataVector2((Vector192*)handlers[j++].Item1);
										break;
									case ValueType.ArrayVector3:
										parameters[i] = NativeMethods.GetVectorDataVector3((Vector192*)handlers[j++].Item1);
										break;
									case ValueType.ArrayVector4:
										parameters[i] = NativeMethods.GetVectorDataVector4((Vector192*)handlers[j++].Item1);
										break;
									case ValueType.ArrayMatrix4x4:
										parameters[i] = NativeMethods.GetVectorDataMatrix4x4((Vector192*)handlers[j++].Item1);
										break;
								}
							}

							if (j >= handle)
								break;
						}
					}
				}
				#endregion

			}
			
			#region Cleanup Temp Storage
			
			finally 
			{
				DestroyStorage(handlers, handle);

				for (int i = 0; i < pin; ++i)
				{
					pins[i].Free();
				}
			}

			#endregion

			return ret;
		};
	}

	private static object CreateReturnStorage(ValueType retType)
	{
		switch (retType)
		{
			case ValueType.String:
				return new String192();
			case ValueType.Any:
				return new Variant256();
			case ValueType.ArrayBool:
			case ValueType.ArrayChar8:
			case ValueType.ArrayChar16:
			case ValueType.ArrayInt8:
			case ValueType.ArrayInt16:
			case ValueType.ArrayInt32:
			case ValueType.ArrayInt64:
			case ValueType.ArrayUInt8:
			case ValueType.ArrayUInt16:
			case ValueType.ArrayUInt32:
			case ValueType.ArrayUInt64:
			case ValueType.ArrayPointer:
			case ValueType.ArrayFloat:
			case ValueType.ArrayDouble:
			case ValueType.ArrayString: 
			case ValueType.ArrayAny: 
			case ValueType.ArrayVector2: 
			case ValueType.ArrayVector3: 
			case ValueType.ArrayVector4: 
			case ValueType.ArrayMatrix4x4: 
				return new Vector192();
			case ValueType.Vector2:
				return new Vector2();
			case ValueType.Vector3:
				return new Vector3();
			case ValueType.Vector4:
				return new Vector4();
			case ValueType.Matrix4x4:
				return new Matrix4x4();
			default:
				throw new TypeNotFoundException();
		}
	}

	private static nint Pin(ref object paramValue, ref GCHandle handle)
	{
		handle = GCHandle.Alloc(paramValue, GCHandleType.Pinned);
		return handle.AddrOfPinnedObject();
	}

	private static unsafe void DestroyStorage((nint, ValueType)* handlers, int count)
	{
		for (int i = 0; i < count; i++)
		{
			var (ptr, type) = handlers[i];
			switch (type)
			{
				case ValueType.Vector2:
				case ValueType.Vector3:
				case ValueType.Vector4:
				case ValueType.Matrix4x4:
					// no dtor
					break;
				case ValueType.String:
					NativeMethods.DestroyString((String192*)ptr);
					break;
				case ValueType.Any:
					NativeMethods.DestroyVariant((Variant256*)ptr);
					break;
				case ValueType.ArrayBool:
					NativeMethods.DestroyVectorBool((Vector192*)ptr);
					break;
				case ValueType.ArrayChar8:
					NativeMethods.DestroyVectorChar8((Vector192*)ptr);
					break;
				case ValueType.ArrayChar16:
					NativeMethods.DestroyVectorChar16((Vector192*)ptr);
					break;
				case ValueType.ArrayInt8:
					NativeMethods.DestroyVectorInt8((Vector192*)ptr);
					break;
				case ValueType.ArrayInt16:
					NativeMethods.DestroyVectorInt16((Vector192*)ptr);
					break;
				case ValueType.ArrayInt32:
					NativeMethods.DestroyVectorInt32((Vector192*)ptr);
					break;
				case ValueType.ArrayInt64:
					NativeMethods.DestroyVectorInt64((Vector192*)ptr);
					break;
				case ValueType.ArrayUInt8:
					NativeMethods.DestroyVectorUInt8((Vector192*)ptr);
					break;
				case ValueType.ArrayUInt16:
					NativeMethods.DestroyVectorUInt16((Vector192*)ptr);
					break;
				case ValueType.ArrayUInt32:
					NativeMethods.DestroyVectorUInt32((Vector192*)ptr);
					break;
				case ValueType.ArrayUInt64:
					NativeMethods.DestroyVectorUInt64((Vector192*)ptr);
					break;
				case ValueType.ArrayPointer:
					NativeMethods.DestroyVectorIntPtr((Vector192*)ptr);
					break;
				case ValueType.ArrayFloat:
					NativeMethods.DestroyVectorFloat((Vector192*)ptr);
					break;
				case ValueType.ArrayDouble:
					NativeMethods.DestroyVectorDouble((Vector192*)ptr);
					break;
				case ValueType.ArrayString:
					NativeMethods.DestroyVectorString((Vector192*)ptr);
					break;
				case ValueType.ArrayAny:
					NativeMethods.DestroyVectorVariant((Vector192*)ptr);
					break;
				case ValueType.ArrayVector2:
					NativeMethods.DestroyVectorVector2((Vector192*)ptr);
					break;
				case ValueType.ArrayVector3:
					NativeMethods.DestroyVectorVector3((Vector192*)ptr);
					break;
				case ValueType.ArrayVector4:
					NativeMethods.DestroyVectorVector4((Vector192*)ptr);
					break;
				case ValueType.ArrayMatrix4x4:
					NativeMethods.DestroyVectorMatrix4x4((Vector192*)ptr);
					break;
				default:
					throw new TypeNotFoundException();
			}
		}
	}

	public static nint GetFunctionPointerForDelegate(Delegate d)
	{
		if (CachedDelegates.TryGetValue(d, out var callback))
		{
			return callback?.Function ?? Marshal.GetFunctionPointerForDelegate(d);
		}

		MethodInfo methodInfo = d.Method;
		if (IsNeedMarshal(methodInfo.ReturnType) || methodInfo.GetParameters().Any(p => IsNeedMarshal(p.ParameterType)))
		{
			callback = new JitCallback(d);

			nint function = callback.Function;
			if (function == nint.Zero)
			{
				throw new InvalidOperationException($"{methodInfo.Name} (jit error: {callback.Error})");
			}
			
			CachedDelegates.Add(d, callback);
			return function;
		}

		// We must manually keep the delegate from being collected by the garbage collector from managed code.
		CachedDelegates.Add(d, null);
		return Marshal.GetFunctionPointerForDelegate(d);
	}

	private static bool IsNeedMarshal(Type paramType)
	{
		ValueType valueType = TypeUtils.ConvertToValueType(paramType);
		if (valueType == ValueType.Function)
		{
			var methodInfo = paramType.GetMethod("Invoke")!;
			if (IsNeedMarshal(methodInfo.ReturnType) || methodInfo.GetParameters().Any(p => IsNeedMarshal(p.ParameterType)))
			{
				return true;
			}
		}

		return valueType is >= ValueType._ObjectStart and <= ValueType._ObjectEnd;
	}
}
