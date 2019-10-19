using CSharpToTypeScript.CLITool.Services;
using CSharpToTypeScript.Core.DI;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpToTypeScript.CLITool
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddCSharpToTypeScript()
                .AddSingleton<IFileSystem, FileSystem>()
                .BuildServiceProvider();

            using (var cli = new CommandLineApplication<CLI>())
            {
                cli.Conventions.UseDefaultConventions()
                    .UseConstructorInjection(services);

                return cli.Execute(args);
            }
        }
    }
}
