using System.Linq;
using CSharpToTypeScript.Core.Models.TypeNodes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers
{
    internal class NullableConverter : TypeConversionHandler
    {
        private readonly TypeConversionHandler _converter;

        public NullableConverter(TypeConversionHandler converter)
        {
            _converter = converter;
        }

        public override TypeNode Handle(TypeSyntax type)
        {
            if (type is NullableTypeSyntax nullable)
            {
                return new Nullable(of: _converter.Handle(nullable.ElementType));
            }
            else if (type is GenericNameSyntax generic && generic.Identifier.Text == nameof(System.Nullable)
                && generic.TypeArgumentList.Arguments.Count == 1)
            {
                return new Nullable(of: _converter.Handle(generic.TypeArgumentList.Arguments.Single()));
            }

            return base.Handle(type);
        }
    }
}