using System.IO;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Transformations;

namespace CSharpToTypeScript.Core.Services
{
    internal class FileNameConverter : IFileNameConverter
    {
        public string ConvertToTypeScript(string fileName, ModuleNameConversionOptions options)
            => ModuleNameTransformation.Transform(Path.GetFileNameWithoutExtension(fileName), options) + ".ts";
    }
}