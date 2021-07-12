// Copyright 2013-2017 Serilog Contributors
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
using Serilog.Parsing;

namespace Serilog.Sinks.SystemConsole.Rendering
{
    static class Padding
    {
        static readonly char[] PaddingChars = new string(' ', 80).ToCharArray();

        /// <summary>
        /// Writes the provided value to the output, applying direction-based padding when <paramref name="alignment"/> is provided.
        /// </summary>
        /// <param name="output">Output object to write result.</param>
        /// <param name="value">Provided value.</param>
        /// <param name="alignment">The alignment settings to apply when rendering <paramref name="value"/>.</param>
        public static void Apply(TextWriter output, string value, Alignment? alignment)
        {
            if (alignment is null || value.Length >= alignment.Value.Width)
            {
                output.Write(value);
                return;
            }

            var pad = alignment.Value.Width - value.Length;

            if (alignment.Value.Direction == AlignmentDirection.Left)
                output.Write(value);

            if (pad <= PaddingChars.Length)
            {
                output.Write(PaddingChars, 0, pad);
            }
            else
            {
                output.Write(new string(' ', pad));
            }

            if (alignment.Value.Direction == AlignmentDirection.Right)
                output.Write(value);
        }
    }
}