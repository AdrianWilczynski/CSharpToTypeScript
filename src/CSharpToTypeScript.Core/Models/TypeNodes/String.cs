namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class String : ITypeNode
    {
        public string WriteTypeScript() => "string";
    }
}