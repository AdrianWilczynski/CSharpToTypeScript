namespace CSharpToTypeScript.CLITool.Conventions
{
    public static class AngularConventions
    {
        public static void Override(CLI cli)
        {
            cli.TabSize = 2;
            cli.UseKebabCase = true;
            cli.AppendModelSuffix = true;
        }
    }
}