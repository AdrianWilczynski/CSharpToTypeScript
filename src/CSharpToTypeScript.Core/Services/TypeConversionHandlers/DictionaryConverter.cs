using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models.TypeNodes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers
{
    internal class DictionaryConverter : TypeConversionHandler
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

        public override TypeNode Handle(TypeSyntax type)
        {
            if (type is GenericNameSyntax generic && ConvertibleFrom.Contains(generic.Identifier.Text)
                && generic.TypeArgumentList.Arguments.Count == 2)
            {
                var key = _converter.Handle(generic.TypeArgumentList.Arguments[0]);

                if (key is String || key is Number)
                {
                    return new Dictionary(
                        key: key,
                        value: _converter.Handle(generic.TypeArgumentList.Arguments[1]));
                }
            }

            return base.Handle(type);
        }
    }
}