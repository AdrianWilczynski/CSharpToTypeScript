using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models.TypeNodes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers
{
    public class DictionaryConverter : TypeConversionHandler
    {
        private IEnumerable<string> ConvertibleFrom { get; } = new List<string>
        {
            "Dictionary", "IDictionary"
        };

        private readonly TypeConversionHandler _converter;

        public DictionaryConverter(TypeConversionHandler converter)
        {
            _converter = converter;
        }

        public override ITypeNode Handle(TypeSyntax type)
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