namespace CSharpToTypeScript.Core.Options
{
    public class ModuleNameConversionOptions
    {
        public ModuleNameConversionOptions(bool useKebabCase, bool appendModelSuffix, bool removeInterfacePrefix = true)
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