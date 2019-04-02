using System;

using Serilog.Formatting;
using Serilog.Sinks.SystemConsole.Output;
using Serilog.Sinks.SystemConsole.Themes;

namespace Serilog
{
    /// <summary>
    /// Factory utility to construct OutputTemplateRenderer ITextFormatter
    /// </summary>
    public class ConsoleSinkTextFormatFactory
    {
        private const string defaultConsoleOutputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";
        private IFormatProvider _formatProvider;
        private string _outputTemplate;
        private ConsoleTheme _theme;

        private ConsoleSinkTextFormatFactory()
        {
        }

        /// <summary>
        /// Creates new instance of ConsoleSinkTextFormatFactory
        /// </summary>
        /// <returns></returns>
        public static ConsoleSinkTextFormatFactory Create()
            => new ConsoleSinkTextFormatFactory();

        /// <summary>
        /// Sets format provider
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public ConsoleSinkTextFormatFactory With(IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
            return this;
        }

        /// <summary>
        /// Sets the output template format
        /// </summary>
        /// <param name="outputTemplate"></param>
        /// <returns></returns>
        public ConsoleSinkTextFormatFactory WithOutPutTemplate(string outputTemplate)
        {
            _outputTemplate = outputTemplate;
            return this;
        }

        /// <summary>
        /// Sets console theme
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        public ConsoleSinkTextFormatFactory With(ConsoleTheme theme)
        {
            _theme = theme;
            return this;
        }

        /// <summary>
        /// Builds an instance of ITextFormatter from OutputTemplateRenderer
        /// </summary>
        /// <returns>OutputTemplateRenderer</returns>
        public ITextFormatter Build()
        {
            var outputTemplate = string.IsNullOrWhiteSpace(_outputTemplate) ? defaultConsoleOutputTemplate : _outputTemplate;
            var appliedTheme = ConsoleThemeSelector.AppliedTheme(_theme);

            return new OutputTemplateRenderer(appliedTheme, outputTemplate, _formatProvider);
        }
    }
}