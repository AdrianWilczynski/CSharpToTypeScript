using CSharpToTypeScript.Core.DependencyInjection;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CSharpToTypeScript.Core.Tests
{
    public class FileNameConverterShould
    {
        private readonly IFileNameConverter _fileNameConverter;

        public FileNameConverterShould()
        {
            var serviceProvider = new ServiceCollection()
                .AddCSharpToTypeScript()
                .BuildServiceProvider();

            _fileNameConverter = serviceProvider.GetRequiredService<IFileNameConverter>();
        }

        [Fact]
        public void ConvertName()
        {
            var converted = _fileNameConverter.ConvertToTypeScript(
                "MyItemDto.cs",
                new ModuleNameConversionOptions(useKebabCase: false, appendModelSuffix: false));

            Assert.Equal("myItemDto.ts", converted);
        }

        [Fact]
        public void ConvertNameFromPath()
        {
            var converted = _fileNameConverter.ConvertToTypeScript(
                "MyProject/DTOs/MyItemDto.cs",
                new ModuleNameConversionOptions(useKebabCase: false, appendModelSuffix: false));

            Assert.Equal("myItemDto.ts", converted);
        }

        [Fact]
        public void ConvertToKebabCase()
        {
            var converted = _fileNameConverter.ConvertToTypeScript(
                "MyItemDto.cs",
                new ModuleNameConversionOptions(useKebabCase: true, appendModelSuffix: false));

            Assert.Equal("my-item-dto.ts", converted);
        }

        [Fact]
        public void AppendModelSuffix()
        {
            var converted = _fileNameConverter.ConvertToTypeScript(
                "MyItemDto.cs",
                new ModuleNameConversionOptions(useKebabCase: false, appendModelSuffix: true));

            Assert.Equal("myItemDto.model.ts", converted);
        }

        [Fact]
        public void RemoveInterfacePrefix()
        {
            var converted = _fileNameConverter.ConvertToTypeScript(
                "IMyItemDto.cs",
                new ModuleNameConversionOptions(useKebabCase: false, appendModelSuffix: true, removeInterfacePrefix: true));

            Assert.Equal("myItemDto.model.ts", converted);
        }

        [Fact]
        public void PreserveInterfacePrefix()
        {
            var converted = _fileNameConverter.ConvertToTypeScript(
                "IMyItemDto.cs",
                new ModuleNameConversionOptions(useKebabCase: false, appendModelSuffix: true, removeInterfacePrefix: false));

            Assert.Equal("iMyItemDto.model.ts", converted);
        }
    }
}