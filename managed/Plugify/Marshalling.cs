﻿using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Plugify;

public static class Marshalling
{
	private static Dictionary<Delegate, JitCallback> CachedDelegates = new();
	private static Dictionary<nint, Delegate> CachedFunctions = new();

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
				if (paramValue is string str) NativeMethods.AssignString(outValue, str);
				return;
			case ValueType.ArrayBool:
				if (paramValue is Bool8[] arrBool) NativeMethods.AssignVectorBool(outValue, arrBool, arrBool.Length);
				return;
			case ValueType.ArrayChar8:
				if (paramValue is Char8[] arrChar8) NativeMethods.AssignVectorChar8(outValue, arrChar8, arrChar8.Length);
				return;
			case ValueType.ArrayChar16:
				if (paramValue is Char16[] arrChar16) NativeMethods.AssignVectorChar16(outValue, arrChar16, arrChar16.Length);
				return;
			case ValueType.ArrayInt8:
				if (paramValue is sbyte[] arrInt8) NativeMethods.AssignVectorInt8(outValue, arrInt8, arrInt8.Length);
				return;
			case ValueType.ArrayInt16:
				if (paramValue is short[] arrInt16) NativeMethods.AssignVectorInt16(outValue, arrInt16, arrInt16.Length);
				return;
			case ValueType.ArrayInt32:
				if (paramValue is int[] arrInt32) NativeMethods.AssignVectorInt32(outValue, arrInt32, arrInt32.Length);
				return;
			case ValueType.ArrayInt64:
				if (paramValue is long[] arrInt64) NativeMethods.AssignVectorInt64(outValue, arrInt64, arrInt64.Length);
				return;
			case ValueType.ArrayUInt8:
				if (paramValue is byte[] arrUInt8) NativeMethods.AssignVectorUInt8(outValue, arrUInt8, arrUInt8.Length);
				return;
			case ValueType.ArrayUInt16:
				if (paramValue is ushort[] arrUInt16) NativeMethods.AssignVectorUInt16(outValue, arrUInt16, arrUInt16.Length);
				return;
			case ValueType.ArrayUInt32:
				if (paramValue is uint[] arrUInt32) NativeMethods.AssignVectorUInt32(outValue, arrUInt32, arrUInt32.Length);
				return;
			case ValueType.ArrayUInt64:
				if (paramValue is ulong[] arrUInt64) NativeMethods.AssignVectorUInt64(outValue, arrUInt64, arrUInt64.Length);
				return;
			case ValueType.ArrayPointer:
				if (paramValue is nint[] arrIntPtr) NativeMethods.AssignVectorIntPtr(outValue, arrIntPtr, arrIntPtr.Length);
				return;
			case ValueType.ArrayFloat:
				if (paramValue is float[] arrFloat) NativeMethods.AssignVectorFloat(outValue, arrFloat, arrFloat.Length);
				return;
			case ValueType.ArrayDouble:
				if (paramValue is double[] arrDouble) NativeMethods.AssignVectorDouble(outValue, arrDouble, arrDouble.Length);
				return;
			case ValueType.ArrayString:
				if (paramValue is string[] arrString) NativeMethods.AssignVectorString(outValue, arrString, arrString.Length);
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
				return NativeMethods.GetStringData(inValue);
			case ValueType.ArrayBool:
				var arrBool = new Bool8[NativeMethods.GetVectorSizeBool(inValue)];
				NativeMethods.GetVectorDataBool(inValue, arrBool);
				return arrBool;
			case ValueType.ArrayChar8:
				var arrChar8 = new Char8[NativeMethods.GetVectorSizeChar8(inValue)];
				NativeMethods.GetVectorDataChar8(inValue, arrChar8);
				return arrChar8;
			case ValueType.ArrayChar16:
				var arrChar16 = new Char16[NativeMethods.GetVectorSizeChar16(inValue)];
				NativeMethods.GetVectorDataChar16(inValue, arrChar16);
				return arrChar16;
			case ValueType.ArrayInt8:
				var arrInt8 = new sbyte[NativeMethods.GetVectorSizeInt8(inValue)];
				NativeMethods.GetVectorDataInt8(inValue, arrInt8);
				return arrInt8;
			case ValueType.ArrayInt16:
				var arrInt16 = new short[NativeMethods.GetVectorSizeInt16(inValue)];
				NativeMethods.GetVectorDataInt16(inValue, arrInt16);
				return arrInt16;
			case ValueType.ArrayInt32:
				var arrInt32 = new int[NativeMethods.GetVectorSizeInt32(inValue)];
				NativeMethods.GetVectorDataInt32(inValue, arrInt32);
				return arrInt32;
			case ValueType.ArrayInt64:
				var arrInt64 = new long[NativeMethods.GetVectorSizeInt64(inValue)];
				NativeMethods.GetVectorDataInt64(inValue, arrInt64);
				return arrInt64;
			case ValueType.ArrayUInt8:
				var arrUInt8 = new byte[NativeMethods.GetVectorSizeUInt8(inValue)];
				NativeMethods.GetVectorDataUInt8(inValue, arrUInt8);
				return arrUInt8;
			case ValueType.ArrayUInt16:
				var arrUInt16 = new ushort[NativeMethods.GetVectorSizeUInt16(inValue)];
				NativeMethods.GetVectorDataUInt16(inValue, arrUInt16);
				return arrUInt16;
			case ValueType.ArrayUInt32:
				var arrUInt32 = new uint[NativeMethods.GetVectorSizeUInt32(inValue)];
				NativeMethods.GetVectorDataUInt32(inValue, arrUInt32);
				return arrUInt32;
			case ValueType.ArrayUInt64:
				var arrUInt64 = new ulong[NativeMethods.GetVectorSizeUInt64(inValue)];
				NativeMethods.GetVectorDataUInt64(inValue, arrUInt64);
				return arrUInt64;
			case ValueType.ArrayPointer:
				var arrIntPtr = new nint[NativeMethods.GetVectorSizeIntPtr(inValue)];
				NativeMethods.GetVectorDataIntPtr(inValue, arrIntPtr);
				return arrIntPtr;
			case ValueType.ArrayFloat:
				var arrFloat = new float[NativeMethods.GetVectorSizeFloat(inValue)];
				NativeMethods.GetVectorDataFloat(inValue, arrFloat);
				return arrFloat;
			case ValueType.ArrayDouble:
				var arrDouble = new double[NativeMethods.GetVectorSizeDouble(inValue)];
				NativeMethods.GetVectorDataDouble(inValue, arrDouble);
				return arrDouble;
			case ValueType.ArrayString:
				var arrString = new string[NativeMethods.GetVectorSizeString(inValue)];
				NativeMethods.GetVectorDataString(inValue, arrString);
				return arrString;
			case ValueType.Vector2:
			case ValueType.Vector3:
			case ValueType.Vector4:
			case ValueType.Matrix4x4:
			{
				return Marshal.PtrToStructure(inValue, paramType.IsByRef ? paramType.GetElementType() : paramType);
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
	
	public static Delegate GetDelegateForFunctionPointer(nint funcAddress, Type delegateType)
	{
		if (CachedFunctions.TryGetValue(funcAddress, out var @delegate))
		{
			return @delegate;
		}

		MethodInfo methodInfo = delegateType.GetMethod("Invoke");
		if (IsNeedMarshal(methodInfo.ReturnType) || methodInfo.GetParameters().Any(p => IsNeedMarshal(p.ParameterType)))
		{
			@delegate = MethodUtils.CreateObjectArrayDelegate(delegateType, ExternalInvoke(funcAddress, methodInfo));
			CachedFunctions.Add(funcAddress, @delegate);
			return @delegate;
		}

		return Marshal.GetDelegateForFunctionPointer(funcAddress, delegateType);
	}

	private static unsafe nint RCast<T>(T primitive)
	{
		return *(nint*)&primitive;
	}
	
	private static unsafe Func<object[], object> ExternalInvoke(nint funcAddress, MethodInfo methodInfo)
	{
		ManagedType returnType =  new ManagedType(methodInfo.ReturnType);
		ManagedType[] parameterTypes = methodInfo.GetParameters().Select(p => new ManagedType(p.ParameterType)).ToArray();

		bool hasRet = returnType.ValueType is >= ValueType.String and <= ValueType.ArrayString;
		bool hasRefs = parameterTypes.Any(t => t.IsByRef);

		if (!hasRet)
		{
			ValueType firstHidden = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? ValueType.Vector3 : ValueType.Matrix4x4;
			hasRet = returnType.ValueType >= firstHidden && returnType.ValueType <= ValueType.Matrix4x4;
		}

		JitCall call = new JitCall(funcAddress, parameterTypes, returnType);
		if (call.Function == null)
		{
			throw new InvalidOperationException($"Method '{methodInfo.Name}' has JIT generation error: {call.Error}");
		}

		return parameters =>
		{
			List<GCHandle> pins = [];
			List<(nint, ValueType)> handlers = [];

			nint* p = stackalloc nint[hasRet ? parameters.Length + 1 : parameters.Length];
			int index = 0;
			nint* r = stackalloc nint[2]{ 0, 0 };

			#region Allocate Memory for Return

			ValueType retType = returnType.ValueType;
			if (hasRet)
			{
				switch (retType)
				{
					case ValueType.String:
					{
						nint ptr = NativeMethods.AllocateString();
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					case ValueType.ArrayBool:
					{
						nint ptr = NativeMethods.AllocateVectorBool();
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					case ValueType.ArrayChar8:
					{
						nint ptr = NativeMethods.AllocateVectorChar8();
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					case ValueType.ArrayChar16:
					{
						nint ptr = NativeMethods.AllocateVectorChar16();
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					case ValueType.ArrayInt8:
					{
						nint ptr = NativeMethods.AllocateVectorInt8();
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					case ValueType.ArrayInt16:
					{
						nint ptr = NativeMethods.AllocateVectorInt16();
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					case ValueType.ArrayInt32:
					{
						nint ptr = NativeMethods.AllocateVectorInt32();
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					case ValueType.ArrayInt64:
					{
						nint ptr = NativeMethods.AllocateVectorInt64();
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					case ValueType.ArrayUInt8:
					{
						nint ptr = NativeMethods.AllocateVectorUInt8();
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					case ValueType.ArrayUInt16:
					{
						nint ptr = NativeMethods.AllocateVectorUInt16();
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					case ValueType.ArrayUInt32:
					{
						nint ptr = NativeMethods.AllocateVectorUInt32();
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					case ValueType.ArrayUInt64:
					{
						nint ptr = NativeMethods.AllocateVectorUInt64();
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					case ValueType.ArrayPointer:
					{
						nint ptr = NativeMethods.AllocateVectorIntPtr();
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					case ValueType.ArrayFloat:
					{
						nint ptr = NativeMethods.AllocateVectorFloat();
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					case ValueType.ArrayDouble:
					{
						nint ptr = NativeMethods.AllocateVectorDouble();
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					case ValueType.ArrayString:
					{
						nint ptr = NativeMethods.AllocateVectorString();
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					case ValueType.Vector2:
					{
						var tmp = (object)new Vector2();
						nint ptr = Pin(ref tmp, pins);
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					case ValueType.Vector3:
					{
						var tmp = (object)new Vector3();
						nint ptr = Pin(ref tmp, pins);
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					case ValueType.Vector4:
					{
						var tmp = (object)new Vector4();
						nint ptr = Pin(ref tmp, pins);
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					case ValueType.Matrix4x4:
					{
						var tmp = (object)new Matrix4x4();
						nint ptr = Pin(ref tmp, pins);
						handlers.Add((ptr, retType));
						p[index++] = ptr;
						break;
					}
					default:
						throw new TypeNotFoundException();
				}
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
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.Char8:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.Char16:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.Int8:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.Int16:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.Int32:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.Int64:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.UInt8:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.UInt16:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.UInt32:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.UInt64:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.Pointer:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.Float:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.Double:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.Vector2:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.Vector3:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.Vector4:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.Matrix4x4:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.Function:
						{
							object ptr = GetFunctionPointerForDelegate((Delegate)paramValue);
							p[index++] = Pin(ref ptr, pins);
							break;
						}
						case ValueType.String:
						{
							nint ptr = NativeMethods.CreateString((string)paramValue);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayBool:
						{
							var arr = (Bool8[])paramValue;
							nint ptr = NativeMethods.CreateVectorBool(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayChar8:
						{
							var arr = (Char8[])paramValue;
							nint ptr = NativeMethods.CreateVectorChar8(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayChar16:
						{
							var arr = (Char16[])paramValue;
							nint ptr = NativeMethods.CreateVectorChar16(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayInt8:
						{
							var arr = (sbyte[])paramValue;
							nint ptr = NativeMethods.CreateVectorInt8(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayInt16:
						{
							var arr = (short[])paramValue;
							nint ptr = NativeMethods.CreateVectorInt16(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayInt32:
						{
							var arr = (int[])paramValue;
							nint ptr = NativeMethods.CreateVectorInt32(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayInt64:
						{
							var arr = (long[])paramValue;
							nint ptr = NativeMethods.CreateVectorInt64(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayUInt8:
						{
							var arr = (byte[])paramValue;
							nint ptr = NativeMethods.CreateVectorUInt8(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayUInt16:
						{
							var arr = (ushort[])paramValue;
							nint ptr = NativeMethods.CreateVectorUInt16(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayUInt32:
						{
							var arr = (uint[])paramValue;
							nint ptr = NativeMethods.CreateVectorUInt32(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayUInt64:
						{
							var arr = (ulong[])paramValue;
							nint ptr = NativeMethods.CreateVectorUInt64(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayPointer:
						{
							var arr = (nint[])paramValue;
							nint ptr = NativeMethods.CreateVectorIntPtr(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayFloat:
						{
							var arr = (float[])paramValue;
							nint ptr = NativeMethods.CreateVectorFloat(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayDouble:
						{
							var arr = (double[])paramValue;
							nint ptr = NativeMethods.CreateVectorDouble(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayString:
						{
							var arr = (string[])paramValue;
							nint ptr = NativeMethods.CreateVectorString(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						default:
							throw new TypeNotFoundException();
					}
				}
				else
				{
					switch (valueType)
					{
						case ValueType.Bool:
							p[index++] = RCast((Bool8)paramValue);
							break;
						case ValueType.Char8:
							p[index++] = RCast((Char8)paramValue);
							break;
						case ValueType.Char16:
							p[index++] = RCast((Char16)paramValue);
							break;
						case ValueType.Int8:
							p[index++] = RCast((sbyte)paramValue);
							break;
						case ValueType.Int16:
							p[index++] = RCast((short)paramValue);
							break;
						case ValueType.Int32:
							p[index++] = RCast((int)paramValue);
							break;
						case ValueType.Int64:
							p[index++] = RCast((long)paramValue);
							break;
						case ValueType.UInt8:
							p[index++] = RCast((byte)paramValue);
							break;
						case ValueType.UInt16:
							p[index++] = RCast((ushort)paramValue);
							break;
						case ValueType.UInt32:
							p[index++] = RCast((uint)paramValue);
							break;
						case ValueType.UInt64:
							p[index++] = RCast((ulong)paramValue);
							break;
						case ValueType.Pointer:
							p[index++] = (nint)paramValue;
							break;
						case ValueType.Float:
							p[index++] = RCast((float)paramValue);
							break;
						case ValueType.Double:
							p[index++] = RCast((double)paramValue);
							break;
						case ValueType.Vector2:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.Vector3:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.Vector4:
							p[index++] = Pin(ref paramValue, pins);
							break;
						case ValueType.Matrix4x4:
							p[index++] = Pin(ref paramValue, pins);
							break;

						case ValueType.Function:
						{
							object ptr = GetFunctionPointerForDelegate((Delegate)paramValue);
							p[index++] = Pin(ref ptr, pins);
							break;
						}
						case ValueType.String:
						{
							nint ptr = NativeMethods.CreateString((string)paramValue);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayBool:
						{
							var arr = (Bool8[])paramValue;
							nint ptr = NativeMethods.CreateVectorBool(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayChar8:
						{
							var arr = (Char8[])paramValue;
							nint ptr = NativeMethods.CreateVectorChar8(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayChar16:
						{
							var arr = (Char16[])paramValue;
							nint ptr = NativeMethods.CreateVectorChar16(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayInt8:
						{
							var arr = (sbyte[])paramValue;
							nint ptr = NativeMethods.CreateVectorInt8(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayInt16:
						{
							var arr = (short[])paramValue;
							nint ptr = NativeMethods.CreateVectorInt16(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayInt32:
						{
							var arr = (int[])paramValue;
							nint ptr = NativeMethods.CreateVectorInt32(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayInt64:
						{
							var arr = (long[])paramValue;
							nint ptr = NativeMethods.CreateVectorInt64(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayUInt8:
						{
							var arr = (byte[])paramValue;
							nint ptr = NativeMethods.CreateVectorUInt8(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayUInt16:
						{
							var arr = (ushort[])paramValue;
							nint ptr = NativeMethods.CreateVectorUInt16(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayUInt32:
						{
							var arr = (uint[])paramValue;
							nint ptr = NativeMethods.CreateVectorUInt32(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayUInt64:
						{
							var arr = (ulong[])paramValue;
							nint ptr = NativeMethods.CreateVectorUInt64(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayPointer:
						{
							var arr = (nint[])paramValue;
							nint ptr = NativeMethods.CreateVectorIntPtr(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayFloat:
						{
							var arr = (float[])paramValue;
							nint ptr = NativeMethods.CreateVectorFloat(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayDouble:
						{
							var arr = (double[])paramValue;
							nint ptr = NativeMethods.CreateVectorDouble(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						case ValueType.ArrayString:
						{
							var arr = (string[])paramValue;
							nint ptr = NativeMethods.CreateVectorString(arr, arr.Length);
							handlers.Add((ptr, valueType));
							p[index++] = ptr;
							break;
						}
						default:
							throw new TypeNotFoundException();
					}
				}
			}

			#endregion

			#region Call Function

			call.Function(p, r);

			object? ret = null;

			switch (retType)
			{
				case ValueType.Void:
					break;
				case ValueType.Bool:
					ret = *(Bool8*)r;
					break;
				case ValueType.Char8:
					ret = *(Char8*)r;
					break;
				case ValueType.Char16:
					ret = *(Char16*)r;
					break;
				case ValueType.Int8:
					ret = *(sbyte*)r;
					break;
				case ValueType.Int16:
					ret = *(short*)r;
					break;
				case ValueType.Int32:
					ret = *(int*)r;
					break;
				case ValueType.Int64:
					ret = *(long*)r;
					break;
				case ValueType.UInt8:
					ret = *(byte*)r;
					break;
				case ValueType.UInt16:
					ret = *(ushort*)r;
					break;
				case ValueType.UInt32:
					ret = *(uint*)r;
					break;
				case ValueType.UInt64:
					ret = *(ulong*)r;
					break;
				case ValueType.Pointer:
					ret = *(nint*)r;
					break;
				case ValueType.Float:
					ret = *(float*)r;
					break;
				case ValueType.Double:
					ret = *(double*)r;
					break;
				case ValueType.Function: {
					ret = GetDelegateForFunctionPointer(*(nint*)r, methodInfo.ReturnType);
					break;
				}
				case ValueType.Vector2:
				{
					ret = *(Vector2*)r;
					break;
				}
				case ValueType.Vector3:
				{
					if (hasRet)
					{
						nint ptr = handlers[0].Item1;
						ret = *(Vector3*)ptr;
					}
					else
					{
						ret = *(Vector3*)r;
					}
					break;
				}
				case ValueType.Vector4:
				{
					if (hasRet)
					{
						nint ptr = handlers[0].Item1;
						ret = *(Vector4*)ptr;

					}
					else
					{
						ret = *(Vector4*)r;
					}
					break;
				}
				case ValueType.Matrix4x4:
				{
					nint ptr = handlers[0].Item1;
					ret = *(Matrix4x4*)ptr;
					break;
				}
				case ValueType.String:
				{
					nint ptr = handlers[0].Item1;
					ret = NativeMethods.GetStringData(ptr);
					break;
				}
				case ValueType.ArrayBool:
				{
					nint ptr = handlers[0].Item1;
					var arr = new Bool8[NativeMethods.GetVectorSizeBool(ptr)];
					NativeMethods.GetVectorDataBool(ptr, arr);
					ret = arr;
					break;
				}
				case ValueType.ArrayChar8:
				{
					nint ptr = handlers[0].Item1;
					var arr = new Char8[NativeMethods.GetVectorSizeChar8(ptr)];
					NativeMethods.GetVectorDataChar8(ptr, arr);
					ret = arr;
					break;
				}
				case ValueType.ArrayChar16:
				{
					nint ptr = handlers[0].Item1;
					var arr = new Char16[NativeMethods.GetVectorSizeChar16(ptr)];
					NativeMethods.GetVectorDataChar16(ptr, arr);
					ret = arr;
					break;
				}
				case ValueType.ArrayInt8:
				{
					nint ptr = handlers[0].Item1;
					var arr = new sbyte[NativeMethods.GetVectorSizeInt8(ptr)];
					NativeMethods.GetVectorDataInt8(ptr, arr);
					ret = arr;
					break;
				}
				case ValueType.ArrayInt16:
				{
					nint ptr = handlers[0].Item1;
					var arr = new short[NativeMethods.GetVectorSizeInt16(ptr)];
					NativeMethods.GetVectorDataInt16(ptr, arr);
					ret = arr;
					break;
				}
				case ValueType.ArrayInt32:
				{
					nint ptr = handlers[0].Item1;
					var arr = new int[NativeMethods.GetVectorSizeInt32(ptr)];
					NativeMethods.GetVectorDataInt32(ptr, arr);
					ret = arr;
					break;
				}
				case ValueType.ArrayInt64:
				{
					nint ptr = handlers[0].Item1;
					var arr = new long[NativeMethods.GetVectorSizeInt64(ptr)];
					NativeMethods.GetVectorDataInt64(ptr, arr);
					ret = arr;
					break;
				}
				case ValueType.ArrayUInt8:
				{
					nint ptr = handlers[0].Item1;
					var arr = new byte[NativeMethods.GetVectorSizeUInt8(ptr)];
					NativeMethods.GetVectorDataUInt8(ptr, arr);
					ret = arr;
					break;
				}
				case ValueType.ArrayUInt16:
				{
					nint ptr = handlers[0].Item1;
					var arr = new ushort[NativeMethods.GetVectorSizeUInt16(ptr)];
					NativeMethods.GetVectorDataUInt16(ptr, arr);
					ret = arr;
					break;
				}
				case ValueType.ArrayUInt32:
				{
					nint ptr = handlers[0].Item1;
					var arr = new uint[NativeMethods.GetVectorSizeUInt32(ptr)];
					NativeMethods.GetVectorDataUInt32(ptr, arr);
					ret = arr;
					break;
				}
				case ValueType.ArrayUInt64:
				{
					nint ptr = handlers[0].Item1;
					var arr = new ulong[NativeMethods.GetVectorSizeUInt64(ptr)];
					NativeMethods.GetVectorDataUInt64(ptr, arr);
					ret = arr;
					break;
				}
				case ValueType.ArrayPointer:
				{
					nint ptr = handlers[0].Item1;
					var arr = new nint[NativeMethods.GetVectorSizeIntPtr(ptr)];
					NativeMethods.GetVectorDataIntPtr(ptr, arr);
					ret = arr;
					break;
				}
				case ValueType.ArrayFloat:
				{
					nint ptr = handlers[0].Item1;
					var arr = new float[NativeMethods.GetVectorSizeFloat(ptr)];
					NativeMethods.GetVectorDataFloat(ptr, arr);
					ret = arr;
					break;
				}
				case ValueType.ArrayDouble:
				{
					nint ptr = handlers[0].Item1;
					var arr = new double[NativeMethods.GetVectorSizeDouble(ptr)];
					NativeMethods.GetVectorDataDouble(ptr, arr);
					ret = arr;
					break;
				}
				case ValueType.ArrayString:
				{
					nint ptr = handlers[0].Item1;
					var arr = new string[NativeMethods.GetVectorSizeString(ptr)];
					NativeMethods.GetVectorDataString(ptr, arr);
					ret = arr;
					break;
				}
				default:
					throw new TypeNotFoundException();
			}

			#endregion

			#region Pull Refererences Back

			if (hasRefs)
			{
				int j = hasRet ? 1 : 0; // skip first param if has return

				if (j < handlers.Count)
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
									nint ptr = handlers[j++].Item1;
									parameters[i] = NativeMethods.GetStringData(ptr);
									break;
								}
								case ValueType.ArrayBool:
								{
									nint ptr = handlers[j++].Item1;
									var arr = new Bool8[NativeMethods.GetVectorSizeBool(ptr)];
									NativeMethods.GetVectorDataBool(ptr, arr);
									parameters[i] = arr;
									break;
								}
								case ValueType.ArrayChar8:
								{
									nint ptr = handlers[j++].Item1;
									var arr = new Char8[NativeMethods.GetVectorSizeChar8(ptr)];
									NativeMethods.GetVectorDataChar8(ptr, arr);
									parameters[i] = arr;
									break;
								}
								case ValueType.ArrayChar16:
								{
									nint ptr = handlers[j++].Item1;
									var arr = new Char16[NativeMethods.GetVectorSizeChar16(ptr)];
									NativeMethods.GetVectorDataChar16(ptr, arr);
									parameters[i] = arr;
									break;
								}
								case ValueType.ArrayInt8:
								{
									nint ptr = handlers[j++].Item1;
									var arr = new sbyte[NativeMethods.GetVectorSizeInt8(ptr)];
									NativeMethods.GetVectorDataInt8(ptr, arr);
									parameters[i] = arr;
									break;
								}
								case ValueType.ArrayInt16:
								{
									nint ptr = handlers[j++].Item1;
									var arr = new short[NativeMethods.GetVectorSizeInt16(ptr)];
									NativeMethods.GetVectorDataInt16(ptr, arr);
									parameters[i] = arr;
									break;
								}
								case ValueType.ArrayInt32:
								{
									nint ptr = handlers[j++].Item1;
									var arr = new int[NativeMethods.GetVectorSizeInt32(ptr)];
									NativeMethods.GetVectorDataInt32(ptr, arr);
									parameters[i] = arr;
									break;
								}
								case ValueType.ArrayInt64:
								{
									nint ptr = handlers[j++].Item1;
									var arr = new long[NativeMethods.GetVectorSizeInt64(ptr)];
									NativeMethods.GetVectorDataInt64(ptr, arr);
									parameters[i] = arr;
									break;
								}
								case ValueType.ArrayUInt8:
								{
									nint ptr = handlers[j++].Item1;
									var arr = new byte[NativeMethods.GetVectorSizeUInt8(ptr)];
									NativeMethods.GetVectorDataUInt8(ptr, arr);
									parameters[i] = arr;
									break;
								}
								case ValueType.ArrayUInt16:
								{
									nint ptr = handlers[j++].Item1;
									var arr = new ushort[NativeMethods.GetVectorSizeUInt16(ptr)];
									NativeMethods.GetVectorDataUInt16(ptr, arr);
									parameters[i] = arr;
									break;
								}
								case ValueType.ArrayUInt32:
								{
									nint ptr = handlers[j++].Item1;
									var arr = new uint[NativeMethods.GetVectorSizeUInt32(ptr)];
									NativeMethods.GetVectorDataUInt32(ptr, arr);
									parameters[i] = arr;
									break;
								}
								case ValueType.ArrayUInt64:
								{
									nint ptr = handlers[j++].Item1;
									var arr = new ulong[NativeMethods.GetVectorSizeUInt64(ptr)];
									NativeMethods.GetVectorDataUInt64(ptr, arr);
									parameters[i] = arr;
									break;
								}
								case ValueType.ArrayPointer:
								{
									nint ptr = handlers[j++].Item1;
									var arr = new nint[NativeMethods.GetVectorSizeIntPtr(ptr)];
									NativeMethods.GetVectorDataIntPtr(ptr, arr);
									parameters[i] = arr;
									break;
								}
								case ValueType.ArrayFloat:
								{
									nint ptr = handlers[j++].Item1;
									var arr = new float[NativeMethods.GetVectorSizeFloat(ptr)];
									NativeMethods.GetVectorDataFloat(ptr, arr);
									parameters[i] = arr;
									break;
								}
								case ValueType.ArrayDouble:
								{
									nint ptr = handlers[j++].Item1;
									var arr = new double[NativeMethods.GetVectorSizeDouble(ptr)];
									NativeMethods.GetVectorDataDouble(ptr, arr);
									parameters[i] = arr;
									break;
								}
								case ValueType.ArrayString:
								{
									nint ptr = handlers[j++].Item1;
									var arr = new string[NativeMethods.GetVectorSizeString(ptr)];
									NativeMethods.GetVectorDataString(ptr, arr);
									parameters[i] = arr;
									break;
								}
							}
						}

						if (j >= handlers.Count)
							break;
					}
				}
			}

			#endregion

			#region Cleanup Temp Storage

			if (hasRet)
			{
				DeleteReturn(handlers);
			}

			DeleteParams(handlers, hasRet);

			foreach (var pin in pins)
			{
				pin.Free();
			}

			#endregion

			return ret;
		};
	}
	
	private static nint Pin(ref object paramValue, List<GCHandle> pins)
	{
		var handle = GCHandle.Alloc(paramValue, GCHandleType.Pinned);
		pins.Add(handle);
		return handle.AddrOfPinnedObject();
	}

	private static void DeleteReturn(List<(nint, ValueType)> handlers)
	{
		var (ptr, type) = handlers[0];
		switch (type)
		{
			//case ValueType.Vector2:
			case ValueType.Vector3:
			case ValueType.Vector4:
			case ValueType.Matrix4x4:
				// gc will do job
				break;
			case ValueType.String:
				NativeMethods.FreeString(ptr);
				break;
			case ValueType.ArrayBool:
				NativeMethods.FreeVectorBool(ptr);
				break;
			case ValueType.ArrayChar8:
				NativeMethods.FreeVectorChar8(ptr);
				break;
			case ValueType.ArrayChar16:
				NativeMethods.FreeVectorChar16(ptr);
				break;
			case ValueType.ArrayInt8:
				NativeMethods.FreeVectorInt8(ptr);
				break;
			case ValueType.ArrayInt16:
				NativeMethods.FreeVectorInt16(ptr);
				break;
			case ValueType.ArrayInt32:
				NativeMethods.FreeVectorInt32(ptr);
				break;
			case ValueType.ArrayInt64:
				NativeMethods.FreeVectorInt64(ptr);
				break;
			case ValueType.ArrayUInt8:
				NativeMethods.FreeVectorUInt8(ptr);
				break;
			case ValueType.ArrayUInt16:
				NativeMethods.FreeVectorUInt16(ptr);
				break;
			case ValueType.ArrayUInt32:
				NativeMethods.FreeVectorUInt32(ptr);
				break;
			case ValueType.ArrayUInt64:
				NativeMethods.FreeVectorUInt64(ptr);
				break;
			case ValueType.ArrayPointer:
				NativeMethods.FreeVectorIntPtr(ptr);
				break;
			case ValueType.ArrayFloat:
				NativeMethods.FreeVectorFloat(ptr);
				break;
			case ValueType.ArrayDouble:
				NativeMethods.FreeVectorDouble(ptr);
				break;
			case ValueType.ArrayString:
				NativeMethods.FreeVectorString(ptr);
				break;
			default:
				throw new TypeNotFoundException();
		}
	}
	
	private static void DeleteParams(List<(nint, ValueType)> handlers, bool hasRet)
	{
		int j = hasRet ? 1 : 0;
		for (int i = j; i < handlers.Count; i++)
		{
			var (ptr, type) = handlers[i];
			switch (type)
			{
				case ValueType.String:
					NativeMethods.DeleteString(ptr);
					break;
				case ValueType.ArrayBool:
					NativeMethods.DeleteVectorBool(ptr);
					break;
				case ValueType.ArrayChar8:
					NativeMethods.DeleteVectorChar8(ptr);
					break;
				case ValueType.ArrayChar16:
					NativeMethods.DeleteVectorChar16(ptr);
					break;
				case ValueType.ArrayInt8:
					NativeMethods.DeleteVectorInt8(ptr);
					break;
				case ValueType.ArrayInt16:
					NativeMethods.DeleteVectorInt16(ptr);
					break;
				case ValueType.ArrayInt32:
					NativeMethods.DeleteVectorInt32(ptr);
					break;
				case ValueType.ArrayInt64:
					NativeMethods.DeleteVectorInt64(ptr);
					break;
				case ValueType.ArrayUInt8:
					NativeMethods.DeleteVectorUInt8(ptr);
					break;
				case ValueType.ArrayUInt16:
					NativeMethods.DeleteVectorUInt16(ptr);
					break;
				case ValueType.ArrayUInt32:
					NativeMethods.DeleteVectorUInt32(ptr);
					break;
				case ValueType.ArrayUInt64:
					NativeMethods.DeleteVectorUInt64(ptr);
					break;
				case ValueType.ArrayPointer:
					NativeMethods.DeleteVectorIntPtr(ptr);
					break;
				case ValueType.ArrayFloat:
					NativeMethods.DeleteVectorFloat(ptr);
					break;
				case ValueType.ArrayDouble:
					NativeMethods.DeleteVectorDouble(ptr);
					break;
				case ValueType.ArrayString:
					NativeMethods.DeleteVectorString(ptr);
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
			var methodInfo = paramType.GetMethod("Invoke");
			if (IsNeedMarshal(methodInfo.ReturnType) || methodInfo.GetParameters().Any(p => IsNeedMarshal(p.ParameterType)))
			{
				return true;
			}
		}

		return valueType is >= ValueType.String and <= ValueType.ArrayString;
	}
}
