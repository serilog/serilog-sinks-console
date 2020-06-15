// Copyright 2017-2020 Serilog Contributors
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
using System.Runtime.InteropServices;

namespace Serilog.Sinks.SystemConsole.Platform
{
    static class WindowsConsole
    {
        public static void EnableVirtualTerminalProcessing()
        {
#if RUNTIME_INFORMATION
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return;
#else
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                return;
#endif
            var stdout = GetStdHandle(STD_OUTPUT_HANDLE);
            if (stdout != (IntPtr)INVALID_HANDLE && GetConsoleMode(stdout, out var mode))
            {
                SetConsoleMode(stdout, mode | ENABLE_VIRTUAL_TERMINAL_PROCESSING);
            }

            var stdin = GetStdHandle(STD_INPUT_HANDLE);
            if (stdin != (IntPtr)INVALID_HANDLE && GetConsoleMode(stdin, out mode))
            {
                SetConsoleMode(stdin, mode & ~ENABLE_QUICK_EDIT_MODE);
            }
        }

        // https://docs.microsoft.com/en-us/windows/console/setconsolemode
        const int STD_OUTPUT_HANDLE = -11;
        const int STD_INPUT_HANDLE = -10;
        const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;
        const uint ENABLE_QUICK_EDIT_MODE = 0x0040;
        const long INVALID_HANDLE = -1;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int handleId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetConsoleMode(IntPtr handle, out uint mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleMode(IntPtr handle, uint mode);
    }
}
