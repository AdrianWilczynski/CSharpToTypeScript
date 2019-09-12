using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models.FieldTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.FieldTypeConversionHandlers
{
    public class DictionaryConverter : FieldTypeConversionHandler
    {
        private IEnumerable<string> ConvertibleFrom { get; } = new List<string>
        {
            "Dictionary", "IDictionary"
        };

        private readonly FieldTypeConversionHandler _converter;

        public DictionaryConverter(FieldTypeConversionHandler converter)
        {
            _converter = converter;
        }

        public override FieldTypeNode Handle(TypeSyntax type)
        {
            if (type is GenericNameSyntax generic && ConvertibleFrom.Contains(generic.Identifier.Text)
                && generic.TypeArgumentList.Arguments.Count == 2)
            {
                return new Dictionary(
                    key: _converter.Handle(generic.TypeArgumentList.Arguments[0]),
                    value: _converter.Handle(generic.TypeArgumentList.Arguments[1]));
            }

            return base.Handle(type);
        }
    }
}