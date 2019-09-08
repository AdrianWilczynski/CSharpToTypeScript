using System.Linq;
using CSharpToTypeScript.Core.Models.FieldTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.FieldTypeHandlers
{
    public class GenericConverter : FieldTypeConversionHandler
    {
        private readonly FieldTypeConversionHandler _converter;

        public GenericConverter(FieldTypeConversionHandler converter)
        {
            _converter = converter;
        }

        public override IFieldType Handle(TypeSyntax type)
        {
            if (type is GenericNameSyntax generic)
            {
                return new Generic(
                    name: generic.Identifier.Text,
                    arguments: generic.TypeArgumentList.Arguments.Select(_converter.Handle));
            }

            return base.Handle(type);
        }
    }
}