// Copyright 2017 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Globalization;
using System.IO;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.SystemConsole.Rendering;
using Serilog.Sinks.SystemConsole.Themes;

namespace Serilog.Sinks.SystemConsole.Output;

class TimestampTokenRenderer : OutputTemplateTokenRenderer
{
    readonly ConsoleTheme _theme;
    readonly PropertyToken _token;
    readonly string? _format;
    readonly IFormatProvider? _formatProvider;
    readonly bool _convertToUtc;

    public TimestampTokenRenderer(ConsoleTheme theme, PropertyToken token, IFormatProvider? formatProvider, bool convertToUtc)
    {
        _theme = theme;
        _token = token;
        _format = token.Format;
        _formatProvider = formatProvider;
        _convertToUtc = convertToUtc;
    }

    public override void Render(LogEvent logEvent, TextWriter output)
    {
        var _ = 0;
        using (_theme.Apply(output, ConsoleThemeStyle.SecondaryText, ref _))
        {
            if (_token.Alignment is null)
            {
                Render(output, logEvent.Timestamp);
            }
            else
            {
                var buffer = new StringWriter();
                Render(buffer, logEvent.Timestamp);
                var str = buffer.ToString();
                Padding.Apply(output, str, _token.Alignment);
            }
        }
    }

    private void Render(TextWriter output, DateTimeOffset timestamp)
    {
        // When a DateTimeOffset is converted to a string, the default format automatically adds the "+00:00" explicit offset to the output string.
        // As the TimestampTokenRenderer is also used for rendering the UtcTimestamp which is always in UTC by definition, the +00:00 suffix should be avoided.
        // This is done using the same approach as Serilog's MessageTemplateTextFormatter. In case output should be converted to UTC, in order to avoid a zone specifier,
        // the DateTimeOffset is converted to a DateTime which then renders as expected.

        var custom = (ICustomFormatter?)_formatProvider?.GetFormat(typeof(ICustomFormatter));
        if (custom != null)
        {
            output.Write(custom.Format(_format, _convertToUtc ? timestamp.UtcDateTime : timestamp, _formatProvider));
            return;
        }

        if (_convertToUtc)
        {
            RenderDateTime(output, timestamp.UtcDateTime);
        }
        else
        {
            RenderDateTimeOffset(output, timestamp);
        }
    }

    private void RenderDateTimeOffset(TextWriter output, DateTimeOffset timestamp)
    {
#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[32];
        if (timestamp.TryFormat(buffer, out int written, _format, _formatProvider ?? CultureInfo.InvariantCulture))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(timestamp.ToString(_format, _formatProvider ?? CultureInfo.InvariantCulture));
#else
            output.Write(timestamp.ToString(_format, _formatProvider ?? CultureInfo.InvariantCulture));
#endif
    }

    private void RenderDateTime(TextWriter output, DateTime utcTimestamp)
    {
#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[32];
        if (utcTimestamp.TryFormat(buffer, out int written, _format, _formatProvider ?? CultureInfo.InvariantCulture))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(utcTimestamp.ToString(_format, _formatProvider ?? CultureInfo.InvariantCulture));
#else
            output.Write(utcTimestamp.ToString(_format, _formatProvider ?? CultureInfo.InvariantCulture));
#endif
    }
}