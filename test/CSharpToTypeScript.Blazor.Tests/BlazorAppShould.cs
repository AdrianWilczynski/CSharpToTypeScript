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
            NavigateToApp();
            WaitForLoad();
            InputTextIntoMonacoEditor("class MyClass { }");
            var outputEditorText = GetTextFromOutputEditor();

            Assert.Contains("export interface MyClass {", outputEditorText);
            Assert.Contains("}", outputEditorText);
        }

        [Fact]
        public void UseSettings()
        {
            NavigateToApp();
            WaitForLoad();

            _webDriver.FindElement(By.Id("openSettingsButton")).Click();

            var toCamelCaseToggle = _webDriver.FindElement(By.CssSelector("label[for=\"ToCamelCase\"]"));
            var toCamelCaseBackingInput = _webDriver.FindElement(By.Id("ToCamelCase"));

            Assert.True(toCamelCaseBackingInput.Selected);
            toCamelCaseToggle.Click();
            Assert.False(toCamelCaseBackingInput.Selected);

            var tabSizeInput = _webDriver.FindElement(By.Id("TabSize"));
            Assert.Equal("4", tabSizeInput.GetProperty("value"));
            tabSizeInput.Clear();
            tabSizeInput.SendKeys("8");

            _webDriver.FindElement(By.Id("saveSettingsButton")).Click();

            InputTextIntoMonacoEditor("class MyClass" + Keys.Return +
            "{" + Keys.Return +
            "    public int MyProperty { get; set; }" + Keys.Return + Keys.Backspace +
            "}");

            var outputEditorText = GetTextFromOutputEditor();

            Assert.Contains("        MyProperty: number;", outputEditorText);
        }

        #region Helpers
        private void NavigateToApp()
             => _webDriver.Navigate()
                 .GoToUrl("https://localhost:5001/CSharpToTypeScript/");

        private void WaitForLoad()
            => new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30))
                .Until(c =>
                {
                    try
                    {
                        c.FindElement(By.ClassName("monaco-editor"));
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                });

        private void InputTextIntoMonacoEditor(string code)
        {
            var inputEditor = _webDriver.FindElement(By.ClassName("monaco-editor"));

            inputEditor.Click();

            new Actions(_webDriver)
                .SendKeys(code)
                .Perform();
        }

        private string GetTextFromOutputEditor()
            => new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5))
                .Until(c =>
                {
                    var lines = c.FindElements(By.ClassName("view-lines"))[1];

                    return !string.IsNullOrWhiteSpace(lines.Text)
                        ? lines.Text
                        : null;
                });
        #endregion

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
