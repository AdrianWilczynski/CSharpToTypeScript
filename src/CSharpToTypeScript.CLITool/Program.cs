using System;
using CSharpToTypeScript.Core.DI;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

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

                using (var cli = new CommandLineApplication<CLI>())
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
                Console.Error.WriteLine(ex.Message);
                return 1;
#else 
                throw;
#endif
            }
        }
    }
}
