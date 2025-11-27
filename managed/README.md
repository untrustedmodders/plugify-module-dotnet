# Plugify Manifest Generator

This document explains the Plugify manifest generator system for automatically creating `.pplugin` manifest files from C# plugins.

## Overview

The Plugify.Generators project contains source generators that automatically create `.pplugin` manifest files by analyzing your C# code for methods marked with the `[NativeExport]` attribute.

## Components

### 1. NativeExportAttribute (`Plugify/NativeExportAttribute.cs`)

```csharp
[AttributeUsage(AttributeTargets.Method)]
public class NativeExportAttribute : Attribute
{
    public string Name { get; }

    public NativeExportAttribute(string name)
    {
        Name = name;
    }
}
```

Use this attribute to mark static methods that should be exported in the plugin manifest.

### 2. ManifestFileGenerator (`Plugify.Generators/ManifestFileGenerator.cs`)

A Roslyn source generator that:
- Scans for methods with `[NativeExport]` attribute
- Maps C# types to Plugify manifest types
- Generates a C# class containing the manifest JSON
- Uses a ModuleInitializer to write the `.pplugin` file at runtime

### 3. TestGenerator (`Plugify.Generators/TestGenerator.cs`)

A simple diagnostic generator to verify the generator infrastructure is working.

## Usage

### Step 1: Mark Methods for Export

In your plugin project, mark static methods with `[NativeExport]`:

```csharp
using Plugify;
using System.Numerics;

namespace MyPlugin
{
    public static class ExportedFunctions
    {
        [NativeExport("AddNumbers")]
        public static int AddNumbers_Exported(int a, int b)
        {
            return a + b;
        }

        [NativeExport("ProcessData")]
        public static string[] ProcessData_Exported(double[] data, string prefix)
        {
            return data.Select(value => $"{prefix}{value}").ToArray();
        }

        [NativeExport("CalculateDistance")]
        public static float CalculateDistance_Exported(Vector3 a, Vector3 b)
        {
            return Vector3.Distance(a, b);
        }

        [NativeExport("ExecuteWithCallback")]
        public static void ExecuteWithCallback_Exported(int value, string inputStr, MyCallback callback)
        {
            string result = callback(value, inputStr);
            Console.WriteLine($"Callback result: {result}");
        }
    }

    public delegate string MyCallback(int a, string b);
}
```

### Step 2: Build Your Project

```bash
dotnet build YourPlugin.csproj
```

The generator will:
1. Analyze your code for `[NativeExport]` attributes
2. Generate a C# class with the manifest JSON
3. When the assembly is loaded, the ModuleInitializer will write the `.pplugin` file

### Step 3: Load the Plugin

When your plugin assembly is loaded by the Plugify runtime, the `.pplugin` manifest file will be automatically created in the same directory as the assembly.

## Type Mapping

The generator automatically maps C# types to Plugify types:

| C# Type                 | Plugify Type | Supports Ref? |
|-------------------------|--------------|---------------|
| void                    | void         | ❌            |
| bool, Bool8             | bool         | ✅            |
| Char8                   | char8        | ✅            |
| Char16                  | char16       | ✅            |
| sbyte                   | int8         | ✅            |
| short                   | int16        | ✅            |
| int                     | int32        | ✅            |
| long                    | int64        | ✅            |
| byte                    | uint8        | ✅            |
| ushort                  | uint16       | ✅            |
| uint                    | uint32       | ✅            |
| ulong                   | uint64       | ✅            |
| nint, IntPtr            | ptr64/ptr32  | ✅            |
| float                   | float        | ✅            |
| double                  | double       | ✅            |
| string                  | string       | ✅            |
| object                  | any          | ✅            |
| Vector2                 | vec2         | ✅            |
| Vector3                 | vec3         | ✅            |
| Vector4                 | vec4         | ✅            |
| Matrix4x4               | mat4x4       | ✅            |
| T[]                     | type[]       | ✅            |
| Delegate                | function     | ❌            |

## Generated Manifest Example

For the example above, the generator creates:

```json
{
  "name": "MyPlugin",
  "version": "1.0.0.0",
  "methods": [
    {
      "name": "AddNumbers",
      "funcName": "MyPlugin.ExportedFunctions.AddNumbers_Exported",
      "paramTypes": [
        {
          "type": "int32",
          "name": "a"
        },
        {
          "type": "int32",
          "name": "b"
        }
      ],
      "retType": {
        "type": "int32"
      }
    },
    {
      "name": "ProcessData",
      "funcName": "MyPlugin.ExportedFunctions.ProcessData_Exported",
      "paramTypes": [
        {
          "type": "double[]",
          "name": "data"
        },
        {
          "type": "string",
          "name": "prefix"
        }
      ],
      "retType": {
        "type": "string[]"
      }
    },
    {
      "name": "ExecuteWithCallback",
      "funcName": "MyPlugin.ExportedFunctions.ExecuteWithCallback_Exported",
      "paramTypes": [
        {
          "type": "int32",
          "name": "value"
        },
        {
          "type": "string",
          "name": "inputStr"
        },
        {
          "type": "function",
          "name": "callback",
          "prototype": {
            "name": "MyCallback",
            "funcName": "MyCallback_Exported",
            "paramTypes": [
              {
                "type": "int32",
                "name": "a"
              },
              {
                "type": "string",
                "name": "b"
              }
            ],
            "retType": {
              "type": "string"
            }
          }
        }
      ],
      "retType": {
        "type": "void"
      }
    }
  ]
}
```

## Troubleshooting

### Generator Not Running

If the generator doesn't seem to be running:

1. **Check the project reference**: Ensure Plugify is referenced correctly in your plugin project
2. **Clean and rebuild**: `dotnet clean && dotnet build`
3. **Check for errors**: Look for generator errors in the build output
4. **Enable generator output**: Build with `-p:EmitCompilerGeneratedFiles=true -p:CompilerGeneratedFilesOutputPath=Generated`

### Manifest File Not Created

The manifest file is created by a ModuleInitializer when the assembly is first loaded. If you don't see the `.pplugin` file:

1. Make sure the plugin assembly has been loaded at least once
2. Check file system permissions
3. Verify the assembly location is writable

### Alternative: Manual Manifest Creation

If the generator doesn't work in your environment, you can manually create the manifest file:

1. Access the generated manifest JSON:
```csharp
using Plugify.Generated;

string manifestJson = PlugifyManifest.ManifestJson;
string fileName = PlugifyManifest.ManifestFileName;

// Write to file
File.WriteAllText(fileName, manifestJson);
```

2. Or create a post-build event in your `.csproj`:
```xml
<Target Name="GeneratePlugifyManifest" AfterTargets="Build">
    <Exec Command="dotnet run --project ../ManifestTool/ManifestTool.csproj -- $(TargetPath)" />
</Target>
```

## Best Practices

1. **Use descriptive export names**: Choose names that clearly describe the function's purpose
2. **Keep method signatures simple**: Avoid complex nested types
3. **Document your exports**: Add XML documentation comments to exported methods
4. **Test thoroughly**: Verify the generated manifest matches your expectations
5. **Version your plugins**: Use semantic versioning in your assembly version

## Example Plugin

See `ExamplePlugin/Program.cs` for a complete working example with:
- Basic type exports
- Array parameters
- Ref parameters
- Vector/Matrix types
- Callback delegates
- Complex multi-parameter functions

## Building the Generator

To build the generator project:

```bash
cd Plugify.Generators
dotnet build
```

The generator will be automatically used by any project that references the Plugify library.

## Contributing

When adding new Plugify types or features:

1. Update the type mapping in `MapTypeToPlugify()`
2. Add tests in the ExamplePlugin
3. Update this README with examples
4. Rebuild and verify the generated manifest

## License

This generator is part of the Plugify .NET module and is licensed under the MIT License.
