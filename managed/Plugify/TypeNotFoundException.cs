namespace Plugify;

internal class TypeNotFoundException : Exception
{
	internal TypeNotFoundException()
	{
	}

	internal TypeNotFoundException(string message)
		: base(message)
	{
	}

	internal TypeNotFoundException(string message, Exception inner)
		: base(message, inner)
	{
	}
}
