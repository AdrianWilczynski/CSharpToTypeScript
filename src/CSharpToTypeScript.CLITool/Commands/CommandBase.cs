using System.ComponentModel.DataAnnotations;
using CSharpToTypeScript.CLITool.Validation;
using CSharpToTypeScript.Core.Options;
using McMaster.Extensions.CommandLineUtils;

namespace CSharpToTypeScript.CLITool.Commands
{
    [OutputMatchesInput]
    public abstract class CommandBase
    {
        [Argument(0, Description = "Input file or directory path")]
        [InputExists]
        public string Input { get; set; } = ".";

        [Option(ShortName = "o", Description = "Output file or directory path")]
        public string Output { get; set; }

        [Option(ShortName = "t", Description = "Use tabs for indentation")]
        public bool UseTabs { get; set; }

        [Option(ShortName = "ts", Description = "Number of spaces per tab")]
        [Range(1, 8)]
        public int TabSize { get; set; } = 4;

        [Option(ShortName = "se", Description = "Skip 'export' keyword")]
        public bool SkipExport { get; set; }

        [Option(ShortName = "k", Description = "Use kebab case for output file names")]
        public bool UseKebabCase { get; set; }

        [Option(ShortName = "m", Description = "Append '.model' suffix to output file names")]
        public bool AppendModelSuffix { get; set; }

        [Option(ShortName = "c", Description = "Clear output directory")]
        public bool ClearOutputDirectory { get; set; }

        [Option(ShortName = "a", Description = "Use Angular style conventions")]
        public bool AngularMode { get; set; }

        [Option(ShortName = "p", Description = "Override only part of output file between marker comments")]
        public bool PartialOverride { get; set; }

        [Option(ShortName = "pc", Description = "Don't convert field names to camel case")]
        public bool PreserveCasing { get; set; }

        [Option(ShortName = "pip", Description = "Don't remove interface prefixes")]
        public bool PreserveInterfacePrefix { get; set; }

        [Option(ShortName = "d", Description = "Set output type for dates",
        ValueName = nameof(DateOutputType.String) + "|" + nameof(DateOutputType.Date) + "|" + nameof(DateOutputType.Union))]
        public DateOutputType ConvertDatesTo { get; set; }

        [Option(ShortName = "n", Description = "Set output type for nullables",
        ValueName = nameof(NullableOutputType.Null) + "|" + nameof(NullableOutputType.Undefined))]
        public NullableOutputType ConvertNullablesTo { get; set; }

        [Option(ShortName = "i", Description = "Enable import generation",
        ValueName = nameof(ImportGenerationMode.None) + "|" + nameof(ImportGenerationMode.Simple))]
        public ImportGenerationMode ImportGeneration { get; set; }

        [Option(ShortName = "q", Description = "Set quotation marks for import statements",
        ValueName = nameof(QuotationMark.Double) + "|" + nameof(QuotationMark.Single))]
        public QuotationMark QuotationMark { get; set; }
    }
}