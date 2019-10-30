namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal abstract class NamedTypeNode : TypeNode
    {
        protected NamedTypeNode(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}