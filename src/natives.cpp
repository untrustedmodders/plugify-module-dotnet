#include "core.hpp"
#include "managed_type.hpp"
#include "memory.hpp"
#include "module.hpp"
#include <module_export.h>
#include <plugify/jit/call.hpp>
#include <plugify/jit/helpers.hpp>
#include <plugify/string.hpp>
#include <plugify/vector.hpp>

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

std::map<type_index, int32_t> g_numberOfMalloc = { };
std::map<type_index, int32_t> g_numberOfAllocs = { };

std::string_view GetTypeName(type_index type) {
	static std::map<type_index, std::string_view> typeNameMap = {
		{type_id<plg::string>, "String"},
		{type_id<plg::vector<bool>>, "VectorBool"},
		{type_id<plg::vector<char>>, "VectorChar8"},
		{type_id<plg::vector<char16_t>>, "VectorChar16"},
		{type_id<plg::vector<int8_t>>, "VectorInt8"},
		{type_id<plg::vector<int16_t>>, "VectorInt16"},
		{type_id<plg::vector<int32_t>>, "VectorInt32"},
		{type_id<plg::vector<int64_t>>, "VectorInt64"},
		{type_id<plg::vector<uint8_t>>, "VectorUInt8"},
		{type_id<plg::vector<uint16_t>>, "VectorUInt16"},
		{type_id<plg::vector<uint32_t>>, "VectorUInt32"},
		{type_id<plg::vector<uint64_t>>, "VectorUInt64"},
		{type_id<plg::vector<uintptr_t>>, "VectorUIntPtr"},
		{type_id<plg::vector<float>>, "VectorFloat"},
		{type_id<plg::vector<double>>, "VectorDouble"},
		{type_id<plg::vector<plg::string>>, "VectorString"},
		{type_id<JitCall>, "JitCall"},
		{type_id<JitCallback>, "JitCallback"},
	};
	auto it = typeNameMap.find(type);
	if (it != typeNameMap.end()) {
		return std::get<std::string_view>(*it);
	}
	return "unknown";
}

template<typename T>
NETLM_FORCE_INLINE plg::vector<T>* CreateVector(T* arr, int len) requires(!std::is_same_v<T, char*>) {
	auto vector = len == 0 ? new plg::vector<T>() : new plg::vector<T>(arr, arr + len);
	assert(vector);
	++g_numberOfAllocs[type_id<plg::vector<T>>];
	return vector;
}

template<typename T>
NETLM_FORCE_INLINE plg::vector<plg::string>* CreateVector(T* arr, int len) requires(std::is_same_v<T, char*>) {
	auto vector = len == 0 ? new plg::vector<plg::string>() : new plg::vector<plg::string>(arr, arr + len);
	assert(vector);
	++g_numberOfAllocs[type_id<plg::vector<plg::string>>];
	return vector;
}

template<typename T>
NETLM_FORCE_INLINE plg::vector<T>* AllocateVector() requires(!std::is_same_v<T, char*>) {
	auto vector = static_cast<plg::vector<T>*>(std::malloc(sizeof(plg::vector<T>)));
	assert(vector);
	++g_numberOfMalloc[type_id<plg::vector<T>>];
	return vector;
}

template<typename T>
NETLM_FORCE_INLINE plg::vector<plg::string>* AllocateVector() requires(std::is_same_v<T, char*>) {
	auto vector = static_cast<plg::vector<plg::string>*>(std::malloc(sizeof(plg::vector<plg::string>)));
	assert(vector);
	++g_numberOfMalloc[type_id<plg::vector<plg::string>>];
	return vector;
}

template<typename T>
NETLM_FORCE_INLINE void DeleteVector(plg::vector<T>* vector) {
	delete vector;
	--g_numberOfAllocs[type_id<plg::vector<T>>];
	assert(g_numberOfAllocs[type_id<plg::vector<T>>] != -1);
}

template<typename T>
NETLM_FORCE_INLINE void FreeVector(plg::vector<T>* vector) {
	vector->~vector_base();
	std::free(vector);
	--g_numberOfMalloc[type_id<plg::vector<T>>];
	assert(g_numberOfMalloc[type_id<plg::vector<T>>] != -1);
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
NETLM_FORCE_INLINE void AssignVector(plg::vector<T>* vector, T* arr, int len) requires(!std::is_same_v<T, char*>) {
	if (arr == nullptr || len == 0)
		vector->clear();
	else
		vector->assign(arr, arr + len);
}

template<typename T>
NETLM_FORCE_INLINE void AssignVector(plg::vector<plg::string>* vector, T* arr, int len) requires(std::is_same_v<T, char*>) {
	if (arr == nullptr || len == 0)
		vector->clear();
	else
		vector->assign(arr, arr + len);
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
NETLM_FORCE_INLINE void ConstructVector(plg::vector<T>* vector, T* arr, int len) requires(!std::is_same_v<T, char*>) {
	std::construct_at(vector, len == 0 ? plg::vector<T>() : plg::vector<T>(arr, arr + len));
}

template<typename T>
NETLM_FORCE_INLINE void ConstructVector(plg::vector<plg::string>* vector, T* arr, int len) requires(std::is_same_v<T, char*>) {
	std::construct_at(vector, len == 0 ? plg::vector<plg::string>() : plg::vector<plg::string>(arr, arr + len));
}

extern "C" {
	// String Functions

	NETLM_EXPORT plg::string* AllocateString() {
		auto str = static_cast<plg::string*>(std::malloc(sizeof(plg::string)));
		++g_numberOfMalloc[type_id<plg::string>];
		return str;
	}
	NETLM_EXPORT void* CreateString(const char* source) {
		auto str = source == nullptr ? new plg::string() : new plg::string(source);
		++g_numberOfAllocs[type_id<plg::string>];
		return str;
	}
	NETLM_EXPORT const char* GetStringData(plg::string* string) {
		return Memory::StringToHGlobalAnsi(*string);
	}
	NETLM_EXPORT int GetStringLength(plg::string* string) {
		return static_cast<int>(string->length());
	}
	NETLM_EXPORT void ConstructString(plg::string* string, const char* source) {
		if (source == nullptr)
			std::construct_at(string, plg::string());
		else
			std::construct_at(string, source);
	}
	NETLM_EXPORT void AssignString(plg::string* string, const char* source) {
		if (source == nullptr)
			string->clear();
		else
			string->assign(source);
	}
	NETLM_EXPORT void FreeString(plg::string* string) {
		string->~basic_string();
		std::free(string);
		--g_numberOfMalloc[type_id<plg::string>];
		assert(g_numberOfMalloc[type_id<plg::string>] != -1);
	}
	NETLM_EXPORT void DeleteString(plg::string* string) {
		delete string;
		--g_numberOfAllocs[type_id<plg::string>];
		assert(g_numberOfAllocs[type_id<plg::string>] != -1);
	}
	NETLM_EXPORT void DestroyString(plg::string* string) {
		string->~basic_string();
	}

	// CreateVector Functions
	NETLM_EXPORT plg::vector<bool>* CreateVectorBool(bool* arr, int len) { return CreateVector(arr, len); }
	NETLM_EXPORT plg::vector<char>* CreateVectorChar8(char* arr, int len)  { return CreateVector(arr, len); }
	NETLM_EXPORT plg::vector<char16_t>* CreateVectorChar16(char16_t* arr, int len)  { return CreateVector(arr, len); }
	NETLM_EXPORT plg::vector<int8_t>* CreateVectorInt8(int8_t* arr, int len) { return CreateVector(arr, len); }
	NETLM_EXPORT plg::vector<int16_t>* CreateVectorInt16(int16_t* arr, int len) { return CreateVector(arr, len); }
	NETLM_EXPORT plg::vector<int32_t>* CreateVectorInt32(int32_t* arr, int len) { return CreateVector(arr, len); }
	NETLM_EXPORT plg::vector<int64_t>* CreateVectorInt64(int64_t* arr, int len) { return CreateVector(arr, len); }
	NETLM_EXPORT plg::vector<uint8_t>* CreateVectorUInt8(uint8_t* arr, int len) { return CreateVector(arr, len); }
	NETLM_EXPORT plg::vector<uint16_t>* CreateVectorUInt16(uint16_t* arr, int len) { return CreateVector(arr, len); }
	NETLM_EXPORT plg::vector<uint32_t>* CreateVectorUInt32(uint32_t* arr, int len) { return CreateVector(arr, len); }
	NETLM_EXPORT plg::vector<uint64_t>* CreateVectorUInt64(uint64_t* arr, int len) { return CreateVector(arr, len); }
	NETLM_EXPORT plg::vector<uintptr_t>* CreateVectorIntPtr(uintptr_t* arr, int len) { return CreateVector(arr, len); }
	NETLM_EXPORT plg::vector<float>* CreateVectorFloat(float* arr, int len) { return CreateVector(arr, len); }
	NETLM_EXPORT plg::vector<double>* CreateVectorDouble(double* arr, int len) { return CreateVector(arr, len); }
	NETLM_EXPORT plg::vector<plg::string>* CreateVectorString(char* arr[], int len) { return CreateVector(arr, len); }

	// AllocateVector Functions
	NETLM_EXPORT plg::vector<bool>* AllocateVectorBool() { return AllocateVector<bool>(); }
	NETLM_EXPORT plg::vector<char>* AllocateVectorChar8() { return AllocateVector<char>(); }
	NETLM_EXPORT plg::vector<char16_t>* AllocateVectorChar16() { return AllocateVector<char16_t>(); }
	NETLM_EXPORT plg::vector<int8_t>* AllocateVectorInt8() { return AllocateVector<int8_t>(); }
	NETLM_EXPORT plg::vector<int16_t>* AllocateVectorInt16() { return AllocateVector<int16_t>(); }
	NETLM_EXPORT plg::vector<int32_t>* AllocateVectorInt32() { return AllocateVector<int32_t>(); }
	NETLM_EXPORT plg::vector<int64_t>* AllocateVectorInt64() { return AllocateVector<int64_t>(); }
	NETLM_EXPORT plg::vector<uint8_t>* AllocateVectorUInt8() { return AllocateVector<uint8_t>(); }
	NETLM_EXPORT plg::vector<uint16_t>* AllocateVectorUInt16() { return AllocateVector<uint16_t>(); }
	NETLM_EXPORT plg::vector<uint32_t>* AllocateVectorUInt32() { return AllocateVector<uint32_t>(); }
	NETLM_EXPORT plg::vector<uint64_t>* AllocateVectorUInt64()  { return AllocateVector<uint64_t>(); }
	NETLM_EXPORT plg::vector<uintptr_t>* AllocateVectorIntPtr() { return AllocateVector<uintptr_t>(); }
	NETLM_EXPORT plg::vector<float>* AllocateVectorFloat() { return AllocateVector<float>(); }
	NETLM_EXPORT plg::vector<double>* AllocateVectorDouble() { return AllocateVector<double>(); }
	NETLM_EXPORT plg::vector<plg::string>* AllocateVectorString() { return AllocateVector<char*>(); }


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

	// Construct Functions

	NETLM_EXPORT void ConstructVectorBool(plg::vector<bool>* vector, bool* arr, int len) { ConstructVector(vector, arr, len); }
	NETLM_EXPORT void ConstructVectorChar8(plg::vector<char>* vector, char* arr, int len) { ConstructVector(vector, arr, len); }
	NETLM_EXPORT void ConstructVectorChar16(plg::vector<char16_t>* vector, char16_t* arr, int len) { ConstructVector(vector, arr, len); }
	NETLM_EXPORT void ConstructVectorInt8(plg::vector<int8_t>* vector, int8_t* arr, int len) { ConstructVector(vector, arr, len); }
	NETLM_EXPORT void ConstructVectorInt16(plg::vector<int16_t>* vector, int16_t* arr, int len) { ConstructVector(vector, arr, len); }
	NETLM_EXPORT void ConstructVectorInt32(plg::vector<int32_t>* vector, int32_t* arr, int len) { ConstructVector(vector, arr, len); }
	NETLM_EXPORT void ConstructVectorInt64(plg::vector<int64_t>* vector, int64_t* arr, int len) { ConstructVector(vector, arr, len); }
	NETLM_EXPORT void ConstructVectorUInt8(plg::vector<uint8_t>* vector, uint8_t* arr, int len) { ConstructVector(vector, arr, len); }
	NETLM_EXPORT void ConstructVectorUInt16(plg::vector<uint16_t>* vector, uint16_t* arr, int len) { ConstructVector(vector, arr, len); }
	NETLM_EXPORT void ConstructVectorUInt32(plg::vector<uint32_t>* vector, uint32_t* arr, int len) { ConstructVector(vector, arr, len); }
	NETLM_EXPORT void ConstructVectorUInt64(plg::vector<uint64_t>* vector, uint64_t* arr, int len) { ConstructVector(vector, arr, len); }
	NETLM_EXPORT void ConstructVectorIntPtr(plg::vector<uintptr_t>* vector, uintptr_t* arr, int len) { ConstructVector(vector, arr, len); }
	NETLM_EXPORT void ConstructVectorFloat(plg::vector<float>* vector, float* arr, int len) { ConstructVector(vector, arr, len); }
	NETLM_EXPORT void ConstructVectorDouble(plg::vector<double>* vector, double* arr, int len) { ConstructVector(vector, arr, len); }
	NETLM_EXPORT void ConstructVectorString(plg::vector<plg::string>* vector, char* arr[], int len)  { ConstructVector(vector, arr, len); }

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

	// DeleteVector Functions

	NETLM_EXPORT void DeleteVectorBool(plg::vector<bool>* vector) { DeleteVector(vector); }
	NETLM_EXPORT void DeleteVectorChar8(plg::vector<char>* vector) { DeleteVector(vector); }
	NETLM_EXPORT void DeleteVectorChar16(plg::vector<char16_t>* vector) { DeleteVector(vector); }
	NETLM_EXPORT void DeleteVectorInt8(plg::vector<int8_t>* vector) { DeleteVector(vector); }
	NETLM_EXPORT void DeleteVectorInt16(plg::vector<int16_t>* vector) { DeleteVector(vector); }
	NETLM_EXPORT void DeleteVectorInt32(plg::vector<int32_t>* vector) { DeleteVector(vector); }
	NETLM_EXPORT void DeleteVectorInt64(plg::vector<int64_t>* vector) { DeleteVector(vector); }
	NETLM_EXPORT void DeleteVectorUInt8(plg::vector<uint8_t>* vector) { DeleteVector(vector); }
	NETLM_EXPORT void DeleteVectorUInt16(plg::vector<uint16_t>* vector) { DeleteVector(vector); }
	NETLM_EXPORT void DeleteVectorUInt32(plg::vector<uint32_t>* vector) { DeleteVector(vector); }
	NETLM_EXPORT void DeleteVectorUInt64(plg::vector<uint64_t>* vector) { DeleteVector(vector); }
	NETLM_EXPORT void DeleteVectorIntPtr(plg::vector<uintptr_t>* vector) { DeleteVector(vector); }
	NETLM_EXPORT void DeleteVectorFloat(plg::vector<float>* vector) { DeleteVector(vector); }
	NETLM_EXPORT void DeleteVectorDouble(plg::vector<double>* vector) { DeleteVector(vector); }
	NETLM_EXPORT void DeleteVectorString(plg::vector<plg::string>* vector) { DeleteVector(vector); }

	// FreeVector functions

	NETLM_EXPORT void FreeVectorBool(plg::vector<bool>* vector) { FreeVector(vector); }
	NETLM_EXPORT void FreeVectorChar8(plg::vector<char>* vector) { FreeVector(vector); }
	NETLM_EXPORT void FreeVectorChar16(plg::vector<char16_t>* vector) { FreeVector(vector); }
	NETLM_EXPORT void FreeVectorInt8(plg::vector<int8_t>* vector) { FreeVector(vector); }
	NETLM_EXPORT void FreeVectorInt16(plg::vector<int16_t>* vector) { FreeVector(vector); }
	NETLM_EXPORT void FreeVectorInt32(plg::vector<int32_t>* vector) { FreeVector(vector); }
	NETLM_EXPORT void FreeVectorInt64(plg::vector<int64_t>* vector) { FreeVector(vector); }
	NETLM_EXPORT void FreeVectorUInt8(plg::vector<uint8_t>* vector) { FreeVector(vector); }
	NETLM_EXPORT void FreeVectorUInt16(plg::vector<uint16_t>* vector) { FreeVector(vector); }
	NETLM_EXPORT void FreeVectorUInt32(plg::vector<uint32_t>* vector) { FreeVector(vector); }
	NETLM_EXPORT void FreeVectorUInt64(plg::vector<uint64_t>* vector) { FreeVector(vector); }
	NETLM_EXPORT void FreeVectorIntPtr(plg::vector<uintptr_t>* vector) { FreeVector(vector); }
	NETLM_EXPORT void FreeVectorFloat(plg::vector<float>* vector) { FreeVector(vector); }
	NETLM_EXPORT void FreeVectorDouble(plg::vector<double>* vector) { FreeVector(vector); }
	NETLM_EXPORT void FreeVectorString(plg::vector<plg::string>* vector) { FreeVector(vector); }

	// DestroyVector functions

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
		++g_numberOfAllocs[type_id<JitCall>];
		return call;
	}

	NETLM_EXPORT void DeleteCall(JitCall* call) {
		delete call;
		--g_numberOfAllocs[type_id<JitCall>];
		assert(g_numberOfAllocs[type_id<JitCall>] != -1);
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
		++g_numberOfAllocs[type_id<JitCallback>];
		return callback;
	}

	NETLM_EXPORT void DeleteCallback(JitCallback* callback) {
		delete callback;
		--g_numberOfAllocs[type_id<JitCallback>];
		assert(g_numberOfAllocs[type_id<JitCallback>] != -1);
	}

	NETLM_EXPORT void* GetCallbackFunction(JitCallback* callback) {
		return callback ? callback->GetFunction() : nullptr;
	}

	NETLM_EXPORT char* GetCallbackError(JitCallback* callback) {
		return Memory::StringToHGlobalAnsi(callback ? callback->GetError().data() : "Method invalid");
	}
}
