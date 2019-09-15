using CSharpToTypeScript.Core.Utilities;
using CSharpToTypeScript.Core.Models.TypeNodes;

namespace CSharpToTypeScript.Core.Models
{
    public class FieldNode
    {
        public FieldNode(string name, TypeNode type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }
        public TypeNode Type { get; }

        public string WriteTypeScript()
            => Name.ToCamelCase() + "?".If(Type.IsOptional) + ": " + Type.WriteTypeScript() + ";";
    }
}