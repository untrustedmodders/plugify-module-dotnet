#include "managed_assembly.hpp"
#include "managed_functions.hpp"
#include "utils.hpp"

using namespace netlm;

void AssemblyLoader::Unload() {
	for (auto it = _assemblies.rbegin(); it != _assemblies.rend(); ++it) {
		[[maybe_unused]] Bool32 result = Managed.UnloadManagedAssemblyFptr(it->_assemblyId);
	}

	_assemblies.clear();
	_error.clear();
}

ManagedAssembly& AssemblyLoader::LoadAssembly(const fs::path& assemblyPath) {
	std::error_code ec;
	auto absolutePath= fs::absolute(assemblyPath, ec);
	if (ec) {
		_error = std::format("Invalid file path: {}", assemblyPath.string());
		return InvalidAssembly;
	}

	auto path = String::New(absolutePath.c_str());
	auto assemblyId = Managed.LoadManagedAssemblyFptr(path, true, true);
	auto loadStatus = Managed.GetLastLoadStatusFptr();
	String::Free(path);

	switch (loadStatus) {
		case AssemblyLoadStatus::FileNotFound:
			_error = "File not found";
			return InvalidAssembly;
		case AssemblyLoadStatus::FileLoadFailure:
			_error = "File load failure";
			return InvalidAssembly;
		case AssemblyLoadStatus::InvalidFilePath:
			_error = "Invalid file path";
			return InvalidAssembly;
		case AssemblyLoadStatus::InvalidAssembly:
			_error = "Invalid assembly";
			return InvalidAssembly;
		case AssemblyLoadStatus::UnknownError:
			_error = "Unknown error";
			return InvalidAssembly;
		case AssemblyLoadStatus::Success:
			break;
	}

	auto& assembly = _assemblies.emplace_back();

	assembly._assemblyId = assemblyId;
	assembly._loadStatus = loadStatus;

	auto name = Managed.GetAssemblyNameFptr(assembly._assemblyId);
	assembly._name = name;
	String::Free(name);

	int32_t typeCount = 0;
	Managed.GetAssemblyTypesFptr(assembly._assemblyId, nullptr, &typeCount);
	std::vector<ManagedHandle> typeHandles(static_cast<size_t>(typeCount));
	Managed.GetAssemblyTypesFptr(assembly._assemblyId, typeHandles.data(), &typeCount);

	for (auto type : typeHandles) {
		assembly._types.emplace_back(type);
	}

	return assembly;
}

ManagedAssembly& AssemblyLoader::FindAssembly(ManagedGuid assemblyId) {
	auto it = std::find_if(_assemblies.begin(), _assemblies.end(), [assemblyId](const ManagedAssembly& assembly) {
		return assembly._assemblyId == assemblyId;
	});

	if (it != _assemblies.end()) {
		return *it;
	}

	return InvalidAssembly;
}

void ManagedAssembly::AddInternalCall(std::string_view className, std::string_view variableName, void* functionPtr) {
	assert(functionPtr != nullptr);

	std::string assemblyQualifiedName(std::format("{}@{}, {}", className, variableName, _name));

#if NETLM_PLATFORM_WINDOWS
	const auto& name = _internalCallNameStorage.emplace_back(Utils::ConvertUtf8ToWide(assemblyQualifiedName));
#else
	const auto& name = _internalCallNameStorage.emplace_back(std::move(assemblyQualifiedName));
#endif

	_internalCalls.emplace_back(name.c_str(), functionPtr);
}

void ManagedAssembly::UploadInternalCalls(bool warnOnMissing) {

	Managed.SetInternalCallsFptr(_internalCalls.data(), static_cast<int32_t>(_internalCalls.size()), warnOnMissing);

	_internalCalls.clear();
	_internalCallNameStorage.clear();
}

Type& ManagedAssembly::GetType(std::string_view className) {
	for (auto& type : _types) {
		if (type.GetFullName() == className) {
			return type;
		}
	}
	return InvalidType;
}

Type& ManagedAssembly::GetTypeByBaseType(std::string_view baseName) {
	for (auto& type : _types) {
		if (type.GetBaseType().GetFullName() == baseName) {
			return type;
		}
	}
	return InvalidType;
}
