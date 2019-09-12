namespace CSharpToTypeScript.Core.Models.FieldTypes
{
    public class Dictionary : FieldTypeNode
    {
        public Dictionary(FieldTypeNode key, FieldTypeNode value)
        {
            Key = key;
            Value = value;
        }

        public FieldTypeNode Key { get; }
        public FieldTypeNode Value { get; }

        public override string WriteTypeScript()
            => "{ [key: " + Key.WriteTypeScript() + "]: " + Value.WriteTypeScript() + "; }";
    }
}