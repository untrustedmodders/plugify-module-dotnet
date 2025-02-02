#pragma once

#include "core.hpp"
#include "method_info.hpp"

namespace netlm {
	class Type;
	class ManagedAssembly;

	class ManagedObject {
	public:
		template<typename TReturn, typename... TArgs>
		TReturn InvokeMethod(std::string_view methodName, TArgs&&... parameters) const {
			MethodInfo methodInfo = GetMethod(methodName);
			return InvokeMethodRaw<TReturn, TArgs...>(methodInfo, std::forward<TArgs>(parameters)...);
		}

		template<typename... TArgs>
		void InvokeMethod(std::string_view methodName, TArgs&&... parameters) const {
			MethodInfo methodInfo = GetMethod(methodName);
			InvokeMethodRaw<TArgs...>(methodInfo, std::forward<TArgs>(parameters)...);
		}

		template<typename TReturn, typename... TArgs>
		TReturn InvokeMethodRaw(const MethodInfo& methodInfo, TArgs&&... parameters) const {
			constexpr size_t parameterCount = sizeof...(parameters);

			TReturn result{};

			if constexpr (parameterCount > 0) {
				const void* parameterValues[] = { &parameters ... };
				InvokeMethodRetInternal(methodInfo._handle, parameterValues, parameterCount, &result);
			} else {
				InvokeMethodRetInternal(methodInfo._handle, nullptr, nullptr, 0, &result);
			}

			return result;
		}

		template<typename... TArgs>
		void InvokeMethodRaw(const MethodInfo& methodInfo, TArgs&&... parameters) const {
			constexpr size_t parameterCount = sizeof...(parameters);

			if constexpr (parameterCount > 0) {
				const void* parameterValues[] = { &parameters ... };
				InvokeMethodInternal(methodInfo._handle, parameterValues, parameterCount);
			} else {
				InvokeMethodInternal(methodInfo._handle, nullptr, 0);
			}
		}

		template<typename TValue>
		void SetFieldValue(std::string_view fieldName, TValue inValue) const {
			SetFieldValueRaw(fieldName, &inValue);
		}

		template<typename TReturn>
		TReturn GetFieldValue(std::string_view fieldName) const {
			TReturn result{};
			GetFieldValueRaw(fieldName, &result);
			return result;
		}

		template<typename TReturnPointer>
		TReturnPointer* GetFieldPointer(std::string_view fieldName) const {
			TReturnPointer* result = nullptr;
			GetFieldPointerRaw(fieldName, (void**) &result);
			return result;
		}

		template<typename TValue>
		void SetPropertyValue(std::string_view propertyName, TValue inValue) const {
			SetPropertyValueRaw(propertyName, &inValue);
		}

		template<typename TReturn>
		TReturn GetPropertyValue(std::string_view propertyName) const {
			TReturn result{};
			GetPropertyValueRaw(propertyName, &result);
			return result;
		}

		// TODO Cast string_view to string

		void SetFieldValueRaw(std::string_view fieldName, void* inValue) const;
		void GetFieldValueRaw(std::string_view fieldName, void* outValue) const;
		void GetFieldPointerRaw(std::string_view fieldName, void** outPointer) const;
		void SetPropertyValueRaw(std::string_view propertyName, void* inValue) const;
		void GetPropertyValueRaw(std::string_view propertyName, void* outValue) const;

		const Type& GetType() const;
		MethodInfo GetMethod(std::string_view methodName) const;

		void Destroy();

		bool operator==(const ManagedObject& other) const { return _handle == other._handle; }
		operator bool() const { return _handle != nullptr; }
		//void* GetHandle() const { return _handle; }

	//private:
		void InvokeMethodInternal(ManagedHandle methodHandle, const void** parameters, size_t length) const;
		void InvokeMethodRetInternal(ManagedHandle methodHandle, const void** parameters, size_t length, void* resultStorage) const;
		static void InvokeDelegateInternal(ManagedHandle delegateHandle, const void** parameters, size_t length);
		static void InvokeDelegateRetInternal(ManagedHandle delegateHandle, const void** parameters, size_t length, void* resultStorage);

	private:
		void* _handle = nullptr;
		Type* _type = nullptr;

	private:
		friend class ManagedAssembly;
		friend class Type;
	};
}