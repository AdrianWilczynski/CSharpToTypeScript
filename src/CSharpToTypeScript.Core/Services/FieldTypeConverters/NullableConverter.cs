using CSharpToTypeScript.Core.Models.FieldTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.FieldTypeConverters
{
    public class NullableConverter : FieldTypeConverter
    {
        private readonly FieldTypeConverter _converter;

        public NullableConverter(FieldTypeConverter converter)
        {
            _converter = converter;
        }

        public override IFieldType Convert(TypeSyntax type)
        {
            if (type is NullableTypeSyntax nullable)
            {
                return new Nullable(of: _converter.Convert(nullable.ElementType));
            }

            return base.Convert(type);
        }
    }
}