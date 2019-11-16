using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class Number : TypeNode
    {
        public override string WriteTypeScript(CodeConversionOptions options, Context context) => "number";
    }
}