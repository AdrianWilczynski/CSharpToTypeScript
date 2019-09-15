using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models.TypeNodes;
using CSharpToTypeScript.Core.Utilities;

using static CSharpToTypeScript.Core.Utilities.StringUtilities;

namespace CSharpToTypeScript.Core.Models
{
    public class RootTypeNode
    {
        public RootTypeNode(string name, IEnumerable<FieldNode> fields, IEnumerable<string> genericTypeParameters, IEnumerable<TypeNode> baseTypes)
        {
            Name = name;
            Fields = fields;
            GenericTypeParameters = genericTypeParameters;
            BaseTypes = baseTypes;
        }

        public string Name { get; }
        public IEnumerable<FieldNode> Fields { get; }
        public IEnumerable<string> GenericTypeParameters { get; set; }
        public IEnumerable<TypeNode> BaseTypes { get; set; }

        public string WriteTypeScript(bool useTabs, int? tabSize, bool export)
            => "export ".If(export) + "interface " + Name.RemoveInterfacePrefix() + ("<" + GenericTypeParameters.ToCommaSepratedList() + ">").If(GenericTypeParameters.Any()) + (" extends " + BaseTypes.Select(e => e.WriteTypeScript()).ToCommaSepratedList()).If(BaseTypes.Any()) + " {" + NewLine
            + Fields.Select(f => f.WriteTypeScript()).Indent(useTabs, tabSize).LineByLine() + NewLine
            + "}";
    }
}