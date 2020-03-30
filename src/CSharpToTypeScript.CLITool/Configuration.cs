using CSharpToTypeScript.CLITool.Commands;
using CSharpToTypeScript.Core.Options;
using Newtonsoft.Json;

namespace CSharpToTypeScript.CLITool
{
    public class Configuration
    {
        public Configuration() { }

        public Configuration(CommandBase command)
        {
            foreach (var property in typeof(Configuration).GetProperties())
            {
                property.SetValue(this, typeof(CommandBase).GetProperty(property.Name).GetValue(command));
            }
        }

        [JsonProperty("$schema")]
        public const string Schema
            = "https://adrianwilczynski.github.io/CSharpToTypeScript/cs2tsconfig.json";

        public string Input { get; set; }
        public string Output { get; set; }
        public bool? UseTabs { get; set; }
        public int? TabSize { get; set; }
        public bool? SkipExport { get; set; }
        public bool? UseKebabCase { get; set; }
        public bool? AppendModelSuffix { get; set; }
        public bool? ClearOutputDirectory { get; set; }
        public bool? AngularMode { get; set; }
        public bool? PartialOverride { get; set; }
        public bool? PreserveCasing { get; set; }
        public bool? PreserveInterfacePrefix { get; set; }
        public DateOutputType? ConvertDatesTo { get; set; }
        public NullableOutputType? ConvertNullablesTo { get; set; }
        public ImportGenerationMode? ImportGeneration { get; set; }
        public QuotationMark? QuotationMark { get; set; }
        public bool? AppendNewLine { get; set; }

        public void Override(CommandBase command)
        {
            foreach (var property in typeof(Configuration).GetProperties())
            {
                if (property.GetValue(this) is object value)
                {
                    typeof(CommandBase).GetProperty(property.Name).SetValue(command, value);
                }
            }
        }
    }
}