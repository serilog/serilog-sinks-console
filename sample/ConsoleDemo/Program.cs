using Serilog;
using Serilog.Formatting.Display;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Threading;

namespace ConsoleDemo
{
    public class Program
    {
        public static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
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

            Log.CloseAndFlush();

            Console.ReadKey(true);
        }

        static void Fail()
        {
            throw new DivideByZeroException();
        }
    }
}
