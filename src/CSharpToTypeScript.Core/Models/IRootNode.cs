using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Models
{
    public interface IRootNode
    {
        string WriteTypeScript(CodeConversionOptions options);
    }
}