using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.IO;

namespace Serilog.Sinks.SystemConsole;

/// <summary>
/// Wraps an <see cref="ITextFormatter"/> to provide <see cref="IConsoleFormatter"/> functionality.
/// This allows existing text formatters to be used with the console formatter interface.
/// </summary>
public class TextFormatterConsoleFormatter : IConsoleFormatter
{
    readonly ITextFormatter _textFormatter;

    /// <summary>
    /// Creates a new instance that wraps the provided text formatter.
    /// </summary>
    /// <param name="textFormatter">The text formatter to wrap.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="textFormatter"/> is <code>null</code></exception>
    public TextFormatterConsoleFormatter(ITextFormatter textFormatter)
    {
        _textFormatter = textFormatter ?? throw new ArgumentNullException(nameof(textFormatter));
    }

    /// <summary>
    /// Format the log event using the wrapped text formatter.
    /// Note: The theme parameter is ignored since <see cref="ITextFormatter"/> does not support themes.
    /// </summary>
    /// <param name="logEvent">The log event to format.</param>
    /// <param name="theme">The console theme (ignored by this implementation).</param>
    /// <param name="output">The output writer to write the formatted log event to.</param>
    public void Format(LogEvent logEvent, ConsoleTheme theme, TextWriter output)
    {
        _textFormatter.Format(logEvent, output);
    }
}