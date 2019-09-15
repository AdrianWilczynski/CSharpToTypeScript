using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services
{
    public class SyntaxTreeConverter
    {
        private readonly RootTypeConverter _rootTypeConverter = new RootTypeConverter();
        private readonly RootEnumConverter _rootEnumConverter = new RootEnumConverter();

        public IEnumerable<IRootNode> Convert(CompilationUnitSyntax root)
            => _rootTypeConverter.Convert(root).Union(_rootEnumConverter.Convert(root));
    }
}