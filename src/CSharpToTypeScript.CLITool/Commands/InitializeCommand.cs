using CSharpToTypeScript.CLITool.Utilities;
using CSharpToTypeScript.CLITool.Validation;
using McMaster.Extensions.CommandLineUtils;

namespace CSharpToTypeScript.CLITool.Commands
{
    [ConfigurationFileDoesNotExist]
    [Command(Name = "init", Description = "Initialize - create configuration file in current directory")]
    public class InitializeCommand : CommandBase
    {
        public void OnExecute() => ConfigurationFile.Create(new Configuration(this));
    }
}