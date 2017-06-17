﻿// Copyright 2017 Serilog Contributors
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
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Serilog.Sinks.SystemConsole.Themes
{
    /// <summary>
    /// A console theme using the ANSI terminal escape sequences. Recommended
    /// for Linux and Windows 10+.
    /// </summary>
    public class AnsiConsoleTheme : ConsoleTheme
    {
        /// <summary>
        /// A theme in the syle of the original <i>Serilog.Sinks.Literate</i>.
        /// </summary>
        public static AnsiConsoleTheme Literate { get; } = AnsiConsoleThemes.Literate;

        readonly IReadOnlyDictionary<ConsoleThemeStyle, string> _styles;
        const string AnsiStyleReset = "RESET";

        /// <summary>
        /// Construct a theme given a set of styles.
        /// </summary>
        /// <param name="styles">Styles to apply within the theme.</param>
        public AnsiConsoleTheme(IReadOnlyDictionary<ConsoleThemeStyle, string> styles)
        {
            if (styles == null) throw new ArgumentNullException(nameof(styles));
            _styles = styles.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        /// <inheritdoc/>
        public override bool CanBuffer => true;

        /// <inheritdoc/>
        protected override int ResetCharCount { get; } = AnsiStyleReset.Length;

        /// <inheritdoc/>
        public override int Set(TextWriter output, ConsoleThemeStyle style)
        {
            if (_styles.TryGetValue(style, out var ansiStyle))
            {
                output.Write(ansiStyle);
                return ansiStyle.Length;
            }
            return 0;
        }

        /// <inheritdoc/>
        public override void Reset(TextWriter output)
        {
            output.Write(AnsiStyleReset);
        }
    }
}