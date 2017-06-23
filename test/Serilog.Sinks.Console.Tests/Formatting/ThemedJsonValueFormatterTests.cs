using System;
using System.Collections.Generic;
using System.IO;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Formatting;
using Serilog.Sinks.SystemConsole.Themes;
using Xunit;

namespace Serilog.Sinks.Console.Tests.Formatting
{
    public class ThemedJsonValueFormatterTests
    {
        class TestThemedJsonValueFormatter : ThemedJsonValueFormatter
        {
            public TestThemedJsonValueFormatter()
                : base(ConsoleTheme.None, null)
            {
            }

            public string Format(object literal)
            {
                var output = new StringWriter();
                Format(new SequenceValue(new [] {new ScalarValue(literal)}), output, null);
                var o = output.ToString();
                return o.Substring(1, o.Length - 2);
            }
        }

        [Theory]
        [InlineData(123, "123")]
        [InlineData('c', "\"c\"")]
        [InlineData("Hello, world!", "\"Hello, world!\"")]
        [InlineData(true, "true")]
        [InlineData("\\\"\t\r\n\f", "\"\\\\\\\"\\t\\r\\n\\f\"")]
        [InlineData("\u0001", "\"\\u0001\"")]
        [InlineData("a\nb", "\"a\\nb\"")]
        [InlineData(null, "null")]
        public void JsonLiteralTypesAreFormatted(object value, string expectedJson)
        {
            var formatter = new TestThemedJsonValueFormatter();
            Assert.Equal(expectedJson, formatter.Format(value));
        }

        [Fact]
        public void DateTimesFormatAsIso8601()
        {
            JsonLiteralTypesAreFormatted(new DateTime(2016, 01, 01, 13, 13, 13, DateTimeKind.Utc), "\"2016-01-01T13:13:13.0000000Z\"");
        }

        [Fact]
        public void DoubleFormatsAsNumber()
        {
            JsonLiteralTypesAreFormatted(123.45, "123.45");
        }

        [Fact]
        public void DoubleSpecialsFormatAsString()
        {
            JsonLiteralTypesAreFormatted(double.NaN, "\"NaN\"");
            JsonLiteralTypesAreFormatted(double.PositiveInfinity, "\"Infinity\"");
            JsonLiteralTypesAreFormatted(double.NegativeInfinity, "\"-Infinity\"");
        }

        [Fact]
        public void FloatFormatsAsNumber()
        {
            JsonLiteralTypesAreFormatted(123.45f, "123.45");
        }

        [Fact]
        public void FloatSpecialsFormatAsString()
        {
            JsonLiteralTypesAreFormatted(float.NaN, "\"NaN\"");
            JsonLiteralTypesAreFormatted(float.PositiveInfinity, "\"Infinity\"");
            JsonLiteralTypesAreFormatted(float.NegativeInfinity, "\"-Infinity\"");
        }

        [Fact]
        public void DecimalFormatsAsNumber()
        {
            JsonLiteralTypesAreFormatted(123.45m, "123.45");
        }

        static string Format(LogEventPropertyValue value)
        {
            var formatter = new TestThemedJsonValueFormatter();
            var output = new StringWriter();
            formatter.Format(value, output, null);
            return output.ToString();
        }

        [Fact]
        public void ScalarPropertiesFormatAsLiteralValues()
        {
            var f = Format(new ScalarValue(123));
            Assert.Equal("123", f);
        }

        [Fact]
        public void SequencePropertiesFormatAsArrayValue()
        {
            var f = Format(new SequenceValue(new[] { new ScalarValue(123), new ScalarValue(456) }));
            Assert.Equal("[123, 456]", f);
        }

        [Fact]
        public void StructuresFormatAsAnObject()
        {
            var structure = new StructureValue(new[] { new LogEventProperty("A", new ScalarValue(123)) }, "T");
            var f = Format(structure);
            Assert.Equal("{\"A\": 123, \"$type\": \"T\"}", f);
        }

        [Fact]
        public void DictionaryWithScalarKeyFormatsAsAnObject()
        {
            var dict = new DictionaryValue(new Dictionary<ScalarValue, LogEventPropertyValue> {
                { new ScalarValue(12), new ScalarValue(345) }
            });

            var f = Format(dict);
            Assert.Equal("{\"12\": 345}", f);
        }

        [Fact]
        public void SequencesOfSequencesAreFormatted()
        {
            var s = new SequenceValue(new[] { new SequenceValue(new[] { new ScalarValue("Hello") }) });

            var f = Format(s);
            Assert.Equal("[[\"Hello\"]]", f);
        }
    }
}
