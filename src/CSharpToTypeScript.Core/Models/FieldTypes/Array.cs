using System.Linq;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.FieldTypes
{
    public class Array : IFieldType
    {
        public Array(IFieldType of, int rank)
        {
            Of = of;
            Rank = rank;
        }

        public IFieldType Of { get; }
        public int Rank { get; }

        public bool RequiresParenthesis => Of is Nullable;

        public override string ToString()
            => $"{(RequiresParenthesis ? ParenthesizedOf : Of.ToString())}{Brackets}";

        private string ParenthesizedOf => $"({Of})";
        private string Brackets => "[]".Repeat(Rank);
    }
}