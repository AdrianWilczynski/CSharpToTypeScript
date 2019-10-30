using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class Dictionary : TypeNode
    {
        public Dictionary(TypeNode key, TypeNode value)
        {
            Key = key;
            Value = value;
        }

        public TypeNode Key { get; }
        public TypeNode Value { get; }

        public override string WriteTypeScript(CodeConversionOptions options)
            => "{ [key: " + Key.WriteTypeScript(options) + "]: " + Value.WriteTypeScript(options) + "; }";
    }
}