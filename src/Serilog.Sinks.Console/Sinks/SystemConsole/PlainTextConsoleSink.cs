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
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.SystemConsole
{
    class PlainTextConsoleSink : ILogEventSink
    {
        readonly ITextFormatter _textFormatter;
        readonly LogEventLevel? _standardErrorFromLevel;

        public PlainTextConsoleSink(ITextFormatter textFormatter, LogEventLevel? standardErrorFromLevel)
        {
            _textFormatter = textFormatter ?? throw new ArgumentNullException(nameof(textFormatter));
            _standardErrorFromLevel = standardErrorFromLevel;
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));
            var renderSpace = new StringWriter();
            var outputStream = LeveledStreamSelector.GetOutputStream(_standardErrorFromLevel, logEvent.Level);
            _textFormatter.Format(logEvent, renderSpace);
            outputStream.Write(renderSpace.ToString());
        }
    }
}
