#include "gc.hpp"
#include "managed_functions.hpp"

using namespace netlm;

void GC::Collect() {
	Collect(-1, GCCollectionMode::Default, true, false);
}

void GC::Collect(int32_t generation, GCCollectionMode collectionMode, bool blocking, bool compacting) {
	Managed.CollectGarbageFptr(generation, collectionMode, blocking, compacting);
}

void GC::WaitForPendingFinalizers() {
	Managed.WaitForPendingFinalizersFptr();
}
