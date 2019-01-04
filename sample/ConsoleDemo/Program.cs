using Serilog;
using System;
using System.Globalization;
using System.Threading;
using Serilog.Sinks.SystemConsole.Themes;

namespace ConsoleDemo
{
    public class Program
    {
        public static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(
                    theme: AnsiConsoleTheme.Code,
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message}{NewLine}{Properties:lj}{NewLine}{Exception}",
                    formatProvider: new CultureInfo("fr-FR"))
                .CreateLogger();

            try
            {
                Log.Debug("Getting started");

                Log.Information("Hello {Name} from thread {ThreadId}", Environment.GetEnvironmentVariable("USERNAME"), Thread.CurrentThread.ManagedThreadId);

                Log.Warning("No coins remain at position {@Position}", new { Lat = 25, Long = 134 });

                // Tests around formatting with FormatProviders and JSON formatting
                Log.Debug("In OutputTemplate : Message has no :lj specifier but Properties does ...");
                var theDate = DateTime.Now;
                var theNumber = 23.976;
                Log.ForContext("ContextNumber", 23.976)
                    .ForContext("ContextDate", theDate)
                    .Information("This is the message with a number {Number} and a date {Date} and a nested object {@Object} with a French FormatProvider. Message has no :lj specifier but Properties does ...",
                    theNumber,
                    theDate,
                    new { NestedDate = theDate, NestedNumber = theNumber });

                Fail();
            }
            catch (Exception e)
            {
                Log.Error(e, "Something went wrong");
            }

            Log.CloseAndFlush();
        }

        static void Fail()
        {
            throw new DivideByZeroException();
        }
    }
}
