using CSharpToTypeScript.Core.Models.TypeNodes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers
{
    internal class CustomConverter : TypeConversionHandler
    {
        public override TypeNode Handle(TypeSyntax type)
        {
            if (type is IdentifierNameSyntax identified && identified.Identifier.Text != "Object")
            {
                return new Custom(identified.Identifier.Text);
            }

            return base.Handle(type);
        }
    }
}