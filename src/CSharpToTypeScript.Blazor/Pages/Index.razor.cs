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

        protected DotNetObjectReference<Index> ThisDotNetReference { get; }

        protected SettingsModel SettingsModel { get; set; } = new SettingsModel();

        protected bool AreSettingsOpen { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (await JSRuntime.InvokeAsync<string>("localStorage.getItem", nameof(SettingsModel)) is string json)
            {
                SettingsModel = JsonSerializer.Deserialize<SettingsModel>(json);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync(
                    "initializeMonaco",
                    InputEditorContainer, OutputEditorContainer,
                    ThisDotNetReference);
            }
        }

        [JSInvokable]
        public async Task OnInputEditorChangeAsync(string value)
            => await OutputAsConvertedCode(value);

        protected async Task OnSaveSettingsAsync()
        {
            await JSRuntime.InvokeAsync<string>(
                "localStorage.setItem", nameof(SettingsModel), JsonSerializer.Serialize(SettingsModel));

            var inputCode = await JSRuntime.InvokeAsync<string>("getInputEditorValue");

            await OutputAsConvertedCode(inputCode);

            AreSettingsOpen = false;
        }

        protected async Task OutputAsConvertedCode(string value)
        {
            var convertedCode = CodeConverter.ConvertToTypeScript(
                value,
                SettingsModel.MapToCodeConversionOptions());

            await JSRuntime.InvokeVoidAsync("setOutputEditorValue", convertedCode);
        }

        protected void OnOpenSettingsClick() => AreSettingsOpen = true;
        protected void OnSettingsCloseRequested() => AreSettingsOpen = false;

        protected async Task OnCopyClickAsync()
            => await JSRuntime.InvokeVoidAsync("copyToClipboard");

        public void Dispose() => ThisDotNetReference.Dispose();
    }
}