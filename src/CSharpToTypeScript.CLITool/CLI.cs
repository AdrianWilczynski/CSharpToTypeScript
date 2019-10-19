using System;
using System.IO;
using System.Linq;
using CSharpToTypeScript.CLITool.Conventions;
using CSharpToTypeScript.CLITool.Services;
using CSharpToTypeScript.CLITool.Validation;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Services;
using CSharpToTypeScript.Core.Utilities;
using McMaster.Extensions.CommandLineUtils;

namespace CSharpToTypeScript.CLITool
{
    [OutputMatchesInput]
    public class CLI
    {
        private readonly IFileSystem _fileSystem;
        private readonly ICodeConverter _codeConverter;
        private readonly IFileNameConverter _fileNameConverter;

        public CLI(IFileSystem fileSystem, ICodeConverter codeConverter, IFileNameConverter fileNameConverter)
        {
            _fileSystem = fileSystem;
            _codeConverter = codeConverter;
            _fileNameConverter = fileNameConverter;
        }

        [Argument(0, Description = "Input file or directory path")]
        [FileOrDirectoryExists]
        public string Input { get; set; } = ".";

        [Option(ShortName = "o", Description = "Output file or directory path")]
        public string Output { get; set; }

        [Option(ShortName = "t", Description = "Use tabs for indentation")]
        public bool UseTabs { get; set; }

        [Option(ShortName = "s", Description = "Number of spaces per tab")]
        public int TabSize { get; set; } = 4;

        [Option(ShortName = "e", Description = "Emit \"export\" keyword")]
        public bool Export { get; set; } = true;

        [Option(ShortName = "k", Description = "Use kebab case for output file names")]
        public bool UseKebabCase { get; set; }

        [Option(ShortName = "m", Description = "Append \".model\" suffix to output file names")]
        public bool AppendModelSuffix { get; set; }

        [Option(ShortName = "c", Description = "Clear output directory")]
        public bool ClearOutputDirectory { get; set; }

        [Option(ShortName = "a", Description = "Use Angular style conventions")]
        public bool AngularMode { get; set; }

        public CodeConversionOptions CodeConversionOptions => new CodeConversionOptions(Export, UseTabs, TabSize);
        public FileNameConversionOptions FileNameConversionOptions => new FileNameConversionOptions(UseKebabCase, AppendModelSuffix);

        public void OnExecute()
        {
            if (AngularMode)
            {
                AngularConventions.Override(this);
            }

            if (ClearOutputDirectory && !string.IsNullOrWhiteSpace(Output) && _fileSystem.IsExistingDirectory(Output))
            {
                _fileSystem.ClearOutputIfPossible(Input, Output);
            }

            if (Input.EndsWithFileExtension() && _fileSystem.IsExistingFile(Input))
            {
                OnInputIsFile();
            }
            else if (!Input.EndsWithFileExtension() && _fileSystem.IsExistingDirectory(Input))
            {
                OnInputIsDirectory();
            }
        }

        private void OnInputIsFile()
        {
            var content = _fileSystem.ReadAllText(Input);
            var converted = _codeConverter.ConvertToTypeScript(content, CodeConversionOptions);
            var outputPath = GetOutputFilePath(Input, Output, FileNameConversionOptions);

            _fileSystem.WriteAllText(outputPath, converted);
        }

        private void OnInputIsDirectory()
        {
            _fileSystem.GetCSharpFiles(Input)
                .AsParallel()
                .Select(f => new
                {
                    OutputPath = GetOutputFilePath(f, Output, FileNameConversionOptions),
                    Content = _codeConverter.ConvertToTypeScript(_fileSystem.ReadAllText(f), CodeConversionOptions)
                })
                .ForAll(f => _fileSystem.WriteAllText(f.OutputPath, f.Content));
        }

        private string GetOutputFilePath(string input, string output, FileNameConversionOptions options)
        {
            if (output is string && output.EndsWithFileExtension())
            {
                return output;
            }
            else if (output is string && input.EndsWithFileExtension())
            {
                return Path.Join(output, _fileNameConverter.ConvertToTypeScript(input, options));
            }
            else if (input.EndsWithFileExtension())
            {
                return Path.Join(new FileInfo(input).DirectoryName, _fileNameConverter.ConvertToTypeScript(input, options));
            }

            throw new ArgumentException();
        }
    }
}