using CSharpToTypeScript.Core.Models.FieldTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.FieldTypeHandlers
{
    public class QualifiedUnpacker : FieldTypeConversionHandler
    {
        public override IFieldType Handle(TypeSyntax type)
        {
            if (type is QualifiedNameSyntax qualified)
            {
                return base.Handle(qualified.Right);
            }
            return base.Handle(type);
        }
    }
}