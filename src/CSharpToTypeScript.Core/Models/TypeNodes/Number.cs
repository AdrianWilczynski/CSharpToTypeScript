namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class Number : ITypeNode
    {
        public string WriteTypeScript() => "number";
    }
}