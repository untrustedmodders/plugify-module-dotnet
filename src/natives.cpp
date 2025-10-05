#include "core.hpp"
#include "memory.hpp"
#include "module.hpp"
#include "managed_type.hpp"
#include <module_export.h>
#include <plugify/call.hpp>
#include <plg/numerics.hpp>
#include <plg/any.hpp>

PLUGIFY_WARN_PUSH()

#if defined(__clang__)
PLUGIFY_WARN_IGNORE("-Wreturn-type-c-linkage")
#elif defined(_MSC_VER)
PLUGIFY_WARN_IGNORE(4190)
#endif

using namespace netlm;
using namespace plugify;

template<typename T> requires(!std::is_same_v<T, char*>)
PLUGIFY_FORCE_INLINE plg::vector<T> ConstructVector(T* arr, int len) {
	if (arr == nullptr || len == 0) [[unlikely]]
		if (len > 0)
			return plg::vector<T>(static_cast<size_t>(len));
		else
			return {};
	else
		return plg::vector<T>(arr, arr + len);
}

template<typename T> requires(std::is_same_v<T, char*>)
PLUGIFY_FORCE_INLINE plg::vector<plg::string> ConstructVector(T* arr, int len) {
	if (arr == nullptr || len == 0) [[unlikely]]
		if (len > 0)
			return plg::vector<plg::string>(static_cast<size_t>(len));
		else
			return {};
	else
		return plg::vector<plg::string>(arr, arr + len);
}

template<typename T>
PLUGIFY_FORCE_INLINE void DestroyVector(plg::vector<T>* vector) {
	vector->~vector();
}

template<typename T>
PLUGIFY_FORCE_INLINE int GetVectorSize(plg::vector<T>* vector) {
	return static_cast<int>(vector->size());
}

template<typename T> requires(!std::is_same_v<T, char*>)
PLUGIFY_FORCE_INLINE void GetVectorData(plg::vector<T>* vector, T* arr) {
	if (!vector->empty()) {
		std::memcpy(arr, vector->data(), vector->size() * sizeof(T));
	}
}

template<typename T> requires(std::is_same_v<T, char*>)
PLUGIFY_FORCE_INLINE void GetVectorData(plg::vector<plg::string>* vector, T* arr) {
	for (size_t i = 0; i < vector->size(); ++i) {
		Memory::FreeCoTaskMem(arr[i]);
		arr[i] = Memory::StringToHGlobalAnsi((*vector)[i]);
	}
}

template<typename T> requires(!std::is_same_v<T, char*>)
PLUGIFY_FORCE_INLINE void AssignVector(plg::vector<T>* vector, T* arr, int len) {
	if (arr == nullptr || len == 0) [[unlikely]]
		vector->clear();
	else
		vector->assign(arr, arr + len);
}

template<typename T> requires(std::is_same_v<T, char*>)
PLUGIFY_FORCE_INLINE void AssignVector(plg::vector<plg::string>* vector, T* arr, int len) {
	if (arr == nullptr || len == 0) [[unlikely]]
		vector->clear();
	else
		vector->assign(arr, arr + len);
}

namespace plg {
	namespace raw {
		struct vector {
			[[maybe_unused]] uint8_t padding[sizeof(plg::vector<int>)]{};
		};

		struct string {
			[[maybe_unused]] uint8_t padding[sizeof(plg::string)]{};
		};

		struct variant {
			[[maybe_unused]] uint8_t padding[sizeof(plg::any)]{};
		};
	} // namespace raw

	template<typename T, typename V>
	[[nodiscard]] PLUGIFY_FORCE_INLINE T as_raw(V&& value) {
		T ret{};
		std::construct_at(reinterpret_cast<V*>(&ret), std::forward<V>(value));
		return ret;
	}
} // namespace plg


extern "C" {
	// String Functions

	NETLM_EXPORT plg::string ConstructString(const char* source) {
		if (source == nullptr) [[unlikely]]
			return {};
		else
			return { source };
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

	// Variant Functions
	NETLM_EXPORT void DestroyVariant(plg::any* any) {
		any->~variant();
	}

	// Construct Functions

	NETLM_EXPORT plg::vector<bool> ConstructVectorBool(bool* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plg::vector<char> ConstructVectorChar8(char* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plg::vector<char16_t> ConstructVectorChar16(char16_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plg::vector<int8_t> ConstructVectorInt8(int8_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plg::vector<int16_t> ConstructVectorInt16(int16_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plg::vector<int32_t> ConstructVectorInt32(int32_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plg::vector<int64_t> ConstructVectorInt64(int64_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plg::vector<uint8_t> ConstructVectorUInt8(uint8_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plg::vector<uint16_t> ConstructVectorUInt16(uint16_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plg::vector<uint32_t> ConstructVectorUInt32(uint32_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plg::vector<uint64_t> ConstructVectorUInt64(uint64_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plg::vector<uintptr_t> ConstructVectorIntPtr(uintptr_t* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plg::vector<float> ConstructVectorFloat(float* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plg::vector<double> ConstructVectorDouble(double* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plg::vector<plg::string> ConstructVectorString(char* arr[], int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plg::raw::vector ConstructVectorVariant(int len) {
		plg::vector<plg::any> ret(static_cast<size_t>(len));
		return plg::as_raw<plg::raw::vector>(std::move(ret));
		// Fix MSVC -> C linkage function cannot return C++ class 'plg::vector<plg::any,std::allocator<T>>'
	}
	NETLM_EXPORT plg::vector<plg::vec2> ConstructVectorVector2(plg::vec2* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plg::vector<plg::vec3> ConstructVectorVector3(plg::vec3* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plg::vector<plg::vec4> ConstructVectorVector4(plg::vec4* arr, int len) { return ConstructVector(arr, len); }
	NETLM_EXPORT plg::vector<plg::mat4x4> ConstructVectorMatrix4x4(plg::mat4x4* arr, int len) { return ConstructVector(arr, len); }

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
	NETLM_EXPORT void DestroyVectorVariant(plg::vector<plg::any>* vector) { DestroyVector(vector); }
	NETLM_EXPORT void DestroyVectorVector2(plg::vector<plg::vec2>* vector) { DestroyVector(vector); }
	NETLM_EXPORT void DestroyVectorVector3(plg::vector<plg::vec3>* vector) { DestroyVector(vector); }
	NETLM_EXPORT void DestroyVectorVector4(plg::vector<plg::vec4>* vector) { DestroyVector(vector); }
	NETLM_EXPORT void DestroyVectorMatrix4x4(plg::vector<plg::mat4x4>* vector) { DestroyVector(vector); }

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
	NETLM_EXPORT int GetVectorSizeVariant(plg::vector<plg::any>* vector) { return GetVectorSize(vector); }
	NETLM_EXPORT int GetVectorSizeVector2(plg::vector<plg::vec2>* vector) { return GetVectorSize(vector); }
	NETLM_EXPORT int GetVectorSizeVector3(plg::vector<plg::vec3>* vector) { return GetVectorSize(vector); }
	NETLM_EXPORT int GetVectorSizeVector4(plg::vector<plg::vec4>* vector) { return GetVectorSize(vector); }
	NETLM_EXPORT int GetVectorSizeMatrix4x4(plg::vector<plg::mat4x4>* vector) { return GetVectorSize(vector); }

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
	NETLM_EXPORT plg::any* GetVectorDataVariant(plg::vector<plg::any>* vector, int at) { return &vector->at(static_cast<size_t>(at)); }
	NETLM_EXPORT void GetVectorDataVector2(plg::vector<plg::vec2>* vector, plg::vec2* arr) { GetVectorData(vector, arr); }
	NETLM_EXPORT void GetVectorDataVector3(plg::vector<plg::vec3>* vector, plg::vec3* arr) { GetVectorData(vector, arr); }
	NETLM_EXPORT void GetVectorDataVector4(plg::vector<plg::vec4>* vector, plg::vec4* arr) { GetVectorData(vector, arr); }
	NETLM_EXPORT void GetVectorDataMatrix4x4(plg::vector<plg::mat4x4>* vector, plg::mat4x4* arr) { GetVectorData(vector, arr); }

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
	NETLM_EXPORT void AssignVectorVariant(plg::vector<plg::any>* vector, int len) { vector->resize(static_cast<size_t>(len)); }
	NETLM_EXPORT void AssignVectorVector2(plg::vector<plg::vec2>* vector, plg::vec2* arr, int len) { AssignVector(vector, arr, len); }
	NETLM_EXPORT void AssignVectorVector3(plg::vector<plg::vec3>* vector, plg::vec3* arr, int len) { AssignVector(vector, arr, len); }
	NETLM_EXPORT void AssignVectorVector4(plg::vector<plg::vec4>* vector, plg::vec4* arr, int len) { AssignVector(vector, arr, len); }
	NETLM_EXPORT void AssignVectorMatrix4x4(plg::vector<plg::mat4x4>* vector, plg::mat4x4* arr, int len) { AssignVector(vector, arr, len); }
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
		Signature sig(CallConv::CDecl, retHidden ? typeHidden : ret.type);

#if !NETLM_ARCH_ARM
		if (retHidden) {
			sig.AddArg(ret.type);
		}
#endif

		for (int i = 0; i < count; ++i) {
			const auto& [type, ref] = params[i];
			sig.AddArg(ref ? ValueType::Pointer : type);
		}

		JitCall* call = new JitCall{};
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

	NETLM_EXPORT JitCallback* NewCallback(const char* name, void* delegate) {
		std::shared_ptr<Method> method = g_netlm.FindMethod(name);
		if (method == nullptr || delegate == nullptr)
			return nullptr;

		JitCallback* callback = new JitCallback{};
		callback->GetJitFunc(*method, &DotnetLanguageModule::DelegateCall, delegate);
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
