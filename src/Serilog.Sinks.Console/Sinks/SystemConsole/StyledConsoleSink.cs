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
using System.IO;
using System.Text;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Output;
using Serilog.Sinks.SystemConsole.Themes;

namespace Serilog.Sinks.SystemConsole
{
    class StyledConsoleSink : ILogEventSink
    {
        readonly LogEventLevel? _standardErrorFromLevel;
        readonly ConsoleTheme _theme;
        readonly OutputTemplateRenderer _renderer;
        readonly TextWriter _stdout, _stderr;
        readonly object _syncRoot = new object();

        const int DefaultWriteBuffer = 256;

        public StyledConsoleSink(
            ConsoleTheme theme,
            string outputTemplate,
            IFormatProvider formatProvider,
            LogEventLevel? standardErrorFromLevel)
        {
            _standardErrorFromLevel = standardErrorFromLevel;
            _theme = theme ?? throw new ArgumentNullException(nameof(theme));
            _renderer = new OutputTemplateRenderer(_theme, outputTemplate, formatProvider);

            if (_theme.CanBuffer)
            {
                _stdout = new StreamWriter(Console.OpenStandardOutput(), Console.Out.Encoding);
                _stderr = new StreamWriter(Console.OpenStandardError(), Console.Error.Encoding);
            }
            else
            {
                _stdout = Console.Out;
                _stderr = Console.Error;
            }
        }

        public void Emit(LogEvent logEvent)
        {
            var output = SelectOutputStream(logEvent.Level);

            if (_theme.CanBuffer)
            {
                var buffer = new StringWriter(new StringBuilder(DefaultWriteBuffer));
                _renderer.Render(logEvent, buffer);
                lock (_syncRoot)
                {
                    output.Write(buffer.ToString());
                    output.Flush();
                }
            }
            else
            {
                lock (_syncRoot)
                {
                    _renderer.Render(logEvent, output);
                    output.Flush();
                }
            }
        }

        TextWriter SelectOutputStream(LogEventLevel logEventLevel)
        {
            if (!_standardErrorFromLevel.HasValue)
                return _stdout;

            return logEventLevel < _standardErrorFromLevel ? _stdout : _stderr;
        }
    }
}
