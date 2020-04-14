using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models
{
    internal class EnumMemberNode : IWritableNode
    {
        public EnumMemberNode(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public string Value { get; }

        public string WriteTypeScript(CodeConversionOptions options, Context context)
        {
            var value = options.StringEnums
                ? Name.TransformIf(options.EnumStringToCamelCase, StringUtilities.ToCamelCase)
                      .InQuotes(options.QuotationMark)
                : Value?.SquashWhistespace();

            return Name + (" = " + value).If(!string.IsNullOrWhiteSpace(value));
        }
    }
}