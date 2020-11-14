using System;
using System.Globalization;
using Xunit;


namespace TypographyHelper.Tests.Unit
{
    public class StringFormatTests
    {
        [Fact]
        public void StartWithLowerTest()
        {
            var value = 11;
            const int align = 10;
            const string format = "C";
            
            var result = $"{value:C}";
            Assert.Equal("11,00 ₽", result);

            result = $"|{value, 10:C}|";
            Assert.Equal("|   11,00 ₽|", result);

            result = $"|{value,align:C}|";
            Assert.Equal("|   11,00 ₽|", result);
        }
    }
}
