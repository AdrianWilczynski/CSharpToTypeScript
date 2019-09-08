using System.Collections.Generic;
using CSharpToTypeScript.Core.Models.FieldTypes;

namespace CSharpToTypeScript.Core.Services.FieldTypeHandlers
{
    public class StringConverter : BasicTypeConverterBase<String>
    {
        protected override IEnumerable<string> ConvertibleFrom { get; } = new List<string>
        {
            "String", "string", "Char", "char", "DateTime", "DateTimeOffset", "TimeSpan", "Guid"
        };
    }
}