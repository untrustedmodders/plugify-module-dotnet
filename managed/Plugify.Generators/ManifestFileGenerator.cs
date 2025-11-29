using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Plugify.Generators;

[Generator(LanguageNames.CSharp)]
public class ManifestFileGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        const string nativeExportAttributeName = "Plugify.NativeExportAttribute";

        // Filter for methods with NativeExport attribute
        var exportedMethods = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                nativeExportAttributeName,
                predicate: (node, _) => node is MethodDeclarationSyntax,
                transform: TransformMethod)
            .Where(method => method is not null);

        // Combine with compilation to get assembly info
        var compilationAndMethods = context.CompilationProvider.Combine(exportedMethods.Collect());

        // Generate a C# class that contains the manifest and write logic
        context.RegisterSourceOutput(compilationAndMethods, (spc, source) =>
        {
            var (compilation, methods) = source;
            GenerateManifestClass(spc, compilation, methods);
        });
    }

    private static ExportedMethod? TransformMethod(GeneratorAttributeSyntaxContext context, CancellationToken cancellationToken)
    {
        if (context.TargetSymbol is not IMethodSymbol methodSymbol)
            return null;

        if (!methodSymbol.IsStatic)
        {
            return null; // Only static methods can be exported
        }

        // Get the export name from the attribute
        var attribute = context.Attributes.FirstOrDefault();
        if (attribute?.ConstructorArguments.Length != 1)
            return null;

        var exportName = attribute.ConstructorArguments[0].Value?.ToString();
        if (string.IsNullOrEmpty(exportName))
            return null;

        var method = new ExportedMethod
        {
            ExportName = exportName!,
            MethodName = GetFullMethodName(methodSymbol),
            ReturnType = MapTypeToPlugify(methodSymbol.ReturnType),
            Parameters = methodSymbol.Parameters
                .Select(p => new MethodParameter
                {
                    Name = p.Name,
                    Type = MapTypeToPlugify(p.Type),
                    IsRef = p.RefKind == RefKind.Ref
                })
                .ToList()
        };

        return method;
    }

    private static string GetFullMethodName(IMethodSymbol methodSymbol)
    {
        var containingType = methodSymbol.ContainingType;
        var namespaceName = containingType.ContainingNamespace?.ToDisplayString();
        var typeName = containingType.Name;

        if (string.IsNullOrEmpty(namespaceName) || namespaceName == "<global namespace>")
            return $"{typeName}.{methodSymbol.Name}";

        return $"{namespaceName}.{typeName}.{methodSymbol.Name}";
    }

    private static PlugifyType MapTypeToPlugify(ITypeSymbol typeSymbol)
    {
        // Handle arrays
        if (typeSymbol is IArrayTypeSymbol arrayType)
        {
            var elementType = MapTypeToPlugify(arrayType.ElementType);
            return new PlugifyType
            {
                TypeName = elementType.TypeName + "[]",
                IsArray = true,
                ElementType = elementType
            };
        }

        // Handle delegates (function pointers)
        if (typeSymbol.TypeKind == TypeKind.Delegate)
        {
            var invokeMethod = typeSymbol.GetMembers("Invoke")
                .OfType<IMethodSymbol>()
                .FirstOrDefault();

            if (invokeMethod != null)
            {
                return new PlugifyType
                {
                    TypeName = "function",
                    IsDelegate = true,
                    DelegateName = typeSymbol.Name,
                    DelegateReturnType = MapTypeToPlugify(invokeMethod.ReturnType),
                    DelegateParameters = invokeMethod.Parameters
                        .Select(p => new MethodParameter
                        {
                            Name = p.Name,
                            Type = MapTypeToPlugify(p.Type),
                            IsRef = p.RefKind == RefKind.Ref
                        })
                        .ToList()
                };
            }
        }

        // Handle enums
        if (typeSymbol.TypeKind == TypeKind.Enum)
        {
            var namedType = typeSymbol as INamedTypeSymbol;
            var underlyingType = namedType?.EnumUnderlyingType;

            if (underlyingType != null)
            {
                var underlyingPlugifyType = MapTypeToPlugify(underlyingType);

                // Get enum values
                var enumValues = typeSymbol.GetMembers()
                    .OfType<IFieldSymbol>()
                    .Where(f => f.IsConst && f.HasConstantValue)
                    .Select(f => new EnumValue
                    {
                        Name = f.Name,
                        Value = Convert.ToInt64(f.ConstantValue)
                    })
                    .OrderBy(v => v.Value)
                    .ToList();

                return new PlugifyType
                {
                    TypeName = underlyingPlugifyType.TypeName,
                    IsEnum = true,
                    EnumName = typeSymbol.Name,
                    EnumValues = enumValues
                };
            }
        }

        // Map basic types
        var typeName = typeSymbol.ToDisplayString();
        var plugifyTypeName = typeName switch
        {
            "void" => "void",
            "bool" or "Plugify.Bool8" => "bool",
            "Plugify.Char8" => "char8",
            "Plugify.Char16" => "char16",
            "sbyte" => "int8",
            "short" => "int16",
            "int" => "int32",
            "long" => "int64",
            "byte" => "uint8",
            "ushort" => "uint16",
            "uint" => "uint32",
            "ulong" => "uint64",
            "nint" when IntPtr.Size == 8 => "ptr64",
            "nint" when IntPtr.Size == 4 => "ptr32",
            "System.IntPtr" when IntPtr.Size == 8 => "ptr64",
            "System.IntPtr" when IntPtr.Size == 4 => "ptr32",
            "float" => "float",
            "double" => "double",
            "string" => "string",
            "object" => "any",
            "System.Numerics.Vector2" => "vec2",
            "System.Numerics.Vector3" => "vec3",
            "System.Numerics.Vector4" => "vec4",
            "System.Numerics.Matrix4x4" => "mat4x4",
            _ => typeName
        };

        return new PlugifyType { TypeName = plugifyTypeName };
    }

    private static void GenerateManifestClass(SourceProductionContext context, Compilation compilation, ImmutableArray<ExportedMethod?> methods)
    {
        var validMethods = methods.Where(m => m != null).Cast<ExportedMethod>().ToList();

        // Always generate a diagnostic class to verify the generator is running
        var assemblyName = compilation.AssemblyName ?? "UnknownPlugin";

        // Generate a diagnostic class even if no methods are found
        if (validMethods.Count == 0)
        {
            var diagnosticSource = $$"""
                                     // <auto-generated/>
                                     #nullable enable

                                     namespace Plugify.Generated
                                     {
                                         /// <summary>
                                         /// Plugify manifest generator diagnostic - No methods with [NativeExport] attribute found
                                         /// </summary>
                                         internal static class PlugifyManifestDiagnostic
                                         {
                                             public const string Message = "No methods with [NativeExport] attribute found in {{assemblyName}}";
                                         }
                                     }

                                     """;
            context.AddSource("PlugifyManifestDiagnostic.g.cs", diagnosticSource);
            return;
        }

        var versionObj = compilation.Assembly.Identity.Version; // System.Version
        var version = $"{versionObj.Major}.{versionObj.Minor}.{versionObj.Build}";
        
        var descriptionAttr = compilation.Assembly.GetAttributes()
            .FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == "System.Reflection.AssemblyDescriptionAttribute");
        var authorAttr = compilation.Assembly.GetAttributes()
            .FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == "System.Reflection.AssemblyCompanyAttribute");
        var copyrightAttr = compilation.Assembly.GetAttributes()
            .FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == "System.Reflection.AssemblyCopyrightAttribute");
        var productAttr = compilation.Assembly.GetAttributes()
            .FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == "System.Reflection.AssemblyProductAttribute");

        var description = descriptionAttr?.ConstructorArguments.FirstOrDefault().Value?.ToString() ?? "";
        var author = authorAttr?.ConstructorArguments.FirstOrDefault().Value?.ToString() ?? "";
        var copyright = copyrightAttr?.ConstructorArguments.FirstOrDefault().Value?.ToString() ?? "";
        var product = productAttr?.ConstructorArguments.FirstOrDefault().Value?.ToString() ?? "";
        
        // Build the manifest
        var manifest = new PluginManifest
        {
            Name = assemblyName,
            Version = version,
            Description = description,
            Author = author,
            Website = product,
            License = copyright,
            Entry = $"{assemblyName}.dll",
            Language = "dotnet",
            Methods = validMethods.Select(m => new ManifestMethod
            {
                Name = m.ExportName,
                FuncName = m.MethodName,
                ParamTypes = m.Parameters.Select(ConvertParameter).ToList(),
                RetType = ConvertReturnType(m.ReturnType)
            }).ToList()
        };

        // Generate JSON
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        var json = JsonSerializer.Serialize(manifest, options);
        var escapedJson = json.Replace("\"", "\"\""); // Escape for C# verbatim string

        // Generate C# class with manifest (WriteManifest is called by MSBuild post-build task)
        var source = $$"""
                       // <auto-generated/>
                       #nullable enable

                       using System;
                       using System.IO;

                       namespace Plugify.Generated
                       {
                           /// <summary>
                           /// Auto-generated Plugify manifest for {{assemblyName}}
                           /// </summary>
                           internal static class PlugifyManifest
                           {
                               /// <summary>
                               /// The manifest JSON content
                               /// </summary>
                               public const string ManifestJson = @"{{escapedJson}}";

                               /// <summary>
                               /// The manifest file name
                               /// </summary>
                               public const string ManifestFileName = "{{assemblyName}}.pplugin";

                               /// <summary>
                               /// Writes the manifest file to the assembly directory.
                               /// This is invoked by the MSBuild post-build task.
                               /// </summary>
                               internal static void WriteManifest(string assemblyPath = null)
                               {
                                   try
                                   {
                                       string directory;

                                       if (!string.IsNullOrEmpty(assemblyPath))
                                       {
                                           // Use provided path (from MSBuild task)
                                           directory = Path.GetDirectoryName(assemblyPath);
                                       }
                                       else
                                       {
                                           // Fallback to assembly location
                                           var assemblyLocation = typeof(PlugifyManifest).Assembly.Location;
                                           if (string.IsNullOrEmpty(assemblyLocation))
                                               return;

                                           directory = Path.GetDirectoryName(assemblyLocation);
                                       }

                                       if (string.IsNullOrEmpty(directory))
                                           return;

                                       var manifestPath = Path.Combine(directory, ManifestFileName);
                                       File.WriteAllText(manifestPath, ManifestJson);
                                   }
                                   catch
                                   {
                                       // Silently fail - we don't want to crash the build
                                   }
                               }
                           }
                       }

                       """;

        context.AddSource($"PlugifyManifest.g.cs", source);
    }

    private static object ConvertParameter(MethodParameter param)
    {
        if (param.Type.IsDelegate)
        {
            return new
            {
                type = "function",
                name = param.Name,
                @ref = param.IsRef ? "t" : null,
                prototype = new
                {
                    name = param.Type.DelegateName,
                    funcName = "_",
                    paramTypes = param.Type.DelegateParameters?.Select(p => new
                    {
                        type = p.Type.TypeName,
                        name = p.Name,
                        @ref = p.IsRef
                    }).ToList(),
                    retType = new
                    {
                        type = param.Type.DelegateReturnType?.TypeName ?? "void"
                    }
                }
            };
        }

        // Handle enum or enum array
        if (param.Type.IsEnum || (param.Type.IsArray && param.Type.ElementType?.IsEnum == true))
        {
            var enumType = param.Type.IsEnum ? param.Type : param.Type.ElementType;

            return new
            {
                type = param.Type.TypeName,
                name = param.Name,
                @ref = param.IsRef,
                @enum = new
                {
                    name = enumType?.EnumName,
                    values = enumType?.EnumValues?.Select(v => new
                    {
                        value = v.Value,
                        name = v.Name
                    }).ToList()
                }
            };
        }

        return new
        {
            type = param.Type.TypeName,
            name = param.Name,
            @ref = param.IsRef
        };
    }

    private static object ConvertReturnType(PlugifyType returnType)
    {
        // Handle enum or enum array
        if (returnType.IsEnum || (returnType.IsArray && returnType.ElementType?.IsEnum == true))
        {
            var enumType = returnType.IsEnum ? returnType : returnType.ElementType;

            return new
            {
                type = returnType.TypeName,
                @enum = new
                {
                    name = enumType?.EnumName,
                    values = enumType?.EnumValues?.Select(v => new
                    {
                        value = v.Value,
                        name = v.Name
                    }).ToList()
                }
            };
        }

        return new
        {
            type = returnType.TypeName
        };
    }

    private class ExportedMethod
    {
        public string ExportName { get; set; } = "";
        public string MethodName { get; set; } = "";
        public PlugifyType ReturnType { get; set; } = new();
        public List<MethodParameter> Parameters { get; set; } = [];
    }

    private class MethodParameter
    {
        public string Name { get; set; } = "";
        public PlugifyType Type { get; set; } = new();
        public bool IsRef { get; set; }
    }

    private class PlugifyType
    {
        public string TypeName { get; set; } = "";
        public bool IsArray { get; set; }
        public bool IsDelegate { get; set; }
        public bool IsEnum { get; set; }
        public PlugifyType? ElementType { get; set; }
        public string? DelegateName { get; set; }
        public PlugifyType? DelegateReturnType { get; set; }
        public List<MethodParameter>? DelegateParameters { get; set; }
        public string? EnumName { get; set; }
        public List<EnumValue>? EnumValues { get; set; }
    }

    private class EnumValue
    {
        public string Name { get; set; } = "";
        public long Value { get; set; }
    }

    private class PluginManifest
    {
        [JsonPropertyName("$schema")]
        public string Schema { get; set; } = "https://raw.githubusercontent.com/untrustedmodders/plugify/refs/heads/main/schemas/plugin.schema.json";
        public string Name { get; set; } = "";
        public string Version { get; set; } = "";
        public string Description { get; set; } = "";
        public string Author { get; set; } = "";
        public string Website { get; set; } = "";
        public string License { get; set; } = "";
        public string Entry { get; set; } = "";
        public string Language { get; set; } = "";
        public List<ManifestMethod> Methods { get; set; } = [];
    }

    private class ManifestMethod
    {
        public string Name { get; set; } = "";
        public string FuncName { get; set; } = "";
        public List<object> ParamTypes { get; set; } = [];
        public object RetType { get; set; } = new { type = "void" };
    }
}