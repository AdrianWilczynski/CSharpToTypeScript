using CSharpToTypeScript.Core.Utilities;
using CSharpToTypeScript.Core.Models.FieldTypes;

namespace CSharpToTypeScript.Core.Models
{
    public class FieldNode
    {
        public FieldNode(string name, IFieldType type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }
        public IFieldType Type { get; }

        public bool IsOptional => Type is Nullable;

        public override string ToString()
            => $"{Name.ToCamelCase()}{(IsOptional ? "?" : string.Empty)}: {(IsOptional ? ((Nullable)Type).Of : Type)};";
    }
}