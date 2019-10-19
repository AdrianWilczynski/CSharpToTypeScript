using System.Collections.Generic;

namespace CSharpToTypeScript.CLITool.Services
{
    public interface IFileSystem
    {
        void ClearOutputIfPossible(string input, string output);
        void EnsureDirectoryExists(string path);
        IEnumerable<string> GetCSharpFiles(string path);
        bool IsExistingDirectory(string path);
        bool IsExistingFile(string path);
        string ReadAllText(string path);
        void WriteAllText(string path, string content);
    }
}