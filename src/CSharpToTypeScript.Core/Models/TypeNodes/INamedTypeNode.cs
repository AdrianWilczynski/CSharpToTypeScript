namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    public interface INamedTypeNode : ITypeNode
    {
        string Name { get; }
    }
}