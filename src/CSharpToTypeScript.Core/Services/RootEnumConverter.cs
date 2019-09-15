using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services
{
    public class RootEnumConverter
    {
        public RootEnumNode Convert(EnumDeclarationSyntax @enum)
            => new RootEnumNode(
                name: @enum.Identifier.Text,
                members: ConvertEnumMembers(@enum.Members));

        private IEnumerable<EnumMemberNode> ConvertEnumMembers(IEnumerable<EnumMemberDeclarationSyntax> members)
          => members.Select(m => new EnumMemberNode(
                name: m.Identifier.Text,
                value: m.EqualsValue?.Value.ToString()));
    }
}