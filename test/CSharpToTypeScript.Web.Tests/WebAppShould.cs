using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CSharpToTypeScript.Web.Tests
{
    public class WebAppShould : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public WebAppShould(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ConvertCode()
        {
            var client = _factory.CreateClient();
            var browsingContext = BrowsingContext.New(Configuration.Default);

            string requestVerificationToken = await GetVerificationToken(client, browsingContext);

            var response = await client.PostAsync("/", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("InputCode", "class Test {}"),
                new KeyValuePair<string, string>("Settings.TabSize", "4"),
                new KeyValuePair<string, string>("Settings.ConvertDatesTo", "0"),
                new KeyValuePair<string, string>("Settings.ConvertNullablesTo", "0"),
                new KeyValuePair<string, string>("Settings.UseTabs", "false"),
                new KeyValuePair<string, string>("Settings.Export", "true"),
                new KeyValuePair<string, string>("Settings.ToCamelCase", "true"),
                new KeyValuePair<string, string>("Settings.RemoveInterfacePrefix", "true"),
                new KeyValuePair<string, string>("Settings.GenerateImports", "true"),
                new KeyValuePair<string, string>("Settings.UseKebabCase", "false"),
                new KeyValuePair<string, string>("Settings.AppendModelSuffix", "false"),
                new KeyValuePair<string, string>("Settings.QuotationMark", "0"),
                new KeyValuePair<string, string>("__RequestVerificationToken", requestVerificationToken)
            }));

            response.EnsureSuccessStatusCode();

            var responseHtml = await response.Content.ReadAsStringAsync();
            var responseDocument = await browsingContext.OpenAsync(r => r.Content(responseHtml));

            var convertedCode = responseDocument.QuerySelector("#convertedCodeHiddenInput").TextContent;

            Assert.Contains("export interface Test {", convertedCode);
        }

        [Fact]
        public async Task UseProvidedSettings()
        {
            var client = _factory.CreateClient();
            var browsingContext = BrowsingContext.New(Configuration.Default);

            string requestVerificationToken = await GetVerificationToken(client, browsingContext);

            var response = await client.PostAsync("/", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("InputCode", @"class Test 
{
    public int SomeNumber { get; set; }
    public DateTime SomeDate { get; set; }
}"),
                new KeyValuePair<string, string>("Settings.TabSize", "4"),
                new KeyValuePair<string, string>("Settings.ConvertDatesTo", "1"),
                new KeyValuePair<string, string>("Settings.ConvertNullablesTo", "0"),
                new KeyValuePair<string, string>("Settings.UseTabs", "true"),
                new KeyValuePair<string, string>("Settings.Export", "false"),
                new KeyValuePair<string, string>("Settings.ToCamelCase", "false"),
                new KeyValuePair<string, string>("Settings.RemoveInterfacePrefix", "true"),
                new KeyValuePair<string, string>("Settings.GenerateImports", "true"),
                new KeyValuePair<string, string>("Settings.UseKebabCase", "false"),
                new KeyValuePair<string, string>("Settings.AppendModelSuffix", "false"),
                new KeyValuePair<string, string>("Settings.QuotationMark", "0"),
                new KeyValuePair<string, string>("__RequestVerificationToken", requestVerificationToken)
            }));

            response.EnsureSuccessStatusCode();

            var responseHtml = await response.Content.ReadAsStringAsync();
            var responseDocument = await browsingContext.OpenAsync(r => r.Content(responseHtml));

            var convertedCode = responseDocument.QuerySelector("#convertedCodeHiddenInput").TextContent;

            Assert.DoesNotContain("export", convertedCode);
            Assert.Contains("\tSomeNumber: number;", convertedCode);
            Assert.Contains("\tSomeDate: Date;", convertedCode);
        }

        [Fact]
        public async Task RememberSettings()
        {
            var client = _factory.CreateClient();
            var browsingContext = BrowsingContext.New(Configuration.Default);

            var indexHtml = await client.GetStringAsync("/");
            var indexDocument = await browsingContext.OpenAsync(r => r.Content(indexHtml));

            var requestVerificationToken = GetVerificationToken(indexDocument);

            var initialTabSizeValue = indexDocument.QuerySelector("#Settings_TabSize")
                .GetAttribute("value");

            Assert.Equal("4", initialTabSizeValue);

            var response = await client.PostAsync("/", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("InputCode", string.Empty),
                new KeyValuePair<string, string>("Settings.TabSize", "8"),
                new KeyValuePair<string, string>("Settings.ConvertDatesTo", "0"),
                new KeyValuePair<string, string>("Settings.ConvertNullablesTo", "0"),
                new KeyValuePair<string, string>("Settings.UseTabs", "false"),
                new KeyValuePair<string, string>("Settings.Export", "true"),
                new KeyValuePair<string, string>("Settings.ToCamelCase", "true"),
                new KeyValuePair<string, string>("Settings.RemoveInterfacePrefix", "true"),
                new KeyValuePair<string, string>("Settings.GenerateImports", "true"),
                new KeyValuePair<string, string>("Settings.UseKebabCase", "false"),
                new KeyValuePair<string, string>("Settings.AppendModelSuffix", "false"),
                new KeyValuePair<string, string>("Settings.QuotationMark", "0"),
                new KeyValuePair<string, string>("__RequestVerificationToken", requestVerificationToken)
            }));

            response.EnsureSuccessStatusCode();

            var indexHtmlAfterPost = await client.GetStringAsync("/");
            var indexDocumentAfterPost = await browsingContext.OpenAsync(r => r.Content(indexHtmlAfterPost));

            var rememberedTabSizeValue = indexDocumentAfterPost.QuerySelector("#Settings_TabSize")
                .GetAttribute("value");

            Assert.Equal("8", rememberedTabSizeValue);
        }

        private static async Task<string> GetVerificationToken(HttpClient client, IBrowsingContext browsingContext)
        {
            var indexHtml = await client.GetStringAsync("/");
            var indexDocument = await browsingContext.OpenAsync(r => r.Content(indexHtml));

            return GetVerificationToken(indexDocument);
        }

        private static string GetVerificationToken(IDocument indexDocument)
            => indexDocument
                .QuerySelector("input[name=\"__RequestVerificationToken\"]")
                .GetAttribute("value");
    }
}
