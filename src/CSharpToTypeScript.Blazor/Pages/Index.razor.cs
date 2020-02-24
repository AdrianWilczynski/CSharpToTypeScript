using System;
using System.Text.Json;
using System.Threading.Tasks;
using CSharpToTypeScript.Blazor.Models;
using CSharpToTypeScript.Core.Options;
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
        {
            var convertedCode = CodeConverter.ConvertToTypeScript(value, new CodeConversionOptions(
                export: true,
                useTabs: false,
                tabSize: 4,
                importGenerationMode: ImportGenerationMode.Simple));

            await JSRuntime.InvokeVoidAsync("setOutputEditorValue", convertedCode);
        }

        protected async Task OnSaveSettingsAsync()
        {
            await JSRuntime.InvokeAsync<string>(
                "localStorage.setItem", nameof(SettingsModel), JsonSerializer.Serialize(SettingsModel));

            AreSettingsOpen = false;
        }

        protected void OnOpenSettingsClick() => AreSettingsOpen = true;
        protected void OnSettingsCloseRequested() => AreSettingsOpen = false;

        public void Dispose() => ThisDotNetReference.Dispose();
    }
}