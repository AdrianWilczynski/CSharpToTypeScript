using System.Collections.Generic;
using System.Linq;
using CSharpToTypeScript.Core.Models.FieldTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.FieldTypeConverters
{
    public abstract class BasicTypeConverterBase<T> : FieldTypeConverter where T : IFieldType, new()
    {
        protected abstract IEnumerable<string> ConvertibleFrom { get; }

        public override IFieldType Convert(TypeSyntax type)
            => CanConvert(type) ? new T() : base.Convert(type);

        private bool CanConvert(TypeSyntax type)
        {
            if (type is PredefinedTypeSyntax predefined)
            {
                return ConvertibleFrom.Contains(predefined.Keyword.Text);
            }
            else if (type is IdentifierNameSyntax identified)
            {
                return ConvertibleFrom.Contains(identified.Identifier.Text);
            }

            return false;
        }
    }
}