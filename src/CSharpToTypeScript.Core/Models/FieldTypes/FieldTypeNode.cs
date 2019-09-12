using System.Collections.Generic;
using System.Linq;

namespace CSharpToTypeScript.Core.Models.FieldTypes
{
    public abstract class FieldTypeNode
    {
        public virtual bool IsUnionType => false;
        public virtual bool IsOptional => false;

        public abstract string WriteTypeScript();
    }
}