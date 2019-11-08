using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Transformations
{
    internal static class ModuleNameTransformation
    {
        public static string Transform(string name, ModuleNameConversionOptions options)
            => name.TransformIf(options.RemoveInterfacePrefix, StringUtilities.RemoveInterfacePrefix)
                .TransformIfElse(options.UseKebabCase, StringUtilities.ToKebabCase, StringUtilities.ToCamelCase)
                .TransformIf(options.AppendModelSuffix, n => n + ".model");
    }
}