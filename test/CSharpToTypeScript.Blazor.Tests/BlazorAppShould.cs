using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace CSharpToTypeScript.Blazor.Tests
{
    public class BlazorAppShould : IClassFixture<DotnetRunFixture>, IDisposable
    {
        private readonly IWebDriver _webDriver;

        public BlazorAppShould()
        {
            // https://stackoverflow.com/a/57720610
            var driverService = FirefoxDriverService.CreateDefaultService();
            driverService.Host = "::1";

            _webDriver = new FirefoxDriver(driverService, new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            });
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
            OpenSettings();

            var toCamelCaseToggle = _webDriver.FindElement(By.CssSelector("label[for=\"ToCamelCase\"]"));
            var toCamelCaseBackingInput = _webDriver.FindElement(By.Id("ToCamelCase"));

            Assert.True(toCamelCaseBackingInput.Selected);
            toCamelCaseToggle.Click();
            Assert.False(toCamelCaseBackingInput.Selected);

            var tabSizeInput = _webDriver.FindElement(By.Id("TabSize"));
            Assert.Equal("4", tabSizeInput.GetProperty("value"));
            tabSizeInput.Clear();
            tabSizeInput.SendKeys("8");

            SaveAndCloseSettings();

            InputTextIntoMonacoEditor("class MyClass" + Keys.Return +
            "{" + Keys.Return +
            "    public int MyProperty { get; set; }" + Keys.Return + Keys.Backspace +
            "}");

            var outputEditorText = GetTextFromOutputEditor();

            Assert.Contains("        MyProperty: number;", outputEditorText);
        }

        [Fact]
        public void PreserveSettings()
        {
            NavigateToApp();
            WaitForLoad();
            OpenSettings();

            var tabSizeInput = _webDriver.FindElement(By.Id("TabSize"));
            Assert.Equal("4", tabSizeInput.GetProperty("value"));
            tabSizeInput.Clear();
            tabSizeInput.SendKeys("2");

            SaveAndCloseSettings();

            NavigateToApp();
            WaitForLoad();
            OpenSettings();

            var preservedTabSizeValue = _webDriver.FindElement(By.Id("TabSize"))
                .GetProperty("value");

            Assert.Equal("2", preservedTabSizeValue);
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

        private void OpenSettings()
            => _webDriver.FindElement(By.Id("openSettingsButton")).Click();

        private void SaveAndCloseSettings()
            => _webDriver.FindElement(By.Id("saveSettingsButton")).Click();
        #endregion

        public void Dispose()
        {
            _webDriver.Quit();
            _webDriver.Dispose();
        }
    }
}
