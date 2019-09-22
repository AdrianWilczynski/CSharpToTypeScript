using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Models
{
    internal interface IRootNode
    {
        string WriteTypeScript(CodeConversionOptions options);
    }
}