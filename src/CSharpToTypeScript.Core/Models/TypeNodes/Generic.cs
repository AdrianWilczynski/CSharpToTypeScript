using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class Generic : NamedTypeNode
    {
        public Generic(string name, IEnumerable<TypeNode> arguments) : base(name)
        {
            Arguments = arguments;
        }

        public IEnumerable<TypeNode> Arguments { get; }

        public override string WriteTypeScript(CodeConversionOptions options)
            => Name.TransformIf(options.RemoveInterfacePrefix, StringUtilities.RemoveInterfacePrefix) + "<" + Arguments.Select(a => a.WriteTypeScript(options)).ToCommaSepratedList() + ">";
    }
}