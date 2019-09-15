namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    public class Nullable : ITypeNode
    {
        public Nullable(ITypeNode of)
        {
            Of = of;
        }

        public ITypeNode Of { get; }

        public string WriteTypeScript()
            => Of.WriteTypeScript() + " | undefined";
    }
}