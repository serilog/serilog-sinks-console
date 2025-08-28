using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.SystemConsole;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Serilog.Sinks.Console.Tests.Formatting;

public class IConsoleFormatterTests
{
    class TestConsoleFormatter : IConsoleFormatter
    {
        public string LastThemeName { get; private set; } = "";
        public LogEvent? LastLogEvent { get; private set; }

        public void Format(LogEvent logEvent, ConsoleTheme theme, TextWriter output)
        {
            LastLogEvent = logEvent;
            LastThemeName = theme.GetType().Name;
            output.Write($"[{theme.GetType().Name}] {logEvent.MessageTemplate.Text}");
        }
    }

    [Fact]
    public void ConsoleFormatterReceivesLogEventAndTheme()
    {
        var formatter = new TestConsoleFormatter();
        var theme = SystemConsoleThemes.Literate;
        var logEvent = new LogEvent(
            DateTimeOffset.UtcNow,
            LogEventLevel.Information,
            null,
            MessageTemplate.Empty,
            Enumerable.Empty<LogEventProperty>());

        var output = new StringWriter();
        formatter.Format(logEvent, theme, output);

        Assert.Same(logEvent, formatter.LastLogEvent);
        Assert.Equal("SystemConsoleTheme", formatter.LastThemeName);
        Assert.Contains("[SystemConsoleTheme]", output.ToString());
    }

    [Fact]
    public void TextFormatterConsoleFormatterWrapsTextFormatter()
    {
        var textFormatter = new TestTextFormatter();
        var consoleFormatter = new TextFormatterConsoleFormatter(textFormatter);
        var theme = SystemConsoleThemes.Literate;
        var logEvent = new LogEvent(
            DateTimeOffset.UtcNow,
            LogEventLevel.Information,
            null,
            MessageTemplate.Empty,
            Enumerable.Empty<LogEventProperty>());

        var output = new StringWriter();
        consoleFormatter.Format(logEvent, theme, output);

        Assert.Same(logEvent, textFormatter.LastLogEvent);
        Assert.Equal("Test formatted", output.ToString());
    }

    [Fact]
    public void OutputTemplateConsoleFormatterUsesTemplateAndTheme()
    {
        var formatter = new OutputTemplateConsoleFormatter("{Message}");
        var theme = ConsoleTheme.None;
        var logEvent = new LogEvent(
            DateTimeOffset.UtcNow,
            LogEventLevel.Information,
            null,
            new MessageTemplateParser().Parse("Hello World"),
            Enumerable.Empty<LogEventProperty>());

        var output = new StringWriter();
        formatter.Format(logEvent, theme, output);

        Assert.Equal("Hello World", output.ToString());
    }

    class TestTextFormatter : Serilog.Formatting.ITextFormatter
    {
        public LogEvent? LastLogEvent { get; private set; }

        public void Format(LogEvent logEvent, TextWriter output)
        {
            LastLogEvent = logEvent;
            output.Write("Test formatted");
        }
    }
}