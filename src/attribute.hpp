#pragma once

#include "core.hpp"

namespace netlm {
	class Type;

	class Attribute {
	public:
		Attribute() = delete;
		Attribute(ManagedHandle handle) : _handle{handle} {}

		Type& GetType();

		template<typename TReturn>
		TReturn GetFieldValue(std::string_view fieldName) {
			TReturn result{};
			GetFieldValue(fieldName, &result);
			return result;
		}

		bool operator==(const Attribute& other) const { return _handle == other._handle; }
		explicit operator bool() const { return _handle != nullptr; }
		ManagedHandle GetHandle() const { return _handle; }

	private:
		void GetFieldValue(std::string_view fieldName, void* valueVptr) const;

	private:
		ManagedHandle _handle{};
		Type* _type{};
	};
}
