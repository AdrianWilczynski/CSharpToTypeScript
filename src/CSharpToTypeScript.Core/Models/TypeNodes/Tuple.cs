using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class Tuple : TypeNode
    {
        public Tuple(IEnumerable<Element> elements)
        {
            Elements = elements;
        }

        public IEnumerable<Element> Elements { get; }

        public class Element : IWritableNode
        {
            public Element(string name, TypeNode type)
            {
                Name = name;
                Type = type;
            }

            public string Name { get; }
            public TypeNode Type { get; }

            public string WriteTypeScript(CodeConversionOptions options)
                => Name.ToCamelCase() + "?".If(Type is Nullable) + ": " + (Type is Nullable nullable ? nullable.Of.WriteTypeScript(options) : Type.WriteTypeScript(options)) + ";";
        }

        public override string WriteTypeScript(CodeConversionOptions options)
            => "{ " + Elements.Select(e => e.WriteTypeScript(options)).ToSpaceSepratedList() + " }";
    }
}