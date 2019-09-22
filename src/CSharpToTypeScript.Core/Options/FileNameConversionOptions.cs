namespace CSharpToTypeScript.Core.Options
{
    public class FileNameConversionOptions
    {
        public FileNameConversionOptions(bool useKebabCase, bool appendModelSuffix)
        {
            UseKebabCase = useKebabCase;
            AppendModelSuffix = appendModelSuffix;
        }

        public bool UseKebabCase { get; set; }
        public bool AppendModelSuffix { get; set; }
    }
}