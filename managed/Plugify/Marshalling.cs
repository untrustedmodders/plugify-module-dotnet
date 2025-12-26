using System.Collections.Concurrent;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Plugify;

public static class Marshalling
{
    internal struct Callback(nint fn, Delegate del, JitCallback? jit)
    {
        public nint Function = fn;
        public Delegate Delegate = del;
        public JitCallback? Jit = jit;
    }
    
    internal static readonly ConcurrentDictionary<Delegate, Callback> CachedDelegates = new();
    internal static readonly ConcurrentDictionary<nint, Delegate> CachedFunctions = new();
    internal static readonly ConcurrentDictionary<MethodInfo, bool> CachedMethods = new();

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
					if (paramValue is Array arrayInt8) NativeMethods.AssignVectorInt8((Vector192*)outValue, TypeUtils.ConvertFromEnumArray<sbyte>(enumType, arrayInt8));
					return;
				case ValueType.ArrayInt16:
					if (paramValue is Array arrayInt16) NativeMethods.AssignVectorInt16((Vector192*)outValue, TypeUtils.ConvertFromEnumArray<short>(enumType, arrayInt16));
					return;
				case ValueType.ArrayInt32:
					if (paramValue is Array arrayInt32) NativeMethods.AssignVectorInt32((Vector192*)outValue, TypeUtils.ConvertFromEnumArray<int>(enumType, arrayInt32));
					return;
				case ValueType.ArrayInt64:
					if (paramValue is Array arrayInt64) NativeMethods.AssignVectorInt64((Vector192*)outValue, TypeUtils.ConvertFromEnumArray<long>(enumType, arrayInt64));
					return;
				case ValueType.ArrayUInt8:
					if (paramValue is Array arrayUInt8) NativeMethods.AssignVectorUInt8((Vector192*)outValue, TypeUtils.ConvertFromEnumArray<byte>(enumType, arrayUInt8));
					return;
				case ValueType.ArrayUInt16:
					if (paramValue is Array arrayUInt16) NativeMethods.AssignVectorUInt16((Vector192*)outValue, TypeUtils.ConvertFromEnumArray<ushort>(enumType, arrayUInt16));
					return;
				case ValueType.ArrayUInt32:
					if (paramValue is Array arrayUInt32) NativeMethods.AssignVectorUInt32((Vector192*)outValue, TypeUtils.ConvertFromEnumArray<uint>(enumType, arrayUInt32));
					return;
				case ValueType.ArrayUInt64:
					if (paramValue is Array arrayUInt64) NativeMethods.AssignVectorUInt64((Vector192*)outValue, TypeUtils.ConvertFromEnumArray<ulong>(enumType, arrayUInt64));
					return;
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
					*(nint*)outValue = paramValue != null ? GetFunctionPointerForDelegate((Delegate)paramValue) : nint.Zero;
					return;
				case ValueType.String:
					if (paramValue is string str) NativeMethods.AssignString((String192*)outValue, str);
					return;
				case ValueType.Any:
					NativeMethods.AssignVariant((Variant256*)outValue, paramValue);
					return;
				case ValueType.ArrayBool:
					if (paramValue is Bool8[] arrayBool) NativeMethods.AssignVectorBool((Vector192*)outValue, arrayBool);
					return;
				case ValueType.ArrayChar8:
					if (paramValue is Char8[] arrayChar8) NativeMethods.AssignVectorChar8((Vector192*)outValue, arrayChar8);
					return;
				case ValueType.ArrayChar16:
					if (paramValue is Char16[] arrayChar16) NativeMethods.AssignVectorChar16((Vector192*)outValue, arrayChar16);
					return;
				case ValueType.ArrayInt8:
					if (paramValue is sbyte[] arrayInt8) NativeMethods.AssignVectorInt8((Vector192*)outValue, arrayInt8);
					return;
				case ValueType.ArrayInt16:
					if (paramValue is short[] arrayInt16) NativeMethods.AssignVectorInt16((Vector192*)outValue, arrayInt16);
					return;
				case ValueType.ArrayInt32:
					if (paramValue is int[] arrayInt32) NativeMethods.AssignVectorInt32((Vector192*)outValue, arrayInt32);
					return;
				case ValueType.ArrayInt64:
					if (paramValue is long[] arrayInt64) NativeMethods.AssignVectorInt64((Vector192*)outValue, arrayInt64);
					return;
				case ValueType.ArrayUInt8:
					if (paramValue is byte[] arrayUInt8) NativeMethods.AssignVectorUInt8((Vector192*)outValue, arrayUInt8);
					return;
				case ValueType.ArrayUInt16:
					if (paramValue is ushort[] arrayUInt16) NativeMethods.AssignVectorUInt16((Vector192*)outValue, arrayUInt16);
					return;
				case ValueType.ArrayUInt32:
					if (paramValue is uint[] arrayUInt32) NativeMethods.AssignVectorUInt32((Vector192*)outValue, arrayUInt32);
					return;
				case ValueType.ArrayUInt64:
					if (paramValue is ulong[] arrayUInt64) NativeMethods.AssignVectorUInt64((Vector192*)outValue, arrayUInt64);
					return;
				case ValueType.ArrayPointer:
					if (paramValue is nint[] arrayPointer) NativeMethods.AssignVectorIntPtr((Vector192*)outValue, arrayPointer);
					return;
				case ValueType.ArrayFloat:
					if (paramValue is float[] arrayFloat) NativeMethods.AssignVectorFloat((Vector192*)outValue, arrayFloat);
					return;
				case ValueType.ArrayDouble:
					if (paramValue is double[] arrayDouble) NativeMethods.AssignVectorDouble((Vector192*)outValue, arrayDouble);
					return;
				case ValueType.ArrayString:
					if (paramValue is string[] arrayString) NativeMethods.AssignVectorString((Vector192*)outValue, arrayString);
					return;
				case ValueType.ArrayAny:
					if (paramValue is object[] arrayAny) NativeMethods.AssignVectorVariant((Vector192*)outValue, arrayAny);
					return;
				case ValueType.ArrayVector2:
					if (paramValue is Vector2[] arrayVector2) NativeMethods.AssignVectorVector2((Vector192*)outValue, arrayVector2);
					return;
				case ValueType.ArrayVector3:
					if (paramValue is Vector3[] arrayVector3) NativeMethods.AssignVectorVector3((Vector192*)outValue, arrayVector3);
					return;
				case ValueType.ArrayVector4:
					if (paramValue is Vector4[] arrayVector4) NativeMethods.AssignVectorVector4((Vector192*)outValue, arrayVector4);
					return;
				case ValueType.ArrayMatrix4x4:
					if (paramValue is Matrix4x4[] arrayMatrix4x4) NativeMethods.AssignVectorMatrix4x4((Vector192*)outValue, arrayMatrix4x4);
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

		}
		
		/*if (paramType.IsValueType)
		{
			Marshal.StructureToPtr(paramValue, outValue, false);
			return;
		}*/

		throw new NotImplementedException($"Parameter type {paramType.Name} not implemented");
	}

	internal static unsafe object? MarshalPointer(nint inValue, Type paramType)
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
					return TypeUtils.ConvertToEnumArray(enumType,  NativeMethods.GetVectorDataInt8((Vector192*)inValue));
				case ValueType.ArrayInt16:
					return TypeUtils.ConvertToEnumArray(enumType, NativeMethods.GetVectorDataInt16((Vector192*)inValue));
				case ValueType.ArrayInt32:
					return TypeUtils.ConvertToEnumArray(enumType, NativeMethods.GetVectorDataInt32((Vector192*)inValue));
				case ValueType.ArrayInt64:
					return TypeUtils.ConvertToEnumArray(enumType, NativeMethods.GetVectorDataInt64((Vector192*)inValue));
				case ValueType.ArrayUInt8:
					return TypeUtils.ConvertToEnumArray(enumType, NativeMethods.GetVectorDataUInt8((Vector192*)inValue));
				case ValueType.ArrayUInt16:
					return TypeUtils.ConvertToEnumArray(enumType, NativeMethods.GetVectorDataUInt16((Vector192*)inValue));
				case ValueType.ArrayUInt32:
					return TypeUtils.ConvertToEnumArray(enumType, NativeMethods.GetVectorDataUInt32((Vector192*)inValue));
				case ValueType.ArrayUInt64:
					return TypeUtils.ConvertToEnumArray(enumType, NativeMethods.GetVectorDataUInt64((Vector192*)inValue));
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

	public static T GetDelegateForFunctionPointer<T>(nint funcAddress) where T : Delegate
    {
        return (T) GetDelegateForFunctionPointer(funcAddress, typeof(T));
    }

	public static Delegate GetDelegateForFunctionPointer(nint funcAddress, Type? delegateType)
	{
		return CachedFunctions.GetOrAdd(funcAddress, (address) =>
		{
			if (delegateType == null)
			{
				throw new Exception("Type required to properly generate delegate at runtime");
			}

			MethodInfo methodInfo = delegateType.GetMethod("Invoke")!;
			if (CachedMethods.GetOrAdd(methodInfo, CheckIfNeedsMarshal))
			{
				return MethodUtils.CreateObjectArrayDelegate(delegateType, ExternalInvoke(address, methodInfo));
			}
			else
			{
				return Marshal.GetDelegateForFunctionPointer(address, delegateType);
			}
		});
	}

	private static readonly bool IsArm = RuntimeInformation.ProcessArchitecture == Architecture.Arm64 || RuntimeInformation.ProcessArchitecture == Architecture.Arm;
	private static readonly bool Is32Bit = IntPtr.Size == 4;
	private static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
	
	private static unsafe Func<object[], object> ExternalInvoke(nint funcAddress, MethodInfo methodInfo)
	{
		ManagedType returnType =  new ManagedType(methodInfo.ReturnParameter.ParameterType);
		ManagedType[] parameterTypes = methodInfo.GetParameters().Select(p => new ManagedType(p.ParameterType)).ToArray();
		
		bool hasRet = returnType.ValueType is >= ValueType._ObjectStart and <= ValueType._ObjectEnd;
		//bool hasRefs = parameterTypes.Any(t => t.IsByRef);
		
		if (!hasRet)
		{
			ValueType firstHidden = (IsWindows && !IsArm) || Is32Bit ? ValueType.Vector3 : ValueType.Matrix4x4;
			hasRet = returnType.ValueType >= firstHidden && returnType.ValueType <= ValueType.Matrix4x4;
		}
		
		int paramCount = parameterTypes.Length;
		//int objectCount = parameterTypes.Select(t => t.ValueType is >= ValueType._ObjectStart and <= ValueType._ObjectEnd).Count();
		
		if (hasRet)
		{
			//++objectCount;
			++paramCount;
		}

		JitCall jit = new JitCall(funcAddress, parameterTypes, returnType);
		if (jit.Function == null)
		{
			throw new InvalidOperationException($"{methodInfo.Name} (jit error: {jit.Error})");
		}

		return parameters =>
		{
			Arena arena = new Arena(stackalloc byte[4096]);
			Defer defer = new Defer(paramCount);
			ulong* @params = stackalloc ulong[paramCount];
			ulong* @return = stackalloc ulong[2]{ 0, 0 }; // 128bits to fit Vector4
					
			object? ret = null;
			
			nint Pin<T>(T val, ref Arena arena, int size) where T : unmanaged
			{
				var tmp = (T*)arena.Alloc(size);
				*tmp = val;
				return (nint)tmp;
			}

			nint Raw<T>(T val) where T : unmanaged
			{
				return *(nint*)&val;
			}

			int index = 0;

			try
			{
				var retType = returnType.ValueType;
				if (hasRet)
				{
					var size = TypeUtils.SizeOf(retType);
					var ptr = arena.Alloc(size);
					switch (retType)
					{
						case ValueType.Vector2:
						case ValueType.Vector3:
						case ValueType.Vector4:
						case ValueType.Matrix4x4:
							break;
						case ValueType.String:
							defer.Add(() => NativeMethods.DestroyString((String192*)ptr));
							break;
						case ValueType.Any:
							defer.Add(() => NativeMethods.DestroyVariant((Variant256*)ptr));
							break;
						case ValueType.ArrayBool:
							defer.Add(() => NativeMethods.DestroyVectorBool((Vector192*)ptr));
							break;
						case ValueType.ArrayChar8:
							defer.Add(() => NativeMethods.DestroyVectorChar8((Vector192*)ptr));
							break;
						case ValueType.ArrayChar16:
							defer.Add(() => NativeMethods.DestroyVectorChar16((Vector192*)ptr));
							break;
						case ValueType.ArrayInt8:
							defer.Add(() => NativeMethods.DestroyVectorInt8((Vector192*)ptr));
							break;
						case ValueType.ArrayInt16:
							defer.Add(() => NativeMethods.DestroyVectorInt16((Vector192*)ptr));
							break;
						case ValueType.ArrayInt32:
							defer.Add(() => NativeMethods.DestroyVectorInt32((Vector192*)ptr));
							break;
						case ValueType.ArrayInt64:
							defer.Add(() => NativeMethods.DestroyVectorInt64((Vector192*)ptr));
							break;
						case ValueType.ArrayUInt8:
							defer.Add(() => NativeMethods.DestroyVectorUInt8((Vector192*)ptr));
							break;
						case ValueType.ArrayUInt16:
							defer.Add(() => NativeMethods.DestroyVectorUInt16((Vector192*)ptr));
							break;
						case ValueType.ArrayUInt32:
							defer.Add(() => NativeMethods.DestroyVectorUInt32((Vector192*)ptr));
							break;
						case ValueType.ArrayUInt64:
							defer.Add(() => NativeMethods.DestroyVectorUInt64((Vector192*)ptr));
							break;
						case ValueType.ArrayPointer:
							defer.Add(() => NativeMethods.DestroyVectorIntPtr((Vector192*)ptr));
							break;
						case ValueType.ArrayFloat:
							defer.Add(() => NativeMethods.DestroyVectorFloat((Vector192*)ptr));
							break;
						case ValueType.ArrayDouble:
							defer.Add(() => NativeMethods.DestroyVectorDouble((Vector192*)ptr));
							break;
						case ValueType.ArrayString:
							defer.Add(() => NativeMethods.DestroyVectorString((Vector192*)ptr));
							break;
						case ValueType.ArrayAny:
							defer.Add(() => NativeMethods.DestroyVectorVariant((Vector192*)ptr));
							break;
						case ValueType.ArrayVector2:
							defer.Add(() => NativeMethods.DestroyVectorVector2((Vector192*)ptr));
							break;
						case ValueType.ArrayVector3:
							defer.Add(() => NativeMethods.DestroyVectorVector3((Vector192*)ptr));
							break;
						case ValueType.ArrayVector4:
							defer.Add(() => NativeMethods.DestroyVectorVector4((Vector192*)ptr));
							break;
						case ValueType.ArrayMatrix4x4:
							defer.Add(() => NativeMethods.DestroyVectorMatrix4x4((Vector192*)ptr));
							break;
						default:
							throw new TypeNotFoundException();
					}

					@params[index++] = (ulong)ptr;
				}

				for (int i = 0; i < parameters.Length; i++)
				{
					object paramValue = parameters[i];
					ManagedType paramType = parameterTypes[i];
					ValueType valueType = paramType.ValueType;
					var size = TypeUtils.SizeOf(valueType);
					nint ptr;

					if (paramType.IsByRef)
					{
						int at = i;
						switch (valueType)
						{
							case ValueType.Bool:
								ptr = Pin((Bool8)paramValue, ref arena, size);
								defer.Add(() => { parameters[at] = *(Bool8*)ptr; });
								break;
							case ValueType.Char8:
								ptr = Pin((Char8)paramValue, ref arena, size);
								defer.Add(() => { parameters[at] = *(Char8*)ptr; });
								break;
							case ValueType.Char16:
								ptr = Pin((Char16)paramValue, ref arena, size);
								defer.Add(() => { parameters[at] = *(Char16*)ptr; });
								break;
							case ValueType.Int8:
								ptr = Pin((sbyte)paramValue, ref arena, size);
								defer.Add(() => { parameters[at] = *(sbyte*)ptr; });
								break;
							case ValueType.Int16:
								ptr = Pin((short)paramValue, ref arena, size);
								defer.Add(() => { parameters[at] = *(short*)ptr; });
								break;
							case ValueType.Int32:
								ptr = Pin((int)paramValue, ref arena, size);
								defer.Add(() => { parameters[at] = *(int*)ptr; });
								break;
							case ValueType.Int64:
								ptr = Pin((long)paramValue, ref arena, size);
								defer.Add(() => { parameters[at] = *(long*)ptr; });
								break;
							case ValueType.UInt8:
								ptr = Pin((byte)paramValue, ref arena, size);
								defer.Add(() => { parameters[at] = *(byte*)ptr; });
								break;
							case ValueType.UInt16:
								ptr = Pin((ushort)paramValue, ref arena, size);
								defer.Add(() => { parameters[at] = *(ushort*)ptr; });
								break;
							case ValueType.UInt32:
								ptr = Pin((uint)paramValue, ref arena, size);
								defer.Add(() => { parameters[at] = *(uint*)ptr; });
								break;
							case ValueType.UInt64:
								ptr = Pin((ulong)paramValue, ref arena, size);
								defer.Add(() => { parameters[at] = *(ulong*)ptr; });
								break;
							case ValueType.Pointer:
								ptr = Pin((nint)paramValue, ref arena, size);
								defer.Add(() => { parameters[at] = *(nint*)ptr; });
								break;
							case ValueType.Float:
								ptr = Pin((float)paramValue, ref arena, size);
								defer.Add(() => { parameters[at] = *(float*)ptr; });
								break;
							case ValueType.Double:
								ptr = Pin((double)paramValue, ref arena, size);
								defer.Add(() => { parameters[at] = *(double*)ptr; });
								break;
							case ValueType.Vector2:
								ptr = Pin((Vector2)paramValue, ref arena, size);
								defer.Add(() => { parameters[at] = *(Vector2*)ptr; });
								break;
							case ValueType.Vector3:
								ptr = Pin((Vector3)paramValue, ref arena, size);
								defer.Add(() => { parameters[at] = *(Vector3*)ptr; });
								break;
							case ValueType.Vector4:
								ptr = Pin((Vector4)paramValue, ref arena, size);
								defer.Add(() => { parameters[at] = *(Vector4*)ptr; });
								break;
							case ValueType.Matrix4x4:
								ptr = Pin((Matrix4x4)paramValue, ref arena, size);
								defer.Add(() => { parameters[at] = *(Matrix4x4*)ptr; });
								break;
							case ValueType.Function:
								ptr = GetFunctionPointerForDelegate((Delegate)paramValue);
								break;
							case ValueType.String:
								ptr = Pin(NativeMethods.ConstructString((string)paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetStringData((String192*)ptr);
									NativeMethods.DestroyString((String192*)ptr);
								});
								break;
							case ValueType.Any:
								ptr = Pin(NativeMethods.ConstructVariant((object)paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVariantData((Variant256*)ptr)!;
									NativeMethods.DestroyVariant((Variant256*)ptr);
								});
								break;
							case ValueType.ArrayBool:
								ptr = Pin(NativeMethods.ConstructVectorBool((Bool8[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataBool((Vector192*)ptr);
									NativeMethods.DestroyVectorBool((Vector192*)ptr);
								});
								break;
							case ValueType.ArrayChar8:
								ptr = Pin(NativeMethods.ConstructVectorChar8((Char8[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataChar8((Vector192*)ptr);
									NativeMethods.DestroyVectorChar8((Vector192*)ptr);
								});
								break;
							case ValueType.ArrayChar16:
								ptr = Pin(NativeMethods.ConstructVectorChar16((Char16[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataChar16((Vector192*)ptr);
									NativeMethods.DestroyVectorChar16((Vector192*)ptr);
								});
								break;
							case ValueType.ArrayInt8:
								ptr = Pin(NativeMethods.ConstructVectorInt8((sbyte[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataInt8((Vector192*)ptr);
									NativeMethods.DestroyVectorInt8((Vector192*)ptr);
								});
								break;
							case ValueType.ArrayInt16:
								ptr = Pin(NativeMethods.ConstructVectorInt16((short[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataInt16((Vector192*)ptr);
									NativeMethods.DestroyVectorInt16((Vector192*)ptr);
								});
								break;
							case ValueType.ArrayInt32:
								ptr = Pin(NativeMethods.ConstructVectorInt32((int[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataInt32((Vector192*)ptr);
									NativeMethods.DestroyVectorInt32((Vector192*)ptr);
								});
								break;
							case ValueType.ArrayInt64:
								ptr = Pin(NativeMethods.ConstructVectorInt64((long[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataInt64((Vector192*)ptr);
									NativeMethods.DestroyVectorInt64((Vector192*)ptr);
								});
								break;
							case ValueType.ArrayUInt8:
								ptr = Pin(NativeMethods.ConstructVectorUInt8((byte[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataUInt8((Vector192*)ptr);
									NativeMethods.DestroyVectorUInt8((Vector192*)ptr);
								});
								break;
							case ValueType.ArrayUInt16:
								ptr = Pin(NativeMethods.ConstructVectorUInt16((ushort[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataUInt16((Vector192*)ptr);
									NativeMethods.DestroyVectorUInt16((Vector192*)ptr);
								});
								break;
							case ValueType.ArrayUInt32:
								ptr = Pin(NativeMethods.ConstructVectorUInt32((uint[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataUInt32((Vector192*)ptr);
									NativeMethods.DestroyVectorUInt32((Vector192*)ptr);
								});
								break;
							case ValueType.ArrayUInt64:
								ptr = Pin(NativeMethods.ConstructVectorUInt64((ulong[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataUInt64((Vector192*)ptr);
									NativeMethods.DestroyVectorUInt64((Vector192*)ptr);
								});
								break;
							case ValueType.ArrayPointer:
								ptr = Pin(NativeMethods.ConstructVectorIntPtr((nint[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataIntPtr((Vector192*)ptr);
									NativeMethods.DestroyVectorIntPtr((Vector192*)ptr);
								});
								break;
							case ValueType.ArrayFloat:
								ptr = Pin(NativeMethods.ConstructVectorFloat((float[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataFloat((Vector192*)ptr);
									NativeMethods.DestroyVectorFloat((Vector192*)ptr);
								});
								break;
							case ValueType.ArrayDouble:
								ptr = Pin(NativeMethods.ConstructVectorDouble((double[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataDouble((Vector192*)ptr);
									NativeMethods.DestroyVectorDouble((Vector192*)ptr);
								});
								break;
							case ValueType.ArrayString:
								ptr = Pin(NativeMethods.ConstructVectorString((string[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataString((Vector192*)ptr);
									NativeMethods.DestroyVectorString((Vector192*)ptr);
								});
								break;
							case ValueType.ArrayAny:
								ptr = Pin(NativeMethods.ConstructVectorVariant((object[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataVariant((Vector192*)ptr);
									NativeMethods.DestroyVectorVariant((Vector192*)ptr);
								});
								break;
							case ValueType.ArrayVector2:
								ptr = Pin(NativeMethods.ConstructVectorVector2((Vector2[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataVector2((Vector192*)ptr);
									NativeMethods.DestroyVectorVector2((Vector192*)ptr);
								});
								break;
							case ValueType.ArrayVector3:
								ptr = Pin(NativeMethods.ConstructVectorVector3((Vector3[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataVector3((Vector192*)ptr);
									NativeMethods.DestroyVectorVector3((Vector192*)ptr);
								});
								break;
							case ValueType.ArrayVector4:
								ptr = Pin(NativeMethods.ConstructVectorVector4((Vector4[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataVector4((Vector192*)ptr);
									NativeMethods.DestroyVectorVector4((Vector192*)ptr);
								});
								break;
							case ValueType.ArrayMatrix4x4:
								ptr = Pin(NativeMethods.ConstructVectorMatrix4x4((Matrix4x4[])paramValue), ref arena, size);
								defer.Add(() =>
								{
									parameters[at] = NativeMethods.GetVectorDataMatrix4x4((Vector192*)ptr);
									NativeMethods.DestroyVectorMatrix4x4((Vector192*)ptr);
								});
								break;
							default:
								throw new TypeNotFoundException($"Parameter '{parameterTypes[i].ValueType}' uses not supported type for marshalling!");
						}
					}
					else
					{
						switch (valueType)
						{
							case ValueType.Bool:
								ptr = Raw((Bool8)paramValue);
								break;
							case ValueType.Char8:
								ptr = Raw((Char8)paramValue);
								break;
							case ValueType.Char16:
								ptr = Raw((Char16)paramValue);
								break;
							case ValueType.Int8:
								ptr = Raw((sbyte)paramValue);
								break;
							case ValueType.Int16:
								ptr = Raw((short)paramValue);
								break;
							case ValueType.Int32:
								ptr = Raw((int)paramValue);
								break;
							case ValueType.Int64:
								ptr = Raw((long)paramValue);
								break;
							case ValueType.UInt8:
								ptr = Raw((byte)paramValue);
								break;
							case ValueType.UInt16:
								ptr = Raw((ushort)paramValue);
								break;
							case ValueType.UInt32:
								ptr = Raw((uint)paramValue);
								break;
							case ValueType.UInt64:
								ptr = Raw((ulong)paramValue);
								break;
							case ValueType.Pointer:
								ptr = Raw((nint)paramValue);
								break;
							case ValueType.Float:
								ptr = Raw((float)paramValue);
								break;
							case ValueType.Double:
								ptr = Raw((double)paramValue);
								break;
							case ValueType.Vector2:
								ptr = Pin((Vector2)paramValue, ref arena, size);
								break;
							case ValueType.Vector3:
								ptr = Pin((Vector3)paramValue, ref arena, size);
								break;
							case ValueType.Vector4:
								ptr = Pin((Vector4)paramValue, ref arena, size);
								break;
							case ValueType.Matrix4x4:
								ptr = Pin((Matrix4x4)paramValue, ref arena, size);
								break;
							case ValueType.Function:
								ptr = GetFunctionPointerForDelegate((Delegate)paramValue);
								break;
							case ValueType.String:
								ptr = Pin(NativeMethods.ConstructString((string)paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyString((String192*)ptr));
								break;
							case ValueType.Any:
								ptr = Pin(NativeMethods.ConstructVariant((object)paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVariant((Variant256*)ptr));
								break;
							case ValueType.ArrayBool:
								ptr = Pin(NativeMethods.ConstructVectorBool((Bool8[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorBool((Vector192*)ptr));
								break;
							case ValueType.ArrayChar8:
								ptr = Pin(NativeMethods.ConstructVectorChar8((Char8[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorChar8((Vector192*)ptr));
								break;
							case ValueType.ArrayChar16:
								ptr = Pin(NativeMethods.ConstructVectorChar16((Char16[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorChar16((Vector192*)ptr));
								break;
							case ValueType.ArrayInt8:
								ptr = Pin(NativeMethods.ConstructVectorInt8((sbyte[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorInt8((Vector192*)ptr));
								break;
							case ValueType.ArrayInt16:
								ptr = Pin(NativeMethods.ConstructVectorInt16((short[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorInt16((Vector192*)ptr));
								break;
							case ValueType.ArrayInt32:
								ptr = Pin(NativeMethods.ConstructVectorInt32((int[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorInt32((Vector192*)ptr));
								break;
							case ValueType.ArrayInt64:
								ptr = Pin(NativeMethods.ConstructVectorInt64((long[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorInt64((Vector192*)ptr));
								break;
							case ValueType.ArrayUInt8:
								ptr = Pin(NativeMethods.ConstructVectorUInt8((byte[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorUInt8((Vector192*)ptr));
								break;
							case ValueType.ArrayUInt16:
								ptr = Pin(NativeMethods.ConstructVectorUInt16((ushort[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorUInt16((Vector192*)ptr));
								break;
							case ValueType.ArrayUInt32:
								ptr = Pin(NativeMethods.ConstructVectorUInt32((uint[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorUInt32((Vector192*)ptr));
								break;
							case ValueType.ArrayUInt64:
								ptr = Pin(NativeMethods.ConstructVectorUInt64((ulong[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorUInt64((Vector192*)ptr));
								break;
							case ValueType.ArrayPointer:
								ptr = Pin(NativeMethods.ConstructVectorIntPtr((nint[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorIntPtr((Vector192*)ptr));
								break;
							case ValueType.ArrayFloat:
								ptr = Pin(NativeMethods.ConstructVectorFloat((float[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorFloat((Vector192*)ptr));
								break;
							case ValueType.ArrayDouble:
								ptr = Pin(NativeMethods.ConstructVectorDouble((double[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorDouble((Vector192*)ptr));
								break;
							case ValueType.ArrayString:
								ptr = Pin(NativeMethods.ConstructVectorString((string[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorString((Vector192*)ptr));
								break;
							case ValueType.ArrayAny:
								ptr = Pin(NativeMethods.ConstructVectorVariant((object[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorVariant((Vector192*)ptr));
								break;
							case ValueType.ArrayVector2:
								ptr = Pin(NativeMethods.ConstructVectorVector2((Vector2[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorVector2((Vector192*)ptr));
								break;
							case ValueType.ArrayVector3:
								ptr = Pin(NativeMethods.ConstructVectorVector3((Vector3[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorVector3((Vector192*)ptr));
								break;
							case ValueType.ArrayVector4:
								ptr = Pin(NativeMethods.ConstructVectorVector4((Vector4[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorVector4((Vector192*)ptr));
								break;
							case ValueType.ArrayMatrix4x4:
								ptr = Pin(NativeMethods.ConstructVectorMatrix4x4((Matrix4x4[])paramValue), ref arena, size);
								defer.Add(() => NativeMethods.DestroyVectorMatrix4x4((Vector192*)ptr));
								break;
							default:
								throw new TypeNotFoundException($"Parameter '{parameterTypes[i].ValueType}' uses not supported type for marshalling!");
						}
					}

					@params[index++] = (ulong)ptr;
				}

				jit.Function(@params, @return);

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
			}
			finally
			{
				defer.Execute();
			}

			return ret!;
		};

		// old approach using GCHandles 
		// TODO remove after testing new approach
		/*return parameters =>
		{
			int index = 0, pin = 0, handle = 0;
			GCHandle* pins = stackalloc GCHandle[paramCount];
			(nint, ValueType)* handlers = stackalloc (nint, ValueType)[objectCount];
			ulong* @params = stackalloc ulong[paramCount];
			ulong* @return = stackalloc ulong[2]{ 0, 0 }; // 128bits to fit Vector4

			object? ret = null;

			nint Pin(ref object paramValue, ref GCHandle handle)
			{
				handle = GCHandle.Alloc(paramValue, GCHandleType.Pinned);
				return handle.AddrOfPinnedObject();
			}

			ulong Raw<T>(T primitive) where T : unmanaged
			{
				return *(ulong*)(&primitive);
			}

			try
			{
				#region Allocate Memory for Return

				ValueType retType = returnType.ValueType;
				if (hasRet)
				{
					object obj;
					switch (retType)
					{
						case ValueType.String:
							obj = new String192();
							break;
						case ValueType.Any:
							obj = new Variant256();
							break;
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
							obj = new Vector192();
							break;
						case ValueType.Vector2:
							obj = new Vector2();
							break;
						case ValueType.Vector3:
							obj = new Vector3();
							break;
						case ValueType.Vector4:
							obj = new Vector4();
							break;
						case ValueType.Matrix4x4:
							obj = new Matrix4x4();
							break;
						default:
							throw new TypeNotFoundException();
					}
					nint ptr = Pin(ref obj, ref pins[pin++]);
					handlers[handle++] = (ptr, retType);
					@params[index++] = (ulong)ptr;
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
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Char8:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Char16:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Int8:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Int16:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Int32:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Int64:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.UInt8:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.UInt16:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.UInt32:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.UInt64:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Pointer:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Float:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Double:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Vector2:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Vector3:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Vector4:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Matrix4x4:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Function:
							{
								object ptr = GetFunctionPointerForDelegate((Delegate)paramValue);
								@params[index++] = (ulong)Pin(ref ptr, ref pins[pin++]);
								break;
							}
							case ValueType.String:
							{
								object tmp = NativeMethods.ConstructString((string)paramValue);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.Any:
							{
								object tmp = NativeMethods.ConstructVariant(paramValue);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayBool:
							{
								var arr = (Bool8[])paramValue;
								object tmp = NativeMethods.ConstructVectorBool(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayChar8:
							{
								var arr = (Char8[])paramValue;
								object tmp = NativeMethods.ConstructVectorChar8(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayChar16:
							{
								var arr = (Char16[])paramValue;
								object tmp = NativeMethods.ConstructVectorChar16(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayInt8:
							{
								var arr = (sbyte[])paramValue;
								object tmp = NativeMethods.ConstructVectorInt8(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayInt16:
							{
								var arr = (short[])paramValue;
								object tmp = NativeMethods.ConstructVectorInt16(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayInt32:
							{
								var arr = (int[])paramValue;
								object tmp = NativeMethods.ConstructVectorInt32(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayInt64:
							{
								var arr = (long[])paramValue;
								object tmp = NativeMethods.ConstructVectorInt64(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayUInt8:
							{
								var arr = (byte[])paramValue;
								object tmp = NativeMethods.ConstructVectorUInt8(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayUInt16:
							{
								var arr = (ushort[])paramValue;
								object tmp = NativeMethods.ConstructVectorUInt16(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayUInt32:
							{
								var arr = (uint[])paramValue;
								object tmp = NativeMethods.ConstructVectorUInt32(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayUInt64:
							{
								var arr = (ulong[])paramValue;
								object tmp = NativeMethods.ConstructVectorUInt64(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayPointer:
							{
								var arr = (nint[])paramValue;
								object tmp = NativeMethods.ConstructVectorIntPtr(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayFloat:
							{
								var arr = (float[])paramValue;
								object tmp = NativeMethods.ConstructVectorFloat(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayDouble:
							{
								var arr = (double[])paramValue;
								object tmp = NativeMethods.ConstructVectorDouble(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayString:
							{
								var arr = (string[])paramValue;
								object tmp = NativeMethods.ConstructVectorString(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayAny:
							{
								var arr = (object?[])paramValue;
								object tmp = NativeMethods.ConstructVectorVariant(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayVector2:
							{
								var arr = (Vector2[])paramValue;
								object tmp = NativeMethods.ConstructVectorVector2(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayVector3:
							{
								var arr = (Vector3[])paramValue;
								object tmp = NativeMethods.ConstructVectorVector3(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayVector4:
							{
								var arr = (Vector4[])paramValue;
								object tmp = NativeMethods.ConstructVectorVector4(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayMatrix4x4:
							{
								var arr = (Matrix4x4[])paramValue;
								object tmp = NativeMethods.ConstructVectorMatrix4x4(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
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
								@params[index++] = Raw((Bool8)paramValue);
								break;
							case ValueType.Char8:
								@params[index++] = Raw((Char8)paramValue);
								break;
							case ValueType.Char16:
								@params[index++] = Raw((Char16)paramValue);
								break;
							case ValueType.Int8:
								@params[index++] = Raw((sbyte)paramValue);
								break;
							case ValueType.Int16:
								@params[index++] = Raw((short)paramValue);
								break;
							case ValueType.Int32:
								@params[index++] = Raw((int)paramValue);
								break;
							case ValueType.Int64:
								@params[index++] = Raw((long)paramValue);
								break;
							case ValueType.UInt8:
								@params[index++] = Raw((byte)paramValue);
								break;
							case ValueType.UInt16:
								@params[index++] = Raw((ushort)paramValue);
								break;
							case ValueType.UInt32:
								@params[index++] = Raw((uint)paramValue);
								break;
							case ValueType.UInt64:
								@params[index++] = Raw((ulong)paramValue);
								break;
							case ValueType.Pointer:
								@params[index++] = Raw((nint)paramValue);
								break;
							case ValueType.Float:
								@params[index++] = Raw((float)paramValue);
								break;
							case ValueType.Double:
								@params[index++] = Raw((double)paramValue);
								break;
							case ValueType.Vector2:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Vector3:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Vector4:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;
							case ValueType.Matrix4x4:
								@params[index++] = (ulong)Pin(ref paramValue, ref pins[pin++]);
								break;

							case ValueType.Function:
							{
								object ptr = GetFunctionPointerForDelegate((Delegate)paramValue);
								@params[index++] = (ulong)Pin(ref ptr, ref pins[pin++]);
								break;
							}
							case ValueType.String:
							{
								object tmp = NativeMethods.ConstructString((string)paramValue);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.Any:
							{
								object tmp = NativeMethods.ConstructVariant(paramValue);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayBool:
							{
								var arr = (Bool8[])paramValue;
								object tmp = NativeMethods.ConstructVectorBool(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayChar8:
							{
								var arr = (Char8[])paramValue;
								object tmp = NativeMethods.ConstructVectorChar8(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayChar16:
							{
								var arr = (Char16[])paramValue;
								object tmp = NativeMethods.ConstructVectorChar16(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayInt8:
							{
								var arr = (sbyte[])paramValue;
								object tmp = NativeMethods.ConstructVectorInt8(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayInt16:
							{
								var arr = (short[])paramValue;
								object tmp = NativeMethods.ConstructVectorInt16(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayInt32:
							{
								var arr = (int[])paramValue;
								object tmp = NativeMethods.ConstructVectorInt32(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayInt64:
							{
								var arr = (long[])paramValue;
								object tmp = NativeMethods.ConstructVectorInt64(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayUInt8:
							{
								var arr = (byte[])paramValue;
								object tmp = NativeMethods.ConstructVectorUInt8(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayUInt16:
							{
								var arr = (ushort[])paramValue;
								object tmp = NativeMethods.ConstructVectorUInt16(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayUInt32:
							{
								var arr = (uint[])paramValue;
								object tmp = NativeMethods.ConstructVectorUInt32(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayUInt64:
							{
								var arr = (ulong[])paramValue;
								object tmp = NativeMethods.ConstructVectorUInt64(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayPointer:
							{
								var arr = (nint[])paramValue;
								object tmp = NativeMethods.ConstructVectorIntPtr(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayFloat:
							{
								var arr = (float[])paramValue;
								object tmp = NativeMethods.ConstructVectorFloat(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayDouble:
							{
								var arr = (double[])paramValue;
								object tmp = NativeMethods.ConstructVectorDouble(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayString:
							{
								var arr = (string[])paramValue;
								object tmp = NativeMethods.ConstructVectorString(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayAny:
							{
								var arr = (object?[])paramValue;
								object tmp = NativeMethods.ConstructVectorVariant(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayVector2:
							{
								var arr = (Vector2[])paramValue;
								object tmp = NativeMethods.ConstructVectorVector2(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayVector3:
							{
								var arr = (Vector3[])paramValue;
								object tmp = NativeMethods.ConstructVectorVector3(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayVector4:
							{
								var arr = (Vector4[])paramValue;
								object tmp = NativeMethods.ConstructVectorVector4(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
								break;
							}
							case ValueType.ArrayMatrix4x4:
							{
								var arr = (Matrix4x4[])paramValue;
								object tmp = NativeMethods.ConstructVectorMatrix4x4(arr, arr.Length);
								nint ptr = Pin(ref tmp, ref pins[pin++]);
								handlers[handle++] = (ptr, valueType);
								@params[index++] = (ulong)ptr;
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
										parameters[i] = NativeMethods.GetVariantData((Variant256*)handlers[j++].Item1)!;
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
				for (int i = 0; i < handle; i++)
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

				for (int i = 0; i < pin; ++i)
				{
					pins[i].Free();
				}
			}

			#endregion

			return ret!;
		};*/
	}

	public static nint GetFunctionPointerForDelegate(Delegate d)
	{
		return CachedDelegates.GetOrAdd(d, (del) =>
		{
			MethodInfo methodInfo = del.Method;
			if (CachedMethods.GetOrAdd(methodInfo, CheckIfNeedsMarshal))
			{
				var jit = new JitCallback(del);

				nint fn = jit.Function;
				if (fn == nint.Zero)
				{
					throw new InvalidOperationException($"{methodInfo.Name} (jit error: {jit.Error})");
				}

				return new Callback(fn, del, jit);
			}
			else
			{
				return new Callback(Marshal.GetFunctionPointerForDelegate(d), del, null);
			}
		}).Function;
	}
	
	private static bool CheckIfNeedsMarshal(MethodInfo methodInfo)
	{
		return IsNeedMarshal(methodInfo.ReturnType) || methodInfo.GetParameters().Any(p => IsNeedMarshal(p.ParameterType));
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
