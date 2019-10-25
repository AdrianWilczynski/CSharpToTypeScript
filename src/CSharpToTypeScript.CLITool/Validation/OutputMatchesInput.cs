using System;
using System.ComponentModel.DataAnnotations;
using CSharpToTypeScript.CLITool.Utilities;

namespace CSharpToTypeScript.CLITool.Validation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OutputMatchesInput : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value is CLI cli
            && cli.Output?.EndsWithFileExtension() == true && cli.Input?.EndsWithFileExtension() == false)
            {
                return new ValidationResult("If your Output is a file, your Input has to be a file as well.");
            }

            return ValidationResult.Success;
        }
    }
}