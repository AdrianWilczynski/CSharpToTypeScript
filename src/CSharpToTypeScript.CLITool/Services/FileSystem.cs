using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.CLITool.Services
{
    public class FileSystem : IFileSystem
    {
        public void EnsureDirectoryExists(string path)
        {
            if (path.EndsWithFileExtension())
            {
                Directory.CreateDirectory(new FileInfo(path).DirectoryName);
            }
            else
            {
                Directory.CreateDirectory(path);
            }
        }

        public void ClearOutputIfPossible(string input, string output)
        {
            if (Path.GetFullPath(input).StartsWith(Path.GetFullPath(output)) || !Directory.Exists(output))
            {
                return;
            }

            Directory.Delete(output, true);
        }

        public IEnumerable<string> GetCSharpFiles(string path)
            => Directory.GetFiles(path)
                .Where(f => f.EndsWith(".cs"));

        public string ReadAllText(string path)
            => File.ReadAllText(path);

        public void WriteAllText(string path, string content)
            => File.WriteAllText(path, content);

        public bool IsExistingFile(string path)
            => path.EndsWithFileExtension() && File.Exists(path);

        public bool IsExistingDirectory(string path)
            => !path.EndsWithFileExtension() && Directory.Exists(path);
    }
}