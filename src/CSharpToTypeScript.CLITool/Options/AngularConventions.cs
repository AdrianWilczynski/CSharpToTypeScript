using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.CLITool.Options
{
    public class AngularConventions : OptionsBase<AngularConventions>
    {
        public int TabSize { get; } = 2;
        public bool UseKebabCase { get; } = true;
        public bool AppendModelSuffix { get; } = true;
        public QuotationMark QuotationMark { get; } = QuotationMark.Single;
    }
}