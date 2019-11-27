using System.IO;
using System.Linq;
using CSharpToTypeScript.CLITool.Commands;
using CSharpToTypeScript.CLITool.Utilities;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Services;
using Moq;
using Xunit;

namespace CSharpToTypeScript.CLITool.Tests
{
    [Collection(nameof(CLITool))]
    public class ConvertCommandShould : CLITestBase, IClassFixture<CLIFixture>
    {
        private readonly ConvertCommand _convertCommand;

        public ConvertCommandShould(CLIFixture fixture)
        {
            _convertCommand = fixture.ConvertCommand;
        }

        [Fact]
        public void ConvertSingleSimpleFile()
        {
            Prepare(nameof(ConvertSingleSimpleFile));

            var originalFilePath = Path.Join(nameof(ConvertSingleSimpleFile), "SimpleItem.cs");

            File.WriteAllText(originalFilePath, "class SimpleItem { }");

            _convertCommand.Input = originalFilePath;

            _convertCommand.OnExecute();

            var generatedFilePath = Path.Join(nameof(ConvertSingleSimpleFile), "simpleItem.ts");

            Assert.True(File.Exists(generatedFilePath));
            Assert.Equal("export interface SimpleItem {\r\n\r\n}", File.ReadAllText(generatedFilePath));
        }

        [Fact]
        public void ConvertDirectory()
        {
            Prepare(nameof(ConvertDirectory));

            File.WriteAllText(Path.Join(nameof(ConvertDirectory), "File1.cs"), "class Item1 { }");
            File.WriteAllText(Path.Join(nameof(ConvertDirectory), "File2.cs"), "class Item2 { }");
            File.WriteAllText(Path.Join(nameof(ConvertDirectory), "File3.cs"), "class Item3 { }");

            _convertCommand.Input = nameof(ConvertDirectory);

            _convertCommand.OnExecute();

            var convertedFiles = Directory.GetFiles(nameof(ConvertDirectory))
                .Where(f => f.EndsWith(".ts"))
                .Select(Path.GetFileName);

            Assert.Equal(new[] { "file1.ts", "file2.ts", "file3.ts" }, convertedFiles);
        }

        [Fact]
        public void ConvertCurrentDirectoryWhenNoInputProvided()
        {
            Prepare(nameof(ConvertCurrentDirectoryWhenNoInputProvided));

            Directory.SetCurrentDirectory(nameof(ConvertCurrentDirectoryWhenNoInputProvided));

            try
            {
                File.WriteAllText("File1.cs", "class Item4 { }");
                File.WriteAllText("File2.cs", "class Item5 { }");

                _convertCommand.OnExecute();

                Assert.True(File.Exists("file1.ts"));
                Assert.True(File.Exists("file2.ts"));
            }
            finally
            {
                Directory.SetCurrentDirectory("..");
            }
        }

        [Fact]
        public void ConvertSingleFileIntoProvidedOutputFile()
        {
            Prepare(nameof(ConvertSingleFileIntoProvidedOutputFile));

            var inputFilePath = Path.Join(nameof(ConvertSingleFileIntoProvidedOutputFile), "File.cs");
            var outputFilePath = Path.Join(nameof(ConvertSingleFileIntoProvidedOutputFile), "converted.ts");

            File.WriteAllText(inputFilePath, "class Item6 { }");

            _convertCommand.Input = inputFilePath;
            _convertCommand.Output = outputFilePath;

            _convertCommand.OnExecute();

            Assert.True(File.Exists(outputFilePath));
        }

        [Fact]
        public void ConvertSingleFileIntoProvidedOutputDirectory()
        {
            Prepare(nameof(ConvertSingleFileIntoProvidedOutputDirectory));

            var inputFilePath = Path.Join(nameof(ConvertSingleFileIntoProvidedOutputDirectory), "File.cs");
            var outputDirectoryPath = Path.Join(nameof(ConvertSingleFileIntoProvidedOutputDirectory), "models");

            File.WriteAllText(inputFilePath, "class Item7 { }");

            _convertCommand.Input = inputFilePath;
            _convertCommand.Output = outputDirectoryPath;

            _convertCommand.OnExecute();

            Assert.True(File.Exists(Path.Join(outputDirectoryPath, "file.ts")));
        }

        [Fact]
        public void ConvertDirectoryIntoProvidedOutputDirectory()
        {
            Prepare(nameof(ConvertDirectoryIntoProvidedOutputDirectory));

            var inputDirectoryPath = Path.Join(nameof(ConvertDirectoryIntoProvidedOutputDirectory), "Input");
            Directory.CreateDirectory(inputDirectoryPath);

            Directory.SetCurrentDirectory(inputDirectoryPath);

            try
            {
                File.WriteAllText("File1.cs", "class Item8 { }");
                File.WriteAllText("File2.cs", "class Item9 { }");
                File.WriteAllText("File3.cs", "class Item10 { }");

                var outputDirectoryPath = Path.Join("..", "Output");

                _convertCommand.Input = ".";
                _convertCommand.Output = outputDirectoryPath;

                _convertCommand.OnExecute();

                var convertedFiles = Directory.GetFiles(outputDirectoryPath)
                    .Where(f => f.EndsWith(".ts"))
                    .Select(Path.GetFileName);

                Assert.Equal(new[] { "file1.ts", "file2.ts", "file3.ts" }, convertedFiles);
            }
            finally
            {
                Directory.SetCurrentDirectory(Path.Join("..", ".."));
            }
        }

        [Fact]
        public void UseAngularConventionsWhenRequested()
        {
            Prepare(nameof(UseAngularConventionsWhenRequested));

            var originalFilePath = Path.Join(nameof(UseAngularConventionsWhenRequested), "ShoppingCartItem.cs");

            File.WriteAllText(originalFilePath, @"class ShoppingCartItem
{
    public int Id { get; set; }
}");

            _convertCommand.Input = originalFilePath;
            _convertCommand.AngularMode = true;

            _convertCommand.OnExecute();

            var generatedFilePath = Path.Join(nameof(UseAngularConventionsWhenRequested), "shopping-cart-item.model.ts");

            Assert.True(File.Exists(generatedFilePath));
            Assert.Equal(@"export interface ShoppingCartItem {
  id: number;
}",
            File.ReadAllText(generatedFilePath));
        }

        [Fact]
        public void ClearOutputDirectory()
        {
            Prepare(nameof(ClearOutputDirectory));

            var originalFilePath = Path.Join(nameof(ClearOutputDirectory), "Item.cs");
            var outputDirectoryPath = Path.Join(nameof(ClearOutputDirectory), "Output");

            var undesiredFilePath = Path.Join(outputDirectoryPath, "garbage.ts");

            Directory.CreateDirectory(outputDirectoryPath);

            File.WriteAllText(originalFilePath, "class Item11 { }");
            File.WriteAllText(undesiredFilePath, "export interface Garbage { }");

            _convertCommand.Input = originalFilePath;
            _convertCommand.Output = outputDirectoryPath;
            _convertCommand.ClearOutputDirectory = true;

            _convertCommand.OnExecute();

            Assert.False(File.Exists(undesiredFilePath));
        }

        [Fact]
        public void IgnoreClearOutputSettingIfUnsafe()
        {
            Prepare(nameof(IgnoreClearOutputSettingIfUnsafe));

            var outputDirectoryPath = Path.Join(nameof(IgnoreClearOutputSettingIfUnsafe), "Parrent");
            var inputDirectoryPath = Path.Join(outputDirectoryPath, "Input");

            Directory.CreateDirectory(outputDirectoryPath);
            Directory.CreateDirectory(inputDirectoryPath);

            var undesiredFilePath = Path.Join(outputDirectoryPath, "garbage.ts");
            File.WriteAllText(undesiredFilePath, "export interface Garbage { }");

            _convertCommand.Input = inputDirectoryPath;
            _convertCommand.Output = outputDirectoryPath;
            _convertCommand.ClearOutputDirectory = true;

            _convertCommand.OnExecute();

            Assert.True(File.Exists(undesiredFilePath));

            undesiredFilePath = Path.Join(inputDirectoryPath, "trash.ts");
            File.WriteAllText(undesiredFilePath, "export interface Trash { }");

            _convertCommand.Output = inputDirectoryPath;

            _convertCommand.OnExecute();

            Assert.True(File.Exists(undesiredFilePath));
        }

        [Fact]
        public void PreserveContentOutsideOfMarkerComments()
        {
            Prepare(nameof(PreserveContentOutsideOfMarkerComments));

            var inputFilePath = Path.Join(nameof(PreserveContentOutsideOfMarkerComments), "Item.cs");
            var outputFilePath = Path.Join(nameof(PreserveContentOutsideOfMarkerComments), "item.ts");

            File.WriteAllText(outputFilePath, @"// above

// @cs2ts-begin-auto-generated
export interface Item {

}
// @cs2ts-end-auto-generated

// below");
            File.WriteAllText(inputFilePath, "class UpdatedItem { }");

            _convertCommand.Input = inputFilePath;
            _convertCommand.Output = outputFilePath;
            _convertCommand.PartialOverride = true;

            _convertCommand.OnExecute();

            var overriddenOutput = File.ReadAllText(outputFilePath);

            Assert.Contains("// above", overriddenOutput);
            Assert.Contains("// below", overriddenOutput);
            Assert.Contains("interface UpdatedItem", overriddenOutput);
        }

        [Fact]
        public void PreserveCasing()
        {
            Prepare(nameof(PreserveCasing));

            var originalFilePath = Path.Join(nameof(PreserveCasing), "Item.cs");
            var outputFilePath = Path.Join(nameof(PreserveCasing), "item.ts");

            File.WriteAllText(originalFilePath, @"class Item12
{
    public int MyProperty { get; set; }
}");

            _convertCommand.Input = originalFilePath;
            _convertCommand.Output = outputFilePath;
            _convertCommand.PreserveCasing = true;

            _convertCommand.OnExecute();

            Assert.Contains("MyProperty: number;", File.ReadAllText(outputFilePath));
        }

        [Fact]
        public void PreserveInterfacePrefix()
        {
            Prepare(nameof(PreserveInterfacePrefix));

            var originalFilePath = Path.Join(nameof(PreserveInterfacePrefix), "IItemBase.cs");

            File.WriteAllText(originalFilePath, "interface IItemBase { }");

            _convertCommand.Input = originalFilePath;
            _convertCommand.PreserveInterfacePrefix = true;

            _convertCommand.OnExecute();

            var outputFilePath = Path.Join(nameof(PreserveInterfacePrefix), "iItemBase.ts");

            Assert.True(File.Exists(outputFilePath));
            Assert.Contains("export interface IItemBase", File.ReadAllText(outputFilePath));
        }

        [Fact]
        public void ConvertFilesInNestedDirecotories()
        {
            Prepare(nameof(ConvertFilesInNestedDirecotories));

            var sourceFilePath = Path.Join(nameof(ConvertFilesInNestedDirecotories), "Item.cs");

            var nestedDirectoryPath = Path.Join(nameof(ConvertFilesInNestedDirecotories), "Nested");
            var nestedSourceFilePath = Path.Join(nestedDirectoryPath, "NestedItem.cs");

            File.WriteAllText(sourceFilePath, "class Item13 { }");

            Directory.CreateDirectory(nestedDirectoryPath);
            File.WriteAllText(nestedSourceFilePath, "class Item14 { }");

            _convertCommand.Input = nameof(ConvertFilesInNestedDirecotories);

            _convertCommand.OnExecute();

            var outputFilePath = Path.Join(nameof(ConvertFilesInNestedDirecotories), "item.ts");
            var nestedOutputFilePath = Path.Join(nestedDirectoryPath, "nestedItem.ts");

            Assert.True(File.Exists(outputFilePath));
            Assert.True(File.Exists(nestedOutputFilePath));
        }

        [Fact]
        public void GenerateSimpleImports()
        {
            Prepare(nameof(GenerateSimpleImports));

            var sourceFilePath = Path.Join(nameof(GenerateSimpleImports), "Item.cs");

            File.WriteAllText(sourceFilePath, @"class Item15
{
    public ShoppingCartItem MyProperty { get; set; }
}");

            _convertCommand.Input = sourceFilePath;
            _convertCommand.ImportGeneration = ImportGenerationMode.Simple;
            _convertCommand.UseKebabCase = true;
            _convertCommand.AppendModelSuffix = true;

            _convertCommand.OnExecute();

            Assert.StartsWith("import { ShoppingCartItem } from \"./shopping-cart-item.model\";",
                File.ReadAllText(Path.Join(nameof(GenerateSimpleImports), "item.model.ts")));
        }

        [Fact]
        public void UseOptionsFromConfigurationFile()
        {
            Prepare(nameof(UseOptionsFromConfigurationFile));

            Directory.SetCurrentDirectory(nameof(UseOptionsFromConfigurationFile));

            try
            {
                var codeConverterMock = new Mock<ICodeConverter>();
                codeConverterMock
                    .Setup(c => c.ConvertToTypeScript(It.IsAny<string>(), It.IsAny<CodeConversionOptions>()))
                    .Returns("interface Item16 { }");

                var fileNameConverterMock = new Mock<IFileNameConverter>();
                fileNameConverterMock
                    .Setup(f => f.ConvertToTypeScript(It.IsAny<string>(), It.IsAny<ModuleNameConversionOptions>()))
                    .Returns("item.ts");

                File.WriteAllText(ConfigurationFile.FileName, "{ \"preserveCasing\": true }");

                File.WriteAllText("Item.cs", "class Item16 { }");

                var command = new ConvertCommand(codeConverterMock.Object, fileNameConverterMock.Object);

                command.OnExecute();

                Assert.True(command.PreserveCasing);
                codeConverterMock.Verify(c =>
                    c.ConvertToTypeScript(
                        It.IsAny<string>(),
                        It.Is<CodeConversionOptions>(o => !o.ToCamelCase)),
                    Times.Once);
            }
            finally
            {
                Directory.SetCurrentDirectory("..");
            }
        }
    }
}