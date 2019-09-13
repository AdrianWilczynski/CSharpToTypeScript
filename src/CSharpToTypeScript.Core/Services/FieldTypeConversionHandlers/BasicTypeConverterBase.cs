using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models.FieldTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.FieldTypeConversionHandlers
{
    public abstract class BasicTypeConverterBase<T> : FieldTypeConversionHandler where T : FieldTypeNode, new()
    {
        protected abstract IEnumerable<string> ConvertibleFrom { get; }

        public override FieldTypeNode Handle(TypeSyntax type)
            => CanConvert(type) ? new T() : base.Handle(type);

        private bool CanConvert(TypeSyntax type)
            => (type is PredefinedTypeSyntax predefined && ConvertibleFrom.Contains(predefined.Keyword.Text))
            || (type is IdentifierNameSyntax identified && ConvertibleFrom.Contains(identified.Identifier.Text));
    }
}