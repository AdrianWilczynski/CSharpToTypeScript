using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.FieldTypes
{
    public class Generic : FieldTypeNode
    {
        public Generic(string name, IEnumerable<FieldTypeNode> arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public string Name { get; }
        public IEnumerable<FieldTypeNode> Arguments { get; }

        public override string WriteTypeScript()
            => Name.RemoveInterfacePrefix() + "<" + Arguments.Select(a => a.WriteTypeScript()).ToCommaSepratedList() + ">";
    }
}