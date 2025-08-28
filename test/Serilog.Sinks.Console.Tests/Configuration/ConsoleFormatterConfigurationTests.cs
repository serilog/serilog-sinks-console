using Serilog.Events;
using Serilog.Sinks.SystemConsole;
using Serilog.Sinks.SystemConsole.Themes;
using System.IO;
using Xunit;

namespace Serilog.Sinks.Console.Tests.Configuration;

public class ConsoleFormatterConfigurationTests
{
    class TestConsoleFormatter : IConsoleFormatter
    {
        public static int _formatCallCount = 0;

        public void Format(LogEvent logEvent, ConsoleTheme theme, TextWriter output)
        {
            _formatCallCount++;
            output.Write($"Custom: {logEvent.MessageTemplate.Text}");
        }
    }

    [Fact]
    public void CanConfigureWithConsoleFormatter()
    {
        TestConsoleFormatter._formatCallCount = 0;

        using var stream = new MemoryStream();
        var sw = new StreamWriter(stream);
        System.Console.SetOut(sw);

        var config = new LoggerConfiguration()
            .WriteTo.Console(new TestConsoleFormatter(), ConsoleTheme.None);

        var logger = config.CreateLogger();
        logger.Information("Test message");

        sw.Flush();
        stream.Position = 0;

        using var streamReader = new StreamReader(stream);
        var result = streamReader.ReadToEnd();

        Assert.Equal(1, TestConsoleFormatter._formatCallCount);
        Assert.Contains("Custom: Test message", result);
    }

    [Fact]
    public void ConsoleFormatterConfigurationSupportsAllParameters()
    {
        var formatter = new TestConsoleFormatter();

        var config = new LoggerConfiguration()
            .WriteTo.Console(
                consoleFormatter: formatter,
                theme: SystemConsoleThemes.Literate,
                restrictedToMinimumLevel: LogEventLevel.Warning,
                applyThemeToRedirectedOutput: true);

        var logger = config.CreateLogger();

        // This should not reach the formatter since it's below the minimum level
        TestConsoleFormatter._formatCallCount = 0;
        logger.Information("This should not appear");
        Assert.Equal(0, TestConsoleFormatter._formatCallCount);

        // This should reach the formatter
        logger.Warning("This should appear");
        Assert.Equal(1, TestConsoleFormatter._formatCallCount);
    }
}