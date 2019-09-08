using System.Linq;
using CSharpToTypeScript.Core.Models.FieldTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.FieldTypeConverters
{
    public class TupleConverter : FieldTypeConverter
    {
        private readonly FieldTypeConverter _converter;

        public TupleConverter(FieldTypeConverter converter)
        {
            _converter = converter;
        }

        public override IFieldType Convert(TypeSyntax type)
        {
            if (type is TupleTypeSyntax tuple)
            {
                return new Tuple(
                    elements: tuple.Elements.Select((element, index) => new Tuple.Element(
                        name: !string.IsNullOrEmpty(element.Identifier.Text) ? element.Identifier.Text : $"Item{index + 1}",
                        type: _converter.Convert(element.Type))));
            }
            else if (type is GenericNameSyntax generic && generic.Identifier.Text == "Tuple")
            {
                return new Tuple(
                    elements: generic.TypeArgumentList.Arguments.Select((argument, index) => new Tuple.Element(
                        name: $"Item{index + 1}",
                        type: _converter.Convert(argument))));
            }

            return base.Convert(type);
        }
    }
}