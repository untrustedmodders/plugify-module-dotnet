#!/usr/bin/python3
import sys
import argparse
import os
import json
from enum import IntEnum

TYPES_MAP = {
    'void': 'void',
    'bool': 'Bool8',
    'char8': 'Char8',
    'char16': 'Char16',
    'int8': 'sbyte',
    'int16': 'short',
    'int32': 'int',
    'int64': 'long',
    'uint8': 'byte',
    'uint16': 'ushort',
    'uint32': 'uint',
    'uint64': 'ulong',
    'ptr64': 'nint',
    'float': 'float',
    'double': 'double',
    'function': 'delegate',
    'string': 'string',
    'any': 'object?',
    'bool[]': 'Bool8[]',
    'char8[]': 'Char8[]',
    'char16[]': 'Char16[]',
    'int8[]': 'sbyte[]',
    'int16[]': 'short[]',
    'int32[]': 'int[]',
    'int64[]': 'long[]',
    'uint8[]': 'byte[]',
    'uint16[]': 'ushort[]',
    'uint32[]': 'uint[]',
    'uint64[]': 'ulong[]',
    'ptr64[]': 'nint[]',
    'float[]': 'float[]',
    'double[]': 'double[]',
    'string[]': 'string[]',
    'any[]': 'object?[]',
    'vec2[]': 'Vector2[]',
    'vec3[]': 'Vector3[]',
    'vec4[]': 'Vector4[]',
    'mat4x4[]': 'Matrix4x4[]',
    'vec2': 'Vector2',
    'vec3': 'Vector3',
    'vec4': 'Vector4',
    'mat4x4': 'Matrix4x4'
}

CTYPES_MAP = {
    'void': 'void',
    'bool': 'Bool8',
    'char8': 'Char8',
    'char16': 'Char16',
    'int8': 'sbyte',
    'int16': 'short',
    'int32': 'int',
    'int64': 'long',
    'uint8': 'byte',
    'uint16': 'ushort',
    'uint32': 'uint',
    'uint64': 'ulong',
    'ptr64': 'nint',
    'float': 'float',
    'double': 'double',
    'function': 'nint',
    'string': 'String192*',
    'any': 'Variant256*',
    'bool[]': 'Vector192*',
    'char8[]': 'Vector192*',
    'char16[]': 'Vector192*',
    'int8[]': 'Vector192*',
    'int16[]': 'Vector192*',
    'int32[]': 'Vector192*',
    'int64[]': 'Vector192*',
    'uint8[]': 'Vector192*',
    'uint16[]': 'Vector192*',
    'uint32[]': 'Vector192*',
    'uint64[]': 'Vector192*',
    'ptr64[]': 'Vector192*',
    'float[]': 'Vector192*',
    'double[]': 'Vector192*',
    'string[]': 'Vector192*',
    'any[]': 'Vector192*',
    'vec2[]': 'Vector192*',
    'vec3[]': 'Vector192*',
    'vec4[]': 'Vector192*',
    'mat4x4[]': 'Vector192*',
    'vec2': 'Vector2*',
    'vec3': 'Vector3*',
    'vec4': 'Vector4*',
    'mat4x4': 'Matrix4x4*'
}

VAL_TYPESCAST_MAP = {
    'void': '',
    'bool': '',
    'char8': '',
    'char16': '',
    'int8': '',
    'int16': '',
    'int32': '',
    'int64': '',
    'uint8': '',
    'uint16': '',
    'uint32': '',
    'uint64': '',
    'ptr64': '',
    'float': '',
    'double': '',
    'function': 'Marshalling.GetFunctionPointerForDelegate',
    'string': 'NativeMethods.ConstructString',
    'any': 'NativeMethods.ConstructVariant',
    'bool[]': 'NativeMethods.ConstructVectorBool',
    'char8[]': 'NativeMethods.ConstructVectorChar8',
    'char16[]': 'NativeMethods.ConstructVectorChar16',
    'int8[]': 'NativeMethods.ConstructVectorInt8',
    'int16[]': 'NativeMethods.ConstructVectorInt16',
    'int32[]': 'NativeMethods.ConstructVectorInt32',
    'int64[]': 'NativeMethods.ConstructVectorInt64',
    'uint8[]': 'NativeMethods.ConstructVectorUInt8',
    'uint16[]': 'NativeMethods.ConstructVectorUInt16',
    'uint32[]': 'NativeMethods.ConstructVectorUInt32',
    'uint64[]': 'NativeMethods.ConstructVectorUInt64',
    'ptr64[]': 'NativeMethods.ConstructVectorIntPtr',
    'float[]': 'NativeMethods.ConstructVectorFloat',
    'double[]': 'NativeMethods.ConstructVectorDouble',
    'string[]': 'NativeMethods.ConstructVectorString',
    'any[]': 'NativeMethods.ConstructVectorVariant',
    'vec2[]': 'NativeMethods.ConstructVectorVector2',
    'vec3[]': 'NativeMethods.ConstructVectorVector3',
    'vec4[]': 'NativeMethods.ConstructVectorVector4',
    'mat4x4[]': 'NativeMethods.ConstructVectorMatrix4x4',
    'vec2': '',
    'vec3': '',
    'vec4': '',
    'mat4x4': ''
}

RET_TYPESCAST_MAP = {
    'void': '',
    'bool': '',
    'char8': '',
    'char16': '',
    'int8': '',
    'int16': '',
    'int32': '',
    'int64': '',
    'uint8': '',
    'uint16': '',
    'uint32': '',
    'uint64': '',
    'ptr64': '',
    'float': '',
    'double': '',
    'function': '',
    'string': 'String192',
    'any': 'Variant256',
    'bool[]': 'Vector192',
    'char8[]': 'Vector192',
    'char16[]': 'Vector192',
    'int8[]': 'Vector192',
    'int16[]': 'Vector192',
    'int32[]': 'Vector192',
    'int64[]': 'Vector192',
    'uint8[]': 'Vector192',
    'uint16[]': 'Vector192',
    'uint32[]': 'Vector192',
    'uint64[]': 'Vector192',
    'ptr64[]': 'Vector192',
    'float[]': 'Vector192',
    'double[]': 'Vector192',
    'string[]': 'Vector192',
    'any[]': 'Vector192',
    'vec2[]': 'Vector192',
    'vec3[]': 'Vector192',
    'vec4[]': 'Vector192',
    'mat4x4[]': 'Vector192',
    'vec2': 'Vector2',
    'vec3': 'Vector3',
    'vec4': 'Vector4',
    'mat4x4': 'Matrix4x4'
}

ASS_TYPESCAST_MAP = {
    'void': '',
    'bool': '',
    'char8': '',
    'char16': '',
    'int8': '',
    'int16': '',
    'int32': '',
    'int64': '',
    'uint8': '',
    'uint16': '',
    'uint32': '',
    'uint64': '',
    'ptr64': '',
    'float': '',
    'double': '',
    'function': '',
    'string': 'NativeMethods.GetStringData',
    'any': 'NativeMethods.GetVariantData',
    'bool[]': 'NativeMethods.GetVectorDataBool',
    'char8[]': 'NativeMethods.GetVectorDataChar8',
    'char16[]': 'NativeMethods.GetVectorDataChar16',
    'int8[]': 'NativeMethods.GetVectorDataInt8',
    'int16[]': 'NativeMethods.GetVectorDataInt16',
    'int32[]': 'NativeMethods.GetVectorDataInt32',
    'int64[]': 'NativeMethods.GetVectorDataInt64',
    'uint8[]': 'NativeMethods.GetVectorDataUInt8',
    'uint16[]': 'NativeMethods.GetVectorDataUInt16',
    'uint32[]': 'NativeMethods.GetVectorDataUInt32',
    'uint64[]': 'NativeMethods.GetVectorDataUInt64',
    'ptr64[]': 'NativeMethods.GetVectorDataIntPtr',
    'float[]': 'NativeMethods.GetVectorDataFloat',
    'double[]': 'NativeMethods.GetVectorDataDouble',
    'string[]': 'NativeMethods.GetVectorDataString',
    'any[]': 'NativeMethods.GetVectorDataVariant',
    'vec2[]': 'NativeMethods.GetVectorDataVector2',
    'vec3[]': 'NativeMethods.GetVectorDataVector3',
    'vec4[]': 'NativeMethods.GetVectorDataVector4',
    'mat4x4[]': 'NativeMethods.GetVectorDataMatrix4x4',
    'vec2': '',
    'vec3': '',
    'vec4': '',
    'mat4x4': ''
}

SIZ_TYPESCAST_MAP = {
    'void': '',
    'bool': '',
    'char8': '',
    'char16': '',
    'int8': '',
    'int16': '',
    'int32': '',
    'int64': '',
    'uint8': '',
    'uint16': '',
    'uint32': '',
    'uint64': '',
    'ptr64': '',
    'float': '',
    'double': '',
    'function': '',
    'string': '',
    'any': '',
    'bool[]': 'NativeMethods.GetVectorSizeBool',
    'char8[]': 'NativeMethods.GetVectorSizeChar8',
    'char16[]': 'NativeMethods.GetVectorSizeChar16',
    'int8[]': 'NativeMethods.GetVectorSizeInt8',
    'int16[]': 'NativeMethods.GetVectorSizeInt16',
    'int32[]': 'NativeMethods.GetVectorSizeInt32',
    'int64[]': 'NativeMethods.GetVectorSizeInt64',
    'uint8[]': 'NativeMethods.GetVectorSizeUInt8',
    'uint16[]': 'NativeMethods.GetVectorSizeUInt16',
    'uint32[]': 'NativeMethods.GetVectorSizeUInt32',
    'uint64[]': 'NativeMethods.GetVectorSizeUInt64',
    'ptr64[]': 'NativeMethods.GetVectorSizeIntPtr',
    'float[]': 'NativeMethods.GetVectorSizeFloat',
    'double[]': 'NativeMethods.GetVectorSizeDouble',
    'string[]': 'NativeMethods.GetVectorSizeString',
    'any[]': 'NativeMethods.GetVectorSizeVariant',
    'vec2[]': 'NativeMethods.GetVectorSizeVector2',
    'vec3[]': 'NativeMethods.GetVectorSizeVector3',
    'vec4[]': 'NativeMethods.GetVectorSizeVector4',
    'mat4x4[]': 'NativeMethods.GetVectorSizeMatrix4x4',
    'vec2': '',
    'vec3': '',
    'vec4': '',
    'mat4x4': ''
}

DEL_TYPESCAST_MAP = {
    'void': '',
    'bool': '',
    'char8': '',
    'char16': '',
    'int8': '',
    'int16': '',
    'int32': '',
    'int64': '',
    'uint8': '',
    'uint16': '',
    'uint32': '',
    'uint64': '',
    'ptr64': '',
    'float': '',
    'double': '',
    'function': '',
    'string': 'NativeMethods.DestroyString',
    'any': 'NativeMethods.DestroyVariant',
    'bool[]': 'NativeMethods.DestroyVectorBool',
    'char8[]': 'NativeMethods.DestroyVectorChar8',
    'char16[]': 'NativeMethods.DestroyVectorChar16',
    'int8[]': 'NativeMethods.DestroyVectorInt8',
    'int16[]': 'NativeMethods.DestroyVectorInt16',
    'int32[]': 'NativeMethods.DestroyVectorInt32',
    'int64[]': 'NativeMethods.DestroyVectorInt64',
    'uint8[]': 'NativeMethods.DestroyVectorUInt8',
    'uint16[]': 'NativeMethods.DestroyVectorUInt16',
    'uint32[]': 'NativeMethods.DestroyVectorUInt32',
    'uint64[]': 'NativeMethods.DestroyVectorUInt64',
    'ptr64[]': 'NativeMethods.DestroyVectorIntPtr',
    'float[]': 'NativeMethods.DestroyVectorFloat',
    'double[]': 'NativeMethods.DestroyVectorDouble',
    'string[]': 'NativeMethods.DestroyVectorString',
    'any[]': 'NativeMethods.DestroyVectorVariant',
    'vec2[]': 'NativeMethods.DestroyVectorVector2',
    'vec3[]': 'NativeMethods.DestroyVectorVector3',
    'vec4[]': 'NativeMethods.DestroyVectorVector4',
    'mat4x4[]': 'NativeMethods.DestroyVectorMatrix4x4',
    'vec2': '',
    'vec3': '',
    'vec4': '',
    'mat4x4': ''
}

INVALID_NAMES = {
    'abstract',
    'as',
    'base',
    'bool',
    'break',
    'byte',
    'case',
    'catch',
    'char',
    'checked',
    'class',
    'const',
    'continue',
    'decimal',
    'default',
    'delegate',
    'do',
    'double',
    'else',
    'enum',
    'event',
    'explicit',
    'extern',
    'false',
    'finally',
    'fixed',
    'float',
    'for',
    'foreach',
    'goto',
    'if',
    'implicit',
    'in',
    'int',
    'interface',
    'internal',
    'is',
    'lock',
    'long',
    'namespace',
    'new',
    'null',
    'object',
    'operator',
    'out',
    'override',
    'params',
    'private',
    'protected',
    'internal',
    'readonly',
    'ref',
    'return',
    'sbyte',
    'sealed',
    'short',
    'sizeof',
    'stackalloc',
    'static',
    'string',
    'struct',
    'switch',
    'this',
    'throw',
    'true',
    'try',
    'typeof',
    'uint',
    'ulong',
    'unchecked',
    'unsafe',
    'ushort',
    'using',
    'virtual',
    'void',
    'volatile',
    'while'
    # 'add',
    # 'and',
    # 'alias',
    # 'ascending',
    # 'args',
    # 'async',
    # 'await',
    # 'by',
    # 'descending',
    # 'dynamic',
    # 'equals',
    # 'file',
    # 'from',
    # 'get',
    # 'global',
    # 'group',
    # 'init',
    # 'into',
    # 'join',
    # 'let',
    # 'managed',
    # 'nameof',
    # 'nint',
    # 'not',
    # 'notnull',
    # 'nuint',
    # 'on',
    # 'or',
    # 'orderby',
    # 'partial',
    # 'partial',
    # 'record',
    # 'remove',
    # 'required',
    # 'scoped',
    # 'select',
    # 'set',
    # 'unmanaged',
    # 'value',
    # 'var',
    # 'when',
    # 'where',
    # 'where',
    # 'with',
    # 'yield'
}


def is_obj_return(type_name: str) -> bool:
    """
    Checks if the type is a reference type or a collection type (array or object).
    """
    return '[]' in type_name or type_name in {'string', 'any'}


def is_pod_type(type_name: str) -> bool:
    """
    Checks if the type is a Plain Old Data (POD) type, such as vectors or matrices.
    """
    return type_name in {'vec2', 'vec3', 'vec4', 'mat4x4'}


def convert_type(type_name: str, is_ref: bool = False) -> str:
    """
    Converts a type name to its corresponding C# type.
    If the type is a reference, it appends 'ref' to the type.
    """
    base_type = TYPES_MAP.get(type_name, None)
    return f'ref {base_type}' if is_ref else base_type


def convert_dtype(type_name: str, is_ref: bool = False, is_ret: bool = False) -> str:
    """
    Converts a type name to its corresponding C# type, with additional handling for references
    and return types (for POD types, references are forced).
    """
    if not is_ret and is_pod_type(type_name):
        is_ref = True
    return convert_type(type_name, is_ref)


def convert_ctype(param: dict, is_ref: bool = False, is_ret: bool = False) -> str:
    """
    Converts a type name to its corresponding C type.
    If the type is a reference, it appends '*' (pointer) to the type.
    """
    type_name = param.get('type', '')
    if 'enum' in param and not '[]' in type_name:
        name = generate_name(param['enum'].get('name', 'UnnamedEnum'))
        return f'{name}*' if is_ref else name
    base_type = CTYPES_MAP.get(type_name, None)
    if is_ref:
        return f'{base_type}*' if '*' not in base_type else base_type
    elif is_ret and '*' in base_type:
        return base_type[:-1]  # Remove pointer if it's a return type
    return base_type


def adjust_type_name(param: dict, type_name: str, is_ref: bool) -> str:
    if 'delegate' in type_name and 'prototype' in param:
        return generate_name(param['prototype'].get('name', 'UnnamedDelegate'))
    elif 'enum' in param:
        name = generate_name(param['enum'].get('name', 'UnnamedEnum'))
        base_type = f'ref {name}' if is_ref else name
        if '[]' in type_name:
            return f'{base_type}[]'
        else:
            return base_type
    return type_name


def get_type_name(param: dict) -> str:
    is_ref = 'ref' in param and param['ref']
    type_name = convert_type(param['type'], is_ref)
    return adjust_type_name(param, type_name, is_ref)


def get_dtype_name(param: dict, is_ret: bool = False) -> str:
    is_ref = 'ref' in param and param['ref']
    type_name = convert_dtype(param['type'], is_ref, is_ret)
    return adjust_type_name(param, type_name, is_ref)


def is_need_marshal(method: dict) -> bool:
    """
    Determines if a method requires marshalling for parameters or return values.
    This is necessary if any of the parameters or return types are object types.
    """
    ret_type = method.get('retType', {}).get('type')
    if ret_type and is_obj_return(ret_type):
        return True

    # Check if any parameter type requires marshalling
    param_types = method.get('paramTypes', [])
    return any(is_obj_return(param.get('type', None)) for param in param_types)


def generate_name(name: str) -> str:
    """
    Generates a valid C# name by appending an underscore if the name is invalid.
    """
    return f"{name}_" if name in INVALID_NAMES else name


class ParamGen(IntEnum):
    Types = 1
    Names = 2
    TypesNames = 3
    TypesCastNames = 4
    CastNames = 5


def gen_params(method: dict, param_gen: ParamGen) -> str:
    """
    Generates the parameters string for the method based on the param_gen type.
    Handles different modes such as Types, Names, CastNames, and TypesCastNames.
    """

    # Helper function to generate the type of a parameter, considering references and function types
    def gen_param_type(param: dict) -> str:
        if param['type'] == 'function':
            return generate_name(param['prototype']['name'])
        return convert_type(param['type'], 'ref' in param and param['ref'])

    # Helper function to generate the appropriate cast name for parameters, handling function pointers and references
    def gen_param_cast_name(param: dict) -> str:
        if param['type'] == 'function':
            return gen_function_cast_name(param)
        if is_obj_return(param['type']):
            return f'&__{generate_name(param["name"])}'
        if 'vec' in param['type'] or 'mat' in param['type']:
            return gen_vector_matrix_cast(param)
        return gen_ref_cast(param)

    # Helper function to generate the cast name for function parameters
    def gen_function_cast_name(param: dict) -> str:
        if is_need_marshal(param['prototype']):
            return f'__{generate_name(param["name"])}'
        return f'Marshal.GetFunctionPointerForDelegate({generate_name(param["name"])})'

    # Helper function to generate the cast name for vector or matrix types
    def gen_vector_matrix_cast(param: dict) -> str:
        if 'ref' in param and param['ref']:
            return f'__{generate_name(param["name"])}'
        return f'&{generate_name(param["name"])}'

    # Helper function to handle ref type parameters
    def gen_ref_cast(param: dict) -> str:
        if 'ref' in param and param['ref']:
            return f'__{generate_name(param["name"])}'
        return generate_name(param['name'])

    # Helper function to generate the parameter type and cast name for TypesNames
    def gen_param_type_name(param: dict) -> str:
        return f'{get_type_name(param)} {generate_name(param["name"])}'

    # Helper function to generate the parameter type and cast name for TypesCastNames
    def gen_param_types_cast(param: dict) -> str:
        return f'{get_dtype_name(param)} {generate_name(param["name"])}'

    # Function to generate the appropriate string for a single parameter based on the param_gen mode
    def gen_param(param: dict) -> str:
        match param_gen:
            # Handle parameter type generation (e.g., ref, function, delegate)
            case ParamGen.Types:
                return gen_param_type(param)
            # Handle parameter name generation
            case ParamGen.Names:
                return generate_name(param['name'])
            # Handle cast names for marshaling or references
            case ParamGen.CastNames:
                return gen_param_cast_name(param)
            # Handle types with type names
            case ParamGen.TypesNames:
                return gen_param_type_name(param)
            # Handle types with cast names
            case ParamGen.TypesCastNames:
                return gen_param_types_cast(param)
            case _:
                return ''

    # Generate the full parameters list as a string
    parts = []

    # Add each parameter to the list using gen_param
    if method.get('paramTypes'):
        parts.extend([gen_param(param) for param in method['paramTypes']])

    # Join the parts with a comma and space to form the final string
    return ', '.join(parts)


def gen_ctypes(method: dict) -> str:
    # Initialize list to hold type representations
    parts = []

    # Process parameters if they exist
    if method.get('paramTypes'):
        parts.extend([convert_ctype(param, 'ref' in param and param['ref'], False) for param in
                      method['paramTypes']])

    # Add the return type
    ret_type = method.get('retType', {})
    if ret_type:
        parts.append(convert_ctype(ret_type, is_ret=True))

    # Join all parts together and return the final string
    return ', '.join(parts)


def gen_types(method: dict) -> str:
    """
    Generates a string representation of the types for the method, including its parameters and return type.
    """
    # Initialize list to hold type representations
    parts = []

    # Process parameters if they exist
    if method.get('paramTypes'):
        parts.extend([get_type_name(param) for param in method['paramTypes']])

    # Add the return type
    ret_type = method.get('retType', {})
    if ret_type:
        parts.append(get_type_name(ret_type))

    # Join all parts together and return the final string
    return ', '.join(parts)


def gen_paramscast(method: dict, tabs: str) -> str:
    """
    Generates parameter casting code for a method, considering various cases like
    function pointers, references, and vector constructions.
    """

    def gen_param(param: dict) -> str:
        """
        Generates the appropriate casting code for a parameter, considering its type,
        references, and other special cases like function pointers or vector constructions.
        """
        # Check if the parameter type requires a casting transformation (from VAL_TYPESCAST_MAP)
        param_type = VAL_TYPESCAST_MAP.get(param['type'], None)
        name = generate_name(param['name'])

        if 'enum' in param:
            param_type = param_type.replace('NativeMethods.', 'NativeMethodsT.')

        # Handling ConstructVector type (e.g., array or vector construction)
        if 'ConstructVector' in param_type:
            return f'var __{name} = {param_type}({name}, {name}.Length)'

        # Handle function pointer marshalling, skip if it's already marshaled
        if param_type and 'GetFunctionPointerForDelegate' in param_type and not is_need_marshal(param.get('prototype')):
            return ''  # Skip if no marshaling is needed

        # Handle general type-based casting (other than function pointers)
        elif param_type:
            return f'var __{name} = {param_type}({name})'

        # Handle reference types by creating fixed pointers (unsafe code)
        if 'ref' in param and param['ref'] is True:
            if 'enum' in param:
                type_name = generate_name(param['enum'].get('name', 'UnnamedEnum'))
            else:
                type_name = TYPES_MAP.get(param['type'], None)
            return f'fixed({type_name}* __{name} = &{name}) {{'

        # Return an empty string for unhandled cases
        return ''

    def gen_return(param):
        """
        Generates the return value casting code for the method, if applicable.
        """
        return f"{RET_TYPESCAST_MAP.get(param['type'], None)} __retVal_native"

    output_parts = []

    # Handle return type casting if the return type is an object (requires marshaling)
    ret_type = method.get('retType', {})
    if ret_type:
        if is_obj_return(ret_type.get('type')):
            ret_val = gen_return(ret_type)
            if ret_val:
                output_parts.append(f"{tabs}{ret_val};\n")

    # Handle casting for each parameter in the method
    if method.get('paramTypes'):
        for param in method['paramTypes']:
            param_cast = gen_param(param)
            if param_cast:
                output_parts.append(f"{tabs}{param_cast}")
                output_parts.append(';\n' if param_cast[-1] != '{' else '\n')

    # Return the full generated string of parameter and return type casting code
    return ''.join(output_parts)


def gen_paramscast_assign(method: dict, tabs: str) -> str:
    """
    Generates parameter assignment code for a method, including casting, marshaling for 'ref' types,
    and handling special types like 'VectorData'.
    """

    def gen_param(param: dict) -> str:
        """
        Generates assignment code for a single parameter, considering 'ref' types and other special cases like 'VectorData'.
        """
        if 'ref' in param and param['ref'] is True:
            param_type = ASS_TYPESCAST_MAP.get(param['type'], None)
            name = generate_name(param['name'])

            if 'enum' in param:
                param_type = param_type.replace('NativeMethods.', 'NativeMethodsT.')

            # Handle 'VectorData' type
            if 'VectorData' in param_type:
                size = SIZ_TYPESCAST_MAP.get(param['type'], None)
                output = [
                    f'Array.Resize(ref {name}, {size}(&__{name}));\n',  # Resize vector if needed
                    f'{tabs}{param_type}(&__{name}, {name})'  # Marshal vector
                ]
                return ''.join(output)
            elif param_type != '':  # Handle other types
                return f'{name} = {param_type}(&__{name})'
            else:
                return ''
        return ''  # Return empty if no 'ref' is in param

    def gen_return(param):
        """
        Generates assignment code for the return type, considering 'VectorData' and other special cases.
        """
        param_type = ASS_TYPESCAST_MAP.get(param['type'], None)

        if 'enum' in param:
            param_type = param_type.replace('NativeMethods.', 'NativeMethodsT.')

        if 'VectorData' in param_type:
            size = SIZ_TYPESCAST_MAP.get(param['type'], None)
            return_type = convert_type(param['type'], False)
            output = [
                f'__retVal = new {return_type[:-1]}{size}(&__retVal_native)];' + '\n',
                f'{tabs}{param_type}(&__retVal_native, __retVal)'  # Marshal vector return value
            ]
            return ''.join(output)
        elif param_type != '':  # Handle other types
            return f'__retVal = {param_type}(&__retVal_native)'
        else:
            return ''  # Return empty if no valid type

    # List to hold the code parts for parameters and return
    output_parts = []

    # Handle return type if needed
    ret_type = method.get('retType', {})
    if ret_type:
        if is_obj_return(ret_type.get('type')):
            ret_val = gen_return(ret_type)
            if ret_val:
                output_parts.append(f'{tabs}{ret_val};\n')

    # Handle parameter types
    if method.get('paramTypes'):
        for param in method['paramTypes']:
            param_cast = gen_param(param)
            if param_cast:
                output_parts.append(f'{tabs}{param_cast};\n')

    return ''.join(output_parts)


def gen_paramscast_assign_end(method: dict, tabs: str) -> str:
    """
    Generate code for assigning end-of-scope cleanup for method parameters.
    """

    def gen_param(param: dict) -> str:
        """
        Generate cleanup code for a single parameter based on its type and reference status.
        """
        # Check if the parameter is passed by reference
        if param.get('ref'):
            param_type = ASS_TYPESCAST_MAP.get(param.get('type'), None)
            # If the parameter type is in the map but maps to an empty string, close the block
            if param_type == '':
                return '}'
        return ''  # No action needed

    # List to collect the output lines
    output_parts = []

    # Iterate through parameters and generate code for those needing cleanup
    for param in method.get('paramTypes', []):
        param_cast = gen_param(param)
        if param_cast:
            output_parts.append(f'{tabs}{param_cast}\n')

    return ''.join(output_parts)


def gen_paramscast_cleanup(method: dict, tabs: str) -> str:
    """
    Generate code for parameter and return value cleanup using type casts.
    """

    def gen_param(param: dict) -> str:
        """
        Generate type casting cleanup code for a parameter.
        """
        param_type = DEL_TYPESCAST_MAP.get(param.get('type'))
        if param_type:
            param_name = generate_name(param.get('name', 'UnnamedParam'))
            return f'{param_type}(&__{param_name})'
        return ''  # Return empty string if type is not in DEL_TYPESCAST_MAP

    def gen_return(ret_type: dict):
        """
        Generate type casting cleanup code for a return value.
        """
        return_type = DEL_TYPESCAST_MAP.get(ret_type.get('type'))
        if return_type:
            return f'{return_type}(&__retVal_native)'
        return ''  # Return empty string if type is not in DEL_TYPESCAST_MAP

    # Initialize the output parts
    output_parts = []

    # Handle return type cleanup
    ret_type = method.get('retType', {})
    if is_obj_return(ret_type.get('type', '')):
        ret_cast = gen_return(ret_type)
        if ret_cast:
            output_parts.append(f'{tabs}{ret_cast};\n')

    # Handle parameter cleanup
    for param in method.get('paramTypes', []):
        param_cast = gen_param(param)
        if param_cast:
            output_parts.append(f'{tabs}{param_cast};\n')

    return ''.join(output_parts)


def gen_delegate_body(prototype: dict, delegates: set[str]) -> str:
    """
    Generates a C# delegate definition from the provided prototype.
    """
    # Check for duplicate delegates
    delegate_name = prototype.get('name', 'UnnamedDelegate')
    delegate_description = prototype.get('description', '')

    # Check for duplicate delegates
    if delegate_name in delegates:
        return ''  # Skip if already generated

    # Add the delegate name to the set
    delegates.add(delegate_name)

    # Get the return type and convert it
    ret_type = prototype.get('retType', {})
    return_type = get_dtype_name(ret_type, True)

    # Start building the delegate definition
    delegate_code = []
    if delegate_description:
        delegate_code.append(f"\t/// <summary>")
        delegate_code.append(f"\t/// {delegate_description}")
        delegate_code.append(f"\t/// </summary>")
    param_list = gen_params(prototype, ParamGen.TypesCastNames)
    delegate_code.append(f'\tpublic delegate {return_type} {delegate_name}({param_list});')

    # Join the list into a single formatted string
    return '\n'.join(delegate_code)


def gen_enum_body(enum: dict, enum_type: str, enums: set[str]) -> str:
    """
    Generates a C# delegate definition from the provided prototype.
    """
    # Get the return type and convert it
    enum_name = enum.get('name', 'InvalidEnum')
    enum_description = enum.get('description', '')
    enum_values = enum.get('values', [])

    # Check for duplicate enums
    if enum_name in enums:
        return ''  # Skip if already generated

    # Add the enum name to the set
    enums.add(enum_name)

    # Start building the enum definition
    enum_code = []
    if enum_description:
        enum_code.append(f"\t/// <summary>")
        enum_code.append(f"\t/// {enum_description}")
        enum_code.append(f"\t/// </summary>")
    enum_code.append(f"\tpublic enum {enum_name} : {convert_type(enum_type)}\n\t{{")

    # Iterate over the enum values and generate corresponding code
    for i, value in enumerate(enum_values):
        name = value.get('name', 'InvalidName')
        enum_value = value.get('value', str(i))
        description = value.get('description', '')

        # Add summary comment for each value
        if description:
            enum_code.append(f"\t\t/// <summary>")
            enum_code.append(f"\t\t/// {description}")
            enum_code.append(f"\t\t/// </summary>")
        enum_code.append(f"\t\t{name} = {enum_value},")

    # Close the enum definition
    enum_code.append("\t}\n")

    return '\n'.join(enum_code)


def gen_documentation(method: dict, tab_level: int = 0) -> str:
    """
    Generate a C#-style XML documentation block from a JSON block with customizable tabulation.
    """
    # Extract general details
    name = method.get('name', 'UnnamedFunction')
    description = method.get('description', 'No description provided.')
    param_types = method.get('paramTypes', [])
    ret_type = method.get('retType', {}).get('type', 'void')

    # Determine tabulation
    tab = '\t' * tab_level

    # Start building the XML documentation comment
    docstring = [f"{tab}/// <summary>\n"]
    docstring.append(f"{tab}/// {description}\n")
    docstring.append(f"{tab}/// </summary>\n")

    # Add parameters
    for param in param_types:
        param_name = param.get('name', 'UnnamedParam')
        param_type = param.get('type', 'Any')  # Optionally include type in the summary
        param_desc = param.get('description', 'No description available.')
        docstring.append(f"{tab}/// <param name=\"{param_name}\">{param_desc}</param>\n")

    # Add return type
    if ret_type.lower() != 'void':
        ret_desc = method.get('retType', {}).get('description', 'No description available.')
        docstring.append(f"{tab}/// <returns>{ret_desc}</returns>\n")

    # Add callback prototype if present
    for param in param_types:
        if param.get('type') == 'function' and 'prototype' in param:
            prototype = param['prototype']
            proto_name = prototype.get('name', 'UnnamedCallback')
            proto_desc = prototype.get('description', 'No description provided.')
            proto_params = prototype.get('paramTypes', [])
            proto_ret = prototype.get('retType', {})

            # Document callback as part of the function summary
            docstring.append(f"{tab}/// <remarks>\n")
            docstring.append(f"{tab}/// Callback {proto_name}: {proto_desc}\n")

            for proto_param in proto_params:
                p_name = proto_param.get('name', 'UnnamedParam')
                p_desc = proto_param.get('description', 'No description available.')
                docstring.append(f"{tab}/// - Parameter {p_name}: {p_desc}\n")

            if proto_ret:
                proto_ret_type = proto_ret.get('type', 'void')
                proto_ret_desc = proto_ret.get('description', 'No description available.')
                docstring.append(f"{tab}/// - Returns: {proto_ret_desc} ({proto_ret_type})\n")

            docstring.append(f"{tab}/// </remarks>\n")

    return ''.join(docstring)


def generate_method_body(method: dict, ret_type: dict, return_type: str) -> list[str]:
    """Generate the method body for a given method."""
    body = []  # List to hold the method body code
    indent = '\t\t\t'  # Default indentation
    inner_indent = indent  # Inner indentation for try/catch block

    # Check if the return type is an object and if it has a return value

    is_obj_ret = is_obj_return(ret_type['type'])
    has_ret = not is_obj_ret and ret_type['type'] != 'void'

    # Generate and add parameter casting code
    index = 0
    params_cast = gen_paramscast(method, indent)

    # For object return types, declare a return value variable
    has_cast = params_cast and ret_type['type'] != 'void'
    if is_obj_ret or has_cast:
        body.append(f'{indent}{return_type} __retVal;')

    has_try = False
    if params_cast:
        body.append(params_cast)
        index = len(body)  # Mark position to insert try block
        body.append(f'{indent}try {{')  # Start try block
        inner_indent = '\t\t\t\t'  # Adjust inner indentation
        has_try = has_cast

    # Generate the function call with marshaled parameters
    function_call = f'__{method["name"]}({gen_params(method, ParamGen.CastNames)})'
    if is_obj_ret:
        body.append(f'{inner_indent}__retVal_native = {function_call};')
    elif has_try:
        body.append(f'{inner_indent}__retVal = {function_call};')
    elif has_ret:
        body.append(f'{inner_indent}{return_type} __retVal = {function_call};')
    else:
        body.append(f'{inner_indent}{function_call};')

    # Unmarshal native data back into managed data
    assign_cast = gen_paramscast_assign(method, inner_indent)
    if assign_cast:
        body.append(f'{inner_indent}// Unmarshal - Convert native data to managed data.\n{assign_cast}')

    # Cleanup after function call
    cleanup = gen_paramscast_cleanup(method, inner_indent)
    if cleanup:
        body.append(f'{indent}}}\n{indent}finally {{\n{inner_indent}// Perform cleanup.\n{cleanup}{indent}}}')
    elif params_cast:
        # If no cleanup, remove try/catch block and adjust indentation
        body.pop(index)
        for i in range(index, len(body)):
            body[i] = body[i][1:]  # Remove one level of indentation

    # Handle the return value for the method
    if ret_type['type'] == 'function':
        marshal_func = 'Marshalling.GetDelegateForFunctionPointer' if is_need_marshal(
            ret_type['prototype']) else 'Marshal.GetDelegateForFunctionPointer'
        body.append(f'{indent}return {marshal_func}<{ret_type["prototype"]["name"]}>(__retVal);')
    elif ret_type['type'] != 'void':
        body.append(f'{indent}return __retVal;')

    # Handle the assignment end for parameters
    assign_end = gen_paramscast_assign_end(method, indent)
    if assign_end:
        body.append(f'\n{assign_end}')

    return body  # Return the generated method body


def generate_method_code(method: dict) -> str:
    """
    Generate the implementation code for a single method.
    """
    # Safely extract return type and determine if it uses a delegate prototype
    ret_type = method.get('retType', {})
    return_type = get_type_name(ret_type)

    method_name = method.get('name', 'UnnamedFunction')

    # Generate the method signature and body
    func_code = [
        gen_documentation(method, 2),
        f'\t\tinternal static delegate*<{gen_types(method)}> {method_name} = &___{method_name};',
        f'\t\tinternal static delegate* unmanaged[Cdecl]<{gen_ctypes(method)}> __{method_name};',
        f'\t\tprivate static {return_type} ___{method_name}({gen_params(method, ParamGen.TypesNames)})',
        '\t\t{',
        *generate_method_body(method, ret_type, return_type),
        '\t\t}\n'
    ]

    return '\n'.join(func_code)


def generate_delegate_code(pplugin: dict, delegates: set[str]) -> str:
    """
    Generate C# delegate code from a plugin definition.
    """
    # Container for all generated delegate code
    content = []

    def process_prototype(prototype: dict):
        """
        Generate delegate code from the given prototype if it hasn't been processed.
        """
        delegate_code = gen_delegate_body(prototype, delegates)
        if delegate_code:
            content.append(delegate_code)

    # Main loop: Process all exported methods in the plugin
    for method in pplugin.get('exportedMethods', []):
        # Check the return type for a delegate
        ret_type = method.get('retType', {})
        if 'prototype' in ret_type:
            process_prototype(ret_type['prototype'])

        # Check parameters for delegates
        for param in method.get('paramTypes', []):
            if 'prototype' in param:
                process_prototype(param['prototype'])

    content.append('\n\n')

    # Join all generated delegates into a single string
    return '\n'.join(content)


def generate_enum_code(pplugin: dict, enums: set[str]) -> str:
    """
    Generate C# enum code from a plugin definition.
    """
    # Container for all generated enum code
    content = []

    def process_enum(enum_data: dict, enum_type: str):
        """
        Generate enum code from the given enum data if it hasn't been processed.
        """
        enum_code = gen_enum_body(enum_data, enum_type, enums)
        if enum_code:
            content.append(enum_code)

    def process_prototype(prototype: dict):
        """
        Recursively process a function prototype for enums.
        """
        if 'enum' in prototype.get('retType', {}):
            process_enum(prototype['retType']['enum'], prototype['retType'].get('type', ''))

        for param in prototype.get('paramTypes', []):
            if 'enum' in param:
                process_enum(param['enum'], param.get('type', ''))
            if 'prototype' in param:  # Process nested prototypes
                process_prototype(param['prototype'])

    # Main loop: Process all exported methods in the plugin
    for method in pplugin.get('exportedMethods', []):
        if 'retType' in method and 'enum' in method['retType']:
            process_enum(method['retType']['enum'], method['retType'].get('type', ''))

        for param in method.get('paramTypes', []):
            if 'enum' in param:
                process_enum(param['enum'], param.get('type', ''))
            if 'prototype' in param:  # Handle nested function prototypes
                process_prototype(param['prototype'])

    content.append('\n')

    # Join all generated enums into a single string
    return '\n'.join(content)


def generate_header(plugin_name: str, pplugin: dict) -> str:
    """Generate the full header content for the plugin."""
    # GitHub link for reference
    link = 'https://github.com/untrustedmodders/plugify-module-dotnet/blob/main/generator/generator.py'

    # Initialize content with common imports and namespace declaration
    content = [
        'using System;',
        'using System.Numerics;',
        'using System.Runtime.CompilerServices;',
        'using System.Runtime.InteropServices;',
        'using Plugify;',
        '',
        f'// Generated with {link} from {plugin_name}',
        '',
        f'namespace {plugin_name} {{',
        '#pragma warning disable CS0649',
        '',
    ]

    # Append enum definitions
    enums = set()
    content.append(generate_enum_code(pplugin, enums))

    # Append delegate definitions
    delegates = set()
    content.append(generate_delegate_code(pplugin, delegates))

    # Append method implementations
    content.append(f'\tinternal static unsafe class {plugin_name} {{\n')
    for method in pplugin.get('exportedMethods', []):
        content.append(generate_method_code(method))
    content.append('\t}\n#pragma warning restore CS0649\n}')

    # Join and return the complete content as a single string
    return '\n'.join(content)


def main(manifest_path: str, output_dir: str, override: bool):
    """Main function to process the plugin and generate the C# header file."""
    # Validate inputs
    if not os.path.isfile(manifest_path):
        print(f'Manifest file does not exist: {manifest_path}')
        return 1
    if not os.path.isdir(output_dir):
        print(f'Output folder does not exist: {output_dir}')
        return 1

    # Determine plugin name and output file path
    plugin_name = os.path.basename(manifest_path).rsplit('.', 3)[0]
    output_path = os.path.join(output_dir, 'pps', f'{plugin_name}.cs')
    os.makedirs(os.path.dirname(output_path), exist_ok=True)

    # Handle existing file
    if os.path.isfile(output_path) and not override:
        print(f'Output file already exists: {output_path}. Use --override to overwrite existing file.')
        return 1

    try:
        # Read and parse manifest
        with open(manifest_path, 'r', encoding='utf-8') as file:
            pplugin = json.load(file)

        # Generate header content
        content = generate_header(plugin_name, pplugin)

        # Write content to file
        with open(output_path, 'w', encoding='utf-8') as fd:
            fd.write(''.join(content))

    except Exception as e:
        print(f'An error occurred: {e}')
        return 1

    print(f'Header generated at: {output_path}')
    return 0


def get_args():
    """Parse command-line arguments."""
    parser = argparse.ArgumentParser(description='Generate C# .cs include files for plugin manifests.')
    parser.add_argument('manifest', help='Path to the plugin manifest file')
    parser.add_argument('output', help='Path to the output directory')
    parser.add_argument('--override', action='store_true', help='Override existing files')
    return parser.parse_args()


if __name__ == '__main__':
    args = get_args()
    sys.exit(main(args.manifest, args.output, args.override))
