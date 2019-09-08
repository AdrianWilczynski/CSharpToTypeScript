using CSharpToTypeScript.Core.Models.FieldTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.FieldTypeConverters
{
    public class CustomConverter : FieldTypeConverter
    {
        public override IFieldType Convert(TypeSyntax type)
        {
            if (type is IdentifierNameSyntax identified && identified.Identifier.Text != "Object")
            {
                return new Custom(identified.Identifier.Text);
            }

            return base.Convert(type);
        }
    }
}