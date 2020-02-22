using System;
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

        private ElementReference InputEditorContainer { get; set; }
        private ElementReference OutputEditorContainer { get; set; }

        private DotNetObjectReference<Index> ThisDotNetReference { get; }

        private SettingsModel Model { get; } = new SettingsModel();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync(
                    "initializeMonaco",
                    InputEditorContainer,
                    OutputEditorContainer,
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

        public void Dispose() => ThisDotNetReference.Dispose();
    }
}