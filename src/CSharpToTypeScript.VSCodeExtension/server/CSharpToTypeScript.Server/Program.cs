using CSharpToTypeScript.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Server.Services;

namespace CSharpToTypeScript.Server
{
    public static class Program
    {
        public static void Main(string[] _)
        {
            var services = new ServiceCollection()
                .AddCSharpToTypeScript()
                .AddTransient<IStdio, Stdio>()
                .AddTransient<StdioServer>();

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<StdioServer>()
                .Handle();
        }
    }
}
