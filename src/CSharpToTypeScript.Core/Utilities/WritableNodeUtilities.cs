using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models;
using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Utilities
{
    internal static class WritableNodeUtilities
    {
        public static IEnumerable<string> WriteTypeScript(this IEnumerable<IWritableNode> nodes, CodeConversionOptions options, Context context)
            => nodes.Select(n => n.WriteTypeScript(options, context));
    }
}