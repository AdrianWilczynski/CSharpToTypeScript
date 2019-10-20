using System.Collections.Generic;

namespace CSharpToTypeScript.CLITool.Services
{
    public interface IFileSystem
    {
        void ClearOutputIfPossible(string input, string output);
        IEnumerable<string> GetCSharpFiles(string path);
        bool IsExistingDirectory(string path);
        bool IsExistingFile(string path);
        string ReadAllText(string path);
        void WriteAllText(string path, string content);
        void EnsureDirectoryExists(string path);
        bool IsSameOrParrentDirectory(string child, string parrent);
        bool IsRoot(string path);
        string ContainingDirectory(string filePath);
    }
}