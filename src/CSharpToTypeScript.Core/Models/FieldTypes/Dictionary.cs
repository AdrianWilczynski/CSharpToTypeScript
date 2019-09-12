namespace CSharpToTypeScript.Core.Models.FieldTypes
{
    public class Dictionary : FieldType
    {
        public Dictionary(FieldType key, FieldType value)
        {
            Key = key;
            Value = value;
        }

        public FieldType Key { get; }
        public FieldType Value { get; }

        public override string WriteTypeScript()
            => "{ [key: " + Key.WriteTypeScript() + "]: " + Value.WriteTypeScript() + "; }";
    }
}