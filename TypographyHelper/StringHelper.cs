using System;
using System.Text;
using System.Globalization;
using System.Linq;


namespace orlum.TypographyHelper
{
    /// <summary>
    /// Auxiliary functions for formatting strings
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Concatenates all the elements of a string array, using the specified separator between each element.
        /// </summary>
        /// <param name="value">An array that contains the elements to concatenate.</param>
        /// <param name="separator">The string to use as a default separator. Separator is included in the returned string only if value has more than one element. 
        /// If both <paramref name="lastSeparator"/> and <paramref name="separatorBeforeNullAndEmptyValue"/> or any of them are defined they have the highest priority in the appropriate situations.</param>
        /// <param name="lastSeparator">The string to use as a separator beetween last two elements. 
        /// Has the highest priority in comparison with <paramref name="separator"/> and <paramref name="separatorBeforeNullAndEmptyValue"/>. 
        /// Not applicable if not specified or set to null.</param>
        /// <param name="separatorBeforeNullAndEmptyValue">The string to use as a separator beetween elements if second one is null or an empty string.
        /// Has the highest priority in comparison with <paramref name="separator"/>. Not applicable if not specified or set to null.</param>
        /// <param name="skipNullAndEmptyValues">When it is true elements equals null or an empty string will be ignored.</param>
        /// <param name="replaceNullAndEmptyValuesWith">When it is not null each element that is equal null or an empty string will be represented as value of <paramref name="replaceNullAndEmptyValuesWith"/>.</param>
        /// <returns>A string that consists of the strings in value delimited by one of three separator strings.
        /// -or-
        /// <see cref="String.Empty"/> if count is zero, value has no elements, or separator and all the elements of value are Empty.</returns>
        /// <example>The following example shows how to use <see langword="Join"/> to format string.
        /// <code>
        /// using orlum.TypographyHelper;
        ///
        /// public class FormatterExample
        /// {
        ///    public static void Main()
        ///    {
        ///        var value = new object[] { 1, 2, null, 3, null, 4, 5 };
        ///        
        ///        Console.WriteLine(StringHelper.Join(value, ", ", skipNullAndEmptyValues = true));
        ///        Console.WriteLine(StringHelper.Join(value, ", ", lastSeparator = " или ", replaceNullAndEmptyValuesWith = "Н"));
        ///    }
        /// }
        ///
        /// /*
        /// This code produces the following output.
        /// 
        /// 1, 2, 3, 4, 5
        /// 1, 2, Н, 3, Н, 4 или 5
        /// 
        /// */
        ///</code>
        ///</example>
        public static string Join(string[] value, string separator, string lastSeparator = null, 
            string separatorBeforeNullAndEmptyValue = null, bool skipNullAndEmptyValues = false, string replaceNullAndEmptyValuesWith = null)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var result = new StringBuilder(value.Length * 2);

            string previousValue = null;
            bool valuesWasJoinedAlready = false;

            foreach (var element in value)
            {
                if (skipNullAndEmptyValues && String.IsNullOrEmpty(element))
                    continue;

                if (previousValue != null)
                {
                    if (valuesWasJoinedAlready)
                        if (separatorBeforeNullAndEmptyValue != null && String.IsNullOrEmpty(previousValue))
                            result.Append(separatorBeforeNullAndEmptyValue);
                        else
                            result.Append(separator);
                    else
                        valuesWasJoinedAlready = true;

                    if (replaceNullAndEmptyValuesWith != null && String.IsNullOrEmpty(previousValue))
                        result.Append(replaceNullAndEmptyValuesWith);
                    else
                        result.Append(previousValue);
                }

                previousValue = element ?? String.Empty;
            }

            if (previousValue != null)
            {
                if (valuesWasJoinedAlready)
                    if (lastSeparator != null)
                        result.Append(lastSeparator);
                    else if (separatorBeforeNullAndEmptyValue != null && String.IsNullOrEmpty(previousValue))
                        result.Append(separatorBeforeNullAndEmptyValue);
                    else
                        result.Append(separator);

                if (replaceNullAndEmptyValuesWith != null && String.IsNullOrEmpty(previousValue))
                    result.Append(replaceNullAndEmptyValuesWith);
                else
                    result.Append(previousValue);
            }

            return result.ToString();
        }


        /// <summary>
        /// Concatenates string representation of all the elements of an object array and, the specified separator between each element.
        /// </summary>
        /// <param name="value">An array that contains the elements to concatenate.</param>
        /// <param name="separator">The string to use as a default separator. Separator is included in the returned string only if value has more than one element. 
        /// If both <paramref name="lastSeparator"/> and <paramref name="separatorBeforeNullAndEmptyValue"/> or any of them are defined they have the highest priority in the appropriate situations.</param>
        /// <param name="lastSeparator">The string to use as a separator beetween last two elements. 
        /// Has the highest priority in comparison with <paramref name="separator"/> and <paramref name="separatorBeforeNullAndEmptyValue"/>. 
        /// Not applicable if not specified or set to null.</param>
        /// <param name="separatorBeforeNullAndEmptyValue">The string to use as a separator beetween elements if second one is null or an empty string.
        /// Has the highest priority in comparison with <paramref name="separator"/>. Not applicable if not specified or set to null.</param>
        /// <param name="skipNullAndEmptyValues">When it is true elements equals null or an empty string will be ignored.</param>
        /// <param name="replaceNullAndEmptyValuesWith">When it is not null each element that is equal null or an empty string will be represented as value of <paramref name="replaceNullAndEmptyValuesWith"/>.</param>
        /// <returns>A string that consists of the strings in value delimited by one of three separator strings.
        /// -or-
        /// <see cref="String.Empty"/> if count is zero, value has no elements, or separator and all the elements of value are Empty.</returns>
        public static string Join(object[] value, string separator, string lastSeparator = null,
            string separatorBeforeNullAndEmptyValue = null, bool skipNullAndEmptyValues = false, string replaceNullAndEmptyValuesWith = null)
        {
            return Join(value.Select(e => e?.ToString()).ToArray(), separator, lastSeparator, separatorBeforeNullAndEmptyValue, skipNullAndEmptyValues, replaceNullAndEmptyValuesWith);
        }


        /// <summary>
        /// Returns a copy of a string with first letter converted to uppercase.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>A string with first letter in lowercase.
        /// -or-
        /// <see langword="null"/> if string is null
        /// -or-
        /// <see cref="String.Empty"/> if string value is Empty.</returns>
        /// <remarks>
        /// This method takes into account the casing rules of the current culture.
        /// We recommend that you avoid calling string casing methods that substitute default values and instead call methods that require parameters to be explicitly specified. To convert a character to lowercase by using the casing conventions of the current culture, call the <see cref="UncapitalizeFirstLetter(string, CultureInfo)"/> method overload with a value of CurrentCulture for its culture parameter.
        /// </remarks>
        public static string CapitalizeFirstLetter(string value)
        {
            return CapitalizeFirstLetter(value, CultureInfo.CurrentCulture);
        }


        /// <summary>
        /// Returns a copy of a string with first letter converted to uppercase.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="culture">An object that supplies culture-specific casing rules.</param>
        /// <returns>A string with first letter in lowercase.
        /// -or-
        /// <see langword="null"/> if string is null
        /// -or-
        /// <see cref="String.Empty"/> if string value is Empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="culture"/> is <see langword="null"/></exception>
        public static string CapitalizeFirstLetter(string value, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            if (value.Length == 1)
                return value.ToUpper(culture);
            else
                return value.Remove(1).ToUpper(culture) + value.Remove(0, 1);
        }

        
        /// <summary>
        /// Returns a copy of a string with first letter converted to lowercase.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>A string with first letter in lowercase.
        /// -or-
        /// <see langword="null"/> if string is null
        /// -or-
        /// <see cref="String.Empty"/> if string value is Empty.</returns>
        /// <remarks>
        /// This method takes into account the casing rules of the current culture.
        /// We recommend that you avoid calling string casing methods that substitute default values and instead call methods that require parameters to be explicitly specified. To convert a character to lowercase by using the casing conventions of the current culture, call the <see cref="UncapitalizeFirstLetter(string, CultureInfo)"/> method overload with a value of CurrentCulture for its culture parameter.
        /// </remarks>
        public static string UncapitalizeFirstLetter(string value)
        {
            return UncapitalizeFirstLetter(value, CultureInfo.CurrentCulture);
        }


        /// <summary>
        /// Returns a copy of a string with first letter converted to lowercase.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="culture">An object that supplies culture-specific casing rules.</param>
        /// <returns>A string with first letter in lowercase.
        /// -or-
        /// <see langword="null"/> if string is null
        /// -or-
        /// <see cref="String.Empty"/> if string value is Empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="culture"/> is <see langword="null"/></exception>
        public static string UncapitalizeFirstLetter(string value, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            if (value.Length == 1)
                return value.ToLower(culture);
            else
                return value.Remove(1).ToLower(culture) + value.Remove(0, 1);
        }
    }
}
