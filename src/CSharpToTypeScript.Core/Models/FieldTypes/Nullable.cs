namespace CSharpToTypeScript.Core.Models.FieldTypes
{
    public class Nullable : FieldType
    {
        public Nullable(FieldType of)
        {
            Of = of;
        }

        public FieldType Of { get; }

        public override bool IsOptional => true;
        public override bool IsUnionType => true;

        public override string WriteTypeScript()
            => Of.WriteTypeScript() + " | undefined";
    }
}