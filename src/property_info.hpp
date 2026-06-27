#pragma once

#include "core.hpp"
#include "native_string.hpp"

namespace netlm {
	class Type;
	class Attribute;

	class PropertyInfo {
	public:
		PropertyInfo() = delete;
		PropertyInfo(ManagedHandle handle) : _handle{handle} {}

		std::string GetName() const;
		Type& GetType();

		std::vector<Attribute> GetAttributes() const;

		bool operator==(const PropertyInfo& other) const { return _handle == other._handle; }
		explicit operator bool() const { return _handle != nullptr; }
		ManagedHandle GetHandle() const { return _handle; }

	private:
		ManagedHandle _handle{};
		Type* _type{};
	};
}
