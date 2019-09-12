using CSharpToTypeScript.Core.Models.FieldTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.FieldTypeConversionHandlers
{
    public abstract class FieldTypeConversionHandler
    {
        private FieldTypeConversionHandler _nextConverter;

        public virtual FieldType Handle(TypeSyntax type)
           => _nextConverter is null ? new Any() : _nextConverter.Handle(type);

        public FieldTypeConversionHandler SetNext(FieldTypeConversionHandler converter)
            => _nextConverter = converter;
    }
}