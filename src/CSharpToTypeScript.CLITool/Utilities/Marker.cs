using System.Text.RegularExpressions;

using static CSharpToTypeScript.Core.Utilities.StringUtilities;

namespace CSharpToTypeScript.CLITool.Utilities
{
    public static class Marker
    {
        private const string BeginMarker = "@cs2ts-begin-auto-generated";
        private const string EndMarker = "@cs2ts-end-auto-generated";

        private const string Pattern = @"\/\/ ?" + BeginMarker + ".*" + @"\/\/ ?" + EndMarker;

        public static string Update(string oldContent, string newContent)
            => IsMarked(oldContent)
            ? Regex.Replace(oldContent, Pattern, Mark(newContent), RegexOptions.Singleline)
            : Mark(newContent);

        private static string Mark(string content)
            => AsComment(BeginMarker) + NewLine + content + NewLine + AsComment(EndMarker);

        private static bool IsMarked(string content)
            => Regex.IsMatch(content, Pattern, RegexOptions.Singleline);

        private static string AsComment(string text)
            => "// " + text;
    }
}