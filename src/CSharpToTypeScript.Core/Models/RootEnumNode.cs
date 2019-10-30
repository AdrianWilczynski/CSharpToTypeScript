using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Utilities;

using static CSharpToTypeScript.Core.Utilities.StringUtilities;

namespace CSharpToTypeScript.Core.Models
{
    internal class RootEnumNode : IRootNode
    {
        public RootEnumNode(string name, IEnumerable<EnumMemberNode> members)
        {
            Name = name;
            Members = members;
        }

        public string Name { get; }
        public IEnumerable<EnumMemberNode> Members { get; }

        public string WriteTypeScript(CodeConversionOptions options)
            => "export ".If(options.Export) + "enum " + Name + " {" + NewLine
            + string.Join("," + NewLine, Members.Select(m => m.WriteTypeScript(options)).Indent(options.UseTabs, options.TabSize)) + NewLine
            + "}";
    }
}