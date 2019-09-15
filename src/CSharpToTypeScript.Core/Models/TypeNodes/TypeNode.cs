using System.Collections.Generic;
using System.Linq;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    public abstract class TypeNode
    {
        public virtual bool IsUnionType => false;
        public virtual bool IsOptional => false;

        public abstract string WriteTypeScript();
    }
}