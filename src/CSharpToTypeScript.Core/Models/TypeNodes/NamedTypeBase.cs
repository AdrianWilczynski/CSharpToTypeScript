namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    public abstract class NamedTypeBase : TypeNode
    {
        protected NamedTypeBase(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}