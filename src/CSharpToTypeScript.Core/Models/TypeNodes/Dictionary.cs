namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class Dictionary : ITypeNode
    {
        public Dictionary(ITypeNode key, ITypeNode value)
        {
            Key = key;
            Value = value;
        }

        public ITypeNode Key { get; }
        public ITypeNode Value { get; }

        public string WriteTypeScript()
            => "{ [key: " + Key.WriteTypeScript() + "]: " + Value.WriteTypeScript() + "; }";
    }
}