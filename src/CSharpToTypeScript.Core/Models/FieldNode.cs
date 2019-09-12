using CSharpToTypeScript.Core.Utilities;
using CSharpToTypeScript.Core.Models.FieldTypes;

namespace CSharpToTypeScript.Core.Models
{
    public class FieldNode
    {
        public FieldNode(string name, FieldTypeNode type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }
        public FieldTypeNode Type { get; }

        public string WriteTypeScript()
            => Name.ToCamelCase() + "?".If(Type.IsOptional) + ": " + Type.WriteTypeScript() + ";";
    }
}