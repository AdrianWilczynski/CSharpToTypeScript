namespace CSharpToTypeScript.Core.Models.FieldTypes
{
    public class Nullable : IFieldType
    {
        public Nullable(IFieldType of)
        {
            Of = of;
        }

        public IFieldType Of { get; }

        public override string ToString() => $"{Of} | undefined";
    }
}