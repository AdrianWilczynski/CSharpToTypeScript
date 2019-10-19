using System.IO;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Services
{
    internal class FileNameConverter : IFileNameConverter
    {
        public string ConvertToTypeScript(string fileName, FileNameConversionOptions options)
        {
            var convertedName = Path.GetFileNameWithoutExtension(fileName);
            convertedName = convertedName.HasInterfacePrefix() ? convertedName.RemoveInterfacePrefix() : convertedName;
            convertedName = options.UseKebabCase ? convertedName.ToKebabCase() : convertedName.ToCamelCase();
            convertedName = options.AppendModelSuffix ? convertedName + ".model" : convertedName;
            convertedName += ".ts";

            return convertedName;
        }
    }
}