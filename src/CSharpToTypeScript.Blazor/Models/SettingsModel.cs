using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CSharpToTypeScript.Core.Options;

using static MoreLinq.Extensions.BatchExtension;

namespace CSharpToTypeScript.Blazor.Models
{
    public class SettingsModel : IValidatableObject
    {
        public bool UseTabs { get; set; }

        public string UseTabsDescription
            => Multiline("Use tabs for indentation.");

        public bool Export { get; set; } = true;

        public string ExportDescription
            => Multiline("Add \"export\" keyword to emitted type.");

        [Range(2, 8)]
        public int? TabSize { get; set; } = 4;

        public string TabSizeDescription
            => Multiline("Number of spaces per tab.");

        public DateOutputType ConvertDatesTo { get; set; }

        public string ConvertDatesToDescription
            => Multiline("Set output type for dates. You can pick between \"string\","
                + " \"Date\" and \"string | Date\".", 4);

        public NullableOutputType ConvertNullablesTo { get; set; }

        public string ConvertNullablesToDescription
            => Multiline("Set output type for nullables (int?) to either \"null\" or \"undefined\".", 4);

        public bool ToCamelCase { get; set; } = true;

        public string ToCamelCaseDescription
           => Multiline("Convert field names to camel case.");

        public bool RemoveInterfacePrefix { get; set; } = true;

        public string RemoveInterfacePrefixDescription
           => Multiline("Remove \"I\" prefixes.");

        public bool GenerateImports { get; set; } = true;

        public string GenerateImportsDescription
            => Multiline("Generate import statements (flat directory structure and file"
                + " names corresponding to type names).", 4);

        public bool UseKebabCase { get; set; }

        public string UseKebabCaseDescription
            => Multiline("Use kebab-case for file names.");

        public bool AppendModelSuffix { get; set; }

        public string AppendModelSuffixDescription
            => Multiline("Append \".model\" suffix to file names.");

        public QuotationMark QuotationMark { get; set; }

        public string QuotationMarkDescription
            => Multiline("Set quotation marks for import statements & identifiers.");

        public bool AppendNewLine { get; set; }

        public string AppendNewLineDescription
            => Multiline("Append new line to end of file (removes TSLint warning).");

        public bool StringEnums { get; set; }

        public string StringEnumsDescription
            => Multiline("Output string enums.");

        public bool EnumStringToCamelCase { get; set; }

        public string EnumStringToCamelCaseDescription
            => Multiline("Use camel case for enum strings.");

        public OutputType OutputType { get; set; }

        public string OutputTypeDescription
           => Multiline("Class or interface?");

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
                UseKebabCase, AppendModelSuffix, QuotationMark, AppendNewLine, StringEnums, EnumStringToCamelCase, OutputType);

        public string Multiline(string text, int wordsPerLine = 5)
            => string.Join(Environment.NewLine,
                    text.Split(" ")
                        .Batch(wordsPerLine)
                        .Select(line => string.Join(" ", line)));
    }
}