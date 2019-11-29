using System;
using System.ComponentModel.DataAnnotations;
using CSharpToTypeScript.CLITool.Commands;

namespace CSharpToTypeScript.CLITool.Validation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidIndentation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is CommandBase command)
            {
                if (!command.UseTabs && command.TabSize < 1)
                {
                    return new ValidationResult("Use tabs for indentation or specify positive tab size (spaces).");
                }
            }

            return ValidationResult.Success;
        }
    }
}