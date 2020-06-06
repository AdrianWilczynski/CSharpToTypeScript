using System.Collections.Generic;
using CSharpToTypeScript.Core.Constants;
using CSharpToTypeScript.Core.Models.TypeNodes;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers
{
    internal class StringConverter : BasicTypeConverterBase<String>
    {
        protected override IEnumerable<string> ConvertibleFromPredefined => new[]
        {
            PredefinedTypes.String, PredefinedTypes.Char
        };

        protected override IEnumerable<string> ConvertibleFromIdentified => new[]
        {
            nameof(System.String), nameof(System.Char), nameof(System.TimeSpan), nameof(System.Guid), nameof(System.Uri)
        };
    }
}