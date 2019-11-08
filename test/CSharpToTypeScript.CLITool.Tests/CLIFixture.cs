using System;
using System.IO;
using CSharpToTypeScript.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpToTypeScript.CLITool.Tests
{
    public class CLIFixture : IDisposable
    {
        private const string Temp = "temp";

        private readonly ServiceProvider _serviceProvider;

        public CLI CLI => _serviceProvider.GetRequiredService<CLI>();

        public CLIFixture()
        {
            _serviceProvider = new ServiceCollection()
                .AddCSharpToTypeScript()
                .AddTransient<CLI>()
                .BuildServiceProvider();

            Directory.CreateDirectory(Temp);
            Directory.SetCurrentDirectory(Temp);
        }

        public void Dispose()
        {
            Directory.SetCurrentDirectory("./..");
            Directory.Delete(Temp, true);
        }
    }
}