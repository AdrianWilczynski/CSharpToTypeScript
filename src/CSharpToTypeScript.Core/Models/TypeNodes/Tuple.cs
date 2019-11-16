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

        public class Element : IWritableNode, IDependentNode
        {
            public Element(string name, TypeNode type)
            {
                Name = name;
                Type = type;
            }

            public string Name { get; }
            public TypeNode Type { get; }

            public IEnumerable<string> Requires => Type.Requires;

            public string WriteTypeScript(CodeConversionOptions options, Context context)
                => // name
                Name.TransformIf(options.ToCamelCase, StringUtilities.ToCamelCase)
                // separator
                + "?".If(Type.IsOptional(options, out _)) + ": "
                // type
                + (Type.IsOptional(options, out var of) ? of.WriteTypeScript(options, context) : Type.WriteTypeScript(options, context)) + ";";
        }

        public override IEnumerable<string> Requires => Elements.SelectMany(e => e.Requires).Distinct();

        public override string WriteTypeScript(CodeConversionOptions options, Context context)
            => "{ " + Elements.WriteTypeScript(options, context).ToSpaceSepratedList() + " }";
    }
}