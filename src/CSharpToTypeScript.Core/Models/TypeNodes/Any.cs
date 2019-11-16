using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class Any : TypeNode
    {
        public override string WriteTypeScript(CodeConversionOptions options, Context context) => "any";
    }
}