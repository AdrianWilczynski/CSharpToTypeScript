using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models;
using CSharpToTypeScript.Core.Services.FieldTypeConverters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services
{
    public class SyntaxTreeConverter
    {
        public IEnumerable<TypeNode> Convert(CompilationUnitSyntax root)
        {
            var types = root.DescendantNodes()
                .OfType<TypeDeclarationSyntax>();

            return types.Select(t => new TypeNode(
                name: t.Identifier.Text,
                fields: ConvertProperties(GetTransferableProperties(t))));
        }

        private IEnumerable<PropertyDeclarationSyntax> GetTransferableProperties(TypeDeclarationSyntax type)
        {
            bool IsPublic(PropertyDeclarationSyntax property)
                => type is InterfaceDeclarationSyntax
                || property.Modifiers.Any(m => m.Kind() == SyntaxKind.PublicKeyword);

            bool IsNotStatic(PropertyDeclarationSyntax property)
                => property.Modifiers.All(m => m.Kind() != SyntaxKind.StaticKeyword);

            bool IsReadable(PropertyDeclarationSyntax property)
                => property.AccessorList?.Accessors.Any(a => a.Kind() == SyntaxKind.GetAccessorDeclaration) ?? true;

            return type.ChildNodes()
                .OfType<PropertyDeclarationSyntax>()
                .Where(p => IsPublic(p) && IsNotStatic(p) && IsReadable(p));
        }

        private IEnumerable<FieldNode> ConvertProperties(IEnumerable<PropertyDeclarationSyntax> properties)
        {
            var converter = new NumberConverter();

            converter.SetNext(new StringConverter())
                .SetNext(new BooleanConverter())
                .SetNext(new ArrayConverter(converter))
                .SetNext(new TupleConverter(converter))
                .SetNext(new DictionaryConverter(converter))
                .SetNext(new NullableConverter(converter))
                .SetNext(new GenericConverter(converter))
                .SetNext(new CustomConverter());

            return properties.Select(p => new FieldNode(
                name: p.Identifier.Text,
                type: converter.Convert(p.Type)));
        }
    }
}