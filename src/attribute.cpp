#include "attribute.hpp"
#include "managed_functions.hpp"
#include "type_cache.hpp"
#include "type.hpp"

using namespace netlm;

Type& Attribute::GetType() {
	if (!_type) {
		ManagedHandle handle{};
		Managed.GetAttributeTypeFptr(_handle, &handle);
		_type = TypeCache::Get().CacheType(handle);
	}

	return *_type;
}

void Attribute::GetFieldValue(std::string_view fieldName, void* valueVptr) const {
	auto field = String::New(fieldName);
	Managed.GetAttributeFieldValueFptr(_handle, field, valueVptr);
	String::Free(field);
}
