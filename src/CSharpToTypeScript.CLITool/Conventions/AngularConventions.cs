namespace CSharpToTypeScript.CLITool.Conventions
{
    public static class AngularConventions
    {
        public static bool UseTabs { get; } = false;
        public static int TabSize { get; } = 2;
        public static bool Export { get; } = true;
        public static bool UseKebabCase { get; } = true;
        public static bool AppendModelSuffix { get; } = true;

        public static void Override(CLI cli)
        {
            cli.UseTabs = UseTabs;
            cli.TabSize = TabSize;
            cli.Export = Export;
            cli.UseKebabCase = UseKebabCase;
            cli.AppendModelSuffix = true;
        }
    }
}