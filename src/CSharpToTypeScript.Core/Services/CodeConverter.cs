using CSharpToTypeScript.Core.Options;
using Microsoft.CodeAnalysis.CSharp;

namespace CSharpToTypeScript.Core.Services
{
    public class CodeConverter : ICodeConverter
    {
        public string ConvertToTypeScript(string code, CodeConversionOptions options)
        {
            var root = CSharpSyntaxTree.ParseText(code)
                .GetCompilationUnitRoot();

            var converted = new SyntaxTreeConverter()
                .Convert(root);

            return converted.WriteTypeScript(options);
        }
    }
}