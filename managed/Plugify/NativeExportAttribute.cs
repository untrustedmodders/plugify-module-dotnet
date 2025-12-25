namespace Plugify;

[AttributeUsage(AttributeTargets.Method)]
public class NativeExportAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}