using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System.IO;

namespace Serilog.Sinks.SystemConsole;

/// <summary>
/// Formats log events for console output with access to console theme information.
/// This interface allows for advanced custom formatters that can leverage console themes
/// without requiring a custom sink implementation.
/// </summary>
public interface IConsoleFormatter
{
    /// <summary>
    /// Format the log event and write the result to the provided output.
    /// </summary>
    /// <param name="logEvent">The log event to format.</param>
    /// <param name="theme">The console theme that can be used for styling output.</param>
    /// <param name="output">The output writer to write the formatted log event to.</param>
    void Format(LogEvent logEvent, ConsoleTheme theme, TextWriter output);
}