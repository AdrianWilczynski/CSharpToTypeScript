using System;
using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class Date : TypeNode
    {
        public override bool IsUnionType(CodeConversionOptions options)
            => options.ConvertDatesTo == DateOutputType.Union;

        public override string WriteTypeScript(CodeConversionOptions options)
            => options.ConvertDatesTo == DateOutputType.String ? "string"
            : options.ConvertDatesTo == DateOutputType.Date ? "Date"
            : options.ConvertDatesTo == DateOutputType.Union ? "string | Date"
            : throw new ArgumentException();
    }
}