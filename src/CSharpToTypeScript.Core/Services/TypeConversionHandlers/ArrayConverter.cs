using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models.TypeNodes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers
{
    public class ArrayConverter : TypeConversionHandler
    {
        private readonly TypeConversionHandler _converter;

        public ArrayConverter(TypeConversionHandler converter)
        {
            _converter = converter;
        }

        private IEnumerable<string> ConvertibleFrom { get; } = new List<string>
        {
            "List", "IList", "Collection", "ICollection", "Enumerable", "IEnumerable"
        };

        public override TypeNode Handle(TypeSyntax type)
        {
            if (type is ArrayTypeSyntax array)
            {
                return new Array(
                    of: _converter.Handle(array.ElementType),
                    rank: array.RankSpecifiers.Aggregate(0, (total, specifier) => total + specifier.Rank));
            }
            else if (type is IdentifierNameSyntax identified && ConvertibleFrom.Contains(identified.Identifier.Text))
            {
                return new Array(
                    of: new Any(),
                    rank: 1);
            }
            else if (type is GenericNameSyntax generic && ConvertibleFrom.Contains(generic.Identifier.Text)
                && generic.TypeArgumentList.Arguments.Count == 1)
            {
                return new Array(
                    of: _converter.Handle(generic.TypeArgumentList.Arguments.Single()),
                    rank: 1);
            }

            return base.Handle(type);
        }
    }
}