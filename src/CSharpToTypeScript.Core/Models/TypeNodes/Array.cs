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

        public override string WriteTypeScript(CodeConversionOptions options)
            => Of.WriteTypeScript(options).TransformIf(Of.IsUnionType(options), StringUtilities.Parenthesize) + "[]".Repeat(Rank);
    }
}