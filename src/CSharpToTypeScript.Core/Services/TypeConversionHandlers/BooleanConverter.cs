using System.Collections.Generic;
using CSharpToTypeScript.Core.Models.TypeNodes;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers
{
    internal class BooleanConverter : BasicTypeConverterBase<Boolean>
    {
        protected override IEnumerable<string> ConvertibleFrom { get; } = new List<string>
        {
            "Boolean", "bool"
        };
    }
}