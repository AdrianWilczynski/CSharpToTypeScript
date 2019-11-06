namespace CSharpToTypeScript.Core.Options
{
    public class CodeConversionOptions
    {
        public CodeConversionOptions(bool export, bool useTabs, int? tabSize = null,
            DateOutputType convertDatesTo = default, NullableOutputType convertNullablesTo = default,
            bool toCamelCase = true, bool removeInterfacePrefix = true)
        {
            Export = export;
            UseTabs = useTabs;
            TabSize = tabSize;
            ConvertDatesTo = convertDatesTo;
            ConvertNullablesTo = convertNullablesTo;
            ToCamelCase = toCamelCase;
            RemoveInterfacePrefix = removeInterfacePrefix;
        }

        public bool Export { get; set; }
        public bool UseTabs { get; set; }
        public int? TabSize { get; set; }
        public DateOutputType ConvertDatesTo { get; set; }
        public NullableOutputType ConvertNullablesTo { get; set; }
        public bool ToCamelCase { get; set; }
        public bool RemoveInterfacePrefix { get; set; }
    }
}