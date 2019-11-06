namespace CSharpToTypeScript.Core.Options
{
    public class FileNameConversionOptions
    {
        public FileNameConversionOptions(bool useKebabCase, bool appendModelSuffix, bool removeInterfacePrefix = true)
        {
            UseKebabCase = useKebabCase;
            AppendModelSuffix = appendModelSuffix;
            RemoveInterfacePrefix = removeInterfacePrefix;
        }

        public bool UseKebabCase { get; set; }
        public bool AppendModelSuffix { get; set; }
        public bool RemoveInterfacePrefix { get; set; }
    }
}