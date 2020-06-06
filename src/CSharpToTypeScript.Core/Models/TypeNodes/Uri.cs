using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class Uri : TypeNode
    {
        public override string WriteTypeScript(CodeConversionOptions options, Context context) => "string";
    }
}