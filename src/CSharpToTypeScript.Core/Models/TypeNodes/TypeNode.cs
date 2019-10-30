using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal abstract class TypeNode : IWritableNode
    {
        public virtual bool IsUnionType(CodeConversionOptions options) => false;

        public abstract string WriteTypeScript(CodeConversionOptions options);
    }
}