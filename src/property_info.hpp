#pragma once

#include "core.hpp"
#include "native_string.hpp"

namespace netlm {
	class Type;
	class Attribute;

	class PropertyInfo {
	public:
		~PropertyInfo();

		std::string GetName() const;
		Type& GetType();

		std::vector<Attribute> GetAttributes() const;

		bool operator==(const PropertyInfo& other) const { return _handle == other._handle; }
		operator bool() const { return _handle; }
		ManagedHandle GetHandle() const { return _handle; }

	private:
		ManagedHandle _handle{};
		Type* _type = nullptr;

		friend class Type;
		friend class ManagedObject;
	};
}
