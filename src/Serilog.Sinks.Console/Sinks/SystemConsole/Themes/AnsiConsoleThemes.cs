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

using System.Collections.Generic;

namespace Serilog.Sinks.SystemConsole.Themes
{
    static class AnsiConsoleThemes
    {
        public static AnsiConsoleTheme Literate { get; } = new AnsiConsoleTheme(
            new Dictionary<ConsoleThemeStyle, string>
            {
                [ConsoleThemeStyle.Text] = "\u001b[37;1m",
                [ConsoleThemeStyle.SecondaryText] = "\u001b[37m",
                [ConsoleThemeStyle.Punctuation] = "\u001b[30;1m",
                [ConsoleThemeStyle.Invalid] = "\u001b[33;1m",
                [ConsoleThemeStyle.Null] = "\u001b[34;1m",
                [ConsoleThemeStyle.Name] = "\u001b[37m",
                [ConsoleThemeStyle.String] = "\u001b[36;1m",
                [ConsoleThemeStyle.Number] = "\u001b[35;1m",
                [ConsoleThemeStyle.Boolean] = "\u001b[34;1m",
                [ConsoleThemeStyle.Object] = "\u001b[32;1m",
                [ConsoleThemeStyle.LevelVerbose] = "\u001b[37m",
                [ConsoleThemeStyle.LevelDebug] = "\u001b[37m",
                [ConsoleThemeStyle.LevelInformation] = "\u001b[37;1m",
                [ConsoleThemeStyle.LevelWarning] = "\u001b[33;1m",
                [ConsoleThemeStyle.LevelError] = "\u001b[37;1m\u001b[41;1m",
                [ConsoleThemeStyle.LevelFatal] = "\u001b[37;1m\u001b[41;1m"
            });
    }
}
