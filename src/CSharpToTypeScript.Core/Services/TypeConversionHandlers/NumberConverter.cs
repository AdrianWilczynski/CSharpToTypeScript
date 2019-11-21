using System.Collections.Generic;
using CSharpToTypeScript.Core.Constants;
using CSharpToTypeScript.Core.Models.TypeNodes;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers
{
    internal class NumberConverter : BasicTypeConverterBase<Number>
    {
        protected override IEnumerable<string> ConvertibleFromPredefined => new[]
        {
            PredefinedTypes.Byte, PredefinedTypes.SByte, PredefinedTypes.Int, PredefinedTypes.UInt,
            PredefinedTypes.Long, PredefinedTypes.ULong, PredefinedTypes.Float, PredefinedTypes.Double,
            PredefinedTypes.Decimal, PredefinedTypes.Short, PredefinedTypes.UShort
        };

        protected override IEnumerable<string> ConvertibleFromIdentified => new[]
        {
            nameof(System.Byte), nameof(System.SByte), nameof(System.Decimal), nameof(System.Double), nameof(System.Single),
            nameof(System.Int32), nameof(System.UInt32), nameof(System.Int64), nameof(System.UInt64),
            nameof(System.Int16), nameof(System.UInt16)
        };
    }
}