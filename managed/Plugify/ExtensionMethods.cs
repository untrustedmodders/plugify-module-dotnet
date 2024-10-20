namespace Plugify;

internal static class ExtensionMethods
{
	internal static bool IsDelegate(this Type type)
	{
		return typeof(MulticastDelegate).IsAssignableFrom(type.BaseType);
	}
}
