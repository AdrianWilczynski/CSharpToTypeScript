using System.Collections.Generic;
using CSharpToTypeScript.Core.Constants;
using CSharpToTypeScript.Core.Models.TypeNodes;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers
{
    internal class BooleanConverter : BasicTypeConverterBase<Boolean>
    {
        protected override IEnumerable<string> ConvertibleFromPredefined => new[]
        {
            PredefinedTypes.Bool
        };

        protected override IEnumerable<string> ConvertibleFromIdentified => new[]
        {
            nameof(System.Boolean)
        };
    }
}