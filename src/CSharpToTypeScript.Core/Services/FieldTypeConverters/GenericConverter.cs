using System.Linq;
using CSharpToTypeScript.Core.Models.FieldTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.FieldTypeConverters
{
    public class GenericConverter : FieldTypeConverter
    {
        private readonly FieldTypeConverter _converter;

        public GenericConverter(FieldTypeConverter converter)
        {
            _converter = converter;
        }

        public override IFieldType Convert(TypeSyntax type)
        {
            if (type is GenericNameSyntax generic)
            {
                return new Generic(
                    name: generic.Identifier.Text,
                    arguments: generic.TypeArgumentList.Arguments.Select(_converter.Convert));
            }

            return base.Convert(type);
        }
    }
}