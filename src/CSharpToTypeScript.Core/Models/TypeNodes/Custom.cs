using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class Custom : NamedTypeNode
    {
        public Custom(string name) : base(name) { }

        public override string WriteTypeScript(CodeConversionOptions options)
            => Name.TransformIf(options.RemoveInterfacePrefix, StringUtilities.RemoveInterfacePrefix);
    }
}