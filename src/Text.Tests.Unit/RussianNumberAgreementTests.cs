using Xunit;


namespace Orlum.Text.Tests.Unit
{
    public class RussianNumberAgreementTests
    {
        [Theory]
        [InlineData(GrammaticalNumber.Paucal, -3)]
        [InlineData(GrammaticalNumber.Singular, -1)]
        [InlineData(GrammaticalNumber.Paucal, -0.3)]
        [InlineData(GrammaticalNumber.Plural, 0)]
        [InlineData(GrammaticalNumber.Paucal, 0.123)]
        [InlineData(GrammaticalNumber.Singular, 1)]
        [InlineData(GrammaticalNumber.Paucal, 1.5)]
        [InlineData(GrammaticalNumber.Paucal, 2)]
        [InlineData(GrammaticalNumber.Paucal, 2.25)]
        [InlineData(GrammaticalNumber.Paucal, 3)]
        [InlineData(GrammaticalNumber.Paucal, 4)]
        [InlineData(GrammaticalNumber.Paucal, 4.1)]
        [InlineData(GrammaticalNumber.Plural, 5)]
        [InlineData(GrammaticalNumber.Paucal, 5.77)]
        [InlineData(GrammaticalNumber.Plural, 8)]
        [InlineData(GrammaticalNumber.Plural, 11)]
        [InlineData(GrammaticalNumber.Plural, 12)]
        [InlineData(GrammaticalNumber.Plural, 14)]
        [InlineData(GrammaticalNumber.Singular, 21)]
        [InlineData(GrammaticalNumber.Paucal, 22)]
        [InlineData(GrammaticalNumber.Singular, 91)]
        [InlineData(GrammaticalNumber.Plural, 111)]
        internal void FindsConcordForm(GrammaticalNumber expected, double number)
        {
            var result = new RussianNumberAgreement().MatchGrammaticalNumber(number);

            Assert.Equal(expected, result);
        }


        [Fact]
        internal void ThereAreThreeGramaticalNumbers()
        {
            Assert.Equal(3, new RussianNumberAgreement().GrammaticalNumbers.Count);
        }
    }
}
