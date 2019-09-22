using System.IO;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Services
{
    public class FileNameConverter : IFileNameConverter
    {
        public string ConvertToTypeScript(string fileName, FileNameConversionOptions options)
        {
            var convertedName = Path.GetFileNameWithoutExtension(fileName);
            convertedName = options.UseKebabCase ? convertedName.ToKebabCase() : convertedName.ToCamelCase();
            convertedName = options.AppendModelSuffix ? convertedName + ".model" : convertedName;
            convertedName += ".ts";

            return convertedName;
        }
    }
}