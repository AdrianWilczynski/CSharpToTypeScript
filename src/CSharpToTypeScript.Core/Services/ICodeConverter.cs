using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Services
{
    public interface ICodeConverter
    {
        string ConvertToTypeScript(string code, CodeConversionOptions options);
    }
}