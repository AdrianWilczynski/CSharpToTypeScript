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

        public static string Parenthesize(this string text)
            => "(" + text + ")";

        public static string ToCommaSepratedList(this IEnumerable<string> texts)
            => string.Join(", ", texts);

        public static string ToSpaceSepratedList(this IEnumerable<string> texts)
            => string.Join(" ", texts);

        public static string If(this string text, bool condition)
            => condition ? text : string.Empty;

        public static string TransformIf(this string text, bool condition, Func<string, string> transformation)
            => condition ? transformation(text) : text;

        public static IEnumerable<string> Indent(this IEnumerable<string> texts, bool useTabs, int? tabSize)
            => texts.Select(t => Indentation(useTabs, tabSize) + t);

        public static string Indentation(bool useTabs, int? tabSize)
            => useTabs ? "\t"
            : tabSize is int && tabSize > 0 ? " ".Repeat((int)tabSize)
            : throw new ArgumentException();

        public static string NewLine => "\r\n";
    }
}