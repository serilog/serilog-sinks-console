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
            var stdout = GetStdHandle(StandardOutputHandleId);
            if (stdout != (IntPtr)InvalidHandleValue && GetConsoleMode(stdout, out var mode))
            {
                SetConsoleMode(stdout, mode | EnableVirtualTerminalProcessingMode);
            }
        }

        const int StandardOutputHandleId = -11;
        const uint EnableVirtualTerminalProcessingMode = 4;
        const long InvalidHandleValue = -1;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int handleId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetConsoleMode(IntPtr handle, out uint mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleMode(IntPtr handle, uint mode);
    }
}
