using CSharpToTypeScript.Core.Utilities;
using CSharpToTypeScript.Core.Models.TypeNodes;

namespace CSharpToTypeScript.Core.Models
{
    public class FieldNode
    {
        public FieldNode(string name, ITypeNode type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }
        public ITypeNode Type { get; }

        public string WriteTypeScript()
            => Name.ToCamelCase() + "?".If(Type is Nullable) + ": " + (Type is Nullable nullable ? nullable.Of.WriteTypeScript() : Type.WriteTypeScript()) + ";";
    }
}