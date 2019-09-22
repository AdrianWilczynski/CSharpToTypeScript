namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class Any : ITypeNode
    {
        public string WriteTypeScript() => "any";
    }
}