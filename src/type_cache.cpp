#include "type_cache.hpp"

using namespace netlm;

static TypeCache cache;

TypeCache& TypeCache::Get() {
    return cache;
}

Type* TypeCache::CacheType(ManagedHandle handle) {
    return &m_types.emplace_back(handle);
}

void TypeCache::Clear() {
    m_types.clear();
}