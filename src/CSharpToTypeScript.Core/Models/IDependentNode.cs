using System.Collections.Generic;

namespace CSharpToTypeScript.Core.Models
{
    public interface IDependentNode
    {
        IEnumerable<string> Requires { get; }
    }
}