using System.Collections.Generic;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    internal class Array : TypeNode
    {
        public Array(TypeNode of, int rank)
        {
            Of = of;
            Rank = rank;
        }

        public TypeNode Of { get; }
        public int Rank { get; }

        public override IEnumerable<string> Requires => Of.Requires;

        public override string WriteTypeScript(CodeConversionOptions options, Context context)
            => // underlying type
            Of.WriteTypeScript(options, context).TransformIf(Of.IsUnionType(options), StringUtilities.Parenthesize)
            // brackets
            + "[]".Repeat(Rank);
    }
}