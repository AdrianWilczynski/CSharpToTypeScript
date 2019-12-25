using CSharpToTypeScript.CLITool.Commands;
using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.CLITool.Conventions
{
    public static class AngularConventions
    {
        public static void Override(CommandBase command)
        {
            command.TabSize = 2;
            command.UseKebabCase = true;
            command.AppendModelSuffix = true;
            command.QuotationMark = QuotationMark.Single;
        }
    }
}