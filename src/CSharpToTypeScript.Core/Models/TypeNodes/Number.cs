namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    public class Number : ITypeNode
    {
        public string WriteTypeScript() => "number";
    }
}