using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Hosting;
using CSharpToTypeScript.Core.DependencyInjection;

namespace CSharpToTypeScript.Blazor
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddCSharpToTypeScript();

            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }
    }
}
