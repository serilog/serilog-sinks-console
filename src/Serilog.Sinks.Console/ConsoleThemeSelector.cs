using System;

using Serilog.Sinks.SystemConsole.Themes;

namespace Serilog
{
    internal static class ConsoleThemeSelector
    {
        public static ConsoleTheme AppliedTheme(ConsoleTheme theme)
            => Console.IsOutputRedirected || Console.IsErrorRedirected
                ? ConsoleTheme.None
                : theme ?? SystemConsoleThemes.Literate;
    }
}