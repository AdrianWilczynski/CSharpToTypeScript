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