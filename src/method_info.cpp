#include "method_info.hpp"
#include "attribute.hpp"
#include "managed_functions.hpp"
#include "type.hpp"

using namespace netlm;

MethodInfo::~MethodInfo() {
	delete _parameterTypes;
	delete _returnType;
}

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
		_returnType = new Type();
		Managed.GetMethodInfoReturnTypeFptr(_handle, &_returnType->_handle);
	}

	return *_returnType;
}

const std::vector<Type>& MethodInfo::GetParameterTypes() {
	if (!_parameterTypes) {
		int32_t parameterCount;
		Managed.GetMethodInfoParameterTypesFptr(_handle, nullptr, &parameterCount);
		std::vector<ManagedHandle> parameterTypes(static_cast<size_t>(parameterCount));
		Managed.GetMethodInfoParameterTypesFptr(_handle, parameterTypes.data(), &parameterCount);

		_parameterTypes = new std::vector<Type>();
		_parameterTypes->reserve(parameterTypes.size());
		for (size_t i = 0; i < parameterTypes.size(); i++)
			_parameterTypes->emplace_back(parameterTypes[i]);
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

	std::vector<Attribute> result(attributeHandles.size());
	for (size_t i = 0; i < attributeHandles.size(); i++)
		result[i]._handle = attributeHandles[i];

	return result;
}

std::vector<Attribute> MethodInfo::GetParameterAttributes(size_t index) const {
	int32_t parameterIndex = static_cast<int32_t>(index);

	int32_t attributeCount;
	Managed.GetMethodInfoParameterAttributesFptr(_handle, parameterIndex, nullptr, &attributeCount);
	std::vector<ManagedHandle> attributeHandles(static_cast<size_t>(attributeCount));
	Managed.GetMethodInfoParameterAttributesFptr(_handle, parameterIndex, attributeHandles.data(), &attributeCount);

	std::vector<Attribute> result(attributeHandles.size());
	for (size_t i = 0; i < attributeHandles.size(); i++)
		result[i]._handle = attributeHandles[i];

	return result;
}

std::vector<Attribute> MethodInfo::GetReturnAttributes() const {
	int32_t attributeCount;
	Managed.GetMethodInfoReturnAttributesFptr(_handle, nullptr, &attributeCount);
	std::vector<ManagedHandle> attributeHandles(static_cast<size_t>(attributeCount));
	Managed.GetMethodInfoReturnAttributesFptr(_handle, attributeHandles.data(), &attributeCount);

	std::vector<Attribute> result(attributeHandles.size());
	for (size_t i = 0; i < attributeHandles.size(); i++)
		result[i]._handle = attributeHandles[i];

	return result;
}
