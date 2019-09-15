using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Utilities;

using static CSharpToTypeScript.Core.Utilities.StringUtilities;

namespace CSharpToTypeScript.Core.Models
{
    public class TypeNode
    {
        public TypeNode(string name, IEnumerable<FieldNode> fields, IEnumerable<string> genericArguments)
        {
            Name = name;
            Fields = fields;
            GenericArguments = genericArguments;
        }

        public string Name { get; }
        public IEnumerable<FieldNode> Fields { get; }
        public IEnumerable<string> GenericArguments { get; set; }

        public string WriteTypeScript(bool useTabs, int? tabSize, bool export)
            => "export ".If(export) + "interface " + Name.RemoveInterfacePrefix() + ("<" + GenericArguments.ToCommaSepratedList() + ">").If(GenericArguments.Any()) + " {" + NewLine
            + Fields.Select(f => f.WriteTypeScript()).Indent(useTabs, tabSize).LineByLine() + NewLine
            + "}";
    }
}