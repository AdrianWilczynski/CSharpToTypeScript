namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class Boolean : ITypeNode
    {
        public string WriteTypeScript() => "boolean";
    }
}