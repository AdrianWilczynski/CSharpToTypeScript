using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Models
{
    public interface IWritableNode
    {
        string WriteTypeScript(CodeConversionOptions options, Context context);
    }
}