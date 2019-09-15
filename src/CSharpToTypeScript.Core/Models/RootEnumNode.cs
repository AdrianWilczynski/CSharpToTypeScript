using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Utilities;

using static CSharpToTypeScript.Core.Utilities.StringUtilities;

namespace CSharpToTypeScript.Core.Models
{
    public class RootEnumNode : IRootNode
    {
        public RootEnumNode(string name, IEnumerable<EnumMemberNode> members)
        {
            Name = name;
            Members = members.ToList();
        }

        public string Name { get; }
        public List<EnumMemberNode> Members { get; }

        public string WriteTypeScript(bool useTabs, int? tabSize, bool export)
            => "export ".If(export) + "enum " + Name + " {" + NewLine
            + Members.Select((member, index) => member.WriteTypeScript() + ",".If(index != Members.Count - 1)).Indent(useTabs, tabSize).LineByLine() + NewLine
            + "}";
    }
}