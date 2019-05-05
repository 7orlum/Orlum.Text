using Xunit;
using orlum.TypographyHelper;


namespace TypographyHelper.Tests.Unit
{
    public class RussianNumberAgreementTests
    {
        [Theory]
        [InlineData(GramaticalNumber.Paucal, -3)]
        [InlineData(GramaticalNumber.Singular, -1)]
        [InlineData(GramaticalNumber.Paucal, -0.3)]
        [InlineData(GramaticalNumber.Plural, 0)]
        [InlineData(GramaticalNumber.Paucal, 0.123)]
        [InlineData(GramaticalNumber.Singular, 1)]
        [InlineData(GramaticalNumber.Paucal, 1.5)]
        [InlineData(GramaticalNumber.Paucal, 2)]
        [InlineData(GramaticalNumber.Paucal, 3)]
        [InlineData(GramaticalNumber.Paucal, 4)]
        [InlineData(GramaticalNumber.Plural, 5)]
        [InlineData(GramaticalNumber.Plural, 8)]
        [InlineData(GramaticalNumber.Plural, 11)]
        [InlineData(GramaticalNumber.Plural, 12)]
        [InlineData(GramaticalNumber.Plural, 14)]
        [InlineData(GramaticalNumber.Singular, 21)]
        [InlineData(GramaticalNumber.Paucal, 22)]
        [InlineData(GramaticalNumber.Singular, 91)]
        [InlineData(GramaticalNumber.Plural, 111)]
        internal void FindsConcordForm(GramaticalNumber expected, double number)
        {
            var result = new RussianNumberAgreement().ConcordForm(number);

            Assert.Equal(expected, result);
        }


        [Fact]
        internal void ThereAreThreeGramaticalNumbers()
        {
            Assert.Equal(3, new RussianNumberAgreement().AvailableForms.Length);
        }
    }
}
