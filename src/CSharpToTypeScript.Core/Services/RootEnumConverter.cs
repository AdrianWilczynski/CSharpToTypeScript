using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services
{
    public class RootEnumConverter
    {
        public IEnumerable<IRootNode> Convert(CompilationUnitSyntax root)
            => root.DescendantNodes()
                .OfType<EnumDeclarationSyntax>()
                .Select(@enum => (IRootNode)new RootEnumNode(
                    name: @enum.Identifier.Text,
                    members: ConvertEnumMembers(@enum.Members)));

        private IEnumerable<EnumMemberNode> ConvertEnumMembers(IEnumerable<EnumMemberDeclarationSyntax> members)
          => members.Select(m => new EnumMemberNode(
                name: m.Identifier.Text,
                value: m.EqualsValue?.Value.ToString()));
    }
}