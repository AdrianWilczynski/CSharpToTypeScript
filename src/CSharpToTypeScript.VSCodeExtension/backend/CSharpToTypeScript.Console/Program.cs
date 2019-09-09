using CSharpToTypeScript.Core.Services;

namespace CSharpToTypeScript.Console
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var code = args[0];
            var useTabs = bool.Parse(args[1]);
            var tabSize = int.Parse(args[2]);
            var export = bool.Parse(args[3]);

            var converted = new CodeConverter().ConvertToTypeScript(code, useTabs, tabSize, export);

            System.Console.Write(converted);
        }
    }
}
