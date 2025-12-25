using System.Runtime.CompilerServices;

namespace Plugify;

internal ref struct Arena(Span<byte> backing)
{
    private Span<byte> buffer = backing;
    private int offset = 0;

    public static int AlignUp(int val, int alignment = 16)
        => (val + (alignment - 1)) & ~(alignment - 1);

    public unsafe void* Alloc(int size, int alignment = 16)
    {
        int aligned = AlignUp(offset, alignment);
        int newOffset = aligned + size;

        if (newOffset > buffer.Length) 
            throw new InvalidOperationException("Memory pool exhausted");
        
        void* ptr = Unsafe.AsPointer(ref buffer[aligned]);
        offset = newOffset;
        return ptr;
    }

    public void Reset() => offset = 0;
    
    public int Size => buffer.Length;
        
    public int Remaining => buffer.Length - offset;
    
    public int Used => offset;
}