#pragma once

#include "core.hpp"
#include "native_string.hpp"

#include "plg/inplace_vector.hpp"

namespace netlm {
	class Type;
	class Attribute;

	class MethodInfo {
	public:
		MethodInfo() = delete;
		MethodInfo(ManagedHandle handle) : _handle{handle} {}

		std::string GetName() const;
		void* GetFunctionAddress() const;

		Type& GetReturnType();
		const std::vector<Type*>& GetParameterTypes();

		TypeAccessibility GetAccessibility() const;

		std::vector<Attribute> GetAttributes() const;
		std::vector<Attribute> GetParameterAttributes(size_t index) const;
		std::vector<Attribute> GetReturnAttributes() const;

		bool operator==(const MethodInfo& other) const { return _handle == other._handle; }
		explicit operator bool() const { return _handle != nullptr; }
		ManagedHandle GetHandle() const { return _handle; }

	private:
		ManagedHandle _handle{};
		Type* _returnType{};
		std::optional<std::vector<Type*>> _parameterTypes;
	};
}
