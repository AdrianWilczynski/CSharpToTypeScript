using CSharpToTypeScript.Core.Services;

namespace CSharpToTypeScript.Console
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var code = args[0];
            var useTabs = bool.Parse(args[1]);
            var tabSize = int.TryParse(args[2], out var parsed) ? (int?)parsed : null;
            var export = bool.Parse(args[3]);

            var converted = new CodeConverter().ConvertToTypeScript(code, useTabs, tabSize, export);

            System.Console.Write(converted);
        }
    }
}
