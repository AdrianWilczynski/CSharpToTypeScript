using CSharpToTypeScript.Core.Utilities;
using CSharpToTypeScript.Core.Models.TypeNodes;
using CSharpToTypeScript.Core.Options;
using System.Collections.Generic;

namespace CSharpToTypeScript.Core.Models
{
    internal class FieldNode : IWritableNode, IDependentNode
    {
        public FieldNode(string name, TypeNode type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }
        public TypeNode Type { get; }

        public IEnumerable<string> Requires => Type.Requires;

        public string WriteTypeScript(CodeConversionOptions options)
            => // name
            Name.TransformIf(options.ToCamelCase, StringUtilities.ToCamelCase)
            // separator
            + "?".If(Type.IsOptional(options, out _)) + ": "
            // type
            + (Type.IsOptional(options, out var of) ? of.WriteTypeScript(options) : Type.WriteTypeScript(options)) + ";";
    }
}