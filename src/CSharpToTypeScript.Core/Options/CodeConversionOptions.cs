namespace CSharpToTypeScript.Core.Options
{
    public class CodeConversionOptions : ModuleNameConversionOptions
    {
        public CodeConversionOptions(bool export, bool useTabs, int? tabSize = null,
            DateOutputType convertDatesTo = DateOutputType.String, NullableOutputType convertNullablesTo = NullableOutputType.Null,
            bool toCamelCase = true, bool removeInterfacePrefix = true, ImportGenerationMode importGenerationMode = ImportGenerationMode.None,
            bool useKebabCase = false, bool appendModelSuffix = false, QuotationMark quotationMark = QuotationMark.Double,
            bool appendNewLine = false, bool stringEnums = false, bool enumStringToCamelCase = false,
            OutputType outputType = OutputType.Interface)
        : base(useKebabCase, appendModelSuffix, removeInterfacePrefix)
        {
            Export = export;
            UseTabs = useTabs;
            TabSize = tabSize;
            ConvertDatesTo = convertDatesTo;
            ConvertNullablesTo = convertNullablesTo;
            ToCamelCase = toCamelCase;
            ImportGenerationMode = importGenerationMode;
            QuotationMark = quotationMark;
            AppendNewLine = appendNewLine;
            StringEnums = stringEnums;
            EnumStringToCamelCase = enumStringToCamelCase;
            OutputType = outputType;
        }

        public bool Export { get; set; }
        public bool UseTabs { get; set; }
        public int? TabSize { get; set; }
        public DateOutputType ConvertDatesTo { get; set; }
        public NullableOutputType ConvertNullablesTo { get; set; }
        public bool ToCamelCase { get; set; }
        public ImportGenerationMode ImportGenerationMode { get; set; }
        public QuotationMark QuotationMark { get; set; }
        public bool AppendNewLine { get; set; }
        public bool StringEnums { get; set; }
        public bool EnumStringToCamelCase { get; set; }
        public OutputType OutputType { get; set; }
    }
}