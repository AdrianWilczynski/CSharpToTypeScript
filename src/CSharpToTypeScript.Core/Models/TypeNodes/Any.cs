namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    public class Any : ITypeNode
    {
        public string WriteTypeScript() => "any";
    }
}