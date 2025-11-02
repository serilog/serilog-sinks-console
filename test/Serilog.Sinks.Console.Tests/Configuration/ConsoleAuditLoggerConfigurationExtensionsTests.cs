using System;
using System.IO;
using System.Linq;
using Xunit;
using Serilog.Sinks.SystemConsole.Themes;

namespace Serilog.Sinks.Console.Tests.Configuration;

[Collection("ConsoleSequentialTests")]
public class ConsoleAuditLoggerConfigurationExtensionsTests
{
    [Fact]
    public void OutputFormattingIsIgnored()
    {
        using (var stream = new MemoryStream())
        {
            var sw = new StreamWriter(stream);

            System.Console.SetOut(sw);
            var config = new LoggerConfiguration()
                .AuditTo.Console(theme: AnsiConsoleTheme.Literate,
                    applyThemeToRedirectedOutput: false);

            var logger = config.CreateLogger();

            logger.Error("test");
            stream.Position = 0;

            using (var streamReader = new StreamReader(stream))
            {
                var result = streamReader.ReadToEnd();
                var controlCharacterCount = result.Count(c => Char.IsControl(c) && !Char.IsWhiteSpace(c));
                Assert.Equal(0, controlCharacterCount);
            }
        }
    }
        
    [Fact]
    public void OutputFormattingIsPresent()
    {
        using (var stream = new MemoryStream())
        {
            var sw = new StreamWriter(stream);

            System.Console.SetOut(sw);
            var config = new LoggerConfiguration()
                .AuditTo.Console(theme: AnsiConsoleTheme.Literate,
                    applyThemeToRedirectedOutput: true);

            var logger = config.CreateLogger();

            logger.Error("test");
            stream.Position = 0;

            using (var streamReader = new StreamReader(stream))
            {
                var result = streamReader.ReadToEnd();
                var controlCharacterCount = result.Count(c => Char.IsControl(c) && !Char.IsWhiteSpace(c));
                Assert.NotEqual(0, controlCharacterCount);
            }
        }
    }
}