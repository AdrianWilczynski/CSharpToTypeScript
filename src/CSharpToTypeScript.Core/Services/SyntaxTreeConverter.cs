using System;
using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services
{
    public class SyntaxTreeConverter
    {
        private readonly RootTypeConverter _rootTypeConverter = new RootTypeConverter();
        private readonly RootEnumConverter _rootEnumConverter = new RootEnumConverter();

        public IEnumerable<IRootNode> Convert(CompilationUnitSyntax root)
            => root.DescendantNodes()
                .Where(node => (node is TypeDeclarationSyntax type && IsNotStatic(type)) || node is EnumDeclarationSyntax)
                .Select(node
                    => node is TypeDeclarationSyntax type ? (IRootNode)_rootTypeConverter.Convert(type)
                    : node is EnumDeclarationSyntax @enum ? _rootEnumConverter.Convert(@enum)
                    : throw new ArgumentException());

        private bool IsNotStatic(TypeDeclarationSyntax type)
            => type.Modifiers.All(m => m.Kind() != SyntaxKind.StaticKeyword);
    }
}