using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CSharpToTypeScript.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using CSharpToTypeScript.Blazor.Pages;

namespace CSharpToTypeScript.Blazor
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<Index>("app");

            builder.Services.AddCSharpToTypeScript();
            builder.Services.AddBaseAddressHttpClient();

            await builder.Build().RunAsync();
        }
    }
}
