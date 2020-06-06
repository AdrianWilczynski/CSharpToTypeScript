using CSharpToTypeScript.Core.Models.TypeNodes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers
{
    internal class UriConverter : TypeConversionHandler
    {
        public override TypeNode Handle(TypeSyntax type)
        {
            if (type is IdentifierNameSyntax identified && !string.IsNullOrWhiteSpace(identified.Identifier.Text)
                && identified.Identifier.Text == nameof(System.Uri))
            {
                return new Uri();
            }

            return base.Handle(type);
        }     
    }
}