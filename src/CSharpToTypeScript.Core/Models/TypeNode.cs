using System;
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

        public string ToString(bool useTabs, int? tabSize, bool export)
            => $"{(export ? "export " : "")}interface {Name.RemoveInterfacePrefix()} {{{NewLine}{Fields.Select(f => Indentation(useTabs, tabSize) + f).LineByLine()}{NewLine}}}";

        public override string ToString() => ToString(false, 2, true);

        private string Indentation(bool useTabs, int? tabSize)
        {
            if (!useTabs && (tabSize is null || tabSize <= 0))
            {
                throw new ArgumentException();
            }

            return useTabs ? "\t" : " ".Repeat((int)tabSize);
        }
    }
}