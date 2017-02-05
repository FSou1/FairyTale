using System;
using FT.Components.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FT.Tests.Utility {
    [TestClass]
    public class StringFormatterTests {
        [TestMethod]
        public void FormatAddsNewLinesAndWrapWithParagraphsPureText() {
            // act
            var text = " <p>Hello  </p> dear<p> world</p> ";

            var options = FormatTextOptions.Split 
                | FormatTextOptions.Wrap | FormatTextOptions.Join 
                | FormatTextOptions.Trim | FormatTextOptions.Filter;

            var formatOptions = new FairyTalesFormatterOptions {
                SplitSeparator = new[] {"<p>", "</p>"},
                WrapStart = "<p>",
                WrapEnd = "</p>",
                JoinSeparator = $"{Environment.NewLine}{Environment.NewLine}",
                TrimChars = new [] { ' ' },
                FilterCondition = x => string.IsNullOrEmpty(x) || string.IsNullOrWhiteSpace(x)
            };

            // arrange

            text = StringFormatter.Format(text, options, formatOptions);

            // assert
            Assert.AreEqual(text, $"<p>Hello</p>{Environment.NewLine}{Environment.NewLine}" +
                                  $"<p>dear</p>{Environment.NewLine}{Environment.NewLine}" +
                                  $"<p>world</p>");
        }
    }
}