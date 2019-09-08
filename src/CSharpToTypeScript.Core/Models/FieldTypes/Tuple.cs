using System.Collections.Generic;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.FieldTypes
{
    public class Tuple : IFieldType
    {
        public Tuple(IEnumerable<Element> elements)
        {
            Elements = elements;
        }

        public IEnumerable<Element> Elements { get; }

        public class Element
        {
            public Element(string name, IFieldType type)
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

        public override string ToString() => $"{{ {string.Join(" ", Elements)} }}";
    }
}