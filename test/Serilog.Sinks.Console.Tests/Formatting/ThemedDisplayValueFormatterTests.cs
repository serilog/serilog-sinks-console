using System.IO;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Formatting;
using Serilog.Sinks.SystemConsole.Themes;
using Xunit;

namespace Serilog.Sinks.Console.Tests.Formatting
{
    public class ThemedDisplayValueFormatterTests
    {
        [Theory]
        [InlineData("Hello", null, "\"Hello\"")]
        [InlineData("Hello", "l", "Hello")]
        public void StringFormattingIsApplied(string s, string format, string expected)
        {
            var f = new ThemedDisplayValueFormatter(ConsoleTheme.None, null);
            var sw = new StringWriter();
            f.FormatLiteralValue(new ScalarValue(s), sw, format);
            var actual = sw.ToString();
            Assert.Equal(expected, actual);
        }
    }
}
