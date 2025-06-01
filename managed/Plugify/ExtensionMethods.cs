namespace Plugify;

internal static class ExtensionMethods
{
	internal static bool IsDelegate(this Type type)
	{
		return typeof(MulticastDelegate).IsAssignableFrom(type.BaseType);
	}

	internal static Type? GetEnumType(this Type type)
	{
		Type baseType = type;
		if (type.IsByRef)
		{
			baseType = type.GetElementType()!;
		}
		
		if (baseType.IsEnum)
		{
			return baseType;
		}

		if (baseType.IsArray)
		{
			var elementType = baseType.GetElementType()!;
			if (elementType.IsEnum)
			{
				return elementType;
			}
		}

		return null;
	}
}
