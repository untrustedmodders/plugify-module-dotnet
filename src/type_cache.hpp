#pragma once

#include "type.hpp"

namespace netlm {
    class TypeCache {
    public:
        static TypeCache& Get();

        Type* CacheType(ManagedHandle handle);

        void Clear();

    private:
        std::deque<Type> m_types;
    };
}