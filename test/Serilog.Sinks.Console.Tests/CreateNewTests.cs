using Serilog.Sinks.SystemConsole;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using Xunit;

namespace Serilog.Sinks.Console.Tests
{
    public sealed class CreateNewTests
    {
        [Fact]
        void TextFormatterNullThemeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => CreateNew.TextFormatter(null));
        }

        [Fact]
        void ConsoleSinkNullThemeAndFormatProviderThrows()
        {
            Assert.Throws<ArgumentNullException>(() => CreateNew.ConsoleSink(null, (IFormatProvider)null));
        }

        [Fact]
        void ValidInstance()
        {
            CreateNew.ConsoleSink(ConsoleTheme.None, CreateNew.TextFormatter(ConsoleTheme.None));
        }
    }
}