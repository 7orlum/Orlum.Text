using Xunit;
using System.Globalization;

namespace Orlum.Text.Tests.Unit
{
    public class NumericalPhraseFormatProviderTests
    {
        [Theory]
        [InlineData("Запрошено -4 рубля", -4, "{0:NP;ru;Запрошен;Запрошено;Запрошено} {0} {0:NP;ru;рубль;рубля;рублей}")]
        [InlineData("Запрошено 0 рублей", 0, "{0:NP;ru;Запрошен;Запрошено;Запрошено} {0} {0:NP;ru;рубль;рубля;рублей}")]
        [InlineData("Запрошено 0.5 рубля", 0.5, "{0:NP;ru;Запрошен;Запрошено;Запрошено} {0} {0:NP;ru;рубль;рубля;рублей}")]
        [InlineData("Запрошен 1 рубль", 1, "{0:NP;ru;Запрошен;Запрошено;Запрошено} {0} {0:NP;ru;рубль;рубля;рублей}")]
        [InlineData("Запрошен 21 рубль", 21, "{0:NP;ru;Запрошен;Запрошено;Запрошено} {0} {0:NP;ru;рубль;рубля;рублей}")]
        [InlineData("Запрошено 1.33 рубля", 1.33d, "{0:NP;ru;Запрошен;Запрошено;Запрошено} {0} {0:NP;ru;рубль;рубля;рублей}")]
        [InlineData("Запрошено 6 рублей", 6, "{0:NP;ru;Запрошен;Запрошено;Запрошено} {0} {0:NP;ru;рубль;рубля;рублей}")]
        [InlineData("Запрошено 14 рублей", 14, "{0:NP;ru;Запрошен;Запрошено;Запрошено} {0} {0:NP;ru;рубль;рубля;рублей}")]
        [InlineData("Запрошен 131 рубль", 131, "{0:NP;ru;Запрошен;Запрошено;Запрошено} {0} {0:NP;ru;рубль;рубля;рублей}")]
        [InlineData("рублей", ulong.MaxValue, "{0:NP;ru;рубль;рубля;рублей}")]
        [InlineData("0023", 23, "{0:D4}")]
        [InlineData("рублей", double.MaxValue, "{0:NP;ru;рубль;рубля;рублей}")]
        [InlineData("рублей", double.MinValue, "{0:NP;ru;рубль;рубля;рублей}")]
        [InlineData("I have got -4 cats", -4, "I have got {0} {0:NP;en;cat;cats}")]
        [InlineData("I have got 0 cats", 0, "I have got {0} {0:NP;en;cat;cats}")]
        [InlineData("I have got 0.5 cat", 0.5, "I have got {0} {0:NP;en;cat;cats}")]
        [InlineData("I have got 1 cat", 1, "I have got {0} {0:NP;en;cat;cats}")]
        [InlineData("I have got 21 cats", 21, "I have got {0} {0:NP;en;cat;cats}")]
        [InlineData("I have got 1.33 cats", 1.33d, "I have got {0} {0:NP;en;cat;cats}")]
        [InlineData("cats", ulong.MaxValue, "{0:NP;en;cat;cats}")]
        [InlineData("cats", double.MaxValue, "{0:NP;en;cat;cats}")]
        [InlineData("cats", double.MinValue, "{0:NP;en;cat;cats}")]
        public void TakesDifferentTypesOfNumbers(string expected, object number, string format)
        {
            var result = string.Format(new NumericalPhraseFormatProvider(CultureInfo.InvariantCulture), format, number);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("23.0000", 23, "{0:N4}")]
        [InlineData("", null, "{0:N4}")]
        public void DoesNotHideStandardFormatSpecifiers(string expected, object number, string format)
        {
            var result = string.Format(new NumericalPhraseFormatProvider(CultureInfo.InvariantCulture), format, number);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("cats", null, "{0:NP;en;cat;cats}")]
        [InlineData("cats", 0, "{0:NP;en;cat;cats}")]
        public void FormatsNullValueTheSameWayAsZero(string expected, object number, string format)
        {
            var result = string.Format(new NumericalPhraseFormatProvider(CultureInfo.InvariantCulture), format, number);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("I have got 6 cats", 6, "I have got {0} {0:np;EN;cat;cats}")]
        [InlineData("I have got 7 cats", 7, "I have got {0} {0:np;en;cat;cats}")]
        [InlineData("I have got 8 cats", 8, "I have got {0} {0:NP;en;cat;cats}")]
        [InlineData("I have got 9 cats", 9, "I have got {0} {0:np;EN;cat;cats}")]
        public void IsInsensitiveToCaseOfFormatAndLanguageSpecifiers(string expected, object number, string format)
        {
            var result = string.Format(new NumericalPhraseFormatProvider(CultureInfo.InvariantCulture), format, number);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Argument must be convertable to double", "one", "{0:NP;ru;рубль;рубля;рублей}")]
        [InlineData("Argument must be convertable to double", new int[] { 1, 2 }, "{0:NP;ru;рубль;рубля;рублей}")]
        public void ThrowsExceptionOnBadNumberValue(string expected, object number, string format)
        {
            var e = Assert.Throws<ArgumentException>(() => string.Format(new NumericalPhraseFormatProvider(CultureInfo.InvariantCulture), format, number));
            Assert.Equal(expected, e.Message);
        }

        [Theory]
        [InlineData(
            "Expected 3 forms",
            UInt64.MaxValue, "Запрошена обработка {0:NP;ru;рубль;рубля:рублей}")]
        [InlineData("Expected singular and plural forms of the inflected phrase in that exact order, for example {0:NP;en;cow;cows}", 1, "{0:NP;en;cat}")]
        [InlineData("Expected singular and plural forms of the inflected phrase in that exact order, for example {0:NP;en;cow;cows}", 2, "{0:NP;en;cat;cats;dog}")]
        [InlineData("Expected singular and plural forms of the inflected phrase in that exact order, for example {0:NP;en;cow;cows}", 1, "{0:NP;en;}")]
        [InlineData("Expected singular and plural forms of the inflected phrase in that exact order, for example {0:NP;en;cow;cows}", 1, "{0:NP;en}")]
        [InlineData("Expected format string like {0:NP;EN;cow;cows}", 1, "{0:NP}")]
        [InlineData("Expected format string like {0:NP;EN;cow;cows}", 3, "{0:NP;;cat;cats}")]
        public void ThrowsExceptionOnBadFormatString(string expected, object number, string format)
        {
            var e = Assert.Throws<FormatException>(() => string.Format(new NumericalPhraseFormatProvider(CultureInfo.InvariantCulture), format, number));
            Assert.StartsWith(expected, e.Message, StringComparison.InvariantCultureIgnoreCase);
        }

        [Theory]
        [InlineData("The given key '-' was not present in the dictionary.", 5, "{0:NP;-;cat;cats;}")]
        public void ThrowsExceptionOnUnknownLanguageSpecifier(string expected, object number, string format)
        {
            var e = Assert.Throws<KeyNotFoundException>(() => string.Format(new NumericalPhraseFormatProvider(CultureInfo.InvariantCulture), format, number));
            Assert.Equal(expected, e.Message);
        }

        [Fact]
        public void AllowsCustomSetOfNumberAgreements()
        {
            var formatter = new NumericalPhraseFormatProvider(CultureInfo.InvariantCulture);
            formatter.NumberAgreements.Remove("pl");
            formatter.NumberAgreements.Add("ru-ru", new RussianNumberAgreement());

            Assert.Equal("I have got 1 cat", string.Format(formatter, "I have got {0} {0:NP;en;cat;cats}", 1));
            Assert.Equal("Запрошено 6 рублей", string.Format(formatter, "{0:NP;RU;Запрошен;Запрошено;Запрошено} {0} {0:NP;ru;рубль;рубля;рублей}", 6));
            Assert.Throws<KeyNotFoundException>(() => string.Format(formatter, "Zażądano {0} {0:NP;PL;rubel;ruble;rubli;rubla}", 1));
            Assert.Equal("Запрошено 6 рублей", string.Format(formatter, "{0:NP;ru-ru;Запрошен;Запрошено;Запрошено} {0} {0:NP;ru;рубль;рубля;рублей}", 6));
        }
    }
}
