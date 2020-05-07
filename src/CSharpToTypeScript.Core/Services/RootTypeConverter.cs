using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Constants;
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
                name: type.Identifier.ValueText,
                fields: type.ChildNodes()
                    .SelectMany(node => node switch
                    {
                        PropertyDeclarationSyntax property when IsSerializable(property, type) => new[] { ConvertProperty(property) },
                        FieldDeclarationSyntax field when IsSerializable(field) => ConvertField(field),
                        _ => Enumerable.Empty<FieldNode>()
                    }),
                genericTypeParameters: type.TypeParameterList?.Parameters
                    .Select(p => p.Identifier.ValueText)
                    .Where(p => !string.IsNullOrWhiteSpace(p)) ?? Enumerable.Empty<string>(),
                baseTypes: ConvertBaseTypes(
                    type.BaseList?.Types ?? Enumerable.Empty<BaseTypeSyntax>(),
                    type),
                fromInterface: type is InterfaceDeclarationSyntax);

        private FieldNode ConvertProperty(PropertyDeclarationSyntax property)
            => new FieldNode(
                name: property.Identifier.ValueText,
                type: _typeConverter.Handle(property.Type),
                jsonPropertyName: GetJsonPropertyName(property));

        private IEnumerable<FieldNode> ConvertField(FieldDeclarationSyntax field)
           => field.Declaration.Variables.Select(v => new FieldNode(
                name: v.Identifier.ValueText,
                type: _typeConverter.Handle(field.Declaration.Type),
                jsonPropertyName: GetJsonPropertyName(field)))
            .Where(f => !string.IsNullOrWhiteSpace(f.Name));

        private string GetJsonPropertyName(MemberDeclarationSyntax member)
        {
            foreach (var attribute in member.AttributeLists.SelectMany(a => a.Attributes))
            {
                static bool IsNotNamed(AttributeArgumentSyntax attribute)
                    => attribute.NameColon is null && attribute.NameEquals is null;

                static bool ArgumentNameEquals(AttributeArgumentSyntax attribute, string name)
                    => attribute.NameColon?.Name.Identifier.ValueText == name;

                static bool PropertyNameEquals(AttributeArgumentSyntax attribute, string name)
                    => attribute.NameEquals?.Name.Identifier.ValueText == name;

                static bool HasStringExpression(AttributeArgumentSyntax attribute)
                    => attribute.Expression.IsKind(SyntaxKind.StringLiteralExpression);

                var argument = GetAttributeName(attribute) switch
                {
                    Attributes.JsonPropertyName => attribute.ArgumentList?.Arguments
                        .FirstOrDefault(a => HasStringExpression(a)
                            && (IsNotNamed(a) || ArgumentNameEquals(a, AttributeArgumentNames.Name))),
                    Attributes.JsonProperty => attribute.ArgumentList?.Arguments
                        .FirstOrDefault(a => HasStringExpression(a)
                            && (IsNotNamed(a) || ArgumentNameEquals(a, AttributeArgumentNames.PropertyName) || PropertyNameEquals(a, AttributePropertyNames.PropertyName))),
                    _ => null
                };

                if (!(argument is null))
                {
                    return ((LiteralExpressionSyntax)argument.Expression).Token.ValueText;
                }
            }

            return null;
        }

        private IEnumerable<TypeNode> ConvertBaseTypes(IEnumerable<BaseTypeSyntax> baseTypes, TypeDeclarationSyntax containingType)
        {
            var types = baseTypes;
            if (!(containingType is InterfaceDeclarationSyntax))
            {
                types = types.Take(1);
            }

            var namedTypes = types
                .Select(t => _typeConverter.Handle(t.Type))
                .OfType<NamedTypeNode>();

            if (!(containingType is InterfaceDeclarationSyntax))
            {
                namedTypes = namedTypes.Where(t => !t.Name.HasInterfacePrefix());
            }

            return namedTypes;
        }

        private bool IsSerializable(PropertyDeclarationSyntax property, TypeDeclarationSyntax containingType)
            => IsPublic(property, containingType) && IsNotStatic(property)
            && IsGettable(property) && !HasJsonIgnoreAttribute(property);

        private bool IsSerializable(FieldDeclarationSyntax field)
            => IsPublic(field) && IsNotStatic(field) && !HasJsonIgnoreAttribute(field);

        private bool IsNotStatic(MemberDeclarationSyntax member)
            => member.Modifiers.All(m => m.Kind() != SyntaxKind.StaticKeyword);

        private bool HasJsonIgnoreAttribute(MemberDeclarationSyntax member)
            => member.AttributeLists.Any(l => l.Attributes.Any(a => GetAttributeName(a) == Attributes.JsonIgnore));

        private bool IsPublic(MemberDeclarationSyntax property, TypeDeclarationSyntax containingType = null)
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

        private string GetAttributeName(AttributeSyntax attribute)
            => attribute.Name switch
            {
                QualifiedNameSyntax qualified => qualified.Right.Identifier.ValueText,
                _ => attribute.Name.ToString()
            };
    }
}