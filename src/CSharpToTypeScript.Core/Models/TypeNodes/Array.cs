using System.Linq;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    public class Array : TypeNode
    {
        public Array(TypeNode of, int rank)
        {
            Of = of;
            Rank = rank;
        }

        public TypeNode Of { get; }
        public int Rank { get; }

        public override string WriteTypeScript()
            => Of.WriteTypeScript().TransformIf(Of.IsUnionType, StringUtilities.Parenthesize) + "[]".Repeat(Rank);
    }
}