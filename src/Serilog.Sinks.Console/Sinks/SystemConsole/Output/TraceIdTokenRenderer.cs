using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.SystemConsole.Rendering;
using Serilog.Sinks.SystemConsole.Themes;

namespace Serilog.Sinks.SystemConsole.Output;

class TraceIdTokenRenderer : OutputTemplateTokenRenderer
{
    readonly ConsoleTheme _theme;
    readonly Alignment? _alignment;

    public TraceIdTokenRenderer(ConsoleTheme theme, PropertyToken traceIdToken)
    {
        _theme = theme;
        _alignment = traceIdToken.Alignment;
    }

    public override void Render(LogEvent logEvent, TextWriter output)
    {
        if (logEvent.TraceId is not { } traceId)
            return;

        var _ = 0;
        using (_theme.Apply(output, ConsoleThemeStyle.Text, ref _))
        {
            if (_alignment is {} alignment)
                Padding.Apply(output, traceId.ToString(), alignment);
            else
                output.Write(traceId);
        }
    }
}