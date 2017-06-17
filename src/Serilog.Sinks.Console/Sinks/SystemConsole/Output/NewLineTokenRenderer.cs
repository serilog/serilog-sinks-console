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
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.SystemConsole.Rendering;

namespace Serilog.Sinks.SystemConsole.Output
{
    class NewLineTokenRenderer : OutputTemplateTokenRenderer
    {
        readonly Alignment? _alignment;

        public NewLineTokenRenderer(Alignment? alignment)
        {
            _alignment = alignment;
        }

        public override void Render(LogEvent logEvent, TextWriter output)
        {
            if (_alignment.HasValue)
                Padding.Apply(output, Environment.NewLine, _alignment.Value.Widen(Environment.NewLine.Length));
            else
                output.WriteLine();
        }
    }
}