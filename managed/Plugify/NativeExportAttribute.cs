namespace Plugify;

[AttributeUsage(AttributeTargets.Method)]
public class NativeExportAttribute : Attribute
{
    public string Name { get; }

    public NativeExportAttribute(string name)
    {
        Name = name;
    }
}