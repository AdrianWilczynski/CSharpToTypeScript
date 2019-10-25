using System.IO;
using CSharpToTypeScript.Core.DI;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpToTypeScript.CLITool.Tests
{
    public class CLIFixture
    {
        private readonly ServiceProvider _serviceProvider;

        public CLI CLI => _serviceProvider.GetRequiredService<CLI>();

        public CLIFixture()
        {
            _serviceProvider = new ServiceCollection()
                .AddCSharpToTypeScript()
                .AddTransient<CLI>()
                .BuildServiceProvider();

            Directory.SetCurrentDirectory("./../../../scenarios");
        }
    }
}