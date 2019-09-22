namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class Nullable : ITypeNode
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