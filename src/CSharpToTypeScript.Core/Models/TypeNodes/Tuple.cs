using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    public class Tuple : TypeNode
    {
        public Tuple(IEnumerable<Element> elements)
        {
            Elements = elements;
        }

        public IEnumerable<Element> Elements { get; }

        public class Element
        {
            public Element(string name, TypeNode type)
            {
                Name = name;
                Type = type;
            }

            public string Name { get; }
            public TypeNode Type { get; }

            public string WriteTypeScript()
                => Name.ToCamelCase() + "?".If(Type.IsOptional) + ": " + Type.WriteTypeScript() + ";";
        }

        public override string WriteTypeScript()
            => "{ " + Elements.Select(e => e.WriteTypeScript()).ToSpaceSepratedList() + " }";
    }
}