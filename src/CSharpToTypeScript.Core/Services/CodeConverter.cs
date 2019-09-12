using System.Linq;
using Microsoft.CodeAnalysis.CSharp;

using static CSharpToTypeScript.Core.Utilities.StringUtilities;

namespace CSharpToTypeScript.Core.Services
{
    public class CodeConverter
    {
        public string ConvertToTypeScript(string code, bool useTabs, int? tabSize, bool export)
        {
            var root = CSharpSyntaxTree.ParseText(code)
                .GetCompilationUnitRoot();

            var convertedTree = new SyntaxTreeConverter()
                .Convert(root);

            var convertedCode = string.Join(EmptyLine, convertedTree.Select(t => t.WriteTypeScript(useTabs, tabSize, export)));

            return convertedCode;
        }
    }
}