using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models.TypeNodes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers
{
    internal abstract class BasicTypeConverterBase<T> : TypeConversionHandler where T : TypeNode, new()
    {
        protected abstract IEnumerable<string> ConvertibleFrom { get; }

        public override TypeNode Handle(TypeSyntax type)
            => CanConvert(type) ? new T() : base.Handle(type);

        private bool CanConvert(TypeSyntax type)
            => type switch
            {
                PredefinedTypeSyntax predefined => ConvertibleFrom.Contains(predefined.Keyword.Text),
                IdentifierNameSyntax identified => ConvertibleFrom.Contains(identified.Identifier.Text),
                _ => false
            };
    }
}