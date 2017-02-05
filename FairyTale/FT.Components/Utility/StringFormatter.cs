using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FT.Components.Utility {
    public static class StringFormatter {
        public static string Format(string text, FormatTextOptions options, FairyTalesFormatterOptions formatOptions) {

            IList<string> parts = new List<string>();

            if (options.HasFlag(FormatTextOptions.Split)) {
                parts = Split(text, formatOptions.SplitSeparator).ToList();
            }
            if (options.HasFlag(FormatTextOptions.Trim)) {
                parts = Trim(parts, formatOptions.TrimChars).ToList();
            }
            if (options.HasFlag(FormatTextOptions.Filter)) {
                parts = Filter(parts, formatOptions.FilterCondition);
            }
            if (options.HasFlag(FormatTextOptions.Wrap)) {
                parts = Wrap(parts, formatOptions.WrapStart, formatOptions.WrapEnd).ToList();
            }

            return string.Join(formatOptions.JoinSeparator, parts);
        }

        private static IEnumerable<string> Split(string text, string[] separator) {
            return text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        private static IEnumerable<string> Wrap(IEnumerable<string> lines, string start, string end) {
            return lines.Select(s => $"{start}{s}{end}");
        }

        private static IEnumerable<string> Trim(IEnumerable<string> lines, char[] trimChars) {
            return lines.Select(s => s.Trim(trimChars));
        }

        private static IList<string> Filter(IEnumerable<string> parts, Expression<Func<string, bool>> filter) {
            return parts.Where(s => !filter.Compile().Invoke(s)).ToList();
        }
    }

    [Flags]
    public enum FormatTextOptions {
        Split,
        Wrap,
        Join,
        Trim,
        Filter
    }

    public class FairyTalesFormatterOptions {
        public string[] SplitSeparator { get; set; }
        public string WrapStart { get; set; }
        public string WrapEnd { get; set; }
        public string JoinSeparator { get; set; }
        public char[] TrimChars { get; set; }
        public Expression<Func<string, bool>> FilterCondition { get; set; }
    }
}