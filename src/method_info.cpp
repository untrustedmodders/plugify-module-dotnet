#include "method_info.hpp"
#include "attribute.hpp"
#include "managed_functions.hpp"
#include "type_cache.hpp"
#include "type.hpp"

using namespace netlm;

std::string MethodInfo::GetName() const {
	auto name = Managed.GetMethodInfoNameFptr(_handle);
	std::string str(name);
	String::Free(name);
	return str;
}

void* MethodInfo::GetFunctionAddress() const {
	return Managed.GetMethodInfoFunctionAddressFptr(_handle);
}

Type& MethodInfo::GetReturnType() {
	if (!_returnType) {
		ManagedHandle handle{};
		Managed.GetMethodInfoReturnTypeFptr(_handle, &handle);
		_returnType = TypeCache::Get().CacheType(handle);
	}

	return *_returnType;
}

const std::vector<Type*>& MethodInfo::GetParameterTypes() {
	if (!_parameterTypes) {
		_parameterTypes.emplace();

		int32_t typeCount;
		Managed.GetMethodInfoParameterTypesFptr(_handle, nullptr, &typeCount);
		std::vector<ManagedHandle> typeHandles(static_cast<size_t>(typeCount));
		Managed.GetMethodInfoParameterTypesFptr(_handle, typeHandles.data(), &typeCount);

		_parameterTypes->reserve(typeHandles.size());
		for (const auto& typeHandle : typeHandles) {
			_parameterTypes->emplace_back(TypeCache::Get().CacheType(typeHandle));
		}
	}

	return *_parameterTypes;
}

TypeAccessibility MethodInfo::GetAccessibility() const {
	return Managed.GetMethodInfoAccessibilityFptr(_handle);
}

std::vector<Attribute> MethodInfo::GetAttributes() const {
	int32_t attributeCount;
	Managed.GetMethodInfoAttributesFptr(_handle, nullptr, &attributeCount);
	std::vector<ManagedHandle> attributeHandles(static_cast<size_t>(attributeCount));
	Managed.GetMethodInfoAttributesFptr(_handle, attributeHandles.data(), &attributeCount);

	std::vector<Attribute> result;
	result.reserve(attributeHandles.size());
	for (const auto& attributeHandle : attributeHandles)
		result.emplace_back(attributeHandle);

	return result;
}

std::vector<Attribute> MethodInfo::GetParameterAttributes(size_t index) const {
	int32_t parameterIndex = static_cast<int32_t>(index);

	int32_t attributeCount;
	Managed.GetMethodInfoParameterAttributesFptr(_handle, parameterIndex, nullptr, &attributeCount);
	std::vector<ManagedHandle> attributeHandles(static_cast<size_t>(attributeCount));
	Managed.GetMethodInfoParameterAttributesFptr(_handle, parameterIndex, attributeHandles.data(), &attributeCount);

	std::vector<Attribute> result;
	result.reserve(attributeHandles.size());
	for (const auto& attributeHandle : attributeHandles)
		result.emplace_back(attributeHandle);

	return result;
}

std::vector<Attribute> MethodInfo::GetReturnAttributes() const {
	int32_t attributeCount;
	Managed.GetMethodInfoReturnAttributesFptr(_handle, nullptr, &attributeCount);
	std::vector<ManagedHandle> attributeHandles(static_cast<size_t>(attributeCount));
	Managed.GetMethodInfoReturnAttributesFptr(_handle, attributeHandles.data(), &attributeCount);

	std::vector<Attribute> result;
	result.reserve(attributeHandles.size());
	for (const auto& attributeHandle : attributeHandles)
		result.emplace_back(attributeHandle);

	return result;
}
