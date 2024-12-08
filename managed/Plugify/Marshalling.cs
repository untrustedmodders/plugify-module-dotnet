using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Plugify;

public static class Marshalling
{
	internal static readonly Dictionary<Delegate, JitCallback> CachedDelegates = new();
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

			if (paramType.IsEnum)
			{
				Type underlyingType = Enum.GetUnderlyingType(paramType);
				parameters[i] = Enum.ToObject(paramType, MarshalPointer(paramPtr, underlyingType));
			} 
			else
			{
				parameters[i] = MarshalPointer(paramPtr, paramType);
			}
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
		if (paramType.IsEnum)
		{
			// If returnType is an enum we need to get the underlying type and cast the value to it
			paramType = Enum.GetUnderlyingType(paramType);
			paramValue = Convert.ChangeType(paramValue, paramType);
		}
		
		switch (TypeUtils.ConvertToValueType(paramType))
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
				if (paramValue != null)
				{
					*(nint*)outValue = GetFunctionPointerForDelegate((Delegate)paramValue);
				}
				else
				{
					*(nint*)outValue = default;
				}
				return;
			case ValueType.String:
				if (paramValue is string str) NativeMethods.AssignString((String192*)outValue, str);
				return;
			case ValueType.Any:
				var var = (Variant256*)outValue;
				NativeMethods.DestroyVariant(var);
				NativeMethods.SetVariantData(var, paramValue);
				return;
			case ValueType.ArrayBool:
				if (paramValue is Bool8[] arrBool) NativeMethods.AssignVectorBool((Vector192*)outValue, arrBool, arrBool.Length);
				return;
			case ValueType.ArrayChar8:
				if (paramValue is Char8[] arrChar8) NativeMethods.AssignVectorChar8((Vector192*)outValue, arrChar8, arrChar8.Length);
				return;
			case ValueType.ArrayChar16:
				if (paramValue is Char16[] arrChar16) NativeMethods.AssignVectorChar16((Vector192*)outValue, arrChar16, arrChar16.Length);
				return;
			case ValueType.ArrayInt8:
				if (paramValue is sbyte[] arrInt8) NativeMethods.AssignVectorInt8((Vector192*)outValue, arrInt8, arrInt8.Length);
				return;
			case ValueType.ArrayInt16:
				if (paramValue is short[] arrInt16) NativeMethods.AssignVectorInt16((Vector192*)outValue, arrInt16, arrInt16.Length);
				return;
			case ValueType.ArrayInt32:
				if (paramValue is int[] arrInt32) NativeMethods.AssignVectorInt32((Vector192*)outValue, arrInt32, arrInt32.Length);
				return;
			case ValueType.ArrayInt64:
				if (paramValue is long[] arrInt64) NativeMethods.AssignVectorInt64((Vector192*)outValue, arrInt64, arrInt64.Length);
				return;
			case ValueType.ArrayUInt8:
				if (paramValue is byte[] arrUInt8) NativeMethods.AssignVectorUInt8((Vector192*)outValue, arrUInt8, arrUInt8.Length);
				return;
			case ValueType.ArrayUInt16:
				if (paramValue is ushort[] arrUInt16) NativeMethods.AssignVectorUInt16((Vector192*)outValue, arrUInt16, arrUInt16.Length);
				return;
			case ValueType.ArrayUInt32:
				if (paramValue is uint[] arrUInt32) NativeMethods.AssignVectorUInt32((Vector192*)outValue, arrUInt32, arrUInt32.Length);
				return;
			case ValueType.ArrayUInt64:
				if (paramValue is ulong[] arrUInt64) NativeMethods.AssignVectorUInt64((Vector192*)outValue, arrUInt64, arrUInt64.Length);
				return;
			case ValueType.ArrayPointer:
				if (paramValue is nint[] arrIntPtr) NativeMethods.AssignVectorIntPtr((Vector192*)outValue, arrIntPtr, arrIntPtr.Length);
				return;
			case ValueType.ArrayFloat:
				if (paramValue is float[] arrFloat) NativeMethods.AssignVectorFloat((Vector192*)outValue, arrFloat, arrFloat.Length);
				return;
			case ValueType.ArrayDouble:
				if (paramValue is double[] arrDouble) NativeMethods.AssignVectorDouble((Vector192*)outValue, arrDouble, arrDouble.Length);
				return;
			case ValueType.ArrayString:
				if (paramValue is string[] arrString) NativeMethods.AssignVectorString((Vector192*)outValue, arrString, arrString.Length);
				return;
			case ValueType.ArrayAny:
				if (paramValue is object[] arrVariant) NativeMethods.AssignVectorVariant((Vector192*)outValue, arrVariant, arrVariant.Length);
				return;
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
				return NativeMethods.GetVariantData((Variant256*)inValue, paramType);
			case ValueType.ArrayBool:
				var ptrBool = (Vector192*)inValue;
				var arrBool = new Bool8[NativeMethods.GetVectorSizeBool(ptrBool)];
				NativeMethods.GetVectorDataBool(ptrBool, arrBool);
				return arrBool;
			case ValueType.ArrayChar8:
				var ptrChar8 = (Vector192*)inValue;
				var arrChar8 = new Char8[NativeMethods.GetVectorSizeChar8(ptrChar8)];
				NativeMethods.GetVectorDataChar8(ptrChar8, arrChar8);
				return arrChar8;
			case ValueType.ArrayChar16:
				var ptrChar16 = (Vector192*)inValue;
				var arrChar16 = new Char16[NativeMethods.GetVectorSizeChar16(ptrChar16)];
				NativeMethods.GetVectorDataChar16(ptrChar16, arrChar16);
				return arrChar16;
			case ValueType.ArrayInt8:
				var ptrInt8 = (Vector192*)inValue;
				var arrInt8 = new sbyte[NativeMethods.GetVectorSizeInt8(ptrInt8)];
				NativeMethods.GetVectorDataInt8(ptrInt8, arrInt8);
				return arrInt8;
			case ValueType.ArrayInt16:
				var ptrInt16 = (Vector192*)inValue;
				var arrInt16 = new short[NativeMethods.GetVectorSizeInt16(ptrInt16)];
				NativeMethods.GetVectorDataInt16(ptrInt16, arrInt16);
				return arrInt16;
			case ValueType.ArrayInt32:
				var ptrInt32 = (Vector192*)inValue;
				var arrInt32 = new int[NativeMethods.GetVectorSizeInt32(ptrInt32)];
				NativeMethods.GetVectorDataInt32(ptrInt32, arrInt32);
				return arrInt32;
			case ValueType.ArrayInt64:
				var ptrInt64 = (Vector192*)inValue;
				var arrInt64 = new long[NativeMethods.GetVectorSizeInt64(ptrInt64)];
				NativeMethods.GetVectorDataInt64(ptrInt64, arrInt64);
				return arrInt64;
			case ValueType.ArrayUInt8:
				var ptrUInt8 = (Vector192*)inValue;
				var arrUInt8 = new byte[NativeMethods.GetVectorSizeUInt8(ptrUInt8)];
				NativeMethods.GetVectorDataUInt8(ptrUInt8, arrUInt8);
				return arrUInt8;
			case ValueType.ArrayUInt16:
				var ptrUInt16 = (Vector192*)inValue;
				var arrUInt16 = new ushort[NativeMethods.GetVectorSizeUInt16(ptrUInt16)];
				NativeMethods.GetVectorDataUInt16(ptrUInt16, arrUInt16);
				return arrUInt16;
			case ValueType.ArrayUInt32:
				var ptrUInt32 = (Vector192*)inValue;
				var arrUInt32 = new uint[NativeMethods.GetVectorSizeUInt32(ptrUInt32)];
				NativeMethods.GetVectorDataUInt32(ptrUInt32, arrUInt32);
				return arrUInt32;
			case ValueType.ArrayUInt64:
				var ptrUInt64 = (Vector192*)inValue;
				var arrUInt64 = new ulong[NativeMethods.GetVectorSizeUInt64(ptrUInt64)];
				NativeMethods.GetVectorDataUInt64(ptrUInt64, arrUInt64);
				return arrUInt64;
			case ValueType.ArrayPointer:
				var ptrIntPtr = (Vector192*)inValue;
				var arrIntPtr = new nint[NativeMethods.GetVectorSizeIntPtr(ptrIntPtr)];
				NativeMethods.GetVectorDataIntPtr(ptrIntPtr, arrIntPtr);
				return arrIntPtr;
			case ValueType.ArrayFloat:
				var ptrFloat = (Vector192*)inValue;
				var arrFloat = new float[NativeMethods.GetVectorSizeFloat(ptrFloat)];
				NativeMethods.GetVectorDataFloat(ptrFloat, arrFloat);
				return arrFloat;
			case ValueType.ArrayDouble:
				var ptrDouble = (Vector192*)inValue;
				var arrDouble = new double[NativeMethods.GetVectorSizeDouble(ptrDouble)];
				NativeMethods.GetVectorDataDouble(ptrDouble, arrDouble);
				return arrDouble;
			case ValueType.ArrayString:
				var ptrString = (Vector192*)inValue;
				var arrString = new string[NativeMethods.GetVectorSizeString(ptrString)];
				NativeMethods.GetVectorDataString(ptrString, arrString);
				return arrString;
			case ValueType.ArrayAny:
				var ptrVariant = (Vector192*)inValue;
				var arrVariant = new object[NativeMethods.GetVectorSizeVariant(ptrVariant)];
				NativeMethods.GetVectorDataVariant(ptrVariant, arrVariant);
				return arrVariant;
			case ValueType.Vector2:
				return *(Vector2*)inValue;
			case ValueType.Vector3:
				return *(Vector3*)inValue;
			case ValueType.Vector4:
				return *(Vector4*)inValue;
			case ValueType.Matrix4x4:
				return *(Matrix4x4*)inValue;
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
		if (CachedFunctions.TryGetValue(funcAddress, out var @delegate))
		{
			return @delegate;
		}

		if (delegateType == null)
		{
			throw new Exception("Type required to properly generate delegate at runtime");
		}

		MethodInfo methodInfo = delegateType.GetMethod("Invoke")!;
		if (IsNeedMarshal(methodInfo.ReturnType) || methodInfo.GetParameters().Any(p => IsNeedMarshal(p.ParameterType)))
		{
			@delegate = MethodUtils.CreateObjectArrayDelegate(delegateType, ExternalInvoke(funcAddress, methodInfo));
			CachedFunctions.Add(funcAddress, @delegate);
			return @delegate;
		}

		return Marshal.GetDelegateForFunctionPointer(funcAddress, delegateType);
	}

	private static unsafe nint RCast<T>(T primitive) where T : struct
	{
		return *(nint*)&primitive;
	}
	
	private static readonly bool X86 = RuntimeInformation.ProcessArchitecture == Architecture.X64 || RuntimeInformation.ProcessArchitecture == Architecture.X86;
	
	private static unsafe Func<object[], object> ExternalInvoke(nint funcAddress, MethodInfo methodInfo)
	{
		ParameterInfo returnInfo = methodInfo.ReturnParameter;
		ParameterInfo[] parameterInfos = methodInfo.GetParameters();
		
		ManagedType returnType =  new ManagedType(returnInfo.ParameterType);
		ManagedType[] parameterTypes = parameterInfos.Select(p => new ManagedType(p.ParameterType)).ToArray();
		
		bool hasRet = returnType.ValueType is >= ValueType._ObjectStart and <= ValueType._ObjectEnd;
		bool hasRefs = parameterTypes.Any(t => t.IsByRef);
		
		if (!hasRet)
		{
			ValueType firstHidden = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && X86 ? ValueType.Vector3 : ValueType.Matrix4x4;
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
			throw new InvalidOperationException($"Method '{methodInfo.Name}' has JIT generation error: {call.Error}");
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
							default:
								throw new TypeNotFoundException($"Parameter '{parameterInfos[i]}' uses not supported type for marshalling!");
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
							default:
								throw new TypeNotFoundException($"Parameter '{parameterInfos[i]}' uses not supported type for marshalling!");
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
						{
							ret = *(Vector3*)@return[0];
						}
						else
						{
							ret = *(Vector3*)@return;
						}
						break;
					case ValueType.Vector4:
						if (hasRet)
						{
							ret = *(Vector4*)@return[0];
						}
						else
						{
							ret = *(Vector4*)@return;
						}
						break;
					case ValueType.Matrix4x4:
						ret = *(Matrix4x4*)@return[0];
						break;
					case ValueType.String:
					{
						String192* ptr = (String192*)@return[0];
						ret = NativeMethods.GetStringData(ptr);
						break;
					}
					case ValueType.Any:
					{
						Variant256* ptr = (Variant256*)@return[0];
						ret = NativeMethods.GetVariantData(ptr, returnInfo.ParameterType);
						break;
					}
					case ValueType.ArrayBool:
					{
						Vector192* ptr = (Vector192*)@return[0];
						var arr = new Bool8[NativeMethods.GetVectorSizeBool(ptr)];
						NativeMethods.GetVectorDataBool(ptr, arr);
						ret = arr;
						break;
					}
					case ValueType.ArrayChar8:
					{
						Vector192* ptr = (Vector192*)@return[0];
						var arr = new Char8[NativeMethods.GetVectorSizeChar8(ptr)];
						NativeMethods.GetVectorDataChar8(ptr, arr);
						ret = arr;
						break;
					}
					case ValueType.ArrayChar16:
					{
						Vector192* ptr = (Vector192*)@return[0];
						var arr = new Char16[NativeMethods.GetVectorSizeChar16(ptr)];
						NativeMethods.GetVectorDataChar16(ptr, arr);
						ret = arr;
						break;
					}
					case ValueType.ArrayInt8:
					{
						Vector192* ptr = (Vector192*)@return[0];
						var arr = new sbyte[NativeMethods.GetVectorSizeInt8(ptr)];
						NativeMethods.GetVectorDataInt8(ptr, arr);
						ret = arr;
						break;
					}
					case ValueType.ArrayInt16:
					{
						Vector192* ptr = (Vector192*)@return[0];
						var arr = new short[NativeMethods.GetVectorSizeInt16(ptr)];
						NativeMethods.GetVectorDataInt16(ptr, arr);
						ret = arr;
						break;
					}
					case ValueType.ArrayInt32:
					{
						Vector192* ptr = (Vector192*)@return[0];
						var arr = new int[NativeMethods.GetVectorSizeInt32(ptr)];
						NativeMethods.GetVectorDataInt32(ptr, arr);
						ret = arr;
						break;
					}
					case ValueType.ArrayInt64:
					{
						Vector192* ptr = (Vector192*)@return[0];
						var arr = new long[NativeMethods.GetVectorSizeInt64(ptr)];
						NativeMethods.GetVectorDataInt64(ptr, arr);
						ret = arr;
						break;
					}
					case ValueType.ArrayUInt8:
					{
						Vector192* ptr = (Vector192*)@return[0];
						var arr = new byte[NativeMethods.GetVectorSizeUInt8(ptr)];
						NativeMethods.GetVectorDataUInt8(ptr, arr);
						ret = arr;
						break;
					}
					case ValueType.ArrayUInt16:
					{
						Vector192* ptr = (Vector192*)@return[0];
						var arr = new ushort[NativeMethods.GetVectorSizeUInt16(ptr)];
						NativeMethods.GetVectorDataUInt16(ptr, arr);
						ret = arr;
						break;
					}
					case ValueType.ArrayUInt32:
					{
						Vector192* ptr = (Vector192*)@return[0];
						var arr = new uint[NativeMethods.GetVectorSizeUInt32(ptr)];
						NativeMethods.GetVectorDataUInt32(ptr, arr);
						ret = arr;
						break;
					}
					case ValueType.ArrayUInt64:
					{
						Vector192* ptr = (Vector192*)@return[0];
						var arr = new ulong[NativeMethods.GetVectorSizeUInt64(ptr)];
						NativeMethods.GetVectorDataUInt64(ptr, arr);
						ret = arr;
						break;
					}
					case ValueType.ArrayPointer:
					{
						Vector192* ptr = (Vector192*)@return[0];
						var arr = new nint[NativeMethods.GetVectorSizeIntPtr(ptr)];
						NativeMethods.GetVectorDataIntPtr(ptr, arr);
						ret = arr;
						break;
					}
					case ValueType.ArrayFloat:
					{
						Vector192* ptr = (Vector192*)@return[0];
						var arr = new float[NativeMethods.GetVectorSizeFloat(ptr)];
						NativeMethods.GetVectorDataFloat(ptr, arr);
						ret = arr;
						break;
					}
					case ValueType.ArrayDouble:
					{
						Vector192* ptr = (Vector192*)@return[0];
						var arr = new double[NativeMethods.GetVectorSizeDouble(ptr)];
						NativeMethods.GetVectorDataDouble(ptr, arr);
						ret = arr;
						break;
					}
					case ValueType.ArrayString:
					{
						Vector192* ptr = (Vector192*)@return[0];
						var arr = new string[NativeMethods.GetVectorSizeString(ptr)];
						NativeMethods.GetVectorDataString(ptr, arr);
						ret = arr;
						break;
					}
					case ValueType.ArrayAny:
					{
						Vector192* ptr = (Vector192*)@return[0];
						var arr = new object[NativeMethods.GetVectorSizeVariant(ptr)];
						NativeMethods.GetVectorDataVariant(ptr, arr);
						ret = arr;
						break;
					}
					default:
						throw new TypeNotFoundException($"Return '{returnInfo}' uses not supported type for marshalling!");
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
									{
										String192* ptr = (String192*)handlers[j++].Item1;
										parameters[i] = NativeMethods.GetStringData(ptr);
										break;
									}
									case ValueType.Any:
									{
										Variant256* ptr = (Variant256*)handlers[j++].Item1;
										parameters[i] = NativeMethods.GetVariantData(ptr, parameterInfos[i].ParameterType);
										break;
									}
									case ValueType.ArrayBool:
									{
										Vector192* ptr = (Vector192*)handlers[j++].Item1;
										var arr = new Bool8[NativeMethods.GetVectorSizeBool(ptr)];
										NativeMethods.GetVectorDataBool(ptr, arr);
										parameters[i] = arr;
										break;
									}
									case ValueType.ArrayChar8:
									{
										Vector192* ptr = (Vector192*)handlers[j++].Item1;
										var arr = new Char8[NativeMethods.GetVectorSizeChar8(ptr)];
										NativeMethods.GetVectorDataChar8(ptr, arr);
										parameters[i] = arr;
										break;
									}
									case ValueType.ArrayChar16:
									{
										Vector192* ptr = (Vector192*)handlers[j++].Item1;
										var arr = new Char16[NativeMethods.GetVectorSizeChar16(ptr)];
										NativeMethods.GetVectorDataChar16(ptr, arr);
										parameters[i] = arr;
										break;
									}
									case ValueType.ArrayInt8:
									{
										Vector192* ptr = (Vector192*)handlers[j++].Item1;
										var arr = new sbyte[NativeMethods.GetVectorSizeInt8(ptr)];
										NativeMethods.GetVectorDataInt8(ptr, arr);
										parameters[i] = arr;
										break;
									}
									case ValueType.ArrayInt16:
									{
										Vector192* ptr = (Vector192*)handlers[j++].Item1;
										var arr = new short[NativeMethods.GetVectorSizeInt16(ptr)];
										NativeMethods.GetVectorDataInt16(ptr, arr);
										parameters[i] = arr;
										break;
									}
									case ValueType.ArrayInt32:
									{
										Vector192* ptr = (Vector192*)handlers[j++].Item1;
										var arr = new int[NativeMethods.GetVectorSizeInt32(ptr)];
										NativeMethods.GetVectorDataInt32(ptr, arr);
										parameters[i] = arr;
										break;
									}
									case ValueType.ArrayInt64:
									{
										Vector192* ptr = (Vector192*)handlers[j++].Item1;
										var arr = new long[NativeMethods.GetVectorSizeInt64(ptr)];
										NativeMethods.GetVectorDataInt64(ptr, arr);
										parameters[i] = arr;
										break;
									}
									case ValueType.ArrayUInt8:
									{
										Vector192* ptr = (Vector192*)handlers[j++].Item1;
										var arr = new byte[NativeMethods.GetVectorSizeUInt8(ptr)];
										NativeMethods.GetVectorDataUInt8(ptr, arr);
										parameters[i] = arr;
										break;
									}
									case ValueType.ArrayUInt16:
									{
										Vector192* ptr = (Vector192*)handlers[j++].Item1;
										var arr = new ushort[NativeMethods.GetVectorSizeUInt16(ptr)];
										NativeMethods.GetVectorDataUInt16(ptr, arr);
										parameters[i] = arr;
										break;
									}
									case ValueType.ArrayUInt32:
									{
										Vector192* ptr = (Vector192*)handlers[j++].Item1;
										var arr = new uint[NativeMethods.GetVectorSizeUInt32(ptr)];
										NativeMethods.GetVectorDataUInt32(ptr, arr);
										parameters[i] = arr;
										break;
									}
									case ValueType.ArrayUInt64:
									{
										Vector192* ptr = (Vector192*)handlers[j++].Item1;
										var arr = new ulong[NativeMethods.GetVectorSizeUInt64(ptr)];
										NativeMethods.GetVectorDataUInt64(ptr, arr);
										parameters[i] = arr;
										break;
									}
									case ValueType.ArrayPointer:
									{
										Vector192* ptr = (Vector192*)handlers[j++].Item1;
										var arr = new nint[NativeMethods.GetVectorSizeIntPtr(ptr)];
										NativeMethods.GetVectorDataIntPtr(ptr, arr);
										parameters[i] = arr;
										break;
									}
									case ValueType.ArrayFloat:
									{
										Vector192* ptr = (Vector192*)handlers[j++].Item1;
										var arr = new float[NativeMethods.GetVectorSizeFloat(ptr)];
										NativeMethods.GetVectorDataFloat(ptr, arr);
										parameters[i] = arr;
										break;
									}
									case ValueType.ArrayDouble:
									{
										Vector192* ptr = (Vector192*)handlers[j++].Item1;
										var arr = new double[NativeMethods.GetVectorSizeDouble(ptr)];
										NativeMethods.GetVectorDataDouble(ptr, arr);
										parameters[i] = arr;
										break;
									}
									case ValueType.ArrayString:
									{
										Vector192* ptr = (Vector192*)handlers[j++].Item1;
										var arr = new string[NativeMethods.GetVectorSizeString(ptr)];
										NativeMethods.GetVectorDataString(ptr, arr);
										parameters[i] = arr;
										break;
									}
									case ValueType.ArrayAny:
									{
										Vector192* ptr = (Vector192*)handlers[j++].Item1;
										var arr = new object[NativeMethods.GetVectorSizeVariant(ptr)];
										NativeMethods.GetVectorDataVariant(ptr, arr);
										parameters[i] = arr;
										break;
									}
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
			{
				return new String192();
			}
			case ValueType.Any:
			{
				return new Variant256();
			}
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
			{
				return new Vector192();
			}
			case ValueType.Vector2:
			{
				return new Vector2();
			}
			case ValueType.Vector3:
			{
				return new Vector3();
			}
			case ValueType.Vector4:
			{
				return new Vector4();
			}
			case ValueType.Matrix4x4:
			{
				return new Matrix4x4();
			}
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
				default:
					throw new TypeNotFoundException();
			}
		}
	}

	public static nint GetFunctionPointerForDelegate(Delegate d)
	{
		if (CachedDelegates.TryGetValue(d, out var callback))
		{
			return callback.Function;
		}

		MethodInfo methodInfo = d.Method;
		if (IsNeedMarshal(methodInfo.ReturnType) || methodInfo.GetParameters().Any(p => IsNeedMarshal(p.ParameterType)))
		{
			callback = new JitCallback(d);

			nint function = callback.Function;
			if (function == nint.Zero)
			{
				throw new InvalidOperationException($"Method '{methodInfo.Name}' has JIT generation error: {callback.Error}");
			}
			
			CachedDelegates.Add(d, callback);
			return function;
		}

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
