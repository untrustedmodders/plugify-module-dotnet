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
		ManagedAssembly() = delete;
		ManagedAssembly(ManagedGuid id) : _id{id} {}

		ManagedGuid GetID() const { return _id; }
		std::string GetFullName() const;

		void AddInternalCall(std::string_view className, std::string_view variableName, void* functionPtr);
		void UploadInternalCalls(bool warnOnMissing = true);

		const std::vector<Type*>& GetTypes();
		Type& GetType(std::string_view className);
		Type& GetTypeByBaseType(std::string_view baseName);

		bool operator==(const ManagedAssembly& other) const { return _id == other._id; }
		explicit operator bool() const { return static_cast<bool>(_id); }

	private:
		ManagedGuid _id{};
		std::optional<std::vector<Type*>> _types;
		std::deque<string_t> _internalCallNameStorage;
		std::vector<InternalCall> _internalCalls;
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
	};
}
