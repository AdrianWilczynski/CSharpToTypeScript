using CSharpToTypeScript.Core.Utilities;
using CSharpToTypeScript.Core.Models.TypeNodes;
using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Models
{
    internal class FieldNode : IWritableNode
    {
        public FieldNode(string name, TypeNode type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }
        public TypeNode Type { get; }

        public string WriteTypeScript(CodeConversionOptions options)
            => Name.ToCamelCase() + "?".If(Type.IsOptional(options, out _)) + ": " + (Type.IsOptional(options, out var of) ? of.WriteTypeScript(options) : Type.WriteTypeScript(options)) + ";";
    }
}