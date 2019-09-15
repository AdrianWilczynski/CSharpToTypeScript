namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    public class Boolean : ITypeNode
    {
        public string WriteTypeScript() => "boolean";
    }
}