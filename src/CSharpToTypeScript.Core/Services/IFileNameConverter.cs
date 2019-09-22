namespace CSharpToTypeScript.Core.Services
{
    public interface IFileNameConverter
    {
        string ConvertToTypeScript(string fileName, Options.FileNameConversionOptions options);
    }
}