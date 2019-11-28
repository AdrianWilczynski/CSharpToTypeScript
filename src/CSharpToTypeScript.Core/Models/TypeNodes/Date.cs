using System;
using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class Date : TypeNode
    {
        public override bool IsUnionType(CodeConversionOptions options)
            => options.ConvertDatesTo == DateOutputType.Union;

        public override string WriteTypeScript(CodeConversionOptions options, Context context)
            => options.ConvertDatesTo switch
            {
                DateOutputType.String => "string",
                DateOutputType.Date => "Date",
                DateOutputType.Union => "string | Date",
                _ => throw new ArgumentException("Unknown date output type.")
            };
    }
}