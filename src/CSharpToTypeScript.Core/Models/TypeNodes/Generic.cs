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

        public override IEnumerable<string> Requires
            => new[] { Name }.Concat(Arguments.SelectMany(a => a.Requires)).Distinct();

        public IEnumerable<TypeNode> Arguments { get; }

        public override string WriteTypeScript(CodeConversionOptions options, Context context)
            => // name
            Name.TransformIf(options.RemoveInterfacePrefix && !context.GenericTypeParameters.Contains(Name), StringUtilities.RemoveInterfacePrefix)
            // generic type parameters
            + "<" + Arguments.WriteTypeScript(options, context).ToCommaSepratedList() + ">";
    }
}