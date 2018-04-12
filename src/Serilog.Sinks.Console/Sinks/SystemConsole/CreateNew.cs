using Serilog.Formatting;
using Serilog.Sinks.SystemConsole.Output;
using Serilog.Sinks.SystemConsole.Themes;
using System;

namespace Serilog.Sinks.SystemConsole
{
    /// <summary>
    /// Static factory class for creating text formatters and sinks for the console sink.
    /// </summary>
    public static class CreateNew
    {
        const string DefaultConsoleOutputTemplate =
            "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";

        /// <summary>
        /// Creates a new text formatter to be used in the creation of a console sink.
        /// </summary>
        /// <param name="theme">The theme for which to apply to the console sink.</param>
        /// <param name="outputTemplate">The output (message) template used to create the console sink.</param>
        /// <param name="formatProvider">The format provider used to create the console sink.</param>
        /// <returns></returns>
        public static ITextFormatter TextFormatter(ConsoleTheme theme,
                                                   string outputTemplate = DefaultConsoleOutputTemplate,
                                                   IFormatProvider formatProvider = null)
            => new OutputTemplateRenderer(theme ?? throw new ArgumentNullException(nameof(theme)),
                                          outputTemplate,
                                          formatProvider);
    }
}