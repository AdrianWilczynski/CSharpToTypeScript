using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    public class Generic : NamedTypeBase
    {
        public Generic(string name, IEnumerable<TypeNode> arguments) : base(name)
        {
            Arguments = arguments;
        }

        public IEnumerable<TypeNode> Arguments { get; }

        public override string WriteTypeScript()
            => Name.RemoveInterfacePrefix() + "<" + Arguments.Select(a => a.WriteTypeScript()).ToCommaSepratedList() + ">";
    }
}