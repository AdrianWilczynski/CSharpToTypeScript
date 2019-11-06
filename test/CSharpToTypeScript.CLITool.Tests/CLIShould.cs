using System.IO;
using System.Linq;
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

        private void Prepare(string directory)
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }

            Directory.CreateDirectory(directory);
        }

        [Fact]
        public void ConvertSingleSimpleFile()
        {
            Prepare(nameof(ConvertSingleSimpleFile));

            var originalFilePath = Path.Join(nameof(ConvertSingleSimpleFile), "SimpleItem.cs");

            File.WriteAllText(originalFilePath, "class SimpleItem { }");

            _cli.Input = originalFilePath;

            _cli.OnExecute();

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

            _cli.Input = nameof(ConvertDirectory);

            _cli.OnExecute();

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

            File.WriteAllText("File1.cs", "class Item4 { }");
            File.WriteAllText("File2.cs", "class Item5 { }");

            _cli.OnExecute();

            Assert.True(File.Exists("file1.ts"));
            Assert.True(File.Exists("file2.ts"));

            Directory.SetCurrentDirectory("..");
        }

        [Fact]
        public void ConvertSingleFileIntoProvidedOutputFile()
        {
            Prepare(nameof(ConvertSingleFileIntoProvidedOutputFile));

            var inputFilePath = Path.Join(nameof(ConvertSingleFileIntoProvidedOutputFile), "File.cs");
            var outputFilePath = Path.Join(nameof(ConvertSingleFileIntoProvidedOutputFile), "converted.ts");

            File.WriteAllText(inputFilePath, "class Item6 { }");

            _cli.Input = inputFilePath;
            _cli.Output = outputFilePath;

            _cli.OnExecute();

            Assert.True(File.Exists(outputFilePath));
        }

        [Fact]
        public void ConvertSingleFileIntoProvidedOutputDirectory()
        {
            Prepare(nameof(ConvertSingleFileIntoProvidedOutputDirectory));

            var inputFilePath = Path.Join(nameof(ConvertSingleFileIntoProvidedOutputDirectory), "File.cs");
            var outputDirectoryPath = Path.Join(nameof(ConvertSingleFileIntoProvidedOutputDirectory), "models");

            File.WriteAllText(inputFilePath, "class Item7 { }");

            _cli.Input = inputFilePath;
            _cli.Output = outputDirectoryPath;

            _cli.OnExecute();

            Assert.True(File.Exists(Path.Join(outputDirectoryPath, "file.ts")));
        }

        [Fact]
        public void ConvertDirectoryIntoProvidedOutputDirectory()
        {
            Prepare(nameof(ConvertDirectoryIntoProvidedOutputDirectory));

            var inputDirectoryPath = Path.Join(nameof(ConvertDirectoryIntoProvidedOutputDirectory), "Input");
            Directory.CreateDirectory(inputDirectoryPath);

            Directory.SetCurrentDirectory(inputDirectoryPath);

            File.WriteAllText("File1.cs", "class Item8 { }");
            File.WriteAllText("File2.cs", "class Item9 { }");
            File.WriteAllText("File3.cs", "class Item10 { }");

            var outputDirectoryPath = Path.Join("..", "Output");

            _cli.Input = ".";
            _cli.Output = outputDirectoryPath;

            _cli.OnExecute();

            var convertedFiles = Directory.GetFiles(outputDirectoryPath)
                .Where(f => f.EndsWith(".ts"))
                .Select(Path.GetFileName);

            Assert.Equal(new[] { "file1.ts", "file2.ts", "file3.ts" }, convertedFiles);

            Directory.SetCurrentDirectory(Path.Join("..", ".."));
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

            _cli.Input = originalFilePath;
            _cli.AngularMode = true;

            _cli.OnExecute();

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

            _cli.Input = originalFilePath;
            _cli.Output = outputDirectoryPath;
            _cli.ClearOutputDirectory = true;

            _cli.OnExecute();

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

            _cli.Input = inputDirectoryPath;
            _cli.Output = outputDirectoryPath;
            _cli.ClearOutputDirectory = true;

            _cli.OnExecute();

            Assert.True(File.Exists(undesiredFilePath));

            undesiredFilePath = Path.Join(inputDirectoryPath, "trash.ts");
            File.WriteAllText(undesiredFilePath, "export interface Trash { }");

            _cli.Output = inputDirectoryPath;

            _cli.OnExecute();

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

            _cli.Input = inputFilePath;
            _cli.Output = outputFilePath;
            _cli.PartialOverride = true;

            _cli.OnExecute();

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

            _cli.Input = originalFilePath;
            _cli.Output = outputFilePath;
            _cli.PreserveCasing = true;

            _cli.OnExecute();

            Assert.Contains("MyProperty: number;", File.ReadAllText(outputFilePath));
        }

        [Fact]
        public void PreserveInterfacePrefix()
        {
            Prepare(nameof(PreserveInterfacePrefix));

            var originalFilePath = Path.Join(nameof(PreserveInterfacePrefix), "IItemBase.cs");

            File.WriteAllText(originalFilePath, "interface IItemBase { }");

            _cli.Input = originalFilePath;
            _cli.PreserveInterfacePrefix = true;

            _cli.OnExecute();

            var outputFilePath = Path.Join(nameof(PreserveInterfacePrefix), "iItemBase.ts");

            Assert.True(File.Exists(outputFilePath));
            Assert.Contains("export interface IItemBase", File.ReadAllText(outputFilePath));
        }
    }
}