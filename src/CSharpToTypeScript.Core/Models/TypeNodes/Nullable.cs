using System.Collections.Generic;
using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class Nullable : TypeNode
    {
        public Nullable(TypeNode of)
        {
            Of = of;
        }

        public TypeNode Of { get; }

        public override IEnumerable<string> Requires => Of.Requires;

        public override bool IsUnionType(CodeConversionOptions options) => true;

        public override bool IsOptional(CodeConversionOptions options, out TypeNode of)
        {
            of = Of;
            return options.ConvertNullablesTo == NullableOutputType.Undefined;
        }

        public override string WriteTypeScript(CodeConversionOptions options)
            => // underlying type
            Of.WriteTypeScript(options)
            + " | "
            // "null" type
            + (options.ConvertNullablesTo == NullableOutputType.Undefined ? "undefined" : "null");
    }
}