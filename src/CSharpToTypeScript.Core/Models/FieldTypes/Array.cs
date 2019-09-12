using System.Linq;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.FieldTypes
{
    public class Array : FieldType
    {
        public Array(FieldType of, int rank)
        {
            Of = of;
            Rank = rank;
        }

        public FieldType Of { get; }
        public int Rank { get; }

        public override string WriteTypeScript()
            => Of.WriteTypeScript().TransformIf(Of.IsUnionType, StringUtilities.Parenthesize) + "[]".Repeat(Rank);
    }
}