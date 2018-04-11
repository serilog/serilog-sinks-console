using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.SystemConsole.Output;
using Serilog.Sinks.SystemConsole.Themes;
using System;

namespace Serilog.Sinks.SystemConsole
{
    public static class CreateNew
    {
        const string DefaultConsoleOutputTemplate =
            "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";

        /// <summary>
        /// Creates a new ConsoleSink from the provided arguments.
        /// </summary>
        /// <param name="outputTemplate">The output (message) template used to create the console sink.</param>
        /// <param name="formatProvider">The format provider used to create the console sink.</param>
        /// <param name="standardErrorFromLevel">The standard error level for the console sink.</param>
        /// <returns>An instance of a console sink.</returns>
        public static ILogEventSink ConsoleSink(string outputTemplate = DefaultConsoleOutputTemplate,
                                                IFormatProvider formatProvider = null,
                                                LogEventLevel? standardErrorFromLevel = null)
            => ConsoleSink(Console.IsOutputRedirected || Console.IsErrorRedirected ? ConsoleTheme.None : SystemConsoleThemes.Literate,
                           outputTemplate, formatProvider, standardErrorFromLevel);

        /// <summary>
        /// Creates a new ConsoleSink from the provided arguments.
        /// </summary>
        /// <param name="theme">The theme for which to apply to the console sink.</param>
        /// <param name="outputTemplate">The output (message) template used to create the console sink.</param>
        /// <param name="formatProvider">The format provider used to create the console sink.</param>
        /// <param name="standardErrorFromLevel">The standard error level for the console sink.</param>
        /// <returns>An instance of a console sink.</returns>
        public static ILogEventSink ConsoleSink(ConsoleTheme theme,
                                                string outputTemplate = DefaultConsoleOutputTemplate,
                                                IFormatProvider formatProvider = null,
                                                LogEventLevel? standardErrorFromLevel = null)
            => ConsoleSink(theme, TextFormatter(theme, outputTemplate, formatProvider), standardErrorFromLevel);

        /// <summary>
        /// Creates a new ConsoleSink from the provided arguments.
        /// </summary>
        /// <param name="theme">The theme for which to apply to the console sink.</param>
        /// <param name="formatter">The formatter which to use for the console sink.</param>
        /// <param name="standardErrorFromLevel">The standard error level for the console sink.</param>
        /// <returns>An instance of a console sink.</returns>
        public static ILogEventSink ConsoleSink(ConsoleTheme theme, ITextFormatter formatter,
                                                LogEventLevel? standardErrorFromLevel = null)
            => new ConsoleSink(theme ?? throw new ArgumentNullException(nameof(theme)),
                               formatter ?? throw new ArgumentNullException(nameof(formatter)),
                               standardErrorFromLevel);

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