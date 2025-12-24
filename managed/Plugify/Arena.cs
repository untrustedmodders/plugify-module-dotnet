using System.Runtime.CompilerServices;

namespace Plugify;

internal ref struct Arena(Span<byte> backing)
{
    private Span<byte> buffer = backing;
    private int offset = 0;

    internal static int AlignUp(int val, int alignment = 16)
        => (val + (alignment - 1)) & ~(alignment - 1);

    internal unsafe void* Alloc(int size, int alignment = 16)
    {
        int aligned = AlignUp(offset, alignment);
        int newNext = aligned + size;

        if (newNext > buffer.Length)
            throw new InvalidOperationException("Memory pool exhausted");
        
        void* ptr = Unsafe.AsPointer(ref buffer[aligned]);
        offset = newNext;
        return ptr;
    }

    internal void Reset()
    {
        offset = 0;
    }
}