namespace CSharpToTypeScript.Core.Models.FieldTypes
{
    public class Nullable : FieldTypeNode
    {
        public Nullable(FieldTypeNode of)
        {
            Of = of;
        }

        public FieldTypeNode Of { get; }

        public override bool IsOptional => true;
        public override bool IsUnionType => true;

        public override string WriteTypeScript()
            => Of.WriteTypeScript() + " | undefined";
    }
}