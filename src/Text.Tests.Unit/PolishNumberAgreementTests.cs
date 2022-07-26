using Xunit;

namespace Orlum.Text.Tests.Unit
{
    public class PolishNumberAgreementTests
    {
        [Theory]
        [InlineData(GrammaticalNumber.Paucal, -3)]
        [InlineData(GrammaticalNumber.Singular, -1)]
        [InlineData(GrammaticalNumber.Fractional, -0.3)]
        [InlineData(GrammaticalNumber.Plural, 0)]
        [InlineData(GrammaticalNumber.Fractional, 0.123)]
        [InlineData(GrammaticalNumber.Singular, 1)]
        [InlineData(GrammaticalNumber.Fractional, 1.5)]
        [InlineData(GrammaticalNumber.Paucal, 2)]
        [InlineData(GrammaticalNumber.Paucal, 3)]
        [InlineData(GrammaticalNumber.Paucal, 4)]
        [InlineData(GrammaticalNumber.Plural, 5)]
        [InlineData(GrammaticalNumber.Plural, 8)]
        [InlineData(GrammaticalNumber.Plural, 11)]
        [InlineData(GrammaticalNumber.Plural, 12)]
        [InlineData(GrammaticalNumber.Plural, 14)]
        [InlineData(GrammaticalNumber.Plural, 21)]
        [InlineData(GrammaticalNumber.Plural, 22)]
        [InlineData(GrammaticalNumber.Plural, 91)]
        [InlineData(GrammaticalNumber.Plural, 111)]
        internal void FindsConcordForm(GrammaticalNumber expected, double number)
        {
            var result = new PolishNumberAgreement().MatchGrammaticalNumber(number);

            Assert.Equal(expected, result);
        }

        [Fact]
        internal void ThereAreFourGramaticalNumbers()
        {
            Assert.Equal(4, new PolishNumberAgreement().GrammaticalNumbers.Count);
        }
    }
}
