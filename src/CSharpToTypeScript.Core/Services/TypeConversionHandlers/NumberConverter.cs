using System.Collections.Generic;
using CSharpToTypeScript.Core.Models.TypeNodes;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers
{
    internal class NumberConverter : BasicTypeConverterBase<Number>
    {
        protected override IEnumerable<string> ConvertibleFrom { get; } = new List<string>
        {
            "Byte", "SByte", "Decimal", "Double", "Single", "Int32", "UInt32", "Int64", "UInt64", "Int16", "UInt16",
            "byte", "sbyte", "int", "uint", "long", "ulong", "float", "double", "decimal", "short", "ushort"
        };
    }
}