using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using orlum.TypographyHelper;


namespace TypographyHelper.Tests.Unit
{
    public class StringHelperTests
    {
        [Theory]
        [InlineData("grasshopper", "Grasshopper")]
        [InlineData("hELP", "HELP")]
        [InlineData(" Brown ", " Brown ")]
        [InlineData("l", "L")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void StartWithLowerTest(string expected, string value)
        {
            var result = StringHelper.UncapitalizeFirstLetter(value, CultureInfo.InvariantCulture);

            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData("Grasshopper", "grasshopper")]
        [InlineData("HELP", "hELP")]
        [InlineData(" Brown ", " Brown ")]
        [InlineData("L", "l")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void StartWithUpperTest(string expected, string value)
        {
            var result = StringHelper.CapitalizeFirstLetter(value, CultureInfo.InvariantCulture);

            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData("1, 2, , 3, , 4, 5", new object[] { 1, 2, null, 3, null, 4, 5 }, ", ", null, null, false, null)]
        [InlineData("1, 2, , 3, , 4 ��� 5", new object[] { 1, 2, null, 3, null, 4, 5 }, ", ", " ��� ", null, false, null)]
        [InlineData("1, 2, 3, 4, 5", new object[] { 1, 2, null, 3, null, 4, 5 }, ", ", null, null, true, null)]
        [InlineData("1, 2, 3, 4 ��� 5", new object[] { 1, 2, null, 3, null, 4, 5 }, ", ", " ��� ", null, true, null)]

        [InlineData("1, 2, �, 3, �, 4, 5", new object[] { 1, 2, null, 3, null, 4, 5 }, ", ", null, null, false, "�")]
        [InlineData("1, 2, �, 3, �, 4 ��� 5", new object[] { 1, 2, null, 3, null, 4, 5 }, ", ", " ��� ", null, false, "�")]
        [InlineData("1, 2, 3, 4, 5", new object[] { 1, 2, null, 3, null, 4, 5 }, ", ", null, null, true, "�")]
        [InlineData("1, 2, 3, 4 ��� 5", new object[] { 1, 2, null, 3, null, 4, 5 }, ", ", " ��� ", null, true, "�")]

        [InlineData("1, 2,, 3,, 4, 5", new object[] { 1, 2, null, 3, null, 4, 5 }, ", ", null, ",", false, null)]
        [InlineData("1, 2,, 3,, 4 ��� 5", new object[] { 1, 2, null, 3, null, 4, 5 }, ", ", " ��� ", ",", false, null)]
        [InlineData("1, 2, 3, 4, 5", new object[] { 1, 2, null, 3, null, 4, 5 }, ", ", null, ",", true, null)]
        [InlineData("1, 2, 3, 4 ��� 5", new object[] { 1, 2, null, 3, null, 4, 5 }, ", ", " ��� ", ",", true, null)]

        [InlineData("1, 2,�, 3,�, 4, 5", new object[] { 1, 2, null, 3, null, 4, 5 }, ", ", null, ",", false, "�")]
        [InlineData("1, 2,�, 3,�, 4 ��� 5", new object[] { 1, 2, null, 3, null, 4, 5 }, ", ", " ��� ", ",", false, "�")]
        [InlineData("1, 2, 3, 4, 5", new object[] { 1, 2, null, 3, null, 4, 5 }, ", ", null, ",", true, "�")]
        [InlineData("1, 2, 3, 4 ��� 5", new object[] { 1, 2, null, 3, null, 4, 5 }, ", ", " ��� ", ",", true, "�")]

        [InlineData("1, 2, , 3, , 4, 5, ", new object[] { 1, 2, null, 3, null, 4, 5, null }, ", ", null, null, false, null)]
        [InlineData("1, 2, , 3, , 4, 5 ��� ", new object[] { 1, 2, null, 3, null, 4, 5, null }, ", ", " ��� ", null, false, null)]
        [InlineData("1, 2, 3, 4, 5", new object[] { 1, 2, null, 3, null, 4, 5, null }, ", ", null, null, true, null)]
        [InlineData("1, 2, 3, 4 ��� 5", new object[] { 1, 2, null, 3, null, 4, 5, null }, ", ", " ��� ", null, true, null)]

        [InlineData("1, 2, �, 3, �, 4, 5, �", new object[] { 1, 2, null, 3, null, 4, 5, null }, ", ", null, null, false, "�")]
        [InlineData("1, 2, �, 3, �, 4, 5 ��� �", new object[] { 1, 2, null, 3, null, 4, 5, null }, ", ", " ��� ", null, false, "�")]
        [InlineData("1, 2, 3, 4, 5", new object[] { 1, 2, null, 3, null, 4, 5, null }, ", ", null, null, true, "�")]
        [InlineData("1, 2, 3, 4 ��� 5", new object[] { 1, 2, null, 3, null, 4, 5, null }, ", ", " ��� ", null, true, "�")]

        [InlineData("1, 2,, 3,, 4, 5,", new object[] { 1, 2, null, 3, null, 4, 5, null }, ", ", null, ",", false, null)]
        [InlineData("1, 2,, 3,, 4, 5 ��� ", new object[] { 1, 2, null, 3, null, 4, 5, null }, ", ", " ��� ", ",", false, null)]
        [InlineData("1, 2, 3, 4, 5", new object[] { 1, 2, null, 3, null, 4, 5, null }, ", ", null, ",", true, null)]
        [InlineData("1, 2, 3, 4 ��� 5", new object[] { 1, 2, null, 3, null, 4, 5, null }, ", ", " ��� ", ",", true, null)]

        [InlineData("1, 2,�, 3,�, 4, 5,�", new object[] { 1, 2, null, 3, null, 4, 5, null }, ", ", null, ",", false, "�")]
        [InlineData("1, 2,�, 3,�, 4, 5 ��� �", new object[] { 1, 2, null, 3, null, 4, 5, null }, ", ", " ��� ", ",", false, "�")]
        [InlineData("1, 2, 3, 4, 5", new object[] { 1, 2, null, 3, null, 4, 5, null }, ", ", null, ",", true, "�")]
        [InlineData("1, 2, 3, 4 ��� 5", new object[] { 1, 2, null, 3, null, 4, 5, null }, ", ", " ��� ", ",", true, "�")]
        public void JoinTest(string expected, object[] value, string separator, string lastSeparator, string separatorBeforeNullAndEmptyValue,
            bool skipNullAndEmptyValues, string replaceNullAndEmptyValuesWith)
        {
            var result = StringHelper.Join(value, separator, lastSeparator, separatorBeforeNullAndEmptyValue, skipNullAndEmptyValues, replaceNullAndEmptyValuesWith);

            Assert.Equal(expected, result);
        }


        [Theory]
        [MemberData(nameof(ParametersForJoinOptionalParametersTest))]
        public void JoinOptionalParametersTest(string expected, JoinParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var methodJoin = typeof(StringHelper).GetMethods().Single(m => m.Name == "Join" && m.GetParameters()[0].ParameterType == typeof(object[]));
            var result = methodJoin.Invoke(null, parameters.ToArray());

            Assert.Equal(expected, result);
        }


        public static IEnumerable<object[]> ParametersForJoinOptionalParametersTest()
        {
            var result = new List<object[]>();

            var value1 = new object[] { 1, 2, null, 3, null, 4, 5 };

            result.Add(new object[] { "1, 2, , 3, , 4, 5", new JoinParameters(value1, ", ") });
            result.Add(new object[] { "1, 2, , 3, , 4 ��� 5", new JoinParameters(value1, ", ") { lastSeparator = " ��� " } });
            result.Add(new object[] { "1, 2, 3, 4, 5", new JoinParameters(value1, ", ") { skipNullAndEmptyValues = true } });
            result.Add(new object[] { "1, 2, 3, 4 ��� 5", new JoinParameters(value1, ", ") { lastSeparator = " ��� ", skipNullAndEmptyValues = true } });

            result.Add(new object[] { "1, 2, �, 3, �, 4, 5", new JoinParameters(value1, ", ") { replaceNullAndEmptyValuesWith = "�" } });
            result.Add(new object[] { "1, 2, �, 3, �, 4 ��� 5", new JoinParameters(value1, ", ") { lastSeparator = " ��� ", replaceNullAndEmptyValuesWith = "�" } });
            result.Add(new object[] { "1, 2, 3, 4, 5", new JoinParameters(value1, ", ") { skipNullAndEmptyValues = true, replaceNullAndEmptyValuesWith = "�" } });
            result.Add(new object[] { "1, 2, 3, 4 ��� 5", new JoinParameters(value1, ", ") { lastSeparator = " ��� ", skipNullAndEmptyValues = true, replaceNullAndEmptyValuesWith = "�" } });

            result.Add(new object[] { "1, 2,, 3,, 4, 5", new JoinParameters(value1, ", ") { separatorBeforeNullAndEmptyValue = "," } });
            result.Add(new object[] { "1, 2,, 3,, 4 ��� 5", new JoinParameters(value1, ", ") { lastSeparator = " ��� ", separatorBeforeNullAndEmptyValue = "," } });
            result.Add(new object[] { "1, 2, 3, 4, 5", new JoinParameters(value1, ", ") { separatorBeforeNullAndEmptyValue = ",", skipNullAndEmptyValues = true } });
            result.Add(new object[] { "1, 2, 3, 4 ��� 5", new JoinParameters(value1, ", ") { lastSeparator = " ��� ", separatorBeforeNullAndEmptyValue = ",", skipNullAndEmptyValues = true } });

            result.Add(new object[] { "1, 2,�, 3,�, 4, 5", new JoinParameters(value1, ", ") { separatorBeforeNullAndEmptyValue = ",", replaceNullAndEmptyValuesWith = "�" } });
            result.Add(new object[] { "1, 2,�, 3,�, 4 ��� 5", new JoinParameters(value1, ", ") { lastSeparator = " ��� ", separatorBeforeNullAndEmptyValue = ",", replaceNullAndEmptyValuesWith = "�" } });
            result.Add(new object[] { "1, 2, 3, 4, 5", new JoinParameters(value1, ", ") { separatorBeforeNullAndEmptyValue = ",", skipNullAndEmptyValues = true, replaceNullAndEmptyValuesWith = "�" } });
            result.Add(new object[] { "1, 2, 3, 4 ��� 5", new JoinParameters(value1, ", ") { lastSeparator = " ��� ", separatorBeforeNullAndEmptyValue = ",", skipNullAndEmptyValues = true, replaceNullAndEmptyValuesWith = "�" } });

            var value2 = new object[] { 1, 2, null, 3, null, 4, 5, null };

            result.Add(new object[] { "1, 2, , 3, , 4, 5, ", new JoinParameters(value2, ", ") });
            result.Add(new object[] { "1, 2, , 3, , 4, 5 ��� ", new JoinParameters(value2, ", ") { lastSeparator = " ��� " } });
            result.Add(new object[] { "1, 2, 3, 4, 5", new JoinParameters(value2, ", ") { skipNullAndEmptyValues = true } });
            result.Add(new object[] { "1, 2, 3, 4 ��� 5", new JoinParameters(value2, ", ") { lastSeparator = " ��� ", skipNullAndEmptyValues = true } });

            result.Add(new object[] { "1, 2, �, 3, �, 4, 5, �", new JoinParameters(value2, ", ") { replaceNullAndEmptyValuesWith = "�" } });
            result.Add(new object[] { "1, 2, �, 3, �, 4, 5 ��� �", new JoinParameters(value2, ", ") { lastSeparator = " ��� ", replaceNullAndEmptyValuesWith = "�" } });
            result.Add(new object[] { "1, 2, 3, 4, 5", new JoinParameters(value2, ", ") { skipNullAndEmptyValues = true, replaceNullAndEmptyValuesWith = "�" } });
            result.Add(new object[] { "1, 2, 3, 4 ��� 5", new JoinParameters(value2, ", ") { lastSeparator = " ��� ", skipNullAndEmptyValues = true, replaceNullAndEmptyValuesWith = "�" } });

            result.Add(new object[] { "1, 2,, 3,, 4, 5,", new JoinParameters(value2, ", ") { separatorBeforeNullAndEmptyValue = "," } });
            result.Add(new object[] { "1, 2,, 3,, 4, 5 ��� ", new JoinParameters(value2, ", ") { lastSeparator = " ��� ", separatorBeforeNullAndEmptyValue = "," } });
            result.Add(new object[] { "1, 2, 3, 4, 5", new JoinParameters(value2, ", ") { separatorBeforeNullAndEmptyValue = ",", skipNullAndEmptyValues = true } });
            result.Add(new object[] { "1, 2, 3, 4 ��� 5", new JoinParameters(value2, ", ") { lastSeparator = " ��� ", separatorBeforeNullAndEmptyValue = ",", skipNullAndEmptyValues = true } });

            result.Add(new object[] { "1, 2,�, 3,�, 4, 5,�", new JoinParameters(value2, ", ") { separatorBeforeNullAndEmptyValue = ",", replaceNullAndEmptyValuesWith = "�" } });
            result.Add(new object[] { "1, 2,�, 3,�, 4, 5 ��� �", new JoinParameters(value2, ", ") { lastSeparator = " ��� ", separatorBeforeNullAndEmptyValue = ",", replaceNullAndEmptyValuesWith = "�" } });
            result.Add(new object[] { "1, 2, 3, 4, 5", new JoinParameters(value2, ", ") { separatorBeforeNullAndEmptyValue = ",", skipNullAndEmptyValues = true, replaceNullAndEmptyValuesWith = "�" } });
            result.Add(new object[] { "1, 2, 3, 4 ��� 5", new JoinParameters(value2, ", ") { lastSeparator = " ��� ", separatorBeforeNullAndEmptyValue = ",", skipNullAndEmptyValues = true, replaceNullAndEmptyValuesWith = "�" } });

            return result;
        }


        public class JoinParameters
        {
            public object[] value;
            public string separator;
            public string lastSeparator { set { _lastSeparator = value; } }
            public string separatorBeforeNullAndEmptyValue { set { _separatorBeforeNullAndEmptyValue = value; } }
            public bool skipNullAndEmptyValues { set { _skipNullAndEmptyValues = value; } }
            public string replaceNullAndEmptyValuesWith { set { _replaceNullAndEmptyValuesWith = value; } }


            public JoinParameters(object[] value, string separator)
            {
                this.value = value;
                this.separator = separator;
            }


            public object[] ToArray()
            {
                var result = new List<object>();
                result.Add(value);
                result.Add(separator);
                result.Add(_lastSeparator);
                result.Add(_separatorBeforeNullAndEmptyValue);
                result.Add(_skipNullAndEmptyValues);
                result.Add(_replaceNullAndEmptyValuesWith);

                return result.ToArray();
            }


            object _lastSeparator = Type.Missing;
            object _separatorBeforeNullAndEmptyValue = Type.Missing;
            object _skipNullAndEmptyValues = Type.Missing;
            object _replaceNullAndEmptyValuesWith = Type.Missing;
        }
    }
}
