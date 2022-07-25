using Xunit;
using System.Globalization;
using Orlum.Text;


namespace Orlum.Text.Tests.Unit
{
    public class FormatTests
    {
        [Fact]
        public void AcronimsWork()
        {
            var number = 1;
            var result = Format.NP($"I have got {number} {number:NP;en;cat;cats}");
            Assert.Equal("I have got 1 cat", result);

            result = Format.NP($"I have got {number} {number:NP;en;cat;cats}");
            Assert.Equal("I have got 1 cat", result);

            result = Format.CV(CultureInfo.GetCultureInfo("ru-ru"), "USD", $"{number:C}");
            Assert.Equal("1,00 $", result);

            result = Format.NumericalPhrase("I have got {0} {0:NP;en;cat;cats}", number);
            Assert.Equal("I have got 1 cat", result);

            result = Format.NumericalPhrase("I have got {0} {0:NP;en;cat;cats}", number);
            Assert.Equal("I have got 1 cat", result);

            result = Format.CurrencyValue(CultureInfo.GetCultureInfo("ru-ru"), "USD", "{0:C}", number);
            Assert.Equal("1,00 $", result);
        }
    }
}
