using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    public class Custom : NamedTypeBase
    {
        public Custom(string name) : base(name) { }

        public override string WriteTypeScript() => Name.RemoveInterfacePrefix();
    }
}