#include "attribute.hpp"
#include "managed_functions.hpp"
#include "type.hpp"

using namespace netlm;

Attribute::~Attribute() {
	delete _type;
}

Type& Attribute::GetType() {
	if (!_type) {
		_type = new Type();
		Managed.GetAttributeTypeFptr(_handle, &_type->_handle);
	}

	return *_type;
}

void Attribute::GetFieldValue(std::string_view fieldName, void* valueVptr) const {
	auto field = String::New(fieldName);
	Managed.GetAttributeFieldValueFptr(_handle, field, valueVptr);
	String::Free(field);
}
