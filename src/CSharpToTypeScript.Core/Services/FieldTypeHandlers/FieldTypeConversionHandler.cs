using CSharpToTypeScript.Core.Models.FieldTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.FieldTypeHandlers
{
    public abstract class FieldTypeConversionHandler
    {
        private FieldTypeConversionHandler _nextConverter;

        public virtual IFieldType Handle(TypeSyntax type)
           => _nextConverter is null ? new Any() : _nextConverter.Handle(type);

        public FieldTypeConversionHandler SetNext(FieldTypeConversionHandler converter)
            => _nextConverter = converter;
    }
}