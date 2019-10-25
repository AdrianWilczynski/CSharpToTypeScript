using System;
using CSharpToTypeScript.Core.DI;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpToTypeScript.CLITool
{
    public static class Program
    {
        public static void Main(string[] args)
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

                    cli.Execute(args);
                }
            }
#pragma warning disable CS0168
            catch (Exception ex)
            {
#if RELEASE
                Console.Error.WriteLine(ex.Message);
#else 
                throw;
#endif
            }
        }
    }
}
