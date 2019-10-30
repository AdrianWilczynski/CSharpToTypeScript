using System.Collections.Generic;
using CSharpToTypeScript.Core.Models.TypeNodes;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers
{
    internal class DateConverter : BasicTypeConverterBase<Date>
    {
        protected override IEnumerable<string> ConvertibleFrom { get; } = new List<string>
        {
            "DateTime", "DateTimeOffset"
        };
    }
}