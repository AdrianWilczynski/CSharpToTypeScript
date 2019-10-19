namespace CSharpToTypeScript.CLITool.Conventions
{
    public static class AngularConventions
    {
        public static void Override(CLI cli)
        {
            cli.UseTabs = false;
            cli.TabSize = 2;
            cli.Export = true;
            cli.UseKebabCase = true;
            cli.AppendModelSuffix = true;
        }
    }
}