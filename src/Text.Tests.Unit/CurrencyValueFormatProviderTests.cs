using System.Globalization;
using Xunit;

namespace Orlum.Text.Tests.Unit
{
    public class CurrencyValueFormatProviderTests
    {
        [Theory]
        [InlineData("2,34 ₽", 2.34, "C", "RUB")]
        [InlineData("109 928 830,86 $", 109928830.8634, "C", "USD")]
        [InlineData("-44,54 €", -44.535, "C", "EUR")]
        [InlineData("2,34 ¤", 2.34, "C", "---")]
        [InlineData("2,340 ₽", 2.34, "C3", "RUB")]
        [InlineData("2,34", 2.34, "", "RUB")]
        [InlineData("2,34", 2.34, null, "RUB")]
        [InlineData("2,34", 2.34, "G", "RUB")]
        public void FormatsDecimalValues(string expected, decimal amount, string format, string currency)
        {
            var result = amount.ToString(format, new CurrencyValueFormatProvider(currency, CultureInfo.GetCultureInfo("ru-RU")));
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("2,00 ₽", 2, "C", "RUB")]
        [InlineData("109 928 830,00 $", 109928830, "C", "USD")]
        [InlineData("-44,00 €", -44, "C", "EUR")]
        public void FormatsIntValues(string expected, int amount, string format, string currency)
        {
            var result = amount.ToString(format, new CurrencyValueFormatProvider(currency, CultureInfo.GetCultureInfo("ru-RU")));
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("2,34 ₽", 2.34, "C", "RUB")]
        [InlineData("109 928 830,86 $", 109928830.8634, "C", "USD")]
        [InlineData("-44,53 €", -44.535, "C", "EUR")]
        [InlineData("-44,54 €", -44.5351, "C", "EUR")]
        [InlineData("2,34 ¤", 2.34, "C", "---")]
        [InlineData("2,340 ₽", 2.34, "C3", "RUB")]
        [InlineData("2,34", 2.34, "", "RUB")]
        [InlineData("2,34", 2.34, null, "RUB")]
        [InlineData("2,34", 2.34, "G", "RUB")]
        public void FormatsDoubleValues(string expected, double amount, string format, string currency)
        {
            var result = amount.ToString(format, new CurrencyValueFormatProvider(currency, CultureInfo.GetCultureInfo("ru-RU")));
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(2.34, "C", null)]
        public void TrowsExceptionIfCurrencyCodeIsNull(decimal amount, string format, string currency)
        {
            Assert.Throws<ArgumentNullException>(() => amount.ToString(format, new CurrencyValueFormatProvider(currency)));
        }

        [Theory]
        [InlineData("Value must be three-character ISO 4217 currency symbol", 2.34, null, "RU")]
        [InlineData("Value must be three-character ISO 4217 currency symbol", 2.34, "C", "")]
        [InlineData("Value must be three-character ISO 4217 currency symbol", 2.34, "C", "рубл")]
        public void TrowsExceptionIfCurrencyCodeNotContains3Сharacters(string expected, decimal amount, string format, string currency)
        {
            var e = Assert.Throws<ArgumentException>(() => amount.ToString(format, new CurrencyValueFormatProvider(currency)));
            Assert.StartsWith(expected, e.Message, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
