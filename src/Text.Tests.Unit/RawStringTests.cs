using Xunit;

namespace Orlum.Text.Tests.Unit
{
    public class RawStringTests
    {
        [Fact]
        public void CallingMethodWithStringArgumentInvokesMethodOverloadWithRawStringParameter()
        {
            var number = 1;
            var result = Format.NumericalPhrase("I have got {0} {0:NP;en;cat;cats}", number);
            Assert.Equal("I have got 1 cat", result);
        }

        [Fact]
        public void CallingMethodWithFormattableStringArgumentInvokesMethodOverloadWithFormattableStringParameter()
        {
            var number = 1;
            var result = Format.NumericalPhrase($"I have got {number} {number:NP;en;cat;cats}");
            Assert.Equal("I have got 1 cat", result);
        }
    }
}
