using System.Collections.Generic;
using System.Linq;
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

        public override IEnumerable<string> Requires => Key.Requires.Concat(Value.Requires).Distinct();

        public override string WriteTypeScript(CodeConversionOptions options, Context context)
            => "{ [key: " + Key.WriteTypeScript(options, context) + "]: " + Value.WriteTypeScript(options, context) + "; }";
    }
}