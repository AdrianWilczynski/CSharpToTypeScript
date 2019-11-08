namespace CSharpToTypeScript.Core.Services
{
    public interface IFileNameConverter
    {
        string ConvertToTypeScript(string fileName, Options.ModuleNameConversionOptions options);
    }
}