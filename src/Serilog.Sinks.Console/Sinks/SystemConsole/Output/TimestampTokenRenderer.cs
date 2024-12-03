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
    readonly IFormatProvider? _formatProvider;
    readonly bool _convertToUtc;

    public TimestampTokenRenderer(ConsoleTheme theme, PropertyToken token, IFormatProvider? formatProvider, bool convertToUtc)
    {
            _theme = theme;
            _token = token;
            _formatProvider = formatProvider;
            _convertToUtc = convertToUtc;
    }

    public override void Render(LogEvent logEvent, TextWriter output)
    {
        var timestamp = _convertToUtc
            ? logEvent.Timestamp.ToUniversalTime()
            : logEvent.Timestamp;
        var sv = new DateTimeOffsetValue(timestamp);

            var _ = 0;
            using (_theme.Apply(output, ConsoleThemeStyle.SecondaryText, ref _))
            {
                if (_token.Alignment is null)
                {
                    sv.Render(output, _token.Format, _formatProvider);
                }
                else
                {
                    var buffer = new StringWriter();
                    sv.Render(buffer, _token.Format, _formatProvider);
                    var str = buffer.ToString();
                    Padding.Apply(output, str, _token.Alignment);
                }
            }
        }

    readonly struct DateTimeOffsetValue
    {
        public DateTimeOffsetValue(DateTimeOffset value)
        {
                Value = value;
            }

        public DateTimeOffset Value { get; }

        public void Render(TextWriter output, string? format = null, IFormatProvider? formatProvider = null)
        {
            var custom = (ICustomFormatter?)formatProvider?.GetFormat(typeof(ICustomFormatter));
            if (custom != null)
            {
                output.Write(custom.Format(format, Value, formatProvider));
                return;
            }

#if FEATURE_SPAN
                Span<char> buffer = stackalloc char[32];
                if (Value.TryFormat(buffer, out int written, format, formatProvider ?? CultureInfo.InvariantCulture))
                    output.Write(buffer.Slice(0, written));
                else
                    output.Write(Value.ToString(format, formatProvider ?? CultureInfo.InvariantCulture));
#else
            output.Write(Value.ToString(format, formatProvider ?? CultureInfo.InvariantCulture));
#endif
        }
    }
}