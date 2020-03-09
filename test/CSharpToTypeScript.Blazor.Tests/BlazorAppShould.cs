using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace CSharpToTypeScript.Blazor.Tests
{
    public class BlazorAppShould : IDisposable
    {
        private readonly IWebDriver _webDriver;
        private readonly Process _process;

        public BlazorAppShould()
        {
            _webDriver = new ChromeDriver(new ChromeOptions
            {
                AcceptInsecureCertificates = true
            });

            _process = new Process
            {
                StartInfo =
                {
                    FileName = "dotnet",
                    Arguments = "run --pathbase=/CSharpToTypeScript",
                    UseShellExecute = false,
                    WorkingDirectory = Path.Join(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        "..", "..", "..", "..", "..", "src", "CSharpToTypeScript.Blazor")
                }
            };
            _process.Start();
        }

        [Fact]
        public void ConvertCode()
        {
            _webDriver.Navigate()
                .GoToUrl("https://localhost:5001/CSharpToTypeScript/");

            var inputEditor = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30))
                .Until(c =>
                {
                    try
                    {
                        return c.FindElement(By.ClassName("monaco-editor"));
                    }
                    catch
                    {
                        return null;
                    }
                });

            inputEditor.Click();

            new Actions(_webDriver)
                .SendKeys("class MyClass { }")
                .Perform();

            var outputEditorText = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5))
                .Until(c =>
                {
                    var lines = _webDriver.FindElements(By.ClassName("view-lines"))[1];

                    return lines.Text.Contains("MyClass")
                        ? lines.Text
                        : null;
                });

            Assert.Contains("export interface MyClass {", outputEditorText);
            Assert.Contains("}", outputEditorText);
        }

        public void Dispose()
        {
            _webDriver.Quit();
            _webDriver.Dispose();

            _process.CloseMainWindow();
            _process.WaitForExit();
            _process.Dispose();
        }
    }
}
