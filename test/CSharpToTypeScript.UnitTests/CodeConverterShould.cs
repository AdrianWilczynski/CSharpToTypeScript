using System;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Services;
using Xunit;

namespace CSharpToTypeScript.UnitTests
{
    public class CodeConverterShould
    {
        [Fact]
        public void ConvertClass()
        {
            const string csharpCode = @"class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
}";

            var typeScriptCode = new CodeConverter()
                .ConvertToTypeScript(csharpCode, new CodeConversionOptions(true, false, 4));

            Assert.Equal(@"export interface Item {
    id: number;
    name: string;
}", typeScriptCode);
        }
    }
}
