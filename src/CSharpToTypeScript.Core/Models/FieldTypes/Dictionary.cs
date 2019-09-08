namespace CSharpToTypeScript.Core.Models.FieldTypes
{
    public class Dictionary : IFieldType
    {
        public Dictionary(IFieldType key, IFieldType value)
        {
            Key = key;
            Value = value;
        }

        public IFieldType Key { get; }
        public IFieldType Value { get; }

        public override string ToString() => $"{{ [key: {Key}]: {Value}; }}";
    }
}