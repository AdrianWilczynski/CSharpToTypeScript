using CSharpToTypeScript.CLITool.Arguments;
using CSharpToTypeScript.CLITool.Utilities;
using McMaster.Extensions.CommandLineUtils;

namespace CSharpToTypeScript.CLITool.Commands
{
    [Command(Name = "init", Description = "Initialize - create configuration file in current directory")]
    public class InitializeCommand : CommandBase
    {
        public void OnExecute() => ConfigurationFile.Create(new ConfigurationFileArguments(this));
    }
}