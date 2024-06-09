﻿using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Threading;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

try
{
    Log.Debug("Getting started");

    Log.Information("Hello {Name} from thread {ThreadId}", Environment.GetEnvironmentVariable("USERNAME"), Thread.CurrentThread.ManagedThreadId);

    Log.Warning("No coins remain at position {@Position}", new { Lat = 25, Long = 134 });

    Fail();
}
catch (Exception e)
{
    Log.Error(e, "Something went wrong");
}

await Log.CloseAndFlushAsync();

static void Fail()
{
    throw new DivideByZeroException();
}
