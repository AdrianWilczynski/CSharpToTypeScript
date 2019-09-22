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

        public override ITypeNode Handle(TypeSyntax type)
        {
            if (type is NullableTypeSyntax nullable)
            {
                return new Nullable(of: _converter.Handle(nullable.ElementType));
            }

            return base.Handle(type);
        }
    }
}