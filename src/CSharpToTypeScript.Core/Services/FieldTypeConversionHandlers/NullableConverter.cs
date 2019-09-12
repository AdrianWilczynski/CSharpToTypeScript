using CSharpToTypeScript.Core.Models.FieldTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.FieldTypeConversionHandlers
{
    public class NullableConverter : FieldTypeConversionHandler
    {
        private readonly FieldTypeConversionHandler _converter;

        public NullableConverter(FieldTypeConversionHandler converter)
        {
            _converter = converter;
        }

        public override FieldTypeNode Handle(TypeSyntax type)
        {
            if (type is NullableTypeSyntax nullable)
            {
                return new Nullable(of: _converter.Handle(nullable.ElementType));
            }

            return base.Handle(type);
        }
    }
}