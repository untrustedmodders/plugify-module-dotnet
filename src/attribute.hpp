#pragma once

#include "core.hpp"

namespace netlm {
	class Type;

	class Attribute {
	public:
		~Attribute();

		Type& GetType();

		template<typename TReturn>
		TReturn GetFieldValue(std::string_view fieldName) {
			TReturn result{};
			GetFieldValue(fieldName, &result);
			return result;
		}

		bool operator==(const Attribute& other) const { return _handle == other._handle; }
		operator bool() const { return _handle; }
		ManagedHandle GetHandle() const { return _handle; }

	private:
		void GetFieldValue(std::string_view fieldName, void* valueVptr) const;

	private:
		ManagedHandle _handle{};
		Type* _type = nullptr;

		friend class Type;
		friend class MethodInfo;
		friend class FieldInfo;
		friend class PropertyInfo;
		friend class ManagedObject;
	};
}
