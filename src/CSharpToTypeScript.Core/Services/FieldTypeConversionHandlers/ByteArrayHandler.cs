using System.Linq;
using CSharpToTypeScript.Core.Models.FieldTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.FieldTypeConversionHandlers
{
    public class ByteArrayHandler : FieldTypeConversionHandler
    {
        public override FieldType Handle(TypeSyntax type)
        {
            if (type is ArrayTypeSyntax array && array.RankSpecifiers.Last().Rank == 1
                && ((array.ElementType is PredefinedTypeSyntax predefinedOf && predefinedOf.Keyword.Text == "byte")
                    || (array.ElementType is IdentifierNameSyntax identifiedOf && identifiedOf.Identifier.Text == "Byte")))
            {
                if (array.RankSpecifiers.Count > 1)
                {
                    return new Array(
                        of: new String(),
                        rank: array.RankSpecifiers
                            .Take(array.RankSpecifiers.Count - 1)
                            .Aggregate(0, (total, specifier) => total + specifier.Rank));
                }

                return new String();
            }

            return base.Handle(type);
        }
    }
}