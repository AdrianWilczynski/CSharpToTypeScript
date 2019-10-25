using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CSharpToTypeScript.CLITool.Validation
{
    public class NotEmptyOrWhiteSpace : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string @string && Regex.IsMatch(@string, @"^\s*$"))
            {
                return new ValidationResult("Provide not empty value.");
            }

            return ValidationResult.Success;
        }
    }
}