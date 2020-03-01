using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Blazor.Models
{
    public class SettingsModel : IValidatableObject
    {
        public bool UseTabs { get; set; }

        public bool Export { get; set; } = true;

        [Range(2, 8)]
        public int? TabSize { get; set; } = 4;

        public DateOutputType ConvertDatesTo { get; set; }

        public NullableOutputType ConvertNullablesTo { get; set; }

        public bool ToCamelCase { get; set; } = true;

        public bool RemoveInterfacePrefix { get; set; } = true;

        public bool GenerateImports { get; set; } = true;

        public bool UseKebabCase { get; set; }

        public bool AppendModelSuffix { get; set; }

        public QuotationMark QuotationMark { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!UseTabs && TabSize is null)
            {
                yield return new ValidationResult(
                    "Provide Tab Size value or toggle Use Tabs.",
                    new[] { nameof(TabSize), nameof(UseTabs) });
            }
        }

        public SettingsModel Clone()
            => (SettingsModel)MemberwiseClone();

        public CodeConversionOptions MapToCodeConversionOptions()
            => new CodeConversionOptions(Export, UseTabs, TabSize, ConvertDatesTo, ConvertNullablesTo, ToCamelCase, RemoveInterfacePrefix,
                GenerateImports ? ImportGenerationMode.Simple : ImportGenerationMode.None,
                UseKebabCase, AppendModelSuffix, QuotationMark);
    }
}