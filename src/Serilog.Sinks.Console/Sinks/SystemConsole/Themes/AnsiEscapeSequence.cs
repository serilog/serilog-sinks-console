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

namespace Serilog.Sinks.SystemConsole.Themes
{
    static class AnsiEscapeSequence
    {
        public const string Unthemed = "";
        public const string Reset = "\x1b[0m";
        public const string Bold = "\x1b[1m";

        public const string Black = "\x1b[30m";
        public const string Red = "\x1b[31m";
        public const string Green = "\x1b[32m";
        public const string Yellow = "\x1b[33m";
        public const string Blue = "\x1b[34m";
        public const string Magenta = "\x1b[35m";
        public const string Cyan = "\x1b[36m";
        public const string White = "\x1b[37m";

        public const string BrightBlack = "\x1b[30;1m";
        public const string BrightRed = "\x1b[31;1m";
        public const string BrightGreen = "\x1b[32;1m";
        public const string BrightYellow = "\x1b[33;1m";
        public const string BrightBlue = "\x1b[34;1m";
        public const string BrightMagenta = "\x1b[35;1m";
        public const string BrightCyan = "\x1b[36;1m";
        public const string BrightWhite = "\x1b[37;1m";
    }
}