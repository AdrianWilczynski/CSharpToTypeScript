using CSharpToTypeScript.Core.Models.FieldTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.FieldTypeConverters
{
    public abstract class FieldTypeConverter
    {
        private FieldTypeConverter _nextConverter;

        public virtual IFieldType Convert(TypeSyntax type)
           => _nextConverter is null ? new Any() : _nextConverter.Convert(type);

        public FieldTypeConverter SetNext(FieldTypeConverter converter)
            => _nextConverter = converter;
    }
}