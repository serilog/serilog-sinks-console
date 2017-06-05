using Serilog;
using System;
using System.Threading;

namespace ConsoleDemo
{
    public class Program
    {
        public static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Hello {Name} from thread {ThreadId}", Environment.GetEnvironmentVariable("USERNAME"), Thread.CurrentThread.ManagedThreadId);

            Log.CloseAndFlush();

            Console.ReadKey(true);
        }
    }
}
