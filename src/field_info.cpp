#include "field_info.hpp"
#include "attribute.hpp"
#include "managed_functions.hpp"
#include "type_cache.hpp"
#include "type.hpp"

using namespace netlm;

std::string FieldInfo::GetName() const {
	auto name = Managed.GetFieldInfoNameFptr(_handle);
	std::string str(name);
	String::Free(name);
	return str;
}

Type& FieldInfo::GetType() {
	if (!_type) {
		ManagedHandle handle{};
		Managed.GetFieldInfoTypeFptr(_handle, &handle);
		_type = TypeCache::Get().CacheType(handle);
	}

	return *_type;
}

TypeAccessibility FieldInfo::GetAccessibility() const {
	return Managed.GetFieldInfoAccessibilityFptr(_handle);
}

std::vector<Attribute> FieldInfo::GetAttributes() const {
	int32_t attributeCount;
	Managed.GetFieldInfoAttributesFptr(_handle, nullptr, &attributeCount);
	std::vector<ManagedHandle> attributeHandles(static_cast<size_t>(attributeCount));
	Managed.GetFieldInfoAttributesFptr(_handle, attributeHandles.data(), &attributeCount);

	std::vector<Attribute> result;
	result.reserve(attributeHandles.size());
	for (const auto& attributeHandle : attributeHandles)
		result.emplace_back(attributeHandle);

	return result;
}
