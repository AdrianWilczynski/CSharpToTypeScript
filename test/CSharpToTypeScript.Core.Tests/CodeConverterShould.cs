using CSharpToTypeScript.Core.DI;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CSharpToTypeScript.Core.Tests
{
    public class CodeConverterShould
    {
        private readonly ICodeConverter _codeConverter;

        public CodeConverterShould()
        {
            var serviceProvider = new ServiceCollection()
                .AddCSharpToTypeScript()
                .BuildServiceProvider();

            _codeConverter = serviceProvider.GetRequiredService<ICodeConverter>();
        }

        [Fact]
        public void ConvertClass()
        {
            var converted = _codeConverter.ConvertToTypeScript(
                @"class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
}",
                new CodeConversionOptions(export: true, useTabs: false, tabSize: 4));

            Assert.Equal(@"export interface Item {
    id: number;
    name: string;
}", converted);
        }
    }
}
