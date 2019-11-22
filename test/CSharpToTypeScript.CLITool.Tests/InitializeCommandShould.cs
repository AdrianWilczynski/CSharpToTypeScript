using System.IO;
using CSharpToTypeScript.CLITool.Commands;
using CSharpToTypeScript.CLITool.Utilities;
using Xunit;

namespace CSharpToTypeScript.CLITool.Tests
{
    [Collection(nameof(CLITool))]
    public class InitializeCommandShould : CLITestBase, IClassFixture<CLIFixture>
    {
        private readonly InitializeCommand _initializeCommand;

        public InitializeCommandShould(CLIFixture fixture)
        {
            _initializeCommand = fixture.InitializeCommand;
        }

        [Fact]
        public void CreateConfigurationFile()
        {
            Prepare(nameof(CreateConfigurationFile));

            Directory.SetCurrentDirectory(nameof(CreateConfigurationFile));

            try
            {
                _initializeCommand.PreserveCasing = true;
                _initializeCommand.OnExecute();

                Assert.True(File.Exists(ConfigurationFile.FileName));
                Assert.Contains("\"preserveCasing\": true,", File.ReadAllText(ConfigurationFile.FileName));
            }
            finally
            {
                Directory.SetCurrentDirectory("..");
            }
        }
    }
}