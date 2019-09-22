namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal interface INamedTypeNode : ITypeNode
    {
        string Name { get; }
    }
}