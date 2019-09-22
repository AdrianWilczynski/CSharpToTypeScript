using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class Tuple : ITypeNode
    {
        public Tuple(IEnumerable<Element> elements)
        {
            Elements = elements;
        }

        public IEnumerable<Element> Elements { get; }

        public class Element
        {
            public Element(string name, ITypeNode type)
            {
                Name = name;
                Type = type;
            }

            public string Name { get; }
            public ITypeNode Type { get; }

            public string WriteTypeScript()
                => Name.ToCamelCase() + "?".If(Type is Nullable) + ": " + (Type is Nullable nullable ? nullable.Of.WriteTypeScript() : Type.WriteTypeScript()) + ";";
        }

        public string WriteTypeScript()
            => "{ " + Elements.Select(e => e.WriteTypeScript()).ToSpaceSepratedList() + " }";
    }
}