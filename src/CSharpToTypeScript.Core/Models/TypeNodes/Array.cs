using System.Linq;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.TypeNodes
{
    public class Array : ITypeNode
    {
        public Array(ITypeNode of, int rank)
        {
            Of = of;
            Rank = rank;
        }

        public ITypeNode Of { get; }
        public int Rank { get; }

        public string WriteTypeScript()
            => Of.WriteTypeScript().TransformIf(Of is Nullable, StringUtilities.Parenthesize) + "[]".Repeat(Rank);
    }
}