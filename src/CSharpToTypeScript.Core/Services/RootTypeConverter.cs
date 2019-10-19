using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models;
using CSharpToTypeScript.Core.Models.TypeNodes;
using CSharpToTypeScript.Core.Services.TypeConversionHandlers;
using CSharpToTypeScript.Core.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services
{
    internal class RootTypeConverter
    {
        private readonly TypeConversionHandler _typeConverter;

        public RootTypeConverter(TypeConversionHandler typeConverter)
        {
            _typeConverter = typeConverter;
        }

        public RootTypeNode Convert(TypeDeclarationSyntax type)
            => new RootTypeNode(
                name: type.Identifier.Text,
                fields: ConvertProperties(
                    type.ChildNodes()
                    .OfType<PropertyDeclarationSyntax>()
                    .Where(property => IsSerializable(property, type))),
                genericTypeParameters: type.TypeParameterList?.Parameters
                    .Select(p => p.Identifier.Text)
                    .Where(p => !string.IsNullOrWhiteSpace(p)) ?? Enumerable.Empty<string>(),
                baseTypes: ConvertBaseTypes(
                    type.BaseList?.Types ?? Enumerable.Empty<BaseTypeSyntax>(),
                    type));

        private IEnumerable<FieldNode> ConvertProperties(IEnumerable<PropertyDeclarationSyntax> properties)
            => properties.Select(p => new FieldNode(
                name: p.Identifier.Text,
                type: _typeConverter.Handle(p.Type)));

        private IEnumerable<ITypeNode> ConvertBaseTypes(IEnumerable<BaseTypeSyntax> baseTypes, TypeDeclarationSyntax containingType)
        {
            var types = baseTypes;
            if (!(containingType is InterfaceDeclarationSyntax))
            {
                types = types.Take(1);
            }

            var namedTypes = types
                .Select(t => _typeConverter.Handle(t.Type))
                .OfType<INamedTypeNode>();

            if (!(containingType is InterfaceDeclarationSyntax))
            {
                namedTypes = namedTypes.Where(t => !t.Name.HasInterfacePrefix());
            }

            return namedTypes;
        }

        private bool IsSerializable(PropertyDeclarationSyntax property, TypeDeclarationSyntax containingType)
            => IsPublic(property, containingType) && IsNotStatic(property) && IsGettable(property);

        private bool IsNotStatic(PropertyDeclarationSyntax syntax)
            => syntax.Modifiers.All(m => m.Kind() != SyntaxKind.StaticKeyword);

        private bool IsPublic(PropertyDeclarationSyntax property, TypeDeclarationSyntax containingType)
            => containingType is InterfaceDeclarationSyntax
            || property.Modifiers.Any(m => m.Kind() == SyntaxKind.PublicKeyword);

        private bool IsGettable(PropertyDeclarationSyntax property)
            => property.AccessorList is null
            || (HasGetter(property, out var getter) && !HasRestrictedAccess(getter));

        private bool HasGetter(PropertyDeclarationSyntax property, out AccessorDeclarationSyntax getter)
            => (getter = property.AccessorList?.Accessors.FirstOrDefault(IsGetter)) is AccessorDeclarationSyntax;

        private bool IsGetter(AccessorDeclarationSyntax accessor)
            => accessor.Kind() == SyntaxKind.GetAccessorDeclaration;

        private bool HasRestrictedAccess(AccessorDeclarationSyntax accessor)
            => accessor.Modifiers.Any(m => RestrictiveAccessModifiers.Contains(m.Kind()));

        private IEnumerable<SyntaxKind> RestrictiveAccessModifiers { get; }
            = new[] { SyntaxKind.PrivateKeyword, SyntaxKind.ProtectedKeyword, SyntaxKind.InternalKeyword };
    }
}