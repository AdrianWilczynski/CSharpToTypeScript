using System.Collections.Generic;

namespace CSharpToTypeScript.Core.Models
{
    public class Context
    {
        public IEnumerable<string> GenericTypeParameters { get; set; }

        public Context Clone() => (Context)MemberwiseClone();
    }
}