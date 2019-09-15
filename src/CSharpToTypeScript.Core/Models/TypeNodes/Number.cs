namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    public class Number : TypeNode
    {
        public override string WriteTypeScript() => "number";
    }
}