using CSharpToTypeScript.Core.Models.TypeNodes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers
{
    internal class QualifiedUnpacker : TypeConversionHandler
    {
        public override TypeNode Handle(TypeSyntax type)
        {
            if (type is QualifiedNameSyntax qualified)
            {
                return base.Handle(qualified.Right);
            }

            return base.Handle(type);
        }
    }
}