using System;
using System.IO;
using CSharpToTypeScript.CLITool.Utilities;

namespace CSharpToTypeScript.CLITool.Validation
{
    public static class InputExists
    {
        public static void Validate(string input)
        {
            if (input.EndsWithFileExtension() && !File.Exists(input))
            {
                throw new ArgumentException($"The file path '{input}' does not exist.");
            }
            else if (!input.EndsWithFileExtension() && !Directory.Exists(input))
            {
                throw new ArgumentException($"The directory path '{input}' does not exist.");
            }
        }
    }
}