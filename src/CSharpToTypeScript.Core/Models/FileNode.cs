using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Options;

using static CSharpToTypeScript.Core.Utilities.StringUtilities;

namespace CSharpToTypeScript.Core.Models
{
    internal class FileNode : IWritableNode
    {
        public FileNode(IEnumerable<IRootNode> rootNodes)
        {
            RootNodes = rootNodes;
        }

        public IEnumerable<IRootNode> RootNodes { get; }

        public string WriteTypeScript(CodeConversionOptions options)
            => string.Join(EmptyLine, RootNodes.Select(t => t.WriteTypeScript(options)));
    }
}