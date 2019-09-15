using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models.TypeNodes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers
{
    public abstract class BasicTypeConverterBase<T> : TypeConversionHandler where T : TypeNode, new()
    {
        protected abstract IEnumerable<string> ConvertibleFrom { get; }

        public override TypeNode Handle(TypeSyntax type)
            => CanConvert(type) ? new T() : base.Handle(type);

        private bool CanConvert(TypeSyntax type)
            => (type is PredefinedTypeSyntax predefined && ConvertibleFrom.Contains(predefined.Keyword.Text))
            || (type is IdentifierNameSyntax identified && ConvertibleFrom.Contains(identified.Identifier.Text));
    }
}