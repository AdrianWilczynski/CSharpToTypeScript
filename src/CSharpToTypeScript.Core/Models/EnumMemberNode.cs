using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models
{
    public class EnumMemberNode
    {
        public EnumMemberNode(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public string Value { get; }

        public string WriteTypeScript()
            => Name + (" = " + Value?.SquashWhistespace()).If(!string.IsNullOrWhiteSpace(Value));
    }
}