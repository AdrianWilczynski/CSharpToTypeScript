using System.Linq;
using CSharpToTypeScript.Core.Models.TypeNodes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers
{
    public class TupleConverter : TypeConversionHandler
    {
        private readonly TypeConversionHandler _converter;

        public TupleConverter(TypeConversionHandler converter)
        {
            _converter = converter;
        }

        public override ITypeNode Handle(TypeSyntax type)
        {
            if (type is TupleTypeSyntax tuple)
            {
                return new Tuple(
                    elements: tuple.Elements.Select((element, index) => new Tuple.Element(
                        name: !string.IsNullOrEmpty(element.Identifier.Text) ? element.Identifier.Text : $"Item{index + 1}",
                        type: _converter.Handle(element.Type))));
            }
            else if (type is GenericNameSyntax generic && generic.Identifier.Text == "Tuple")
            {
                return new Tuple(
                    elements: generic.TypeArgumentList.Arguments.Select((argument, index) => new Tuple.Element(
                        name: $"Item{index + 1}",
                        type: _converter.Handle(argument))));
            }

            return base.Handle(type);
        }
    }
}