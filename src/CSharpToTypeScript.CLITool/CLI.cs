using System;
using System.ComponentModel.DataAnnotations;
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

        [Option(ShortName = "ts", Description = "Number of spaces per tab")]
        [Range(1, 8)]
        public int TabSize { get; set; } = 4;

        [Option(ShortName = "se", Description = "Skip 'export' keyword")]
        public bool SkipExport { get; set; }

        [Option(ShortName = "k", Description = "Use kebab case for output file names")]
        public bool UseKebabCase { get; set; }

        [Option(ShortName = "m", Description = "Append '.model' suffix to output file names")]
        public bool AppendModelSuffix { get; set; }

        [Option(ShortName = "c", Description = "Clear output directory")]
        public bool ClearOutputDirectory { get; set; }

        [Option(ShortName = "a", Description = "Use Angular style conventions")]
        public bool AngularMode { get; set; }

        [Option(ShortName = "p", Description = "Override only part of output file between marker comments")]
        public bool PartialOverride { get; set; }

        [Option(ShortName = "pc", Description = "Don't convert field names to camel case")]
        public bool PreserveCasing { get; set; }

        [Option(ShortName = "d", Description = "Set output type for dates",
        ValueName = nameof(DateOutputType.String) + "|" + nameof(DateOutputType.Date) + "|" + nameof(DateOutputType.Union))]
        public DateOutputType ConvertDatesTo { get; set; }

        [Option(ShortName = "n", Description = "Set output type for nullables",
        ValueName = nameof(NullableOutputType.Null) + "|" + nameof(NullableOutputType.Undefined))]
        public NullableOutputType ConvertNullablesTo { get; set; }

        public CodeConversionOptions CodeConversionOptions => new CodeConversionOptions(!SkipExport, UseTabs, TabSize, ConvertDatesTo, ConvertNullablesTo, !PreserveCasing);
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

            CreateOrUpdateFile(outputPath, converted, PartialOverride);
        }

        private void OnInputIsDirectory()
        {
            var files = FileSystem.GetFilesWithExtension(Input, "cs")
                .Select(f => new
                {
                    OutputPath = GetOutputFilePath(f, Output, FileNameConversionOptions),
                    Content = _codeConverter.ConvertToTypeScript(File.ReadAllText(f), CodeConversionOptions)
                })
                .Where(f => !string.IsNullOrWhiteSpace(f.Content))
                .GroupBy(f => f.OutputPath)
                .Select(g => g.First());

            foreach (var file in files)
            {
                CreateOrUpdateFile(file.OutputPath, file.Content, PartialOverride);
            }
        }

        private void CreateOrUpdateFile(string path, string content, bool partialOverride)
        {
            FileSystem.EnsureDirectoryExists(path.ContainingDirectory());

            if (partialOverride)
            {
                content = Marker.Update(File.Exists(path) ? File.ReadAllText(path) : string.Empty, content);
            }

            File.WriteAllText(path, content);
        }

        private string GetOutputFilePath(string input, string output, FileNameConversionOptions options)
            => !input.EndsWithFileExtension() ? throw new ArgumentException()
            : output?.EndsWithFileExtension() == true ? output
            : !string.IsNullOrWhiteSpace(output) ? Path.Join(output, _fileNameConverter.ConvertToTypeScript(input, options))
            : Path.Join(input.ContainingDirectory(), _fileNameConverter.ConvertToTypeScript(input, options));
    }
}