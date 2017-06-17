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
                [ConsoleThemeStyle.Text] = "WHITE",
                [ConsoleThemeStyle.SecondaryText] = "GRAY",
                [ConsoleThemeStyle.Punctuation] = "DARKGRAY",
                [ConsoleThemeStyle.Invalid] = "YELLOW",
                [ConsoleThemeStyle.Null] = "BLUE",
                [ConsoleThemeStyle.Name] = "GRAY",
                [ConsoleThemeStyle.String] = "CYAN",
                [ConsoleThemeStyle.Number] = "MAGENTA",
                [ConsoleThemeStyle.Boolean] = "BLUE",
                [ConsoleThemeStyle.Object] = "GREEN",
                [ConsoleThemeStyle.LevelVerbose] = "GRAY",
                [ConsoleThemeStyle.LevelDebug] = "GRAY",
                [ConsoleThemeStyle.LevelInformation] = "WHITE",
                [ConsoleThemeStyle.LevelWarning] = "YELLOW",
                [ConsoleThemeStyle.LevelError] = "WHITE-RED",
                [ConsoleThemeStyle.LevelFatal] = "WHITE-RED"
            });
    }
}
