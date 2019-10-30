namespace CSharpToTypeScript.Core.Options
{
    public class CodeConversionOptions
    {
        public CodeConversionOptions(bool export, bool useTabs, int? tabSize = null,
            DateOutputType convertDatesTo = DateOutputType.String)
        {
            Export = export;
            UseTabs = useTabs;
            TabSize = tabSize;
            ConvertDatesTo = convertDatesTo;
        }

        public bool Export { get; set; }
        public bool UseTabs { get; set; }
        public int? TabSize { get; set; }
        public DateOutputType ConvertDatesTo { get; set; }
    }
}