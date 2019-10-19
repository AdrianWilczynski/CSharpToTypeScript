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
            if (value is CLI cli
                && cli.Input is string && cli.Output is string
                && cli.Output.EndsWithFileExtension() && !cli.Input.EndsWithFileExtension())
            {
                return new ValidationResult("If your Output is a file, your Input has to be a file as well.");
            }

            return ValidationResult.Success;
        }
    }
}