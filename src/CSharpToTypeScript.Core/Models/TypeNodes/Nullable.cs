namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    public class Nullable : TypeNode
    {
        public Nullable(TypeNode of)
        {
            Of = of;
        }

        public TypeNode Of { get; }

        public override bool IsOptional => true;
        public override bool IsUnionType => true;

        public override string WriteTypeScript()
            => Of.WriteTypeScript() + " | undefined";
    }
}