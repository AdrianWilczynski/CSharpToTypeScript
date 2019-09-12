using System.Linq;
using CSharpToTypeScript.Core.Models.FieldTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.FieldTypeConversionHandlers
{
    public class GenericConverter : FieldTypeConversionHandler
    {
        private readonly FieldTypeConversionHandler _converter;

        public GenericConverter(FieldTypeConversionHandler converter)
        {
            _converter = converter;
        }

        public override FieldType Handle(TypeSyntax type)
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