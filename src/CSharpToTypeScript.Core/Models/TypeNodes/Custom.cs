using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    public class Custom : INamedTypeNode
    {
        public Custom(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public string WriteTypeScript() => Name.RemoveInterfacePrefix();
    }
}