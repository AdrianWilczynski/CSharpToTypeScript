using System.Collections.Generic;

namespace CSharpToTypeScript.Blazor
{
    public static class Constants
    {
        public static IEnumerable<(string Name, string Url)> Sites => new[]
        {
            ("GitHub", "https://github.com/AdrianWilczynski/CSharpToTypeScript"),
            ("NuGet", "https://www.nuget.org/packages/CSharpToTypeScript.CLITool"),
            ("Visual Studio Marketplace", "https://marketplace.visualstudio.com/items?itemName=adrianwilczynski.csharp-to-typescript"),
            ("Azure", "https://csharptotypescript.azurewebsites.net")
        };
    }
}