using System.IO;
using CSharpToTypeScript.Core.DI;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpToTypeScript.CLITool.Tests
{
    public class CLIFixture
    {
        public CLI CLI { get; }

        public CLIFixture()
        {
            var serviceProvider = new ServiceCollection()
                .AddCSharpToTypeScript()
                .AddTransient<CLI>()
                .BuildServiceProvider();

            CLI = serviceProvider.GetRequiredService<CLI>();

            Directory.SetCurrentDirectory("./../../../scenarios");
        }
    }
}