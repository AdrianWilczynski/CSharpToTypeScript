using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models.TypeNodes;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Utilities;

using static CSharpToTypeScript.Core.Utilities.StringUtilities;

namespace CSharpToTypeScript.Core.Models
{
    internal class RootTypeNode : IRootNode
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

        public string WriteTypeScript(CodeConversionOptions options)
            => "export ".If(options.Export) + "interface " + Name.TransformIf(options.RemoveInterfacePrefix, StringUtilities.RemoveInterfacePrefix) + ("<" + GenericTypeParameters.ToCommaSepratedList() + ">").If(GenericTypeParameters.Any()) + (" extends " + BaseTypes.Select(e => e.WriteTypeScript(options)).ToCommaSepratedList()).If(BaseTypes.Any()) + " {" + NewLine
            + Fields.Select(f => f.WriteTypeScript(options)).Indent(options.UseTabs, options.TabSize).LineByLine() + NewLine
            + "}";
    }
}