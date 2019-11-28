using System;
using CSharpToTypeScript.CLITool.Utilities;

namespace CSharpToTypeScript.CLITool.Validation
{
    public static class OutputMatchesInput
    {
        public static void Validate(string input, string output)
        {
            if (output?.EndsWithFileExtension() == true && !input.EndsWithFileExtension())
            {
                throw new ArgumentException("If your Output is a file, your Input has to be a file as well.");
            }
        }
    }
}