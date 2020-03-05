using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Utilities
{
    public static class StringUtilities
    {
        public static string InterfacePrefixRegex { get; } = "^I(?=[A-Z])";

        public static string RemoveInterfacePrefix(this string text)
            => Regex.Replace(text, InterfacePrefixRegex, string.Empty);

        public static bool HasInterfacePrefix(this string text)
            => Regex.IsMatch(text, InterfacePrefixRegex);

        public static bool IsValidIdentifier(this string text)
            => Regex.IsMatch(text, "^([a-zA-Z_$][0-9a-zA-Z_$]*|[0-9]+)$");

        public static string EscapeBackslashes(this string text)
            => Regex.Replace(text, @"\\", @"\\");

        public static string EscapeQuotes(this string text, QuotationMark quotationMark)
            => quotationMark switch
            {
                QuotationMark.Double => Regex.Replace(text, "\"", "\\\""),
                QuotationMark.Single => Regex.Replace(text, "'", "\\'"),
                _ => throw new ArgumentException("Unknown quotation mark character.")
            };

        public static string ToCamelCase(this string text)
            => !string.IsNullOrEmpty(text) ?
            Regex.Replace(text, "^[A-Z]", char.ToLowerInvariant(text[0]).ToString())
            : text;

        public static string ToKebabCase(this string text)
            => Regex.Replace(text, "(?<![A-Z]|^)([A-Z])", "-$1").ToLowerInvariant();

        public static string Repeat(this string text, int count)
            => string.Concat(Enumerable.Repeat(text, count));

        public static string LineByLine(this IEnumerable<string> lines, string separator = "")
            => string.Join(separator + NewLine, lines);

        public static string Parenthesize(this string text)
            => "(" + text + ")";

        public static string InQuotes(this string text, QuotationMark quotationMark)
            => quotationMark switch
            {
                QuotationMark.Double => '"' + text + '"',
                QuotationMark.Single => "'" + text + "'",
                _ => throw new ArgumentException("Unknown quotation mark character.")
            };

        public static Func<string, string> InQuotes(QuotationMark quotationMark)
            => (string text) => text.InQuotes(quotationMark);

        public static string ToCommaSepratedList(this IEnumerable<string> texts)
            => string.Join(", ", texts);

        public static string ToSpaceSepratedList(this IEnumerable<string> texts)
            => string.Join(" ", texts);

        public static string ToEmptyLineSeparatedList(this IEnumerable<string> texts)
            => string.Join(EmptyLine, texts);

        public static string If(this string text, bool condition)
            => condition ? text : string.Empty;

        public static string TransformIf(this string text, bool condition, Func<string, string> transformation)
            => condition ? transformation(text) : text;

        public static string TransformIfElse(this string text, bool condition, Func<string, string> transformationIf, Func<string, string> transformationElse)
            => condition ? transformationIf(text) : transformationElse(text);

        public static string SquashWhistespace(this string text)
            => Regex.Replace(text, @"\s+", " ");

        public static IEnumerable<string> Indent(this IEnumerable<string> texts, bool useTabs, int? tabSize)
            => texts.Select(t => Indentation(useTabs, tabSize) + t);

        public static string Indentation(bool useTabs, int? tabSize)
            => useTabs ? "\t"
            : tabSize is int @int && tabSize > 0 ? " ".Repeat(@int)
            : throw new ArgumentException("Use tabs for indentation or specify positive tab size (spaces).");

        public static string NewLine => Environment.NewLine;

        public static string NormalizeNewLine(this string text)
            => Regex.Replace(text, @"\r?\n", NewLine);

        public static string EmptyLine => NewLine.Repeat(2);
    }
}