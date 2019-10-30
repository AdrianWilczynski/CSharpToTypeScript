using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal abstract class TypeNode : IWritableNode
    {
        public virtual bool IsUnionType(CodeConversionOptions options) => false;

        public virtual bool IsOptional(CodeConversionOptions options, out TypeNode of)
        {
            of = null;
            return false;
        }

        public abstract string WriteTypeScript(CodeConversionOptions options);
    }
}