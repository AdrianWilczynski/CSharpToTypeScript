using CSharpToTypeScript.Core.Models.FieldTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.FieldTypeHandlers
{
    public class CustomConverter : FieldTypeConversionHandler
    {
        public override IFieldType Handle(TypeSyntax type)
        {
            if (type is IdentifierNameSyntax identified && identified.Identifier.Text != "Object")
            {
                return new Custom(identified.Identifier.Text);
            }

            return base.Handle(type);
        }
    }
}