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

        private ElementReference InputEditorContainer { get; set; }
        private ElementReference OutputEditorContainer { get; set; }

        private ElementReference Navbar { get; set; }

        private DotNetObjectReference<Index> ThisDotNetReference { get; }

        private SettingsModel SettingsModalModel { get; set; } = new SettingsModel();
        private SettingsModel CurrentSettings { get; set; }
        private bool AreSettingsOpen { get; set; }

        private int ThemeIndex { get; set; }
        private string ThemeDisplayName { get; set; }

        private bool IsSettingRandomThemeInProgress { get; set; }

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

        private async Task OnSaveSettingsAsync()
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

        private void OnSettingsToDefaultClick()
            => SettingsModalModel = new SettingsModel();

        private void OnOpenSettingsClick()
            => AreSettingsOpen = true;

        private void OnSettingsCloseRequested()
            => AreSettingsOpen = false;

        private async Task OnCopyClickAsync()
            => await JSRuntime.InvokeVoidAsync("copyToClipboard");

        private async Task OnBrandClickAsync()
            => await JSRuntime.InvokeVoidAsync("setInputEditorValue", string.Empty);

        private async Task OnSetRandomThemeClick()
        {
            if (IsSettingRandomThemeInProgress)
            {
                return;
            }

            IsSettingRandomThemeInProgress = true;

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

            IsSettingRandomThemeInProgress = false;
        }

        private async Task OnChangeThemeClick()
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