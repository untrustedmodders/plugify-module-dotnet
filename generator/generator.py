#!/usr/bin/python3
import sys
import argparse
import os
import json
from enum import Enum

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
    'any': 'object',
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
    'any[]': 'object[]',
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


def convert_type(type_name: str, is_ref=False) -> str:
    """
    Converts a type name to its corresponding C# type.
    If the type is a reference, it appends 'ref' to the type.
    """
    base_type = TYPES_MAP.get(type_name, None)
    return f'ref {base_type}' if is_ref else base_type


def convert_dtype(type_name: str, is_ref=False, is_ret=False) -> str:
    """
    Converts a type name to its corresponding C# type, with additional handling for references
    and return types (for POD types, references are forced).
    """
    if not is_ret and is_pod_type(type_name):
        is_ref = True
    return convert_type(type_name, is_ref)


def convert_ctype(type_name: str, is_ref=False, is_ret=False) -> str:
    """
    Converts a type name to its corresponding C type.
    If the type is a reference, it appends '*' (pointer) to the type.
    """
    base_type = CTYPES_MAP.get(type_name, None)

    if is_ref:
        return f'{base_type}*' if '*' not in base_type else base_type
    elif is_ret and '*' in base_type:
        return base_type[:-1]  # Remove pointer if it's a return type
    return base_type


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


def generate_name(name):
    """
    Generates a valid C# name by appending an underscore if the name is invalid.
    """
    return f"{name}_" if name in INVALID_NAMES else name


class ParamGen(Enum):
    Types = 1
    Names = 2
    TypesNames = 3
    TypesCastNames = 4
    CastNames = 5


def gen_params(method: dict, param_gen: ParamGen) -> str:
    """
    Generates the parameters string for the method based on the param_gen type.
    Handles different modes such as Types, Names, CastNames, and TypesCastNames.

    Args:
        method (dict): The method metadata containing the parameters.
        param_gen (ParamGen): The mode of generation (e.g., Types, Names, CastNames).

    Returns:
        str: A string representing the method's parameters based on the selected mode.
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
        type_str = convert_type(param['type'], 'ref' in param and param['ref'])
        if 'delegate' in type_str and 'prototype' in param:
            type_str = generate_name(param['prototype']['name'])
        return f'{type_str} {generate_name(param["name"])}'

    # Helper function to generate the parameter type and cast name for TypesCastNames
    def gen_param_types_cast(param: dict) -> str:
        type_str = convert_dtype(param['type'], 'ref' in param and param['ref'])
        if 'delegate' in type_str and 'prototype' in param:
            type_str = generate_name(param['prototype']['name'])
        return f'{type_str} {generate_name(param["name"])}'

    # Function to generate the appropriate string for a single parameter based on the param_gen mode
    def gen_param(param: dict) -> str:
        # Handle parameter type generation (e.g., ref, function, delegate)
        if param_gen == ParamGen.Types:
            return gen_param_type(param)

        # Handle parameter name generation
        elif param_gen == ParamGen.Names:
            return generate_name(param['name'])

        # Handle cast names for marshaling or references
        elif param_gen == ParamGen.CastNames:
            return gen_param_cast_name(param)

        # Handle types with type names
        elif param_gen == ParamGen.TypesNames:
            return gen_param_type_name(param)

        # Handle types with cast names
        elif param_gen == ParamGen.TypesCastNames:
            return gen_param_types_cast(param)

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
        parts.extend([convert_ctype(param.get('type', None), 'ref' in param and param['ref'], False) for param in
                      method['paramTypes']])

    # Add the return type
    ret_type = method.get('retType', {})
    if ret_type:
        parts.append(convert_ctype(ret_type.get('type', ''), is_ret=True))

    # Join all parts together and return the final string
    return ', '.join(parts)


def gen_types(method: dict) -> str:
    """
    Generates a string representation of the types for the method, including its parameters and return type.
    """

    def gen_param(param: dict) -> str:
        """
        Convert a parameter's type to a string representation, handling references and delegates if present.
        """
        type_str = convert_type(param.get('type', ''), 'ref' in param and param['ref'] is True)

        # Check for delegate type and adjust if a prototype exists
        if 'delegate' in type_str and 'prototype' in param:
            type_str = generate_name(param['prototype']['name'])

        return type_str

    # Initialize list to hold type representations
    parts = []

    # Process parameters if they exist
    if method.get('paramTypes'):
        parts.extend([gen_param(param) for param in method['paramTypes']])

    # Add the return type
    ret_type = method.get('retType', {})
    if ret_type:
        parts.append(gen_param(ret_type))

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
            ctype = TYPES_MAP.get(param['type'], None)
            return f'fixed({ctype}* __{name} = &{name}) {{'

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
        if 'VectorData' in param_type:
            size = SIZ_TYPESCAST_MAP.get(param['type'], None)
            return_type = convert_type(param['type'], False)
            output = [
                f'__retVal = new {return_type[:-1]}{size}(&__retVal_native);' + '\n',
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


def gen_delegate_body(prototype: dict, delegates: set):
    """
    Generates a C# delegate definition from the provided prototype.
    """
    # Get the return type and convert it
    ret_type = prototype.get('retType', {})
    return_type = convert_dtype(
        ret_type.get('type', ''),
        ret_type.get('ref', False),
        True
    )

    if 'delegate' in return_type and 'prototype' in ret_type:
        return_type = generate_name(ret_type['prototype'].get('name', 'UnnamedDelegate'))

    param_list = gen_params(prototype, ParamGen.TypesCastNames)
    delegate = f'\tpublic delegate {return_type} {prototype.get("name", "UnnamedDelegate")}({param_list});\n'

    # Avoid duplicate delegates
    if delegate not in delegates:
        delegates.add(delegate)
        return delegate
    return None


def gen_documentation(method: dict, tab_level: int = 0) -> str:
    """
    Generate a Doxygen-style comment block from a JSON block with customizable tabulation.

    Args:
        method (dict): The input JSON data describing the function.
        tab_level (int): The level of tabulation for the generated comment.

    Returns:
        str: The generated Doxygen comment block.
    """
    # Extract general details
    name = method.get('name', 'UnnamedFunction')
    description = method.get('description', 'No description provided.')
    param_types = method.get('paramTypes', [])
    ret_type = method.get('retType', {}).get('type', 'void')

    # Determine tabulation
    tab = '    ' * tab_level

    # Start building the Doxygen comment
    docstring = [f"{tab}/**\n"]
    docstring.append(f"{tab} * @brief {description}\n")
    docstring.append(f"{tab} *\n")
    docstring.append(f"{tab} * @function {name}\n")

    # Add parameters
    for param in param_types:
        param_name = param.get('name', 'UnnamedParam')
        param_type = param.get('type', 'Any')
        param_desc = param.get('description', 'No description available.')
        docstring.append(f"{tab} * @param {param_name} ({param_type}): {param_desc}\n")

    # Add return type
    if ret_type.lower() != 'void':
        ret_desc = method.get('retType', {}).get('description', 'No description available.')
        docstring.append(f"{tab} *\n{tab} * @return {ret_type}: {ret_desc}\n")

    # Add callback prototype if present
    for param in param_types:
        if param.get('type') == 'function' and 'prototype' in param:
            prototype = param['prototype']
            proto_name = prototype.get('name', 'UnnamedCallback')
            proto_desc = prototype.get('description', 'No description provided.')
            proto_params = prototype.get('paramTypes', [])
            proto_ret = prototype.get('retType', {})

            docstring.append(f"{tab} *\n{tab} * @callback {proto_name}\n")
            docstring.append(f"{tab} * @brief {proto_desc}\n")
            docstring.append(f"{tab} *\n")

            for proto_param in proto_params:
                p_name = proto_param.get('name', 'UnnamedParam')
                p_type = proto_param.get('type', 'Any')
                p_desc = proto_param.get('description', 'No description available.')
                docstring.append(f"{tab} * @param {p_name} ({p_type}): {p_desc}\n")

            if proto_ret:
                proto_ret_type = proto_ret.get('type', 'void')
                proto_ret_desc = proto_ret.get('description', 'No description available.')
                docstring.append(f"{tab} *\n{tab} * @return (callback): {proto_ret_type}: {proto_ret_desc}\n")

    # Close Doxygen comment
    docstring.append(f"{tab} */")

    return ''.join(docstring)


def generate_method_body(method: dict, ret_type: dict, return_type: str) -> list[str]:
    """Generate the method body for a given method."""
    body = []  # List to hold the method body code
    indent = '\t\t\t'  # Default indentation
    inner_indent = indent  # Inner indentation for try/catch block

    # Check if the return type is an object and if it has a return value

    is_obj_ret = is_obj_return(ret_type['type'])
    has_ret = not is_obj_ret and ret_type['type'] != 'void'

    # For object return types, declare a return value variable
    if is_obj_ret:
        body.append(f'{indent}{return_type} __retVal;')

    # Generate and add parameter casting code
    index = 0
    params_cast = gen_paramscast(method, indent)
    if params_cast:
        body.append(params_cast)
        index = len(body)  # Mark position to insert try block
        body.append(f'{indent}try {{')  # Start try block
        inner_indent = '\t\t\t\t'  # Adjust inner indentation

    # Generate the function call with marshaled parameters
    function_call = f'__{method["name"]}({gen_params(method, ParamGen.CastNames)})'
    if is_obj_ret:
        body.append(f'{inner_indent}__retVal_native = {function_call};')
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
    return_type = convert_type(
        ret_type.get('type', ''),
        ret_type.get('ref', False)
    )

    if 'delegate' in return_type and 'prototype' in ret_type:
        prototype_name = ret_type['prototype'].get('name', 'UnnamedDelegate')
        return_type = generate_name(prototype_name)

    method_name = method.get('name', 'UnnamedFunction')

    # Generate the method signature and body
    func_code = [
        gen_documentation(method, 2),
        f'\t\tinternal static delegate*<{gen_types(method)}> {method_name} = &___{method_name};',
        f'\t\tinternal static delegate* unmanaged[Cdecl]<{gen_ctypes(method)}> __{method_name};',
        f'\t\tprivate static {return_type} ___{method_name}({gen_params(method, ParamGen.TypesNames)})',
        '\t\t{',
        generate_method_body(method, ret_type, return_type),
        '\t\t}\n'
    ]

    return '\n'.join(func_code)


def generate_delegate_code(pplugin: dict, delegates: set) -> str:
    """Generate delegate code."""
    content = []
    for method in pplugin.get('exportedMethods', []):
        ret_type = method.get('retType', {})
        if 'prototype' in ret_type:
            delegate = gen_delegate_body(ret_type['prototype'], delegates)
            if delegate:
                content.append(delegate)

        for param_type in method.get('paramTypes', []):
            if 'prototype' in param_type:
                delegate = gen_delegate_body(param_type['prototype'], delegates)
                if delegate:
                    content.append(delegate)

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

    # Append delegate definitions
    delegates = set()
    content.append(generate_delegate_code(pplugin, delegates))

    # Append method implementations
    content.append(f'\n\tinternal static unsafe class {plugin_name} {{\n')
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
    plugin_name = os.path.splitext(os.path.basename(manifest_path))[0]
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