using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.CLITool.Services
{
    public class FileSystem : IFileSystem
    {
        public string ReadAllText(string path)
            => File.ReadAllText(path);

        public void WriteAllText(string path, string content)
        {
            EnsureDirectoryExists(path);
            File.WriteAllText(path, content);
        }

        public void ClearOutputIfPossible(string input, string output)
        {
            if (!IsSameOrParrentDirectory(input, output) && !IsRoot(output))
            {
                Directory.Delete(output, true);
            }
        }

        public IEnumerable<string> GetCSharpFiles(string path)
            => Directory.GetFiles(path)
                .Where(f => f.EndsWith(".cs"));

        public bool IsExistingFile(string path)
            => File.Exists(path);

        public bool IsExistingDirectory(string path)
            => Directory.Exists(path);

        public void EnsureDirectoryExists(string path)
        {
            var directoryPath = path.EndsWithFileExtension() ? ContainingDirectory(path) : path;

            if (!IsExistingDirectory(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public bool IsSameOrParrentDirectory(string child, string parrent)
            => Path.GetFullPath(child).StartsWith(Path.GetFullPath(parrent));

        public bool IsRoot(string path)
            => Path.GetPathRoot(path) == path;

        public string ContainingDirectory(string filePath)
            => new FileInfo(filePath).DirectoryName;
    }
}