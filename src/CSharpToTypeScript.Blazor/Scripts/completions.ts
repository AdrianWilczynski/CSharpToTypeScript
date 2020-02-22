export function getSnippets() {
    return [
        {
            label: 'class',
            insertText: `class \${1:Name} 
{
    $0
}`
        },
        {
            label: 'interface',
            insertText: `interface I\${1:Name} 
{
    $0
}`
        },
        {
            label: 'enum',
            insertText: `enum \${1:Name} 
{
    $0
}`
        },
        {
            label: 'namespace',
            insertText: `namespace \${1:Name} 
{
    $0
}`
        },
        {
            label: 'prop',
            insertText: 'public ${1:int} ${2:MyProperty} { get; set; }'
        },
        {
            label: 'propfull',
            insertText: `private \${1:int} \${2:myVar};
public int \${3:MyProperty}
{
    get { return \$2; }
    set { \$2 = value; }
}`
        },
        {
            label: 'sample',
            insertText: `using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyProject.DTOs
{
    public class Item : BaseItem
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsWhatever { get; set; }
        public IEnumerable<string> Collection { get; set; }
        public double[] Array { get; set; }
        public (int, string) Tuple { get; set; }
        public int? Nullable { get; set; }
        public GenericItem<string> Generic { get; set; }
        public Dictionary<string, string> Dictionary { get; set; }
        public DateTime Date { get; set; }
        public string Hello => "Hello World!";
        public byte[] File { get; set; }

        [JsonIgnore]
        public string IgnoreMe { get; set; }

        [JsonProperty("new_name")]
        public string RenameMe { get; set; }
    }

    public class GenericItem<T>
    {
        public T Stuff { get; set; }
    }

    public class BaseItem
    {
        public ImportMe Imported { get; set; }
    }
}`
        }
    ]
}

export function getKeywords() {
    return [
        {
            label: 'class',
            insertText: 'class'
        },
        {
            label: 'interface',
            insertText: 'interface'
        },
        {
            label: 'enum',
            insertText: 'enum'
        },
        {
            label: 'namespace',
            insertText: 'namespace'
        },
        {
            label: 'using',
            insertText: 'using'
        },
        {
            label: 'public',
            insertText: 'public'
        },
        {
            label: 'private',
            insertText: 'private'
        },
        {
            label: 'protected',
            insertText: 'protected'
        },
        {
            label: 'static',
            insertText: 'static'
        },
        {
            label: 'const',
            insertText: 'const'
        },
        {
            label: 'get',
            insertText: 'get'
        },
        {
            label: 'set',
            insertText: 'set'
        },
        {
            label: 'char',
            insertText: 'char'
        },
        {
            label: 'ulong',
            insertText: 'ulong'
        },
        {
            label: 'byte',
            insertText: 'byte'
        },
        {
            label: 'decimal',
            insertText: 'decimal'
        },
        {
            label: 'double',
            insertText: 'double'
        },
        {
            label: 'int',
            insertText: 'int'
        },
        {
            label: 'sbyte',
            insertText: 'sbyte'
        },
        {
            label: 'float',
            insertText: 'float'
        },
        {
            label: 'long',
            insertText: 'long'
        },
        {
            label: 'object',
            insertText: 'object'
        },
        {
            label: 'bool',
            insertText: 'bool'
        },
        {
            label: 'short',
            insertText: 'short'
        },
        {
            label: 'string',
            insertText: 'string'
        },
        {
            label: 'uint',
            insertText: 'uint'
        },
        {
            label: 'ushort',
            insertText: 'ushort'
        }
    ]
}

export function getAttributes() {
    return [
        {
            label: 'JsonProperty',
            insertText: 'JsonProperty'
        },
        {
            label: 'JsonPropertyName',
            insertText: 'JsonPropertyName'
        },
        {
            label: 'JsonIgnore',
            insertText: 'JsonIgnore'
        }
    ]
}

export function getStructs() {
    return [
        {
            label: 'DateTime',
            insertText: 'DateTime'
        },
        {
            label: 'DateTimeOffset',
            insertText: 'DateTimeOffset'
        },
        {
            label: 'TimeSpan',
            insertText: 'TimeSpan'
        },
        {
            label: 'Guid',
            insertText: 'Guid'
        },
        {
            label: 'Boolean',
            insertText: 'Boolean'
        },
        {
            label: 'Char',
            insertText: 'Char'
        },
        {
            label: 'Byte',
            insertText: 'Byte'
        },
        {
            label: 'SByte',
            insertText: 'SByte'
        },
        {
            label: 'Decimal',
            insertText: 'Decimal'
        },
        {
            label: 'Double',
            insertText: 'Double'
        },
        {
            label: 'Single',
            insertText: 'Single'
        },
        {
            label: 'Int32',
            insertText: 'Int32'
        },
        {
            label: 'UInt32',
            insertText: 'UInt32'
        },
        {
            label: 'Int64',
            insertText: 'Int64'
        },
        {
            label: 'UInt64',
            insertText: 'UInt64'
        },
        {
            label: 'Int16',
            insertText: 'Int16'
        },
        {
            label: 'UInt16',
            insertText: 'UInt16'
        }
    ]
}

export function getInterfaces() {
    return [
        {
            label: 'IEnumerable',
            insertText: 'IEnumerable'
        },
        {
            label: 'IDictionary',
            insertText: 'IDictionary'
        }
    ]
}

export function getClasses() {
    return [
        {
            label: 'List',
            insertText: 'List'
        },
        {
            label: 'Dictionary',
            insertText: 'Dictionary'
        },
        {
            label: 'Tuple',
            insertText: 'Tuple'
        },
        {
            label: 'String',
            insertText: 'String'
        },
    ]
}

export function getNamespaces() {
    return [
        {
            label: 'System',
            insertText: 'System'
        },
        {
            label: 'Collections',
            insertText: 'Collections'
        },
        {
            label: 'Generic',
            insertText: 'Generic'
        },
        {
            label: 'Newtonsoft',
            insertText: 'Newtonsoft'
        },
        {
            label: 'Text',
            insertText: 'Text'
        },
        {
            label: 'Json',
            insertText: 'Json'
        },
        {
            label: 'Serialization',
            insertText: 'Serialization'
        }
    ]
}

export function getNames() {
    return [
        {
            label: 'name',
            insertText: 'name'
        },
        {
            label: 'propertyName',
            insertText: 'propertyName'
        },
        {
            label: 'PropertyName',
            insertText: 'PropertyName'
        }
    ]
}