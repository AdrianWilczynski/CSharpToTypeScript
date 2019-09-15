namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    public class String : ITypeNode
    {
        public string WriteTypeScript() => "string";
    }
}