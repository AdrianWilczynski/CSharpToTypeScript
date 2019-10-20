using System;
using System.IO;
using System.Linq;
using CSharpToTypeScript.Core.DI;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CSharpToTypeScript.CLITool.Tests
{
    public class CLIShould : IClassFixture<CLIFixture>
    {
        private readonly CLI _cli;

        public CLIShould(CLIFixture fixture)
        {
            _cli = fixture.CLI;
        }

        [Fact]
        public void ConvertSingleSimpleFile()
        {
            if (Directory.Exists(nameof(ConvertSingleSimpleFile)))
            {
                Directory.Delete(nameof(ConvertSingleSimpleFile), true);
            }

            Directory.CreateDirectory(nameof(ConvertSingleSimpleFile));

            var originalFilePath = Path.Join(nameof(ConvertSingleSimpleFile), "Item.cs");
            File.WriteAllText(originalFilePath, "class Item { }");

            _cli.Input = originalFilePath;

            _cli.OnExecute();

            var generatedFilePath = Path.Join(nameof(ConvertSingleSimpleFile), "item.ts");

            Assert.True(File.Exists(generatedFilePath));
            Assert.Equal("export interface Item {" + Environment.NewLine + Environment.NewLine + "}", File.ReadAllText(generatedFilePath));
        }

        [Fact]
        public void ConvertDirectory()
        {
            if (Directory.Exists(nameof(ConvertDirectory)))
            {
                Directory.Delete(nameof(ConvertDirectory), true);
            }

            Directory.CreateDirectory(nameof(ConvertDirectory));

            File.WriteAllText(Path.Join(nameof(ConvertDirectory), "File1.cs"), string.Empty);
            File.WriteAllText(Path.Join(nameof(ConvertDirectory), "File2.cs"), string.Empty);
            File.WriteAllText(Path.Join(nameof(ConvertDirectory), "File3.cs"), string.Empty);

            _cli.Input = nameof(ConvertDirectory);

            _cli.OnExecute();

            var convertedFiles = Directory.GetFiles(nameof(ConvertDirectory))
                .Where(f => f.EndsWith(".ts"))
                .Select(Path.GetFileName);

            Assert.Equal(new[] { "file1.ts", "file2.ts", "file3.ts" }, convertedFiles);
        }
    }
}