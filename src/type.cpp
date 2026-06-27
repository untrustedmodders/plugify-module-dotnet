#include "type.hpp"
#include "attribute.hpp"
#include "managed_functions.hpp"
#include "type_cache.hpp"

using namespace netlm;

std::string Type::GetFullName() const {
	auto name = Managed.GetFullTypeNameFptr(_handle);
	std::string str(name);
	String::Free(name);
	return str;
}

std::string Type::GetAssemblyQualifiedName() const {
	auto name = Managed.GetAssemblyQualifiedNameFptr(_handle);
	std::string str(name);
	String::Free(name);
	return str;
}

Type& Type::GetBaseType() {
	if (!_baseType) {
		ManagedHandle handle{};
		Managed.GetBaseTypeFptr(_handle, &handle);
		_baseType = TypeCache::Get().CacheType(handle);
	}

	return *_baseType;
}

int32_t Type::GetSize() const {
	return Managed.GetTypeSizeFptr(_handle);
}

bool Type::IsSubclassOf(const Type& other) const {
	return Managed.IsTypeSubclassOfFptr(_handle, other._handle);
}

bool Type::IsAssignableTo(const Type& other) const {
	return Managed.IsTypeAssignableToFptr(_handle, other._handle);
}

bool Type::IsAssignableFrom(const Type& other) const {
	return Managed.IsTypeAssignableFromFptr(_handle, other._handle);
}

std::vector<MethodInfo> Type::GetMethods() const {
	int32_t methodCount = 0;
	Managed.GetTypeMethodsFptr(_handle, nullptr, &methodCount);
	std::vector<ManagedHandle> methodHandles(static_cast<size_t>(methodCount));
	Managed.GetTypeMethodsFptr(_handle, methodHandles.data(), &methodCount);

	std::vector<MethodInfo> result;
	result.reserve(methodHandles.size());
	for (const auto& methodHandle : methodHandles)
		result.emplace_back(methodHandle);

	return result;
}

std::vector<FieldInfo> Type::GetFields() const {
	int32_t fieldCount = 0;
	Managed.GetTypeFieldsFptr(_handle, nullptr, &fieldCount);
	std::vector<ManagedHandle> fieldHandles(static_cast<size_t>(fieldCount));
	Managed.GetTypeFieldsFptr(_handle, fieldHandles.data(), &fieldCount);

	std::vector<FieldInfo> result;
	result.reserve(fieldHandles.size());
	for (const auto& fieldHandle : fieldHandles)
		result.emplace_back(fieldHandle);

	return result;
}

std::vector<PropertyInfo> Type::GetProperties() const {
	int32_t propertyCount = 0;
	Managed.GetTypePropertiesFptr(_handle, nullptr, &propertyCount);
	std::vector<ManagedHandle> propertyHandles(static_cast<size_t>(propertyCount));
	Managed.GetTypePropertiesFptr(_handle, propertyHandles.data(), &propertyCount);

	std::vector<PropertyInfo> result;
	result.reserve(propertyHandles.size());
	for (const auto& propertyHandle : propertyHandles)
		result.emplace_back(propertyHandle);

	return result;
}

MethodInfo Type::GetMethod(std::string_view methodName) const {
	auto name = String::New(methodName);
	ManagedHandle handle{};
	Managed.GetTypeMethodFptr(_handle, name, &handle);
	String::Free(name);

	return handle;
}

FieldInfo Type::GetField(std::string_view fieldName) const {
	auto name = String::New(fieldName);
	ManagedHandle handle{};
	Managed.GetTypeFieldFptr(_handle, name, &handle);
	String::Free(name);

	return handle;
}

PropertyInfo Type::GetProperty(std::string_view propertyName) const {
	auto name = String::New(propertyName);
	ManagedHandle handle{};
	Managed.GetTypePropertyFptr(_handle, name, &handle);
	String::Free(name);

	return handle;
}

bool Type::HasAttribute(const Type& attributeType) const {
	return Managed.HasTypeAttributeFptr(_handle, attributeType._handle);
}

std::vector<Attribute> Type::GetAttributes() const {
	int32_t attributeCount;
	Managed.GetTypeAttributesFptr(_handle, nullptr, &attributeCount);
	std::vector<ManagedHandle> attributeHandles(static_cast<size_t>(attributeCount));
	Managed.GetTypeAttributesFptr(_handle, attributeHandles.data(), &attributeCount);

	std::vector<Attribute> result;
	result.reserve(attributeHandles.size());
	for (const auto& attributeHandle : attributeHandles)
		result.emplace_back(attributeHandle);

	return result;
}

ManagedType Type::GetManagedType() const {
	return Managed.GetTypeManagedTypeFptr(_handle);
}

bool Type::IsClass() const {
	return Managed.IsClassFptr(_handle);
}

bool Type::IsEnum() const {
	return Managed.IsEnumFptr(_handle);
}

bool Type::IsValueType() const {
	return Managed.IsValueTypeFptr(_handle);
}

bool Type::IsSZArray() const {
	return Managed.IsTypeSZArrayFptr(_handle);
}

bool Type::IsByRef() const {
	return Managed.IsTypeByRefFptr(_handle);
}

std::vector<std::string> Type::GetEnumNames() const {
	int size;
	Managed.GetEnumNamesFptr(_handle, nullptr, &size);
	std::vector<String> names(static_cast<size_t>(size));
	Managed.GetEnumNamesFptr(_handle, names.data(), &size);
	std::vector<std::string> namesStr;
	namesStr.reserve(static_cast<size_t>(size));
	for (auto& name : names) {
		namesStr.emplace_back(name);
		String::Free(name);
	}
	return namesStr;
}

std::vector<int64_t> Type::GetEnumValues() const {
	int size;
	Managed.GetEnumValuesFptr(_handle, nullptr, &size);
	std::vector<int64_t> values(static_cast<size_t>(size));
	Managed.GetEnumValuesFptr(_handle, values.data(), &size);
	return values;
}

Type& Type::GetElementType() {
	if (!_elementType) {
		ManagedHandle handle{};
		Managed.GetElementTypeFptr(_handle, &handle);
		_elementType = TypeCache::Get().CacheType(handle);
	}

	return *_elementType;
}

// TODO: Cache methods

ManagedObject Type::CreateInstanceInternal(const void** parameters, size_t length) const {
	ManagedHandle handle = Managed.CreateObjectFptr(_handle, false, parameters, static_cast<int32_t>(length));
	return ManagedObject{ handle, const_cast<Type*>(this) };
}

void Type::InvokeStaticMethodInternal(ManagedHandle methodHandle, const void** parameters, size_t length) const {
	Managed.InvokeStaticMethodFptr(_handle, methodHandle, parameters, static_cast<int32_t>(length));
}

void Type::InvokeStaticMethodRetInternal(ManagedHandle methodHandle, const void** parameters, size_t length, void* resultStorage) const {
	Managed.InvokeStaticMethodRetFptr(_handle, methodHandle, parameters, static_cast<int32_t>(length), resultStorage);
}
