using System;
using System.Threading.Tasks;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CSharpToTypeScript.Blazor.Pages
{
    public partial class Index : IDisposable
    {
        public Index() => _thisDotNetReference = DotNetObjectReference.Create(this);

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ICodeConverter CodeConverter { get; set; }

        private ElementReference InputEditorContainer { get; set; }
        private ElementReference OutputEditorContainer { get; set; }

        private readonly DotNetObjectReference<Index> _thisDotNetReference;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync(
                    "initializeMonaco",
                    InputEditorContainer,
                    OutputEditorContainer,
                    _thisDotNetReference);
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

        public void Dispose() => _thisDotNetReference.Dispose();
    }
}