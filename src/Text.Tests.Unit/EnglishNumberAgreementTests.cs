using Xunit;


namespace Orlum.Text.Tests.Unit
{
    public class EnglishNumberAgreementTests
    {
        [Theory]
        [InlineData(GrammaticalNumber.Plural, 0)]
        [InlineData(GrammaticalNumber.Singular, 1)]
        [InlineData(GrammaticalNumber.Plural, 1.5)]
        [InlineData(GrammaticalNumber.Plural, 2)]
        [InlineData(GrammaticalNumber.Plural, 91)]
        internal void FindsConcordForm(GrammaticalNumber expected, double number)
        {
            var result = new EnglishNumberAgreement().MatchGrammaticalNumber(number);

            Assert.Equal(expected, result);
        }


        [Fact]
        internal void ThereAreTwoGramaticalNumbers()
        {
            Assert.Equal(2, new EnglishNumberAgreement().GrammaticalNumbers.Count);
        }
    }
}
