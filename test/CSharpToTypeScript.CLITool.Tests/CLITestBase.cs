using System.IO;

namespace CSharpToTypeScript.CLITool.Tests
{
    public abstract class CLITestBase
    {
        protected void Prepare(string directory)
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }

            Directory.CreateDirectory(directory);
        }
    }
}