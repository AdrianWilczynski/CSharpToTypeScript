using System;
using System.IO;
using CSharpToTypeScript.CLITool.Commands;
using CSharpToTypeScript.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpToTypeScript.CLITool.Tests
{
    public class CLIFixture : IDisposable
    {
        private readonly string Temp = "temp";

        private readonly ServiceProvider _serviceProvider;

        public ConvertCommand ConvertCommand => _serviceProvider.GetRequiredService<ConvertCommand>();
        public InitializeCommand InitializeCommand => _serviceProvider.GetRequiredService<InitializeCommand>();

        public CLIFixture()
        {
            _serviceProvider = new ServiceCollection()
                .AddCSharpToTypeScript()
                .AddTransient<ConvertCommand>()
                .AddTransient<InitializeCommand>()
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