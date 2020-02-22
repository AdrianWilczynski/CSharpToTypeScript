using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Blazor.Models
{
    public class SettingsModel
    {
        public bool UseTabs { get; set; }
        public bool Export { get; set; } = true;
        public int? TabSize { get; set; } = 4;
        public DateOutputType ConvertDatesTo { get; set; }
        public NullableOutputType ConvertNullablesTo { get; set; }
        public bool ToCamelCase { get; set; } = true;
        public bool RemoveInterfacePrefix { get; set; } = true;
        public bool GenerateImports { get; set; } = true;
        public bool UseKebabCase { get; set; }
        public bool AppendModelSuffix { get; set; }
        public QuotationMark QuotationMark { get; set; }
    }
}