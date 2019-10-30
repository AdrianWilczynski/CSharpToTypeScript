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

        public override bool IsUnionType(CodeConversionOptions options) => true;

        public override string WriteTypeScript(CodeConversionOptions options)
            => Of.WriteTypeScript(options) + " | null";
    }
}