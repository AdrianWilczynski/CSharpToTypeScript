using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal abstract class TypeNode : IWritableNode, IDependentNode
    {
        public virtual IEnumerable<string> Requires => Enumerable.Empty<string>();

        public virtual bool IsUnionType(CodeConversionOptions options) => false;

        public virtual bool IsOptional(CodeConversionOptions options, out TypeNode of)
        {
            of = null;
            return false;
        }

        public abstract string WriteTypeScript(CodeConversionOptions options, Context context);
    }
}