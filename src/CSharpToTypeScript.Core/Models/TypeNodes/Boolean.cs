using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class Boolean : TypeNode
    {
        public override string WriteTypeScript(CodeConversionOptions options, Context context) => "boolean";
    }
}