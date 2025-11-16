using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;

namespace Plugify.Generators;

[Generator(LanguageNames.CSharp)]
public class ImportsGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Remove code bellow
        
        /*
        const string nativeImportAttributeName = "Plugify.NativeImportAttribute";

        bool Filter(SyntaxNode node, CancellationToken cancellationToken)
        {
            return true;
        };
        
        bool NeedMarshal(ITypeSymbol typeSymbol)
        {
            if (typeSymbol.GetMembers("Invoke").FirstOrDefault(m => m.Kind == SymbolKind.Method) is not IMethodSymbol invoke)
                return false;

            return IsRequiredMarshaling(invoke.ReturnType) || invoke.Parameters.Any(p => IsRequiredMarshaling(p.Type));

            bool IsRequiredMarshaling(ITypeSymbol type)
            {
                switch (type.SpecialType)
                {
                    case SpecialType.System_String:
                    case SpecialType.System_Array:
                        return true;

                    default:
                        if (type.TypeKind == TypeKind.Delegate && NeedMarshal(type))
                        {
                            return true;
                        }
                        break;
                }

                return false;
            }
        }

        FunctionNameSignatureTuple Transform(GeneratorAttributeSyntaxContext attributeSyntaxContext, CancellationToken cancellationToken)
        {
            FunctionNameSignatureTuple fnSig = new FunctionNameSignatureTuple();

            IMethodSymbol methodSymbol = ((IMethodSymbol)attributeSyntaxContext.TargetSymbol);

            fnSig.Name = methodSymbol.ContainingNamespace.ToString();
            
            fnSig.FunctionName = methodSymbol.Name;

            fnSig.FunctionReturnType = methodSymbol.ReturnType.ToDisplayString();

            StringBuilder parameterBuilder = new StringBuilder();
            StringBuilder argumentBuilder = new StringBuilder();
            StringBuilder signatureBuilder = new StringBuilder();

            List<IParameterSymbol> marshalingToDo = [];

            for (int i = 0; i < methodSymbol.Parameters.Length; i++)
            {
                IParameterSymbol parameter = methodSymbol.Parameters[i];

                switch (parameter.Type.SpecialType)
                {
                    case SpecialType.System_String:
                    case SpecialType.System_Array:
                        marshalingToDo.Add(parameter);
                        break;
                    default:
                        if (parameter.Type.IsValueType && parameter.RefKind == RefKind.Ref || parameter.Type.TypeKind == TypeKind.Delegate)
                        {
                            marshalingToDo.Add(parameter);
                        }
                        break;
                }

                if (parameter.RefKind == RefKind.Ref)
                {
                    parameterBuilder.Append("ref ");
                }

                parameterBuilder.Append($"{parameter.Type} ");

                parameterBuilder.Append(parameter.Name);

                if (parameter.HasExplicitDefaultValue)
                {
                    parameterBuilder.Append(" = ");
                    if (parameter.ExplicitDefaultValue is null)
                    {
                        parameterBuilder.Append("null");
                    }
                    else
                    {
                        if (parameter.ExplicitDefaultValue is bool booleanValue)
                        {
                            parameterBuilder.Append(booleanValue ? "true" : "false");
                        }
                        else
                        {
                            parameterBuilder.Append(parameter.ExplicitDefaultValue!);
                        }
                    }
                }

                if (i + 1 < methodSymbol.Parameters.Length)
                    parameterBuilder.Append(", ");

                switch (parameter.Type.SpecialType)
                {
                    case SpecialType.System_String:
                    case SpecialType.System_Array:
                        argumentBuilder.Append($"__{parameter.Name}");
                        break;
                    default:
                        if (parameter.Type.TypeKind == TypeKind.Delegate)
                        {
                            if (NeedMarshal(parameter.Type))
                                argumentBuilder.Append($"__{parameter.Name}");
                        }
                        else if (parameter.RefKind == RefKind.Ref)
                        {
                            argumentBuilder.Append($"__{parameter.Name}");
                        }
                        else if (parameter.Type.ContainingNamespace != null && parameter.Type.ContainingNamespace.ToString() == "System.Numerics")
                        {
                            argumentBuilder.Append($"&{parameter.Name}");
                        }
                        else
                        {
                            argumentBuilder.Append(parameter.Name);
                        }
                        break;
                }

                if (i + 1 < methodSymbol.Parameters.Length)
                {
                    argumentBuilder.Append(", ");
                }

                switch (parameter.Type.SpecialType)
                {
                    case SpecialType.System_String:
                    case SpecialType.System_Array:
                        signatureBuilder.Append("nint, ");
                        break;
                    case SpecialType.System_Boolean:
                        signatureBuilder.Append("byte, ");
                        break;
                    case SpecialType.System_Char:
                        signatureBuilder.Append("ushort, ");
                        break;
                    default:
                        if (parameter.Type.TypeKind == TypeKind.Delegate)
                        {
                            signatureBuilder.Append("nint, ");
                        }
                        else
                        {
                            signatureBuilder.Append($"{parameter.Type.ToDisplayString()}, ");
                        }
                        break;
                }
            }

            switch (methodSymbol.ReturnType.SpecialType)
            {
                case SpecialType.System_String:
                case SpecialType.System_Array:
                    signatureBuilder.Append("nint");
                    break;
                case SpecialType.System_Boolean:
                    signatureBuilder.Append("byte");
                    break;
                case SpecialType.System_Char:
                    signatureBuilder.Append("ushort");
                    break;
                default:
                    if (methodSymbol.ReturnType.TypeKind == TypeKind.Delegate)
                    {
                        signatureBuilder.Append("nint");
                    }
                    else
                    {
                        signatureBuilder.Append($"{methodSymbol.ReturnType.ToDisplayString()}");
                    }
                    break;
            }

            fnSig.FunctionParameters = parameterBuilder.ToString();

            fnSig.FunctionCallArguments = argumentBuilder.ToString();

            fnSig.FunctionPointerSignature = signatureBuilder.ToString();

            if (marshalingToDo.Count > 0)
            {
                StringBuilder marshalParameterNewBuilder = new StringBuilder();
                StringBuilder marshalParameterRefBuilder = new StringBuilder();
                StringBuilder marshalParameterFreeBuilder = new StringBuilder();

                foreach (var parameter in marshalingToDo)
                {
                    switch (parameter.Type.SpecialType)
                    {
                        case SpecialType.System_String:
                        {
                            marshalParameterNewBuilder.AppendLine($"nint __{parameter.Name} = NativeMethods.CreateString({parameter.Name});");
                            marshalParameterRefBuilder.AppendLine($"{parameter.Name} = NativeMethods.GetStringData(__{parameter.Name});");
                            marshalParameterFreeBuilder.AppendLine($"NativeMethods.DeleteString(__{parameter.Name});");
                            break;
                        }
						
                        case SpecialType.System_Array:
                        {
                            string suffix = "###"; 
                            marshalParameterNewBuilder.AppendLine($"nint __{parameter.Name} = NativeMethods.CreateVector{suffix}({parameter.Name}, {parameter.Name}.Length);");
                            marshalParameterRefBuilder.AppendLine($"NativeMethods.GetVectorData{suffix}(__{parameter.Name}, {parameter.Name});");
                            marshalParameterFreeBuilder.AppendLine($"NativeMethods.DeleteVector{suffix}(__{parameter.Name});");
                            break;
                        }

                        default:
                        {
                            if (parameter.Type.TypeKind == TypeKind.Delegate)
                            {
                                if (NeedMarshal(parameter.Type))
                                    marshalParameterNewBuilder.AppendLine($"var __{parameter.Name} = Marshalling.GetDelegateForMarshalling({parameter.Name});");
                            }
                            else
                            {
                                marshalParameterNewBuilder.AppendLine($"fixed({parameter.Type.ToDisplayString()}* __{parameter.Name} = &{parameter.Name}) {{");
                                fnSig.FixedScopes++;
                            }
                            //marshalParameterRefBuilder.AppendLine("");
                            //marshalParameterFreeBuilder.Append("");
                            break;
                        }
                    }
                }

                fnSig.MarshalNew = marshalParameterNewBuilder.ToString();
                fnSig.MarshalRef = marshalParameterRefBuilder.ToString();
                fnSig.MarshalFree = marshalParameterFreeBuilder.ToString();
            }

            return fnSig;
        }

        IncrementalValuesProvider<FunctionNameSignatureTuple> values = context.SyntaxProvider.ForAttributeWithMetadataName(nativeImportAttributeName, Filter, Transform);

        context.RegisterSourceOutput(values.Collect(), (spc, functions) =>
        {
            foreach (var group in functions.GroupBy(x => x.Name))
            {
                StringBuilder source = new StringBuilder();

                source.AppendLine("/// Auto-generated ///");
                source.AppendLine("#nullable enable");
                source.AppendLine();

                source.AppendLine($"namespace {group.Key};");
                source.AppendLine($"internal unsafe partial class {group.Key}");
                source.AppendLine("{");

                foreach (FunctionNameSignatureTuple function in group)
                {
                    string functionType = function.FunctionParameters.Length != 0 
                        ? $"{function.FunctionParameters.Remove(function.FunctionParameters.IndexOf(' '))}, {function.FunctionReturnType}" : function.FunctionReturnType;
                    source.AppendLine($"internal static delegate* <{functionType}> {function.FunctionName.Substring(3)} = &{function.FunctionName};");
                    
                    string functionPointerType = $"delegate* unmanaged[Cdecl, SuppressGCTransition]<{function.FunctionPointerSignature}>";
                    source.AppendLine($"internal static {functionPointerType} {function.FunctionName.Substring(1)};");
             
                    source.AppendLine($"private static partial {function.FunctionReturnType} {function.FunctionName}({function.FunctionParameters})");
                    source.AppendLine("{");
                    
                    bool tryCatch = function.MarshalNew is not null && function.MarshalRef is not null;
                    
                    if (function.FunctionReturnType == "void")
                    {
                        if (!string.IsNullOrEmpty(function.MarshalNew))
                        {
                            source.AppendLine(function.MarshalNew);
                        }
                        
                        if (tryCatch)
                        {
                            source.AppendLine("try {");
                        }

                        source.AppendLine($"{function.FunctionName.Substring(1)}({function.FunctionCallArguments});");

                        if (!string.IsNullOrEmpty(function.MarshalRef))
                        {
                            source.AppendLine(function.MarshalRef);
                        }

                        if (tryCatch)
                        {
                            source.AppendLine("}");
                            source.AppendLine("finally {");
                        }

                        if (!string.IsNullOrEmpty(function.MarshalFree))
                        {
                            source.AppendLine(function.MarshalFree);
                        }
                        
                        if (tryCatch)
                        {
                            source.AppendLine("}");
                        }
                        
                        for (int i = 0; i < function.FixedScopes; i++)
                        {
                            source.AppendLine("}");
                        }
                    }
                    else
                    {
                        // TODO: 
                    }
                    
                    source.AppendLine("}");
                }

                source.AppendLine("}");

                spc.AddSource($"{group.Key}.g.cs", source.ToString());
            }
        });*/
    }

    private record struct FunctionNameSignatureTuple(
        string Name,
        string FunctionName, 
        string FunctionReturnType,
        string FunctionParameters, 
        string FunctionCallArguments,
        string FunctionPointerSignature, 
        string? MarshalNew,
        string? MarshalRef, 
        string? MarshalFree,
        int FixedScopes
        );
}