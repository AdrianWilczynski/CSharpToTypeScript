using System.Linq;
using Microsoft.CodeAnalysis.CSharp;

using static CSharpToTypeScript.Core.Utilities.StringUtilities;

namespace CSharpToTypeScript.Core.Services
{
    public class CodeConverter
    {
        public string ConvertToTypeScript(string inputCode, bool useTabs, int? tabSize, bool export)
        {
            var root = CSharpSyntaxTree.ParseText(inputCode)
                .GetCompilationUnitRoot();

            var convertedTree = new SyntaxTreeConverter()
                .Convert(root);

            var outputCode = string.Join(
                NewLine.Repeat(2),
                convertedTree.Select(t => t.WriteTypeScript(useTabs, tabSize, export)));

            return outputCode;
        }
    }
}