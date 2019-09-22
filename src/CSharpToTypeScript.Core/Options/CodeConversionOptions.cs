namespace CSharpToTypeScript.Core.Options
{
    public class CodeConversionOptions
    {
        public CodeConversionOptions(bool export, bool useTabs, int? tabSize = null)
        {
            Export = export;
            UseTabs = useTabs;
            TabSize = tabSize;
        }

        public bool Export { get; set; }
        public bool UseTabs { get; set; }
        public int? TabSize { get; set; }
    }
}