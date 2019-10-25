using System.ComponentModel.DataAnnotations;
using System.IO;
using CSharpToTypeScript.CLITool.Utilities;

namespace CSharpToTypeScript.CLITool.Validation
{
    public class InputExists : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string input && !string.IsNullOrWhiteSpace(input))
            {
                if (input.EndsWithFileExtension() && !File.Exists(input))
                {
                    return new ValidationResult($"The file path '{input}' does not exist.");
                }
                else if (!input.EndsWithFileExtension() && !Directory.Exists(input))
                {
                    return new ValidationResult($"The directory path '{input}' does not exist.");
                }
            }

            return ValidationResult.Success;
        }
    }
}