using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models;
using CSharpToTypeScript.Core.Services.FieldTypeConversionHandlers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services
{
    public class SyntaxTreeConverter
    {
        private readonly FieldTypeConversionHandler _fieldTypeConverter = FieldTypeConverterFactory.Create();

        public IEnumerable<TypeNode> Convert(CompilationUnitSyntax root)
            => root.DescendantNodes()
                .OfType<TypeDeclarationSyntax>()
                .Where(IsSerializable)
                .Select(type => new TypeNode(
                    name: type.Identifier.Text,
                    fields: ConvertProperties(
                        type.ChildNodes()
                        .OfType<PropertyDeclarationSyntax>()
                        .Where(property => IsSerializable(property, type)))));

        private IEnumerable<FieldNode> ConvertProperties(IEnumerable<PropertyDeclarationSyntax> properties)
            => properties.Select(p => new FieldNode(
                    name: p.Identifier.Text,
                    type: _fieldTypeConverter.Handle(p.Type)));

        private bool IsSerializable(TypeDeclarationSyntax type)
            => IsNotStatic(type);

        private bool IsSerializable(PropertyDeclarationSyntax property, TypeDeclarationSyntax containingType)
            => IsPublic(property, containingType) && IsNotStatic(property) && IsGettable(property);

        private bool IsNotStatic(MemberDeclarationSyntax syntax)
            => syntax.Modifiers.All(m => m.Kind() != SyntaxKind.StaticKeyword);

        private bool IsPublic(PropertyDeclarationSyntax property, TypeDeclarationSyntax containingType)
            => containingType is InterfaceDeclarationSyntax
            || property.Modifiers.Any(m => m.Kind() == SyntaxKind.PublicKeyword);

        private bool IsGettable(PropertyDeclarationSyntax property)
            => property.AccessorList is null
            || (HasGetter(property, out var getter) && !HasRestrictedAccess(getter));

        private bool HasGetter(PropertyDeclarationSyntax property, out AccessorDeclarationSyntax getter)
            => (getter = property.AccessorList.Accessors.FirstOrDefault(IsGetter)) != null;

        private bool IsGetter(AccessorDeclarationSyntax accessor)
            => accessor.Kind() == SyntaxKind.GetAccessorDeclaration;

        private bool HasRestrictedAccess(AccessorDeclarationSyntax accessor)
            => accessor.Modifiers.Any(m => RestrictiveAccessModifiers.Contains(m.Kind()));

        private readonly IEnumerable<SyntaxKind> RestrictiveAccessModifiers
            = new[] { SyntaxKind.PrivateKeyword, SyntaxKind.ProtectedKeyword, SyntaxKind.InterfaceKeyword };
    }
}