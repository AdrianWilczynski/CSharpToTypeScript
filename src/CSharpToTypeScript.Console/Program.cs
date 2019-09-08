using CSharpToTypeScript.Core.Services;
using Microsoft.Extensions.Configuration;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Console
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var code = configuration.GetValue<string>("code");
            var tabSize = configuration.GetValue<int?>("tabSize") ?? 2;
            var useTabs = configuration.GetValue<bool?>("useTabs") ?? false;
            var export = configuration.GetValue<bool?>("export") ?? true;

            var converted = new CodeConverter().ConvertToTypeScript(code, useTabs, tabSize, export);

            System.Console.Write(converted);
        }
    }
}
