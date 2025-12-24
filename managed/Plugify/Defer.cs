namespace Plugify;

internal class Defer(int capacity)
{
    private List<Action> _buffer = new(capacity);

    internal void Add(Action a)
    {
        _buffer.Add(a);
    }

    internal void Execute()
    {
        for (int i = _buffer.Count - 1; i >= 0; --i)
        {
            _buffer[i]();
        }
    }
}