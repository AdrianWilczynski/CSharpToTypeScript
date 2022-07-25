using System.Text.Json;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Services;
using CSharpToTypeScript.Server;
using CSharpToTypeScript.Server.DTOs;
using Xunit;
using Moq;
using Server.Services;

namespace CSharpToTypeScript.VSCodeExtension.Server.Tests
{
    public class ServerShould
    {
        [Fact]
        public void HandleRequests()
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var firstRequest = JsonSerializer.Serialize(
                new Input { Code = "class First { }", Export = true, UseTabs = false, TabSize = 4, ConvertDatesTo = DateOutputType.String, ConvertNullablesTo = NullableOutputType.Undefined, ToCamelCase = true },
                jsonSerializerOptions);

            var secondRequest = JsonSerializer.Serialize(
                new Input { Code = "class Second { }", Export = true, UseTabs = false, TabSize = 2, ConvertDatesTo = DateOutputType.Date, ConvertNullablesTo = NullableOutputType.Undefined, ToCamelCase = true },
                jsonSerializerOptions);

            var stdioMock = new Mock<IStdio>();
            stdioMock.SetupSequence(s => s.ReadLine())
                .Returns(firstRequest)
                .Returns(secondRequest)
                .Returns("EXIT");

            var codeConverterMock = new Mock<ICodeConverter>();
            codeConverterMock.SetupSequence(c => c.ConvertToTypeScript(It.IsAny<string>(), It.IsAny<CodeConversionOptions>()))
                .Returns("export interface First { }")
                .Returns("export interface Second { }");

            var fileNameConverterMock = new Mock<IFileNameConverter>();
            fileNameConverterMock.Setup(f => f.ConvertToTypeScript(It.IsAny<string>(), It.IsAny<ModuleNameConversionOptions>()))
                .Returns("item.ts");

            var server = new StdioServer(codeConverterMock.Object, stdioMock.Object, fileNameConverterMock.Object);

            server.Handle();

            stdioMock.Verify(s => s.ReadLine(), Times.Exactly(3));

            stdioMock.Verify(s =>
                s.WriteLine(It.Is<string>(response => response.Contains("export interface First { }"))),
                Times.Once);

            stdioMock.Verify(s => s.WriteLine(It.IsAny<string>()), Times.Exactly(2));

            codeConverterMock.Verify(c =>
                c.ConvertToTypeScript("class Second { }", It.Is<CodeConversionOptions>(options => options.TabSize == 2 && options.ConvertDatesTo == DateOutputType.Date)),
                Times.Once);

            codeConverterMock.Verify(c =>
                c.ConvertToTypeScript(It.IsAny<string>(), It.IsAny<CodeConversionOptions>()),
                Times.Exactly(2));
        }
    }
}