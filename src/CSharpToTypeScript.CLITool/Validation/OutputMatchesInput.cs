using System;
using System.ComponentModel.DataAnnotations;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.CLITool.Validation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OutputMatchesInput : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value is Program program)
            {
                if (program.Input is string && program.Output is string
                && program.Output.EndsWithFileExtension() && !program.Input.EndsWithFileExtension())
                {
                    return new ValidationResult("If your Output is a file, your Input has to be a file as well.");
                }
            }

            return ValidationResult.Success;
        }
    }
}