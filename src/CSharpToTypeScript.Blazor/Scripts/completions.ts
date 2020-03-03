import * as monaco from 'monaco-editor';

export function getSnippets(range: monaco.IRange): monaco.languages.CompletionItem[] {
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
        public System.String MoreText { get; private set; }
        public bool IsWhatever { get; set; }
        public IEnumerable<string> Collection { get; set; }
        public double[] Array { get; set; }
        public string[,] Array2D { get; set; }
        public string[][] ArrayOfArrays { get; set; }
        public (int, string) Tuple { get; set; }
        public int? Nullable { get; set; }
        public List<double?> MaybeNumbers { get; set; }
        public GenericItem<string> Generic { get; set; }
        public Dictionary<string, string> Dictionary { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Hello => "Hello World!";
        public byte[] File { get; set; }
        public SomeEnum SomeEnum { get; set; }
        public ISomeInterface SomeInterface { get; set; }

        [JsonIgnore]
        public string IgnoreMe { get; set; }

        [JsonProperty("new_name")]
        public string RenameMe { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "someOtherName")]
        public string RenameMeToo { get; set; }

        [JsonProperty(propertyName: "(╯°□°）╯︵ ┻━┻)")]
        public string InvalidIdentifier { get; set; }

        private int _backingField;

        public int BackedProperty
        {
            get { return _backingField; }
            set { _backingField = value; }
        }

        public int fieldNotProperty;
        public int first, second;
    }

    public class GenericItem<T>
    {
        public T Stuff { get; set; }
    }

    public class BaseItem
    {
        public ImportMe Imported { get; set; }
    }

    public interface ISomeInterface
    {
        int SomeProperty { get; set; }
    }

    public enum SomeEnum
    {
        Value,
        OtherValue
    }
}`
        }
    ].map(s => {
        return {
            ...s,
            kind: monaco.languages.CompletionItemKind.Snippet,
            insertTextRules: monaco.languages.CompletionItemInsertTextRule.InsertAsSnippet,
            range: range
        };
    });
}

export function getKeywords(range: monaco.IRange) {
    return [
        'class',
        'interface',
        'enum',
        'namespace',
        'using',
        'public',
        'private',
        'protected',
        'static',
        'const',
        'get',
        'set',
        'char',
        'ulong',
        'byte',
        'decimal',
        'double',
        'int',
        'sbyte',
        'float',
        'long',
        'object',
        'bool',
        'short',
        'string',
        'uint',
        'ushort'
    ].map(v => toCompletion(v, monaco.languages.CompletionItemKind.Keyword, range));
}

export function getAttributes(range: monaco.IRange) {
    return [
        'JsonProperty',
        'JsonPropertyName',
        'JsonIgnore'
    ].map(v => toCompletion(v, monaco.languages.CompletionItemKind.Class, range));
}

export function getStructs(range: monaco.IRange) {
    return [
        'DateTime',
        'DateTimeOffset',
        'TimeSpan',
        'Guid',
        'Boolean',
        'Char',
        'Byte',
        'SByte',
        'Decimal',
        'Double',
        'Single',
        'Int32',
        'UInt32',
        'Int64',
        'UInt64',
        'Int16',
        'UInt16'
    ].map(v => toCompletion(v, monaco.languages.CompletionItemKind.Struct, range));
}

export function getInterfaces(range: monaco.IRange) {
    return [
        'IEnumerable',
        'IDictionary'
    ].map(v => toCompletion(v, monaco.languages.CompletionItemKind.Interface, range));
}

export function getClasses(range: monaco.IRange) {
    return [
        'List',
        'Dictionary',
        'Tuple',
        'String'
    ].map(v => toCompletion(v, monaco.languages.CompletionItemKind.Class, range));
}

export function getNamespaces(range: monaco.IRange) {
    return [
        'System',
        'Collections',
        'Generic',
        'Newtonsoft',
        'Text',
        'Json',
        'Serialization'
    ].map(v => toCompletion(v, monaco.languages.CompletionItemKind.Module, range));
}

export function getNames(range: monaco.IRange) {
    return [
        'name',
        'propertyName',
        'PropertyName'
    ].map(v => toCompletion(v, monaco.languages.CompletionItemKind.Variable, range));
}

function toCompletion(
    value: string,
    kind: monaco.languages.CompletionItemKind,
    range: monaco.IRange): monaco.languages.CompletionItem {
    return {
        label: value,
        insertText: value,
        kind: kind,
        range: range
    };
}