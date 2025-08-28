using Serilog.Events;
using Serilog.Sinks.SystemConsole.Output;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.IO;

namespace Serilog.Sinks.SystemConsole;

/// <summary>
/// A console formatter that uses output templates and applies console themes.
/// This is similar to <see cref="OutputTemplateRenderer"/> but implements <see cref="IConsoleFormatter"/>.
/// </summary>
public class OutputTemplateConsoleFormatter : IConsoleFormatter
{
    readonly string _outputTemplate;
    readonly IFormatProvider? _formatProvider;

    /// <summary>
    /// Creates a new output template console formatter.
    /// </summary>
    /// <param name="outputTemplate">A message template describing the format used to write to the sink.</param>
    /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="outputTemplate"/> is <code>null</code></exception>
    public OutputTemplateConsoleFormatter(string outputTemplate, IFormatProvider? formatProvider = null)
    {
        _outputTemplate = outputTemplate ?? throw new ArgumentNullException(nameof(outputTemplate));
        _formatProvider = formatProvider;
    }

    /// <summary>
    /// Format the log event using the output template and apply the provided theme.
    /// </summary>
    /// <param name="logEvent">The log event to format.</param>
    /// <param name="theme">The console theme to apply for styling.</param>
    /// <param name="output">The output writer to write the formatted log event to.</param>
    public void Format(LogEvent logEvent, ConsoleTheme theme, TextWriter output)
    {
        var renderer = new OutputTemplateRenderer(theme, _outputTemplate, _formatProvider);
        renderer.Format(logEvent, output);
    }
}