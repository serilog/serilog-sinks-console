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
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Serilog.Sinks.SystemConsole.Themes
{
    /// <summary>
    /// A console theme using the styling facilities of the <see cref="System.Console"/> class. Recommended
    /// for Windows versions prior to Windows 10.
    /// </summary>
    public class SystemConsoleTheme : ConsoleTheme
    {
        /// <summary>
        /// A theme using only gray, black and white.
        /// </summary>
        public static SystemConsoleTheme Grayscale { get; } = SystemConsoleThemes.Grayscale;

        /// <summary>
        /// A theme in the syle of the original <i>Serilog.Sinks.Literate</i>.
        /// </summary>
        public static SystemConsoleTheme Literate { get; } = SystemConsoleThemes.Literate;

        /// <summary>
        /// A theme based on the original Serilog "colored console" sink.
        /// </summary>
        public static SystemConsoleTheme Colored { get; } = SystemConsoleThemes.Colored;

        readonly IReadOnlyDictionary<ConsoleThemeStyle, SystemConsoleThemeStyle> _styles;

        /// <summary>
        /// Construct a theme given a set of styles.
        /// </summary>
        /// <param name="styles">Styles to apply within the theme.</param>
        public SystemConsoleTheme(IReadOnlyDictionary<ConsoleThemeStyle, SystemConsoleThemeStyle> styles)
        {
            if (styles == null) throw new ArgumentNullException(nameof(styles));
            _styles = styles.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        /// <inheritdoc/>
        public IReadOnlyDictionary<ConsoleThemeStyle, SystemConsoleThemeStyle> Styles => _styles;

        /// <inheritdoc/>
        public override bool CanBuffer => false;

        /// <inheritdoc/>
        protected override int ResetCharCount { get; } = 0;

        /// <inheritdoc/>
        public override int Set(TextWriter output, ConsoleThemeStyle style)
        {
            if (_styles.TryGetValue(style, out var wcts))
            {
                if (wcts.Foreground.HasValue)
                    Console.ForegroundColor = wcts.Foreground.Value;
                if (wcts.Background.HasValue)
                    Console.BackgroundColor = wcts.Background.Value;
            }

            return 0;
        }

        /// <inheritdoc/>
        public override void Reset(TextWriter output)
        {
            Console.ResetColor();
        }
    }
}