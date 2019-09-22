using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class Generic : INamedTypeNode
    {
        public Generic(string name, IEnumerable<ITypeNode> arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public string Name { get; }
        public IEnumerable<ITypeNode> Arguments { get; }

        public string WriteTypeScript()
            => Name.RemoveInterfacePrefix() + "<" + Arguments.Select(a => a.WriteTypeScript()).ToCommaSepratedList() + ">";
    }
}