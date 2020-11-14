using System;
using System.Linq;
using System.Globalization;
using Xunit;
using Orlum.TypographyHelper.DecimalExtention;


namespace TypographyHelper.Tests.Unit
{
    public class DecimalExtensionTests
    {
        [Theory]
        [InlineData("2,34 ₽", 2.34, "RUB")]
        //[InlineData("$109 928 830,86", 109928830.8634, "USD")]
        //[InlineData("-44,54 €", -44.535, "EUR")]
        //[InlineData("¤2,34", 2.34, "---")]
        public void FormatCurrencyTest(string expected, decimal amount, string currency)
        {
            var result = DecimalExtensionMethods.FormatCurrency(amount, currency, CultureInfo.GetCultureInfo("ru-RU"));

            Assert.Equal(expected, result);
        }


        [Fact]
        public void FormatCurrencyNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => DecimalExtensionMethods.FormatCurrency(5, null, CultureInfo.GetCultureInfo("ru-RU")));
        }
    }
}
