using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.IO;

namespace ConsoleFormatterExample;

public class CustomConsoleFormatter : IConsoleFormatter
{
    public void Format(LogEvent logEvent, ConsoleTheme theme, TextWriter output)
    {
        var levelPrefix = GetLevelPrefix(logEvent.Level);

        output.Write(levelPrefix);
        output.Write(" ");
        output.Write($"[{logEvent.Timestamp:HH:mm:ss}]");
        output.Write(" ");
        output.Write(logEvent.RenderMessage());
        output.WriteLine(" ");

        // Render exception if present
        if (logEvent.Exception != null)
        {
            output.WriteLine(logEvent.Exception.ToString());
        }
    }

    private static string GetLevelPrefix(LogEventLevel level) => level switch
    {
        LogEventLevel.Verbose => "TRACE",
        LogEventLevel.Debug => "DEBUG",
        LogEventLevel.Information => "INFO",
        LogEventLevel.Warning => "WARN",
        LogEventLevel.Error => "ERROR",
        LogEventLevel.Fatal => "FATAL",
        _ => "UNKNOWN"
    };
}

// Example formatter that uses JSON output
public class JsonConsoleFormatter : IConsoleFormatter
{
    public void Format(LogEvent logEvent, ConsoleTheme theme, TextWriter output)
    {
        // Custom JSON-like formatting
        output.Write("{");
        output.Write($"\"timestamp\":\"{logEvent.Timestamp:yyyy-MM-ddTHH:mm:ss.fffZ}\",");
        output.Write($"\"level\":\"{logEvent.Level}\",");
        output.Write($"\"message\":\"{logEvent.RenderMessage().Replace("\"", "\\\"")}\"");

        if (logEvent.Exception != null)
        {
            output.Write($",\"exception\":\"{logEvent.Exception.Message.Replace("\"", "\\\"")}\"");
        }

        output.WriteLine("}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== IConsoleFormatter Demo ===\n");

        // Example 1: Using custom console formatter
        Console.WriteLine("1. Custom Console Formatter:");
        var logger1 = new LoggerConfiguration()
            .WriteTo.Console(new CustomConsoleFormatter(), AnsiConsoleTheme.Literate)
            .CreateLogger();

        logger1.Information("This is an info message with custom formatting");
        logger1.Warning("This is a warning message");
        logger1.Error("This is an error message");

        Console.WriteLine();

        // Example 2: Using JSON console formatter
        Console.WriteLine("2. JSON Console Formatter:");
        var logger2 = new LoggerConfiguration()
            .WriteTo.Console(new JsonConsoleFormatter(), ConsoleTheme.None)
            .CreateLogger();

        logger2.Information("This message will be formatted as JSON");
        logger2.Warning("JSON formatting works regardless of theme");

        Console.WriteLine();

        // Example 3: Using OutputTemplateConsoleFormatter  
        Console.WriteLine("3. Output Template Console Formatter:");
        var logger3 = new LoggerConfiguration()
            .WriteTo.Console(
                new OutputTemplateConsoleFormatter("[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}"),
                AnsiConsoleTheme.Literate)
            .CreateLogger();

        logger3.Information("This uses an output template with theme");
        logger3.Warning("Template formatting with theme colors");

        Console.WriteLine();

        // Example 4: Traditional approach still works
        Console.WriteLine("4. Traditional approach (for comparison):");
        var logger4 = new LoggerConfiguration()
            .WriteTo.Console(
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}",
                theme: AnsiConsoleTheme.Literate)
            .CreateLogger();

        logger4.Information("This is the traditional approach");
        logger4.Warning("Still works as before");

        Console.WriteLine("\n=== Demo Complete ===");
        Console.WriteLine("\nKey Benefits of IConsoleFormatter:");
        Console.WriteLine("- Complete control over formatting logic");
        Console.WriteLine("- Access to ConsoleTheme for styling (when using public APIs)");
        Console.WriteLine("- No need to write a custom sink");
        Console.WriteLine("- Backward compatibility with existing approaches");
    }
}