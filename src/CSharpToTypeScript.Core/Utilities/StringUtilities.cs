using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSharpToTypeScript.Core.Utilities
{
    public static class StringUtilities
    {
        public static string ToCamelCase(this string text)
            => Regex.Replace(text, "^[A-Z]", Char.ToLowerInvariant(text[0]).ToString());

        public static string RemoveInterfacePrefix(this string text)
            => Regex.Replace(text, $"^I(?=[A-Z])", string.Empty);

        public static string Repeat(this string text, int count)
            => string.Concat(Enumerable.Repeat(text, count));

        public static string LineByLine(this IEnumerable<string> lines)
            => string.Join(NewLine, lines);

        public static string NewLine => "\r\n";
    }
}