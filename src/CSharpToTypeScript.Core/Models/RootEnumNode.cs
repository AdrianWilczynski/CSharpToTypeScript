using System.Collections.Generic;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Utilities;

using static CSharpToTypeScript.Core.Utilities.StringUtilities;

namespace CSharpToTypeScript.Core.Models
{
    internal class RootEnumNode : RootNode
    {
        public RootEnumNode(string name, IEnumerable<EnumMemberNode> members)
        {
            Name = name;
            Members = members;
        }

        public override string Name { get; }
        public IEnumerable<EnumMemberNode> Members { get; }

        public override string WriteTypeScript(CodeConversionOptions options, Context context)
            =>  // keywords
            "export ".If(options.Export) + "enum "
             // name
             + Name
            // body
            + " {" + NewLine
            // members
            + Members.WriteTypeScript(options, context).Indent(options.UseTabs, options.TabSize).LineByLine(separator: ",") + NewLine
            + "}";
    }
}