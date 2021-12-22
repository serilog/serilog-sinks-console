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
                [ConsoleThemeStyle.Text] = "\x1b[38;5;0015m",
                [ConsoleThemeStyle.SecondaryText] = "\x1b[38;5;0007m",
                [ConsoleThemeStyle.TertiaryText] = "\x1b[38;5;0008m",
                [ConsoleThemeStyle.Invalid] = "\x1b[38;5;0011m",
                [ConsoleThemeStyle.Null] = "\x1b[38;5;0027m",
                [ConsoleThemeStyle.Name] = "\x1b[38;5;0007m",
                [ConsoleThemeStyle.String] = "\x1b[38;5;0045m",
                [ConsoleThemeStyle.Number] = "\x1b[38;5;0200m",
                [ConsoleThemeStyle.Boolean] = "\x1b[38;5;0027m",
                [ConsoleThemeStyle.Scalar] = "\x1b[38;5;0085m",
                [ConsoleThemeStyle.LevelVerbose] = "\x1b[38;5;0007m",
                [ConsoleThemeStyle.LevelDebug] = "\x1b[38;5;0007m",
                [ConsoleThemeStyle.LevelInformation] = "\x1b[38;5;0015m",
                [ConsoleThemeStyle.LevelWarning] = "\x1b[38;5;0011m",
                [ConsoleThemeStyle.LevelError] = "\x1b[38;5;0015m\x1b[48;5;0196m",
                [ConsoleThemeStyle.LevelFatal] = "\x1b[38;5;0015m\x1b[48;5;0196m",
            });

        public static AnsiConsoleTheme Grayscale { get; } = new AnsiConsoleTheme(
            new Dictionary<ConsoleThemeStyle, string>
            {
                [ConsoleThemeStyle.Text] = "\x1b[37;1m",
                [ConsoleThemeStyle.SecondaryText] = "\x1b[37m",
                [ConsoleThemeStyle.TertiaryText] = "\x1b[30;1m",
                [ConsoleThemeStyle.Invalid] = "\x1b[37;1m\x1b[47m",
                [ConsoleThemeStyle.Null] = "\x1b[1m\x1b[37;1m",
                [ConsoleThemeStyle.Name] = "\x1b[37m",
                [ConsoleThemeStyle.String] = "\x1b[1m\x1b[37;1m",
                [ConsoleThemeStyle.Number] = "\x1b[1m\x1b[37;1m",
                [ConsoleThemeStyle.Boolean] = "\x1b[1m\x1b[37;1m",
                [ConsoleThemeStyle.Scalar] = "\x1b[1m\x1b[37;1m",
                [ConsoleThemeStyle.LevelVerbose] = "\x1b[30;1m",
                [ConsoleThemeStyle.LevelDebug] = "\x1b[30;1m",
                [ConsoleThemeStyle.LevelInformation] = "\x1b[37;1m",
                [ConsoleThemeStyle.LevelWarning] = "\x1b[37;1m\x1b[47m",
                [ConsoleThemeStyle.LevelError] = "\x1b[30m\x1b[47m",
                [ConsoleThemeStyle.LevelFatal] = "\x1b[30m\x1b[47m",
            });

        public static AnsiConsoleTheme Code { get; } = new AnsiConsoleTheme(
            new Dictionary<ConsoleThemeStyle, string>
            {
                [ConsoleThemeStyle.Text] = "\x1b[38;5;0253m",
                [ConsoleThemeStyle.SecondaryText] = "\x1b[38;5;0246m",
                [ConsoleThemeStyle.TertiaryText] = "\x1b[38;5;0242m",
                [ConsoleThemeStyle.Invalid] = "\x1b[33;1m",
                [ConsoleThemeStyle.Null] = "\x1b[38;5;0038m",
                [ConsoleThemeStyle.Name] = "\x1b[38;5;0081m",
                [ConsoleThemeStyle.String] = "\x1b[38;5;0216m",
                [ConsoleThemeStyle.Number] = "\x1b[38;5;151m",
                [ConsoleThemeStyle.Boolean] = "\x1b[38;5;0038m",
                [ConsoleThemeStyle.Scalar] = "\x1b[38;5;0079m",
                [ConsoleThemeStyle.LevelVerbose] = "\x1b[37m",
                [ConsoleThemeStyle.LevelDebug] = "\x1b[37m",
                [ConsoleThemeStyle.LevelInformation] = "\x1b[37;1m",
                [ConsoleThemeStyle.LevelWarning] = "\x1b[38;5;0229m",
                [ConsoleThemeStyle.LevelError] = "\x1b[38;5;0197m\x1b[48;5;0238m",
                [ConsoleThemeStyle.LevelFatal] = "\x1b[38;5;0197m\x1b[48;5;0238m",
            });

        public static AnsiConsoleTheme Sixteen { get; } = new AnsiConsoleTheme(
            new Dictionary<ConsoleThemeStyle, string>
            {
                [ConsoleThemeStyle.Text] = AnsiEscapeSequence.Unthemed,
                [ConsoleThemeStyle.SecondaryText] = AnsiEscapeSequence.Unthemed,
                [ConsoleThemeStyle.TertiaryText] = AnsiEscapeSequence.Unthemed,
                [ConsoleThemeStyle.Invalid] = AnsiEscapeSequence.Yellow,
                [ConsoleThemeStyle.Null] = AnsiEscapeSequence.Blue,
                [ConsoleThemeStyle.Name] = AnsiEscapeSequence.Unthemed,
                [ConsoleThemeStyle.String] = AnsiEscapeSequence.Cyan,
                [ConsoleThemeStyle.Number] = AnsiEscapeSequence.Magenta,
                [ConsoleThemeStyle.Boolean] = AnsiEscapeSequence.Blue,
                [ConsoleThemeStyle.Scalar] = AnsiEscapeSequence.Green,
                [ConsoleThemeStyle.LevelVerbose] = AnsiEscapeSequence.Unthemed,
                [ConsoleThemeStyle.LevelDebug] = AnsiEscapeSequence.Bold,
                [ConsoleThemeStyle.LevelInformation] = AnsiEscapeSequence.BrightCyan,
                [ConsoleThemeStyle.LevelWarning] = AnsiEscapeSequence.BrightYellow,
                [ConsoleThemeStyle.LevelError] = AnsiEscapeSequence.BrightRed,
                [ConsoleThemeStyle.LevelFatal] = AnsiEscapeSequence.BrightRed,
            });
    }
}
