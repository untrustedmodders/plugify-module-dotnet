#include "property_info.hpp"
#include "attribute.hpp"
#include "managed_functions.hpp"
#include "type_cache.hpp"
#include "type.hpp"

using namespace netlm;

std::string PropertyInfo::GetName() const {
	auto name = Managed.GetPropertyInfoNameFptr(_handle);
	std::string str(name);
	String::Free(name);
	return str;
}

Type& PropertyInfo::GetType() {
	if (!_type) {
		ManagedHandle handle{};
		Managed.GetPropertyInfoTypeFptr(_handle, &handle);
		_type = TypeCache::Get().Add(handle);
	}

	return *_type;
}

std::vector<Attribute> PropertyInfo::GetAttributes() const {
	int32_t attributeCount;
	Managed.GetPropertyInfoAttributesFptr(_handle, nullptr, &attributeCount);
	std::vector<ManagedHandle> attributeHandles(static_cast<size_t>(attributeCount));
	Managed.GetPropertyInfoAttributesFptr(_handle, attributeHandles.data(), &attributeCount);

	std::vector<Attribute> result;
	result.reserve(attributeHandles.size());
	for (const auto& attributeHandle : attributeHandles)
		result.emplace_back(attributeHandle);

	return result;
}
