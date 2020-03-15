using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
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
            // https://stackoverflow.com/a/57720610
            var driverService = FirefoxDriverService.CreateDefaultService();
            driverService.Host = "::1";

            _webDriver = new FirefoxDriver(driverService, new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            });

            _process = new Process
            {
                StartInfo =
                {
                    FileName = "dotnet",
                    Arguments = "run --pathbase=/CSharpToTypeScript",
                    WorkingDirectory = Path.Join(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        "..", "..", "..", "..", "..", "src", "CSharpToTypeScript.Blazor"),
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };
            _process.Start();

            while (!_process.StandardOutput.ReadLine().Contains("Now listening on:")) { }
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
                    var lines = c.FindElements(By.ClassName("view-lines"))[1];

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
            if (!_process.WaitForExit(TimeSpan.FromSeconds(3).Milliseconds))
            {
                try { _process.Kill(); } catch { }
            }
            _process.Dispose();
        }
    }
}
