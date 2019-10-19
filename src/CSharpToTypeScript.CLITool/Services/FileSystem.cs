using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.CLITool.Services
{
    public class FileSystem : IFileSystem
    {
        public void EnsureDirectoryExists(string path)
            => Directory.CreateDirectory(path.EndsWithFileExtension() ? new FileInfo(path).DirectoryName : path);

        public void ClearOutputIfPossible(string input, string output)
        {
            if (!IsSameOrParrentDirectory(input, output) && IsExistingDirectory(output))
            {
                Directory.Delete(output, true);
            }
        }

        public bool IsSameOrParrentDirectory(string child, string parrent)
            => Path.GetFullPath(child).StartsWith(Path.GetFullPath(parrent));

        public IEnumerable<string> GetCSharpFiles(string path)
            => Directory.GetFiles(path)
                .Where(f => f.EndsWith(".cs"));

        public string ReadAllText(string path)
            => File.ReadAllText(path);

        public void WriteAllText(string path, string content)
        {
            EnsureDirectoryExists(path);
            File.WriteAllText(path, content);
        }

        public bool IsExistingFile(string path)
            => File.Exists(path);

        public bool IsExistingDirectory(string path)
            => Directory.Exists(path);
    }
}