using System;
using System.IO;
using System.Linq;
using CSharpToTypeScript.CLITool.Conventions;
using CSharpToTypeScript.CLITool.Utilities;
using CSharpToTypeScript.CLITool.Validation;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Services;
using McMaster.Extensions.CommandLineUtils;

namespace CSharpToTypeScript.CLITool
{
    [Command(Name = "cs2ts", Description = "Convert C# Models, ViewModels and DTOs into their TypeScript equivalents")]
    [OutputMatchesInput]
    public class CLI
    {
        private readonly ICodeConverter _codeConverter;
        private readonly IFileNameConverter _fileNameConverter;

        public CLI(ICodeConverter codeConverter, IFileNameConverter fileNameConverter)
        {
            _codeConverter = codeConverter;
            _fileNameConverter = fileNameConverter;
        }

        [Argument(0, Description = "Input file or directory path")]
        [InputExists]
        public string Input { get; set; } = ".";

        [Option(ShortName = "o", Description = "Output file or directory path")]
        public string Output { get; set; }

        [Option(ShortName = "t", Description = "Use tabs for indentation")]
        public bool UseTabs { get; set; }

        [Option(ShortName = "s", Description = "Number of spaces per tab")]
        public int TabSize { get; set; } = 4;

        [Option(ShortName = "e", Description = "Emit 'export' keyword")]
        public bool Export { get; set; } = true;

        [Option(ShortName = "k", Description = "Use kebab case for output file names")]
        public bool UseKebabCase { get; set; }

        [Option(ShortName = "m", Description = "Append '.model' suffix to output file names")]
        public bool AppendModelSuffix { get; set; }

        [Option(ShortName = "c", Description = "Clear output directory")]
        public bool ClearOutputDirectory { get; set; }

        [Option(ShortName = "a", Description = "Use Angular style conventions")]
        public bool AngularMode { get; set; }

        public string MyProperty { get; set; } = Directory.GetCurrentDirectory();

        public CodeConversionOptions CodeConversionOptions => new CodeConversionOptions(Export, UseTabs, TabSize);
        public FileNameConversionOptions FileNameConversionOptions => new FileNameConversionOptions(UseKebabCase, AppendModelSuffix);

        public void OnExecute()
        {
            if (AngularMode)
            {
                AngularConventions.Override(this);
            }

            if (ClearOutputDirectory && !string.IsNullOrWhiteSpace(Output)
            && Directory.Exists(Output) && !Output.IsSameOrParrentDirectory(Input))
            {
                Directory.Delete(Output, true);
            }

            if (Input.EndsWithFileExtension() && File.Exists(Input))
            {
                OnInputIsFile();
            }
            else if (!Input.EndsWithFileExtension() && Directory.Exists(Input))
            {
                OnInputIsDirectory();
            }
        }

        private void OnInputIsFile()
        {
            var content = File.ReadAllText(Input);
            var converted = _codeConverter.ConvertToTypeScript(content, CodeConversionOptions);
            var outputPath = GetOutputFilePath(Input, Output, FileNameConversionOptions);

            FileSystem.EnsureDirectoryExists(outputPath.ContainingDirectory());
            File.WriteAllText(outputPath, converted);
        }

        private void OnInputIsDirectory()
        {
            var files = FileSystem.GetFilesWithExtension(Input, "cs")
                .Select(f => new
                {
                    OutputPath = GetOutputFilePath(f, Output, FileNameConversionOptions),
                    Content = _codeConverter.ConvertToTypeScript(File.ReadAllText(f), CodeConversionOptions)
                })
                .GroupBy(f => f.OutputPath)
                .Select(g => g.First());

            foreach (var file in files)
            {
                FileSystem.EnsureDirectoryExists(file.OutputPath.ContainingDirectory());
                File.WriteAllText(file.OutputPath, file.Content);
            }
        }

        private string GetOutputFilePath(string input, string output, FileNameConversionOptions options)
            => !input.EndsWithFileExtension() ? throw new ArgumentException()
            : output?.EndsWithFileExtension() == true ? output
            : !string.IsNullOrWhiteSpace(output) ? Path.Join(output, _fileNameConverter.ConvertToTypeScript(input, options))
            : Path.Join(input.ContainingDirectory(), _fileNameConverter.ConvertToTypeScript(input, options));
    }
}