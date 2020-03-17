using System;
using System.Text.Json;
using System.Threading.Tasks;
using CSharpToTypeScript.Blazor.Models;
using CSharpToTypeScript.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CSharpToTypeScript.Blazor.Pages
{
    public partial class Index : IDisposable
    {
        public Index() => ThisDotNetReference = DotNetObjectReference.Create(this);

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ICodeConverter CodeConverter { get; set; }

        protected ElementReference InputEditorContainer { get; set; }
        protected ElementReference OutputEditorContainer { get; set; }

        protected ElementReference Navbar { get; set; }

        protected DotNetObjectReference<Index> ThisDotNetReference { get; }

        protected SettingsModel SettingsModalModel { get; set; } = new SettingsModel();
        protected SettingsModel CurrentSettings { get; set; }
        protected bool AreSettingsOpen { get; set; }

        public int ThemeIndex { get; set; }
        public string ThemeDisplayName { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (await JSRuntime.InvokeAsync<string>("localStorage.getItem", nameof(SettingsModalModel)) is string json)
            {
                SettingsModalModel = JsonSerializer.Deserialize<SettingsModel>(json);
            }

            CurrentSettings = SettingsModalModel.Clone();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync(
                    "initializeMonaco",
                    InputEditorContainer, OutputEditorContainer,
                    Navbar,
                    ThisDotNetReference);

                ThemeIndex = int.TryParse(
                    await JSRuntime.InvokeAsync<string>("localStorage.getItem", nameof(ThemeIndex)),
                    out var themeIndex)
                ? themeIndex
                : 0;

                await SetTheme();
                StateHasChanged();
            }
        }

        [JSInvokable]
        public async Task OnInputEditorChangeAsync(string value)
            => await OutputAsConvertedCode(value);

        protected async Task OnSaveSettingsAsync()
        {
            await JSRuntime.InvokeAsync<string>(
                "localStorage.setItem", nameof(SettingsModalModel), JsonSerializer.Serialize(SettingsModalModel));

            CurrentSettings = SettingsModalModel.Clone();

            var inputCode = await JSRuntime.InvokeAsync<string>("getInputEditorValue");

            await OutputAsConvertedCode(inputCode);

            AreSettingsOpen = false;
        }

        private async Task OutputAsConvertedCode(string value)
        {
            var convertedCode = CodeConverter.ConvertToTypeScript(
                value,
                CurrentSettings.MapToCodeConversionOptions());

            await JSRuntime.InvokeVoidAsync("setOutputEditorValue", convertedCode);
        }

        protected void OnSettingsToDefaultClick()
            => SettingsModalModel = new SettingsModel();

        protected void OnOpenSettingsClick()
            => AreSettingsOpen = true;

        protected void OnSettingsCloseRequested()
            => AreSettingsOpen = false;

        protected async Task OnCopyClickAsync()
            => await JSRuntime.InvokeVoidAsync("copyToClipboard");

        protected async Task OnBrandClickAsync()
            => await JSRuntime.InvokeVoidAsync("setInputEditorValue", string.Empty);

        protected async Task OnSetRandomThemeClick()
        {
            var steps = new Random().Next(1, Constants.Themes.Count);

            for (int i = 0; i < steps; i++)
            {
                if (i != 0)
                {
                    await Task.Delay(TimeSpan.FromSeconds(0.25));
                }

                await OnChangeThemeClick();
                StateHasChanged();
            }
        }

        protected async Task OnChangeThemeClick()
        {
            if (ThemeIndex == Constants.Themes.Count - 1)
            {
                ThemeIndex = 0;
            }
            else
            {
                ThemeIndex++;
            }

            await SetTheme();

            await JSRuntime.InvokeVoidAsync("localStorage.setItem", nameof(ThemeIndex), ThemeIndex.ToString());
        }

        private async Task SetTheme()
        {
            ThemeDisplayName = Constants.Themes[ThemeIndex].DisplayName;
            await JSRuntime.InvokeVoidAsync("setTheme", Constants.Themes[ThemeIndex].Name);
        }

        public void Dispose() => ThisDotNetReference.Dispose();
    }
}