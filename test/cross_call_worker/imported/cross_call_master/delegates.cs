using System;
using System.Numerics;

using Plugify;

// Generated from cross_call_master.pplugin

namespace cross_call_master {
#pragma warning disable CS0649

	public delegate int NoParamReturnFunctionCallbackFunc();

	public delegate void FuncVoid();

	public delegate Bool8 FuncBool();

	public delegate Char8 FuncChar8();

	public delegate Char16 FuncChar16();

	public delegate sbyte FuncInt8();

	public delegate short FuncInt16();

	public delegate int FuncInt32();

	public delegate long FuncInt64();

	public delegate byte FuncUInt8();

	public delegate ushort FuncUInt16();

	public delegate uint FuncUInt32();

	public delegate ulong FuncUInt64();

	public delegate nint FuncPtr();

	public delegate float FuncFloat();

	public delegate double FuncDouble();

	public delegate string FuncString();

	public delegate object FuncAny();

	public delegate FuncFunctionInner FuncFunction();

	public delegate void FuncFunctionInner();

	public delegate Bool8[] FuncBoolVector();

	public delegate Char8[] FuncChar8Vector();

	public delegate Char16[] FuncChar16Vector();

	public delegate sbyte[] FuncInt8Vector();

	public delegate short[] FuncInt16Vector();

	public delegate int[] FuncInt32Vector();

	public delegate long[] FuncInt64Vector();

	public delegate byte[] FuncUInt8Vector();

	public delegate ushort[] FuncUInt16Vector();

	public delegate uint[] FuncUInt32Vector();

	public delegate ulong[] FuncUInt64Vector();

	public delegate nint[] FuncPtrVector();

	public delegate float[] FuncFloatVector();

	public delegate double[] FuncDoubleVector();

	public delegate string[] FuncStringVector();

	public delegate object[] FuncAnyVector();

	public delegate Vector2[] FuncVec2Vector();

	public delegate Vector3[] FuncVec3Vector();

	public delegate Vector4[] FuncVec4Vector();

	public delegate Matrix4x4[] FuncMat4x4Vector();

	public delegate Vector2 FuncVec2();

	public delegate Vector3 FuncVec3();

	public delegate Vector4 FuncVec4();

	public delegate Matrix4x4 FuncMat4x4();
	
	public delegate AliasBool FuncAliasBool();

	public delegate AliasChar8 FuncAliasChar8();

	public delegate AliasChar16 FuncAliasChar16();

	public delegate AliasInt8 FuncAliasInt8();

	public delegate AliasInt16 FuncAliasInt16();

	public delegate AliasInt32 FuncAliasInt32();

	public delegate AliasInt64 FuncAliasInt64();

	public delegate AliasUInt8 FuncAliasUInt8();

	public delegate AliasUInt16 FuncAliasUInt16();

	public delegate AliasUInt32 FuncAliasUInt32();

	public delegate AliasUInt64 FuncAliasUInt64();

	public delegate AliasPtr FuncAliasPtr();

	public delegate AliasFloat FuncAliasFloat();

	public delegate AliasDouble FuncAliasDouble();

	public delegate AliasString FuncAliasString();

	public delegate AliasAny FuncAliasAny();

	public delegate AliasFunction FuncAliasFunction();

	public delegate void FuncAliasFunctionInner();

	public delegate AliasBoolVector FuncAliasBoolVector();

	public delegate AliasChar8Vector FuncAliasChar8Vector();

	public delegate AliasChar16Vector FuncAliasChar16Vector();

	public delegate AliasInt8Vector FuncAliasInt8Vector();

	public delegate AliasInt16Vector FuncAliasInt16Vector();

	public delegate AliasInt32Vector FuncAliasInt32Vector();

	public delegate AliasInt64Vector FuncAliasInt64Vector();

	public delegate AliasUInt8Vector FuncAliasUInt8Vector();

	public delegate AliasUInt16Vector FuncAliasUInt16Vector();

	public delegate AliasUInt32Vector FuncAliasUInt32Vector();

	public delegate AliasUInt64Vector FuncAliasUInt64Vector();

	public delegate AliasPtrVector FuncAliasPtrVector();

	public delegate AliasFloatVector FuncAliasFloatVector();

	public delegate AliasDoubleVector FuncAliasDoubleVector();

	public delegate AliasStringVector FuncAliasStringVector();

	public delegate AliasAnyVector FuncAliasAnyVector();

	public delegate AliasVec2Vector FuncAliasVec2Vector();

	public delegate AliasVec3Vector FuncAliasVec3Vector();

	public delegate AliasVec4Vector FuncAliasVec4Vector();

	public delegate AliasMat4x4Vector FuncAliasMat4x4Vector();

	public delegate AliasVec2 FuncAliasVec2();

	public delegate AliasVec3 FuncAliasVec3();

	public delegate AliasVec4 FuncAliasVec4();

	public delegate AliasMat4x4 FuncAliasMat4x4();

	public delegate string FuncAliasAll(Bool8 aBool, Char8 aChar8, Char16 aChar16, sbyte aInt8, short aInt16, int aInt32, long aInt64, nint aPtr, float aFloat, double aDouble, string aString, object aAny, ref Vector2 aVec2, ref Vector3 aVec3, ref Vector4 aVec4, ref Matrix4x4 aMat4x4, Bool8[] aBoolVec, Char8[] aChar8Vec, Char16[] aChar16Vec, sbyte[] aInt8Vec, short[] aInt16Vec, int[] aInt32Vec, long[] aInt64Vec, nint[] aPtrVec, float[] aFloatVec, double[] aDoubleVec, string[] aStringVec, object[] aAnyVec, Vector2[] aVec2Vec, Vector3[] aVec3Vec, Vector4[] aVec4Vec);

	public delegate int Func1(ref Vector3 a);

	public delegate Char8 Func2(float a, long b);

	public delegate void Func3(nint a, ref Vector4 b, string c);

	public delegate Vector4 Func4(Bool8 a, int b, Char16 c, ref Matrix4x4 d);

	public delegate Bool8 Func5(sbyte a, ref Vector2 b, nint c, double d, ulong[] e);

	public delegate long Func6(string a, float b, float[] c, short d, byte[] e, nint f);

	public delegate double Func7(Char8[] vecC, ushort u16, Char16 ch16, uint[] vecU32, ref Vector4 vec4, Bool8 b, ulong u64);

	public delegate Matrix4x4 Func8(ref Vector3 vec3, uint[] vecU32, short i16, Bool8 b, ref Vector4 vec4, Char16[] vecC16, Char16 ch16, int i32);

	public delegate void Func9(float f, ref Vector2 vec2, sbyte[] vecI8, ulong u64, Bool8 b, string str, ref Vector4 vec4, short i16, nint ptr);

	public delegate uint Func10(ref Vector4 vec4, ref Matrix4x4 mat, uint[] vecU32, ulong u64, Char8[] vecC, int i32, Bool8 b, ref Vector2 vec2, long i64, double d);

	public delegate nint Func11(Bool8[] vecB, Char16 ch16, byte u8, double d, ref Vector3 vec3, sbyte[] vecI8, long i64, ushort u16, float f, ref Vector2 vec2, uint u32);

	public delegate Bool8 Func12(nint ptr, double[] vecD, uint u32, double d, Bool8 b, int i32, sbyte i8, ulong u64, float f, nint[] vecPtr, long i64, Char8 ch);

	public delegate string Func13(long i64, Char8[] vecC, ushort d, float f, Bool8[] b, ref Vector4 vec4, string str, int int32, ref Vector3 vec3, nint ptr, ref Vector2 vec2, byte[] arr, short i16);

	public delegate string[] Func14(Char8[] vecC, uint[] vecU32, ref Matrix4x4 mat, Bool8 b, Char16 ch16, int i32, float[] vecF, ushort u16, byte[] vecU8, sbyte i8, ref Vector3 vec3, ref Vector4 vec4, double d, nint ptr);

	public delegate short Func15(short[] vecI16, ref Matrix4x4 mat, ref Vector4 vec4, nint ptr, ulong u64, uint[] vecU32, Bool8 b, float f, Char16[] vecC16, byte u8, int i32, ref Vector2 vec2, ushort u16, double d, byte[] vecU8);

	public delegate nint Func16(Bool8[] vecB, short i16, sbyte[] vecI8, ref Vector4 vec4, ref Matrix4x4 mat, ref Vector2 vec2, ulong[] vecU64, Char8[] vecC, string str, long i64, uint[] vecU32, ref Vector3 vec3, float f, double d, sbyte i8, ushort u16);

	public delegate void Func17(ref int i32);

	public delegate Vector2 Func18(ref sbyte i8, ref short i16);

	public delegate void Func19(ref uint u32, ref Vector3 vec3, ref uint[] vecU32);

	public delegate int Func20(ref Char16 ch16, ref Vector4 vec4, ref ulong[] vecU64, ref Char8 ch);

	public delegate float Func21(ref Matrix4x4 mat, ref int[] vecI32, ref Vector2 vec2, ref Bool8 b, ref double extraParam);

	public delegate ulong Func22(ref nint ptr64Ref, ref uint uint32Ref, ref double[] vectorDoubleRef, ref short int16Ref, ref string plgStringRef, ref Vector4 plgVector4Ref);

	public delegate void Func23(ref ulong uint64Ref, ref Vector2 plgVector2Ref, ref short[] vectorInt16Ref, ref Char16 char16Ref, ref float floatRef, ref sbyte int8Ref, ref byte[] vectorUInt8Ref);

	public delegate Matrix4x4 Func24(ref Char8[] vectorCharRef, ref long int64Ref, ref byte[] vectorUInt8Ref, ref Vector4 plgVector4Ref, ref ulong uint64Ref, ref nint[] vectorptr64Ref, ref double doubleRef, ref nint[] vectorptr64Ref2);

	public delegate double Func25(ref int int32Ref, ref nint[] vectorptr64Ref, ref Bool8 boolRef, ref byte uint8Ref, ref string plgStringRef, ref Vector3 plgVector3Ref, ref long int64Ref, ref Vector4 plgVector4Ref, ref ushort uint16Ref);

	public delegate Char8 Func26(ref Char16 char16Ref, ref Vector2 plgVector2Ref, ref Matrix4x4 plgMatrix4x4Ref, ref float[] vectorFloatRef, ref short int16Ref, ref ulong uint64Ref, ref uint uint32Ref, ref ushort[] vectorUInt16Ref, ref nint ptr64Ref, ref Bool8 boolRef);

	public delegate byte Func27(ref float floatRef, ref Vector3 plgVector3Ref, ref nint ptr64Ref, ref Vector2 plgVector2Ref, ref short[] vectorInt16Ref, ref Matrix4x4 plgMatrix4x4Ref, ref Bool8 boolRef, ref Vector4 plgVector4Ref, ref sbyte int8Ref, ref int int32Ref, ref byte[] vectorUInt8Ref);

	public delegate string Func28(ref nint ptr64Ref, ref ushort uint16Ref, ref uint[] vectorUInt32Ref, ref Matrix4x4 plgMatrix4x4Ref, ref float floatRef, ref Vector4 plgVector4Ref, ref string plgStringRef, ref ulong[] vectorUInt64Ref, ref long int64Ref, ref Bool8 boolRef, ref Vector3 plgVector3Ref, ref float[] vectorFloatRef);

	public delegate string[] Func29(ref Vector4 plgVector4Ref, ref int int32Ref, ref sbyte[] vectorInt8Ref, ref double doubleRef, ref Bool8 boolRef, ref sbyte int8Ref, ref ushort[] vectorUInt16Ref, ref float floatRef, ref string plgStringRef, ref Matrix4x4 plgMatrix4x4Ref, ref ulong uint64Ref, ref Vector3 plgVector3Ref, ref long[] vectorInt64Ref);

	public delegate int Func30(ref nint ptr64Ref, ref Vector4 plgVector4Ref, ref long int64Ref, ref uint[] vectorUInt32Ref, ref Bool8 boolRef, ref string plgStringRef, ref Vector3 plgVector3Ref, ref byte[] vectorUInt8Ref, ref float floatRef, ref Vector2 plgVector2Ref, ref Matrix4x4 plgMatrix4x4Ref, ref sbyte int8Ref, ref float[] vectorFloatRef, ref double doubleRef);

	public delegate Vector3 Func31(ref Char8 charRef, ref uint uint32Ref, ref ulong[] vectorUInt64Ref, ref Vector4 plgVector4Ref, ref string plgStringRef, ref Bool8 boolRef, ref long int64Ref, ref Vector2 vec2Ref, ref sbyte int8Ref, ref ushort uint16Ref, ref short[] vectorInt16Ref, ref Matrix4x4 mat4x4Ref, ref Vector3 vec3Ref, ref float floatRef, ref double[] vectorDoubleRef);

	public delegate double Func32(ref int p1, ref ushort p2, ref sbyte[] p3, ref Vector4 p4, ref nint p5, ref uint[] p6, ref Matrix4x4 p7, ref ulong p8, ref string p9, ref long p10, ref Vector2 p11, ref sbyte[] p12, ref Bool8 p13, ref Vector3 p14, ref byte p15, ref Char16[] p16);

	public delegate void Func33(ref object variant);

	public delegate Example[] FuncEnum(Example p1, ref Example[] p2);

#pragma warning restore CS0649
}
