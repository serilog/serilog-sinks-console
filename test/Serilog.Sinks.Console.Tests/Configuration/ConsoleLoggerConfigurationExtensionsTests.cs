using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using System.Linq.Expressions;
using Serilog.Sinks.SystemConsole.Themes;

namespace Serilog.Sinks.Console.Tests.Configuration
{
    public class ConsoleLoggerConfigurationExtensionsTests
    {
        [Fact]
        public void OutputFormattingIsIgnored()
        {
            using (var stream = new MemoryStream())
            {
                var sw = new StreamWriter(stream);

                System.Console.SetOut(sw);
                var config = new LoggerConfiguration()
                    .WriteTo.Console(theme: AnsiConsoleTheme.Literate,
                        applyThemeOnOutputRedirection: false);

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
                    .WriteTo.Console(theme: AnsiConsoleTheme.Literate,
                        applyThemeOnOutputRedirection: true);

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
}