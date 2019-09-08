using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models.FieldTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.FieldTypeConverters
{
    public class DictionaryConverter : FieldTypeConverter
    {
        private IEnumerable<string> ConvertibleFrom { get; } = new List<string>
        {
            "Dictionary", "IDictionary"
        };

        private readonly FieldTypeConverter _converter;

        public DictionaryConverter(FieldTypeConverter converter)
        {
            _converter = converter;
        }

        public override IFieldType Convert(TypeSyntax type)
        {
            if (type is GenericNameSyntax generic && ConvertibleFrom.Contains(generic.Identifier.Text)
                && generic.TypeArgumentList.Arguments.Count == 2)
            {
                return new Dictionary(
                    key: _converter.Convert(generic.TypeArgumentList.Arguments[0]),
                    value: _converter.Convert(generic.TypeArgumentList.Arguments[1]));
            }

            return base.Convert(type);
        }
    }
}