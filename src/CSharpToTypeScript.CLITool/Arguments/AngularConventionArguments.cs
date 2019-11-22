using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.CLITool.Arguments
{
    public class AngularConventionArguments : ArgumentsBase<AngularConventionArguments>
    {
        public int TabSize { get; } = 2;
        public bool UseKebabCase { get; } = true;
        public bool AppendModelSuffix { get; } = true;
        public QuotationMark QuotationMark { get; } = QuotationMark.Single;
    }
}