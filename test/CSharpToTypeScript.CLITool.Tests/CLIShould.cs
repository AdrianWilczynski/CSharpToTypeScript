using System;
using System.IO;
using CSharpToTypeScript.Core.DI;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CSharpToTypeScript.CLITool.Tests
{
    public class CLIShould
    {
        private readonly CLI _cli;

        public CLIShould()
        {
            var serviceProvider = new ServiceCollection()
                .AddCSharpToTypeScript()
                .AddTransient<CLI>()
                .BuildServiceProvider();

            _cli = serviceProvider.GetRequiredService<CLI>();

            Directory.SetCurrentDirectory("./../../../scenarios");
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
    }
}