#include "core.hpp"
#include "memory.hpp"
#include "module.hpp"
#include "managed_type.hpp"
#include <module_export.h>
#include <plugify/jit/call.hpp>
#include <plugify/jit/helpers.hpp>
#include <plugify/math.hpp>

#if defined(__clang__)
#define NETLM_FORCE_INLINE [[gnu::always_inline]] [[gnu::gnu_inline]] extern inline
#elif defined(__GNUC__)
#define NETLM_FORCE_INLINE [[gnu::always_inline]] inline
#elif defined(_MSC_VER)
#pragma warning(error: 4714)
#define NETLM_FORCE_INLINE __forceinline
#else
#define NETLM_FORCE_INLINE inline
#endif

using namespace netlm;
using namespace plugify;

template<typename T>
NETLM_FORCE_INLINE plugify::Vector ConstructVector(T* arr, int len) requires(!std::is_same_v<T, char*>) {
	plugify::Vector ret;
	if (arr == nullptr || len == 0) [[unlikely]]
		std::construct_at(reinterpret_cast<plg::vector<T>*>(&ret));
	else
		std::construct_at(reinterpret_cast<plg::vector<T>*>(&ret), arr, arr + len);
	return ret;
}

template<typename T>
NETLM_FORCE_INLINE plugify::Vector ConstructVector(T* arr, int len) requires(std::is_same_v<T, char*>) {
	plugify::Vector ret;
	if (arr == nullptr || len == 0) [[unlikely]]
		std::construct_at(reinterpret_cast<plg::vector<plg::string>*>(&ret));
	else
		std::construct_at(reinterpret_cast<plg::vector<plg::string>*>(&ret), arr, arr + len);
	return ret;
}

template<typename T>
NETLM_FORCE_INLINE void DestroyVector(plg::vector<T>* vector) {
	vector->~vector_base();
}

template<typename T>
NETLM_FORCE_INLINE int GetVectorSize(plg::vector<T>* vector) {
	return static_cast<int>(vector->size());
}

template<typename T>
NETLM_FORCE_INLINE void GetVectorData(plg::vector<T>* vector, T* arr) requires(!std::is_same_v<T, char*>) {
	for (size_t i = 0; i < vector->size(); ++i) {
		arr[i] = (*vector)[i];
	}
}

template<typename T>
NETLM_FORCE_INLINE void GetVectorData(plg::vector<plg::string>* vector, T* arr) requires(std::is_same_v<T, char*>) {
	for (size_t i = 0; i < vector->size(); ++i) {
		Memory::FreeCoTaskMem(arr[i]);
		arr[i] = Memory::StringToHGlobalAnsi((*vector)[i]);
	}
}

template<typename T>
NETLM_FORCE_INLINE void AssignVector(plg::vector<T>* vector, T* arr, int len) requires(!std::is_same_v<T, char*>) {
	if (arr == nullptr || len == 0) [[unlikely]]
		vector->clear();
	else
		vector->assign(arr, arr + len);
}

template<typename T>
NETLM_FORCE_INLINE void AssignVector(plg::vector<plg::string>* vector, T* arr, int len) requires(std::is_same_v<T, char*>) {
	if (arr == nullptr || len == 0) [[unlikely]]
		vector->clear();
	else
		vector->assign(arr, arr + len);
}

extern "C" {
	// String Functions

	NETLM_EXPORT plugify::String ConstructString(const char* source) {
		plugify::String ret;
		if (source == nullptr) [[unlikely]]
			std::construct_at(reinterpret_cast<plg::string*>(&ret));
		else
			std::construct_at(reinterpret_cast<plg::string*>(&ret), source);
		return ret;
	}
	NETLM_EXPORT void DestroyString(plg::string* string) {
		string->~basic_string();
	}
	NETLM_EXPORT int GetStringLength(plg::string* string) {
		return static_cast<int>(string->length());
	}
	NETLM_EXPORT const char* GetStringData(plg::string* string) {
		return Memory::StringToHGlobalAnsi(*string);
	}
	NETLM_EXPORT void AssignString(plg::string* string, const char* source) {
		if (source == nullptr) [[unlikely]]
			string->clear();
		else
			string->assign(source);
	}

	// Construct Functions

	NETLM_EXPORT plugify::Vector ConstructVectorBool(bool* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plugify::Vector ConstructVectorChar8(char* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plugify::Vector ConstructVectorChar16(char16_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plugify::Vector ConstructVectorInt8(int8_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plugify::Vector ConstructVectorInt16(int16_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plugify::Vector ConstructVectorInt32(int32_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plugify::Vector ConstructVectorInt64(int64_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plugify::Vector ConstructVectorUInt8(uint8_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plugify::Vector ConstructVectorUInt16(uint16_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plugify::Vector ConstructVectorUInt32(uint32_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plugify::Vector ConstructVectorUInt64(uint64_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plugify::Vector ConstructVectorIntPtr(uintptr_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plugify::Vector ConstructVectorFloat(float* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plugify::Vector ConstructVectorDouble(double* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plugify::Vector ConstructVectorString(char* arr[], int len)  { return ConstructVector(arr, len); }

	// DestroyVector Functions

	NETLM_EXPORT void DestroyVectorBool(plg::vector<bool>* vector) { DestroyVector(vector); }
	NETLM_EXPORT void DestroyVectorChar8(plg::vector<char>* vector) { DestroyVector(vector); }
	NETLM_EXPORT void DestroyVectorChar16(plg::vector<char16_t>* vector) { DestroyVector(vector); }
	NETLM_EXPORT void DestroyVectorInt8(plg::vector<int8_t>* vector) { DestroyVector(vector); }
	NETLM_EXPORT void DestroyVectorInt16(plg::vector<int16_t>* vector) { DestroyVector(vector); }
	NETLM_EXPORT void DestroyVectorInt32(plg::vector<int32_t>* vector) { DestroyVector(vector); }
	NETLM_EXPORT void DestroyVectorInt64(plg::vector<int64_t>* vector) { DestroyVector(vector); }
	NETLM_EXPORT void DestroyVectorUInt8(plg::vector<uint8_t>* vector) { DestroyVector(vector); }
	NETLM_EXPORT void DestroyVectorUInt16(plg::vector<uint16_t>* vector) { DestroyVector(vector); }
	NETLM_EXPORT void DestroyVectorUInt32(plg::vector<uint32_t>* vector) { DestroyVector(vector); }
	NETLM_EXPORT void DestroyVectorUInt64(plg::vector<uint64_t>* vector) { DestroyVector(vector); }
	NETLM_EXPORT void DestroyVectorIntPtr(plg::vector<uintptr_t>* vector) { DestroyVector(vector); }
	NETLM_EXPORT void DestroyVectorFloat(plg::vector<float>* vector) { DestroyVector(vector); }
	NETLM_EXPORT void DestroyVectorDouble(plg::vector<double>* vector) { DestroyVector(vector); }
	NETLM_EXPORT void DestroyVectorString(plg::vector<plg::string>* vector) { DestroyVector(vector); }

	// GetVectorSize Functions
	
	NETLM_EXPORT int GetVectorSizeBool(plg::vector<bool>* vector) { return GetVectorSize(vector); }
	NETLM_EXPORT int GetVectorSizeChar8(plg::vector<char>* vector) { return GetVectorSize(vector); }
	NETLM_EXPORT int GetVectorSizeChar16(plg::vector<char16_t>* vector) { return GetVectorSize(vector); }
	NETLM_EXPORT int GetVectorSizeInt8(plg::vector<int8_t>* vector) { return GetVectorSize(vector); }
	NETLM_EXPORT int GetVectorSizeInt16(plg::vector<int16_t>* vector) { return GetVectorSize(vector); }
	NETLM_EXPORT int GetVectorSizeInt32(plg::vector<int32_t>* vector) { return GetVectorSize(vector); }
	NETLM_EXPORT int GetVectorSizeInt64(plg::vector<int64_t>* vector) { return GetVectorSize(vector); }
	NETLM_EXPORT int GetVectorSizeUInt8(plg::vector<uint8_t>* vector) { return GetVectorSize(vector); }
	NETLM_EXPORT int GetVectorSizeUInt16(plg::vector<uint16_t>* vector) { return GetVectorSize(vector); }
	NETLM_EXPORT int GetVectorSizeUInt32(plg::vector<uint32_t>* vector) { return GetVectorSize(vector); }
	NETLM_EXPORT int GetVectorSizeUInt64(plg::vector<uint64_t>* vector) { return GetVectorSize(vector); }
	NETLM_EXPORT int GetVectorSizeIntPtr(plg::vector<uintptr_t>* vector) { return GetVectorSize(vector); }
	NETLM_EXPORT int GetVectorSizeFloat(plg::vector<float>* vector) { return GetVectorSize(vector); }
	NETLM_EXPORT int GetVectorSizeDouble(plg::vector<double>* vector) { return GetVectorSize(vector); }
	NETLM_EXPORT int GetVectorSizeString(plg::vector<plg::string>* vector) { return GetVectorSize(vector); }

	// GetVectorData Functions

	NETLM_EXPORT void GetVectorDataBool(plg::vector<bool>* vector, bool* arr) { GetVectorData(vector, arr); }
	NETLM_EXPORT void GetVectorDataChar8(plg::vector<char>* vector, char* arr) { GetVectorData(vector, arr); }
	NETLM_EXPORT void GetVectorDataChar16(plg::vector<char16_t>* vector, char16_t* arr) { GetVectorData(vector, arr); }
	NETLM_EXPORT void GetVectorDataInt8(plg::vector<int8_t>* vector, int8_t* arr) { GetVectorData(vector, arr); }
	NETLM_EXPORT void GetVectorDataInt16(plg::vector<int16_t>* vector, int16_t* arr) { GetVectorData(vector, arr); }
	NETLM_EXPORT void GetVectorDataInt32(plg::vector<int32_t>* vector, int32_t* arr) { GetVectorData(vector, arr); }
	NETLM_EXPORT void GetVectorDataInt64(plg::vector<int64_t>* vector, int64_t* arr) { GetVectorData(vector, arr); }
	NETLM_EXPORT void GetVectorDataUInt8(plg::vector<uint8_t>* vector, uint8_t* arr) { GetVectorData(vector, arr); }
	NETLM_EXPORT void GetVectorDataUInt16(plg::vector<uint16_t>* vector, uint16_t* arr) { GetVectorData(vector, arr); }
	NETLM_EXPORT void GetVectorDataUInt32(plg::vector<uint32_t>* vector, uint32_t* arr) { GetVectorData(vector, arr); }
	NETLM_EXPORT void GetVectorDataUInt64(plg::vector<uint64_t>* vector, uint64_t* arr) { GetVectorData(vector, arr); }
	NETLM_EXPORT void GetVectorDataIntPtr(plg::vector<uintptr_t>* vector, uintptr_t* arr) { GetVectorData(vector, arr); }
	NETLM_EXPORT void GetVectorDataFloat(plg::vector<float>* vector, float* arr) { GetVectorData(vector, arr); }
	NETLM_EXPORT void GetVectorDataDouble(plg::vector<double>* vector, double* arr) { GetVectorData(vector, arr); }
	NETLM_EXPORT void GetVectorDataString(plg::vector<plg::string>* vector, char* arr[]) { GetVectorData(vector, arr); }

	// AssignVector Functions

	NETLM_EXPORT void AssignVectorBool(plg::vector<bool>* vector, bool* arr, int len) { AssignVector(vector, arr, len); }
	NETLM_EXPORT void AssignVectorChar8(plg::vector<char>* vector, char* arr, int len) { AssignVector(vector, arr, len); }
	NETLM_EXPORT void AssignVectorChar16(plg::vector<char16_t>* vector, char16_t* arr, int len) { AssignVector(vector, arr, len); }
	NETLM_EXPORT void AssignVectorInt8(plg::vector<int8_t>* vector, int8_t* arr, int len) { AssignVector(vector, arr, len); }
	NETLM_EXPORT void AssignVectorInt16(plg::vector<int16_t>* vector, int16_t* arr, int len) { AssignVector(vector, arr, len); }
	NETLM_EXPORT void AssignVectorInt32(plg::vector<int32_t>* vector, int32_t* arr, int len) { AssignVector(vector, arr, len); }
	NETLM_EXPORT void AssignVectorInt64(plg::vector<int64_t>* vector, int64_t* arr, int len) { AssignVector(vector, arr, len); }
	NETLM_EXPORT void AssignVectorUInt8(plg::vector<uint8_t>* vector, uint8_t* arr, int len) { AssignVector(vector, arr, len); }
	NETLM_EXPORT void AssignVectorUInt16(plg::vector<uint16_t>* vector, uint16_t* arr, int len) { AssignVector(vector, arr, len); }
	NETLM_EXPORT void AssignVectorUInt32(plg::vector<uint32_t>* vector, uint32_t* arr, int len) { AssignVector(vector, arr, len); }
	NETLM_EXPORT void AssignVectorUInt64(plg::vector<uint64_t>* vector, uint64_t* arr, int len) { AssignVector(vector, arr, len); }
	NETLM_EXPORT void AssignVectorIntPtr(plg::vector<uintptr_t>* vector, uintptr_t* arr, int len) { AssignVector(vector, arr, len); }
	NETLM_EXPORT void AssignVectorFloat(plg::vector<float>* vector, float* arr, int len) { AssignVector(vector, arr, len); }
	NETLM_EXPORT void AssignVectorDouble(plg::vector<double>* vector, double* arr, int len) { AssignVector(vector, arr, len); }
	NETLM_EXPORT void AssignVectorString(plg::vector<plg::string>* vector, char* arr[], int len) { AssignVector(vector, arr, len); }
}

extern "C" {
	// Jit Functions

	NETLM_EXPORT JitCall* NewCall(void* target, ManagedType* params, int count, ManagedType ret) {
		if (target == nullptr)
			return nullptr;

#if NETLM_ARCH_ARM
		ValueType typeHidden = ValueType::Void;
#else
		ValueType typeHidden = ValueType::Pointer;
#endif

		bool retHidden = ValueUtils::IsHiddenParam(ret.type);
		asmjit::FuncSignature sig(asmjit::CallConvId::kHost, asmjit::FuncSignature::kNoVarArgs, JitUtils::GetRetTypeId(retHidden ? typeHidden : ret.type));

#if !NETLM_ARCH_ARM
		if (retHidden) {
			sig.addArg(JitUtils::GetValueTypeId(ret.type));
		}
#endif

		for (int i = 0; i < count; ++i) {
			const auto& [type, ref] = params[i];
			sig.addArg(JitUtils::GetValueTypeId(ref ? ValueType::Pointer : type));
		}

		JitCall* call = new JitCall(g_netlm.GetRuntime());
		call->GetJitFunc(sig, target, JitCall::WaitType::None, retHidden);
		return call;
	}

	NETLM_EXPORT void DeleteCall(JitCall* call) {
		delete call;
	}

	NETLM_EXPORT void* GetCallFunction(JitCall* call) {
		return call ? call->GetFunction() : nullptr;
	}

	NETLM_EXPORT char* GetCallError(JitCall* call) {
		return Memory::StringToHGlobalAnsi(call ? call->GetError().data() : "Target invalid");
	}

	NETLM_EXPORT JitCallback* NewCallback(ManagedGuid guid, const char* name, void* delegate) {
		MethodRef method = g_netlm.FindMethod(guid, name);
		if (method == nullptr || delegate == nullptr)
			return nullptr;

		JitCallback* callback = new JitCallback(g_netlm.GetRuntime());
		callback->GetJitFunc(method, &DotnetLanguageModule::DelegateCall, delegate);
		return callback;
	}

	NETLM_EXPORT void DeleteCallback(JitCallback* callback) {
		delete callback;
	}

	NETLM_EXPORT void* GetCallbackFunction(JitCallback* callback) {
		return callback ? callback->GetFunction() : nullptr;
	}

	NETLM_EXPORT char* GetCallbackError(JitCallback* callback) {
		return Memory::StringToHGlobalAnsi(callback ? callback->GetError().data() : "Method invalid");
	}
}
