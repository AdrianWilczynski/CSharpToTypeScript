using System;
using CSharpToTypeScript.CLITool.Commands;
using CSharpToTypeScript.Core.DependencyInjection;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

#if RELEASE
using System.Linq;
using CSharpToTypeScript.CLITool.Utilities;
#endif

namespace CSharpToTypeScript.CLITool
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var services = new ServiceCollection()
                    .AddCSharpToTypeScript()
                    .BuildServiceProvider();

                using (var cli = new CommandLineApplication<ConvertCommand>())
                {
                    cli.Conventions.UseDefaultConventions()
                        .UseConstructorInjection(services);

                    return cli.Execute(args);
                }
            }
#pragma warning disable CS0168
            catch (Exception ex)
            {
#if RELEASE
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(string.Join(" ---> ", ExceptionMessage.Flatten(ex).Distinct()));
                Console.ResetColor();

                return 1;
#else
                throw;
#endif
            }
        }
    }
}
