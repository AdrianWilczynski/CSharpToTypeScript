using CSharpToTypeScript.CLITool.Services;
using CSharpToTypeScript.Core.Services;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpToTypeScript.CLITool
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddSingleton<ICodeConverter, CodeConverter>()
                .AddSingleton<IFileNameConverter, FileNameConverter>()
                .AddSingleton<IFileSystem, FileSystem>()
                .BuildServiceProvider();

            var cli = new CommandLineApplication<CLI>();

            cli.Conventions.UseDefaultConventions()
                .UseConstructorInjection(services);

            return cli.Execute(args);
        }
    }
}
