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
        void ValidInstance()
        {
            CreateNew.TextFormatter(ConsoleTheme.None);
        }
    }
}