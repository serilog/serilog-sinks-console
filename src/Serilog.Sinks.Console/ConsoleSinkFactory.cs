using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.SystemConsole;
using Serilog.Sinks.SystemConsole.Themes;

namespace Serilog
{
    /// <summary>
    /// Factory class to help construct ILogEventSink
    /// </summary>
    public class ConsoleSinkFactory
    {
        private LogEventLevel? _standardErrorFromLevel;
        private ITextFormatter _textFormatter;
        private ConsoleTheme _theme;

        private ConsoleSinkFactory()
        {
        }

        /// <summary>
        /// Creates a new instance of the ConsoleSinkFactory
        /// </summary>
        /// <returns></returns>
        public static ConsoleSinkFactory Create() => new ConsoleSinkFactory();

        /// <summary>
        /// Sets console theme
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        public ConsoleSinkFactory With(ConsoleTheme theme)
        {
            _theme = theme;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textFormatter"></param>
        /// <returns></returns>
        public ConsoleSinkFactory With(ITextFormatter textFormatter)
        {
            _textFormatter = textFormatter;
            return this;
        }

        /// <summary>
        /// Sets the standard error from level
        /// </summary>
        /// <param name="standardErrorFromLevel"></param>
        /// <returns></returns>
        public ConsoleSinkFactory With(LogEventLevel? standardErrorFromLevel)
        {
            _standardErrorFromLevel = standardErrorFromLevel;
            return this;
        }

        /// <summary>
        /// Uses given set parameters to construct an instance of ILogEventSink
        /// </summary>
        /// <returns>Implementation of Serilog.ILogEventSink</returns>
        public ILogEventSink Build()
        {
            var appliedTheme = ConsoleThemeSelector.AppliedTheme(_theme);
            return new ConsoleSink(appliedTheme, _textFormatter, _standardErrorFromLevel);
        }
    }
}