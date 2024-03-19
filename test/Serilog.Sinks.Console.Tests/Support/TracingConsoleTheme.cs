using Serilog.Sinks.SystemConsole.Themes;

namespace Serilog.Sinks.SystemConsole.Tests;

class TracingConsoleTheme : ConsoleTheme
{
    const string End = "</>";

    public override bool CanBuffer => true;

    protected override int ResetCharCount { get; } = End.Length;

    public override int Set(TextWriter output, ConsoleThemeStyle style)
    {
        var start = $"<{style.ToString().ToLowerInvariant()}>";
        output.Write(start);
        return start.Length;
    }

    public override void Reset(TextWriter output)
    {
        output.Write(End);
    }
}