using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.FieldTypes
{
    public class Tuple : FieldTypeNode
    {
        public Tuple(IEnumerable<Element> elements)
        {
            Elements = elements;
        }

        public IEnumerable<Element> Elements { get; }

        public class Element
        {
            public Element(string name, FieldTypeNode type)
            {
                Name = name;
                Type = type;
            }

            public string Name { get; }
            public FieldTypeNode Type { get; }

            public string WriteTypeScript()
                => Name.ToCamelCase() + "?".If(Type.IsOptional) + ": " + Type.WriteTypeScript() + ";";
        }

        public override string WriteTypeScript()
            => "{ " + Elements.Select(e => e.WriteTypeScript()).ToSpaceSepratedList() + " }";
    }
}