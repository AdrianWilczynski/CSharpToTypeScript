using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Utilities;

using static CSharpToTypeScript.Core.Utilities.StringUtilities;

namespace CSharpToTypeScript.Core.Models
{
    public class TypeNode
    {
        public TypeNode(string name, IEnumerable<FieldNode> fields)
        {
            Name = name;
            Fields = fields;
        }

        public string Name { get; }
        public IEnumerable<FieldNode> Fields { get; }

        public string WriteTypeScript(bool useTabs, int? tabSize, bool export)
            => "export ".If(export) + "interface " + Name.RemoveInterfacePrefix() + " {" + NewLine
            + Fields.Select(f => f.WriteTypeScript()).Indent(useTabs, tabSize).LineByLine() + NewLine
            + "}";
    }
}