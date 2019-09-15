namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    public class Dictionary : TypeNode
    {
        public Dictionary(TypeNode key, TypeNode value)
        {
            Key = key;
            Value = value;
        }

        public TypeNode Key { get; }
        public TypeNode Value { get; }

        public override string WriteTypeScript()
            => "{ [key: " + Key.WriteTypeScript() + "]: " + Value.WriteTypeScript() + "; }";
    }
}