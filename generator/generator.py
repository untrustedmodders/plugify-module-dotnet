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
    #'add',
    #'and',
    #'alias',
    #'ascending',
    #'args',
    #'async',
    #'await',
    #'by',
    #'descending',
    #'dynamic',
    #'equals',
    #'file',
    #'from',
    #'get',
    #'global',
    #'group',
    #'init',
    #'into',
    #'join',
    #'let',
    #'managed',
    #'nameof',
    #'nint',
    #'not',
    #'notnull',
    #'nuint',
    #'on',
    #'or',
    #'orderby',
    #'partial',
    #'partial',
    #'record',
    #'remove',
    #'required',
    #'scoped',
    #'select',
    #'set',
    #'unmanaged',
    #'value',
    #'var',
    #'when',
    #'where',
    #'where',
    #'with',
    #'yield'
}


def is_obj_return(type_name):
    return '[]' in type_name or type_name == 'string' or type_name == 'any'


def is_pod_type(type_name):
    return type_name == 'vec2' or type_name == 'vec3' or type_name == 'vec4' or type_name == 'mat4x4'


def validate_manifest(pplugin):
    parse_errors = []
    methods = pplugin.get('exportedMethods')
    if type(methods) is list:
        for i, method in enumerate(methods):
            if type(method) is dict:
                if type(method.get('type')) is str:
                    parse_errors += [f'root.exportedMethods[{i}].type not string']
            else:
                parse_errors += [f'root.exportedMethods[{i}] not object']
    else:
        parse_errors += ['root.exportedMethods not array']
    return parse_errors


def convert_type(type_name, is_ref=False):
    type = TYPES_MAP.get(type_name, 'int')
    if is_ref:
        return 'ref ' + type
    else:
        return type


def convert_dtype(type_name, is_ref=False, is_ret=False):
    if is_ret == False and is_pod_type(type_name):
        is_ref = True
    type = TYPES_MAP.get(type_name, 'int')
    if is_ref:
        return 'ref ' + type
    else:
        return type


def convert_ctype(type_name, is_ref=False, is_ret=False):
    type = CTYPES_MAP.get(type_name, 'int')
    if is_ref:
        if '*' in type:
            return type
        else:
            return type + '*'
    else:
        if is_ret and '*' in type:
            return type[:-1]
        else:
            return type


def is_need_marshal(method):
    ret_type = method['retType']['type'];
    if is_obj_return(ret_type):
        return True
    if method['paramTypes']:
        it = iter(method['paramTypes'])
        for p in it:
            if is_obj_return(p['type']):
                return True
    return False


def generate_name(name):
    if name in INVALID_NAMES:
        return name + '_'
    else:
        return name


class ParamGen(Enum):
    Types = 1
    Names = 2
    TypesNames = 3
    TypesCastNames = 4
    CastNames = 5


def gen_delegate(prototype):
    ret_type = prototype['retType']
    return_type = convert_dtype(ret_type['type'], 'ref' in ret_type and ret_type['ref'] is True, True)
    return (f'\tpublic delegate {return_type} '
            f'{prototype["name"]}({gen_params(prototype, ParamGen.TypesCastNames)});\n')


def gen_params(method, param_gen: ParamGen):
    def gen_param(param):
        if param_gen == ParamGen.Types:
            type = convert_type(param['type'], 'ref' in param and param['ref'] is True)
            if param['type'] == 'function':
                type = generate_name(param['prototype']['name'])
            return type
        elif param_gen == ParamGen.Names:
            return generate_name(param['name'])
        elif param_gen == ParamGen.CastNames:
            if param['type'] == 'function':
                if is_need_marshal(param['prototype']):
                    return f'__{generate_name(param["name"])}'
                else:
                    return f'Marshal.GetFunctionPointerForDelegate({generate_name(param["name"])})'
            elif is_obj_return(param['type']):
                return '&__' + generate_name(param['name'])
            elif 'vec' in param['type'] or 'mat' in param['type']:
                if 'ref' in param and param['ref'] is True:
                    return '__' + generate_name(param['name'])
                else:
                    return '&' + generate_name(param['name'])
            else:
                if 'ref' in param and param['ref'] is True:
                    return '__' + generate_name(param['name'])
                else:
                    return generate_name(param['name'])
        elif param_gen == ParamGen.TypesCastNames:
            type = convert_dtype(param['type'], 'ref' in param and param['ref'] is True)
            if 'delegate' in type and 'prototype' in param:
                type = generate_name(param['prototype']['name'])
            return f'{type} {generate_name(param["name"])}'
        type = convert_type(param['type'], 'ref' in param and param['ref'] is True)
        if 'delegate' in type and 'prototype' in param:
            type = generate_name(param['prototype']['name'])
        return f'{type} {generate_name(param["name"])}'

    output_string = ''
    if method['paramTypes']:
        it = iter(method['paramTypes'])
        output_string += gen_param(next(it))
        for p in it:
            output_string += f', {gen_param(p)}'
    return output_string


def gen_ctypes(method):
    output_string = ''
    ret_type = method['retType']
    if method['paramTypes']:
        it = iter(method['paramTypes'])
        param = next(it)
        output_string += convert_ctype(param['type'], 'ref' in param and param['ref'] is True)
        for p in it:
            output_string += f', {convert_ctype(p["type"], "ref" in p and p["ref"] is True)}'
    if output_string != '':
        output_string += ', '
    output_string += f'{convert_ctype(ret_type["type"], is_ret=True)}'
    return output_string


def gen_types(method):
    def gen_param(param):
        type = convert_type(param['type'], 'ref' in param and param['ref'] is True)
        if 'delegate' in type and 'prototype' in param:
            type = generate_name(param['prototype']['name'])
        return type

    output_string = ''
    ret_type = method['retType']
    if method['paramTypes']:
        it = iter(method['paramTypes'])
        param = next(it)
        output_string += gen_param(param)
        for p in it:
            output_string += f', {gen_param(p)}'
    if output_string != '':
        output_string += ', '
    output_string += f'{gen_param(ret_type)}'
    return output_string


def gen_paramscast(method, tabs):
    def gen_param(param):
        type = VAL_TYPESCAST_MAP.get(param['type'], 'int')
        name = generate_name(param['name'])
        if 'ConstructVector' in type:
            return f'var __{name} = {type}({name}, {name}.Length)'
        elif type != '':
            if 'GetFunctionPointerForDelegate' in type and not is_need_marshal(param['prototype']):
                return ''
            else:
                return f'var __{name} = {type}({name})'
        else:
            if 'ref' in param and param['ref'] is True:
                ctype = TYPES_MAP.get(param['type'], 'int')
                return f'fixed({ctype}* __{name} = &{name}) {{'
            else:
                return ''

    def gen_return(param):
        type = RET_TYPESCAST_MAP.get(param['type'], 'int')
        return f'{type} __retVal_native'

    output_string = ''
    ret_type = method['retType']
    is_obj_ret = is_obj_return(ret_type['type'])
    if is_obj_ret:
        ret = gen_return(ret_type)
        if ret != '':
            output_string += f'{tabs}{ret};\n'
    if method['paramTypes']:
        it = iter(method['paramTypes'])
        param = gen_param(next(it))
        if param != '':
            output_string += f'{tabs}{param}'
            if output_string[-1] != '{':
                output_string += ';\n'
            else:
                output_string += '\n'
        for p in it:
            param = gen_param(p)
            if param != '':
                output_string += f'{tabs}{param}'
                if output_string[-1] != '{':
                    output_string += ';\n'
                else:
                    output_string += '\n'
    return output_string


def gen_paramscast_assign(method, tabs):
    def gen_param(param):
        if 'ref' in param and param['ref'] is True:
            type = ASS_TYPESCAST_MAP.get(param['type'], 'int')
            name = generate_name(param['name'])
            if 'VectorData' in type:
                size = SIZ_TYPESCAST_MAP.get(param['type'], 'int')
                output = f'Array.Resize(ref {name}, {size}(&__{name}));\n'
                output += f'{tabs}{type}(&__{name}, {name})'
                return output
            elif type != '':
                return f'{name} = {type}(&__{name})'
            else:
                return ''
        else:
            return ''
    def gen_return(param):
        type = ASS_TYPESCAST_MAP.get(param['type'], 'int')
        if 'VectorData' in type:
            size = SIZ_TYPESCAST_MAP.get(param['type'], 'int')
            return_type = convert_type(param['type'], False)
            output = f'__retVal = new {return_type[:-1]}{size}(&__retVal_native)];\n'
            output += f'{tabs}{type}(&__retVal_native, __retVal)'
            return output
        elif type != '':
            return f'__retVal = {type}(&__retVal_native)'
        else:
            return ''

    output_string = ''
    ret_type = method['retType']
    is_obj_ret = is_obj_return(ret_type['type'])
    if is_obj_ret:
        ret = gen_return(ret_type)
        if ret != '':
            output_string += f'{tabs}{ret};\n'
    if method['paramTypes']:
        it = iter(method['paramTypes'])
        param = gen_param(next(it))
        if param != '':
            output_string += f'{tabs}{param};\n'
        for p in it:
            param = gen_param(p)
            if param != '':
                output_string += f'{tabs}{param};\n'
    return output_string


def gen_paramscast_assign_end(method, tabs):
    def gen_param(param):
        if 'ref' in param and param['ref'] is True:
            type = ASS_TYPESCAST_MAP.get(param['type'], 'int')
            if type != '':
                return ''
            else:
                return '}'
        else:
            return ''

    output_string = ''
    if method['paramTypes']:
        it = iter(method['paramTypes'])
        param = gen_param(next(it))
        if param != '':
            output_string += f'{tabs}{param}\n'
        for p in it:
            param = gen_param(p)
            if param != '':
                output_string += f'{tabs}{param}\n'
    return output_string


def gen_paramscast_cleanup(method, tabs):
    def gen_param(param):
        type = DEL_TYPESCAST_MAP.get(param['type'], 'int')
        if type == '':
            return ''
        else:
            return f'{type}(&__{generate_name(param["name"])})'

    def gen_return(param):
        type = DEL_TYPESCAST_MAP.get(param['type'], 'int')
        if type == '':
            return ''
        else:
            return f'{type}(&__retVal_native)'

    output_string = ''
    ret_type = method['retType']
    is_obj_ret = is_obj_return(ret_type['type'])
    if is_obj_ret:
        ret = gen_return(ret_type)
        if ret != '':
            output_string += f'{tabs}{ret};\n'
    if method['paramTypes']:
        it = iter(method['paramTypes'])
        param = gen_param(next(it))
        if param != '':
            output_string += f'{tabs}{param};\n'
        for p in it:
            param = gen_param(p)
            if param != '':
                output_string += f'{tabs}{param};\n'
    return output_string


def gen_wrapper(param, delegates):
    content = ''
    prototype = param['prototype']
    delegate = gen_delegate(prototype)
    if delegate not in delegates:
        content += delegate
        delegates.add(delegate)
    return content


def main(manifest_path, output_dir, override):
    if not os.path.isfile(manifest_path):
        print(f'Manifest file not exists {manifest_path}')
        return 1
    if not os.path.isdir(output_dir):
        print(f'Output folder not exists {output_dir}')
        return 1

    plugin_name = os.path.splitext(os.path.basename(manifest_path))[0]
    header_dir = os.path.join(output_dir, 'pps')
    if not os.path.exists(header_dir):
        os.makedirs(header_dir, exist_ok=True)
    header_file = os.path.join(header_dir, f'{plugin_name}.cs')
    if os.path.isfile(header_file) and not override:
        print(f'Already exists {header_file}')
        return 1

    with open(manifest_path, 'r', encoding='utf-8') as fd:
        pplugin = json.load(fd)

    parse_errors = validate_manifest(pplugin)
    if parse_errors:
        print('Parse fail:')
        for error in parse_errors:
            print(f'  {error}')
        return 1

    link = 'https://github.com/untrustedmodders/plugify-module-dotnet/blob/main/generator/generator.py'

    content = ('using System;\n'
                'using System.Numerics;\n'
                'using System.Runtime.CompilerServices;\n'
                'using System.Runtime.InteropServices;\n'
                'using Plugify;\n'
                '\n'
                f'//generated with {link} from {plugin_name} \n'
                '\n'
                f'namespace {plugin_name}\n{{'
                '\n'
                '#pragma warning disable CS0649\n'
                '\n'
                )

    delegates = set()

    # Declare delegates
    for method in pplugin['exportedMethods']:
        ret_type = method['retType']
        if 'prototype' in ret_type:
            content += gen_wrapper(ret_type, delegates)
        for param_type in method['paramTypes']:
            if 'prototype' in param_type:
                content += gen_wrapper(param_type, delegates)

    content += f'\n\tinternal static unsafe class {plugin_name}\n\t{{\n'

    for method in pplugin['exportedMethods']:
        func = (f'\t\tinternal static delegate* <{gen_types(method)}> {method["name"]} = &___{method["name"]};\n'
                f'\t\tinternal static delegate* unmanaged[Cdecl]<{gen_ctypes(method)}> __{method["name"]};\n')

        ret_type = method['retType']
        return_type = convert_type(ret_type['type'], 'ref' in ret_type and ret_type['ref'] is True)
        if 'delegate' in return_type and 'prototype' in ret_type:
            return_type = generate_name(ret_type['prototype']['name'])
        func += (f'\t\tprivate static {return_type} '
                f'___{method["name"]}({gen_params(method, ParamGen.TypesNames)})\n'
                '\t\t{\n')
        
        nrm_tabs = '\t\t\t'
        add_tabs = nrm_tabs

        is_obj_ret = is_obj_return(ret_type['type'])
        has_ret = not is_obj_ret and ret_type['type'] != 'void'
        
        if is_obj_ret:
            func += f'{nrm_tabs}{return_type} __retVal;\n'
            
        ret = ''
        params = gen_paramscast(method, nrm_tabs)
        if params != '':
            if has_ret:
                func += f'{params}\n{nrm_tabs}{return_type} __retVal;\n{nrm_tabs}try {{\n'
            else:
                func += f'{params}\n{nrm_tabs}try {{\n'
            add_tabs = '\t\t\t\t'
        else:
            ret = 'var '
       
        if is_obj_ret:
            func += f'{add_tabs}__retVal_native = __{method["name"]}({gen_params(method, ParamGen.CastNames)});\n'
        elif has_ret:
            func += f'{add_tabs}{ret}__retVal = __{method["name"]}({gen_params(method, ParamGen.CastNames)});\n'
        else:
            func += f'{add_tabs}__{method["name"]}({gen_params(method, ParamGen.CastNames)});\n'

        params = gen_paramscast_assign(method, add_tabs)
        if params != '':
            func += f'{add_tabs}// Unmarshal - Convert native data to managed data.\n{params}'

        params = gen_paramscast_cleanup(method, add_tabs)
        if params != '':
            func += f'{nrm_tabs}}}\n{nrm_tabs}finally {{\n{add_tabs}// Perform cleanup of callee allocated resources.\n{params}{nrm_tabs}}}\n'
        elif ret == '':
            func = func.replace(f'{nrm_tabs}try {{\n\t', '')
        
        if ret_type['type'] == 'function':
            if is_need_marshal(ret_type['prototype']):
                func += f'{nrm_tabs}return Marshalling.GetDelegateForFunctionPointer(__retVal, typeof({ret_type["prototype"]["name"]}));\n'
            else:
                func += f'{nrm_tabs}return Marshal.GetDelegateForFunctionPointer<{ret_type["prototype"]["name"]}>(__retVal);\n'
        elif ret_type['type'] != 'void':
            func += f'{nrm_tabs}return __retVal;\n'

        params = gen_paramscast_assign_end(method, nrm_tabs)
        if params != '':
            func += f'\n{params}'

        func += '\t\t}\n'
        
        content += func
        
    content += ('\t}\n' 
                '#pragma warning restore CS0649\n'
                '}\n')

    with open(header_file, 'w', encoding='utf-8') as fd:
        fd.write(content)

    return 0


def get_args():
    parser = argparse.ArgumentParser()
    parser.add_argument('manifest')
    parser.add_argument('output')
    parser.add_argument('--override', action='store_true')
    return parser.parse_args()


if __name__ == '__main__':
    args = get_args()
    sys.exit(main(args.manifest, args.output, args.override))
