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
            Members = members;
        }

        public string Name { get; }
        public IEnumerable<EnumMemberNode> Members { get; }

        public string WriteTypeScript(bool useTabs, int? tabSize, bool export)
            => "export ".If(export) + "enum " + Name + " {" + NewLine
            + string.Join("," + NewLine, Members.Select(m => m.WriteTypeScript()).Indent(useTabs, tabSize)) + NewLine
            + "}";
    }
}