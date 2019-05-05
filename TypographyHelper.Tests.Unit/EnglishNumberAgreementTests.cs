using Xunit;
using orlum.TypographyHelper;


namespace TypographyHelper.Tests.Unit
{
    public class EnglishNumberAgreementTests
    {
        [Theory]
        [InlineData(GramaticalNumber.Plural, 0)]
        [InlineData(GramaticalNumber.Singular, 1)]
        [InlineData(GramaticalNumber.Plural, 1.5)]
        [InlineData(GramaticalNumber.Plural, 2)]
        [InlineData(GramaticalNumber.Plural, 91)]
        internal void FindsConcordForm(GramaticalNumber expected, double number)
        {
            var result = new EnglishNumberAgreement().ConcordForm(number);

            Assert.Equal(expected, result);
        }


        [Fact]
        internal void ThereAreTwoGramaticalNumbers()
        {
            Assert.Equal(2, new EnglishNumberAgreement().AvailableForms.Length);
        }
    }
}
