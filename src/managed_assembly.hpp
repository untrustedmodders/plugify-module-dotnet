#pragma once

#include "core.hpp"
#include "type.hpp"

namespace netlm {

	enum class AssemblyLoadStatus {
		Success,
		FileNotFound,
		FileLoadFailure,
		InvalidFilePath,
		InvalidAssembly,
		UnknownError
	};

	class ManagedAssembly {
	public:
		ManagedGuid GetAssemblyID() const { return _assemblyId; }
		AssemblyLoadStatus GetLoadStatus() const { return _loadStatus; }
		std::string_view GetFullName() const { return _name; }

		void AddInternalCall(std::string_view className, std::string_view variableName, void* functionPtr);
		void UploadInternalCalls(bool warnOnMissing = true);

		Type& GetType(std::string_view className);
		const std::vector<Type>& GetTypes() const { return _types; }
		Type& GetTypeByBaseType(std::string_view baseName);

		bool operator==(const ManagedAssembly& other) const { return _assemblyId == other._assemblyId; }
		operator bool() const { return _assemblyId; }

	private:
		ManagedGuid _assemblyId{};
		AssemblyLoadStatus _loadStatus = AssemblyLoadStatus::UnknownError;
		std::string _name;
		std::vector<string_t> _internalCallNameStorage;
		std::vector<InternalCall> _internalCalls;
		std::vector<Type> _types;

		static inline Type InvalidType;

		friend class HostInstance;
		friend class AssemblyLoader;
	};

	using AssemblyList = std::vector<ManagedAssembly>;

	class AssemblyLoader {
	public:
		void Unload();

		ManagedAssembly& LoadAssembly(const fs::path& assemblyPath);
		ManagedAssembly& FindAssembly(ManagedGuid assemblyId);
		AssemblyList& GetLoadedAssemblies() { return _assemblies; }
		const std::string& GetError() { return _error; }

	private:
		AssemblyList _assemblies;
		std::string _error;

		static inline ManagedAssembly InvalidAssembly;

		friend class HostInstance;
	};
}