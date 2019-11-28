using System;
using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models.TypeNodes;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers
{
    internal class DateConverter : BasicTypeConverterBase<Date>
    {
        protected override IEnumerable<string> ConvertibleFromPredefined => Enumerable.Empty<string>();

        protected override IEnumerable<string> ConvertibleFromIdentified => new[]
        {
            nameof(DateTime), nameof(DateTimeOffset)
        };
    }
}