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
            => Name.ToCamelCase() + "?".If(Type is Nullable) + ": " + (Type is Nullable nullable ? nullable.Of.WriteTypeScript(options) : Type.WriteTypeScript(options)) + ";";
    }
}