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

using System.IO;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.SystemConsole.Themes;

namespace Serilog.Sinks.SystemConsole.Output
{
    class ExceptionTokenRenderer : OutputTemplateTokenRenderer
    {
        const string StackFrameLinePrefix = "   ";

        readonly ConsoleTheme _theme;

        public ExceptionTokenRenderer(ConsoleTheme theme, PropertyToken pt)
        {
            _theme = theme;
        }

        public override void Render(LogEvent logEvent, TextWriter output)
        {
            // Padding is never applied by this renderer.

            if (logEvent.Exception is null)
                return;

            var lines = new StringReader(logEvent.Exception.ToString());
            string? nextLine;
            while ((nextLine = lines.ReadLine()) != null)
            {
                var style = nextLine.StartsWith(StackFrameLinePrefix) ? ConsoleThemeStyle.SecondaryText : ConsoleThemeStyle.Text;
                var _ = 0;
                using (_theme.Apply(output, style, ref _))
                    output.Write(nextLine);
                output.WriteLine();
            }
        }
    }
}