using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using CSharpToTypeScript.CLITool.Commands;
using CSharpToTypeScript.CLITool.Utilities;

namespace CSharpToTypeScript.CLITool.Validation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InputExists : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is CommandBase command)
            {
                if (command.Input is null)
                {
                    return new ValidationResult($"Input cannot be null!");
                }
                else if (command.Input.EndsWithFileExtension() && !File.Exists(command.Input))
                {
                    return new ValidationResult($"The file path '{command.Input}' does not exist.");
                }
                else if (!command.Input.EndsWithFileExtension() && !Directory.Exists(command.Input))
                {
                    return new ValidationResult($"The directory path '{command.Input}' does not exist.");
                }
            }

            return ValidationResult.Success;
        }
    }
}