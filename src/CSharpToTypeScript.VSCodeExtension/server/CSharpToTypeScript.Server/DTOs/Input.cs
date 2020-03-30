using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Server.DTOs
{
    public class Input
    {
        public string Code { get; set; }
        public string FileName { get; set; }
        public bool UseTabs { get; set; }
        public int? TabSize { get; set; }
        public bool Export { get; set; }
        public DateOutputType ConvertDatesTo { get; set; }
        public NullableOutputType ConvertNullablesTo { get; set; }
        public bool ToCamelCase { get; set; }
        public bool RemoveInterfacePrefix { get; set; }
        public bool GenerateImports { get; set; }
        public bool UseKebabCase { get; set; }
        public bool AppendModelSuffix { get; set; }
        public QuotationMark QuotationMark { get; set; }
        public bool AppendNewLine { get; set; }

        public CodeConversionOptions MapToCodeConversionOptions()
            => new CodeConversionOptions(Export, UseTabs, TabSize,
                ConvertDatesTo, ConvertNullablesTo, ToCamelCase, RemoveInterfacePrefix,
                GenerateImports ? ImportGenerationMode.Simple : ImportGenerationMode.None,
                UseKebabCase, AppendModelSuffix, QuotationMark, AppendNewLine);
    }
}