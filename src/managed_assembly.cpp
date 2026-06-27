#include "managed_assembly.hpp"
#include "managed_functions.hpp"
#include "type_cache.hpp"
#include "utils.hpp"

using namespace netlm;

static ManagedGuid InvalidId{};
static ManagedHandle InvalidHandle{};
static ManagedAssembly InvalidAssembly{InvalidId};
static Type InvalidType{InvalidHandle};

void AssemblyLoader::Unload() {
	for (auto it = _assemblies.rbegin(); it != _assemblies.rend(); ++it) {
		[[maybe_unused]] Bool32 result = Managed.UnloadManagedAssemblyFptr(it->GetID());
	}

	_assemblies.clear();
	_error.clear();
}

ManagedAssembly& AssemblyLoader::LoadAssembly(const fs::path& assemblyPath) {
	std::error_code ec;
	auto absolutePath = fs::absolute(assemblyPath, ec);
	if (ec) {
		_error = std::format("Invalid file path: {}", assemblyPath.string());
		return InvalidAssembly;
	}

	auto path = String::New(absolutePath.c_str());
	auto id = Managed.LoadManagedAssemblyFptr(path, true, true);
	String::Free(path);

	switch (Managed.GetLastLoadStatusFptr()) {
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

	return _assemblies.emplace_back(id);
}

ManagedAssembly& AssemblyLoader::FindAssembly(ManagedGuid assemblyId) {
	auto it = std::find_if(_assemblies.begin(), _assemblies.end(), [&assemblyId](const ManagedAssembly& assembly) {
		return assembly.GetID() == assemblyId;
	});

	if (it != _assemblies.end()) {
		return *it;
	}

	return InvalidAssembly;
}

std::string ManagedAssembly::GetFullName() const {
	auto name = Managed.GetAssemblyNameFptr(_id);
	std::string str(name);
	String::Free(name);
	return str;
}

const std::vector<Type*>& ManagedAssembly::GetTypes() {
	if (!_types) {
		_types.emplace();

		int32_t typeCount = 0;
		Managed.GetAssemblyTypesFptr(_id, nullptr, &typeCount);
		std::vector<ManagedHandle> typeHandles(static_cast<size_t>(typeCount));
		Managed.GetAssemblyTypesFptr(_id, typeHandles.data(), &typeCount);

		_types->reserve(typeHandles.size());
		for (auto typeHandle : typeHandles) {
			_types->emplace_back(TypeCache::Get().Add(typeHandle));
		}
	}
	return *_types;
}

void ManagedAssembly::AddInternalCall(std::string_view className, std::string_view variableName, void* functionPtr) {
	assert(functionPtr != nullptr);

	std::string assemblyQualifiedName(std::format("{}@{}, {}", className, variableName, GetFullName()));

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
	for (auto& type : GetTypes()) {
		if (type->GetFullName() == className) {
			return *type;
		}
	}
	return InvalidType;
}

Type& ManagedAssembly::GetTypeByBaseType(std::string_view baseName) {
	for (auto& type : GetTypes()) {
		if (type->GetBaseType().GetFullName() == baseName) {
			return *type;
		}
	}
	return InvalidType;
}
