using Xunit;
using System;
using System.Globalization;
using Orlum.Text.DecimalExtention;


namespace Orlum.Text.Tests.Unit
{
    public class DecimalExtensionTests
    {
        [Theory]
        [InlineData("2,34 ₽", 2.34, "C", "RUB")]
        [InlineData("109 928 830,86 $", 109928830.8634, "C", "USD")]
        [InlineData("-44,54 €", -44.535, "C", "EUR")]
        [InlineData("2,34 ¤", 2.34, "C", "---")]
        [InlineData("2,340 ₽", 2.34, "C3", "RUB")]
        [InlineData("2,34 ₽", 2.34, "", "RUB")]
        [InlineData("2,34 ₽", 2.34, null, "RUB")]
        public void ToStringFormatValues(string expected, decimal amount, string format, string currency)
        {
            var result = amount.ToString(format, currency, CultureInfo.GetCultureInfo("ru-RU"));
            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData(2.34, "C", null)]
        public void ToStringTrowExceptionIfCurrencyCodeIsNull(decimal amount, string format, string currency)
        {
            Assert.Throws<ArgumentNullException>(() => amount.ToString(format, currency));
        }


        [Theory]
        [InlineData("Value must be three-character ISO 4217 currency symbol", 2.34, null, "RU")]
        [InlineData("Value must be three-character ISO 4217 currency symbol", 2.34, "C", "")]
        [InlineData("Value must be three-character ISO 4217 currency symbol", 2.34, "C", "рубл")]
        public void ToStringTrowExceptionIfCurrencyCodeNotContains3Сharacters(string expected, decimal amount, string format, string currency)
        {
            var e = Assert.Throws<ArgumentException>(() => amount.ToString(format, currency));
            Assert.StartsWith(expected, e.Message, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
