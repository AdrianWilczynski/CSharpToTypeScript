namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    public abstract class NamedTypeBase : ITypeNode
    {
        protected NamedTypeBase(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public abstract string WriteTypeScript();
    }
}