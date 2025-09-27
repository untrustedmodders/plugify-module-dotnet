#pragma once

#include "core.hpp"
#include "native_string.hpp"

#include "plg/inplace_vector.hpp"

namespace netlm {
	class Type;
	class Attribute;

	class MethodInfo {
	public:
		~MethodInfo();

		std::string GetName() const;
		void* GetFunctionAddress() const;

		Type& GetReturnType();
		const std::vector<Type>& GetParameterTypes();

		TypeAccessibility GetAccessibility() const;

		std::vector<Attribute> GetAttributes() const;
		std::vector<Attribute> GetParameterAttributes(size_t index) const;
		std::vector<Attribute> GetReturnAttributes() const;

		bool operator==(const MethodInfo& other) const { return _handle == other._handle; }
		operator bool() const { return _handle; }
		ManagedHandle GetHandle() const { return _handle; }

	private:
		ManagedHandle _handle{};
		Type* _returnType = nullptr;
		std::vector<Type>* _parameterTypes = nullptr;

		friend class Type;
		friend class ManagedObject;
	};
}
