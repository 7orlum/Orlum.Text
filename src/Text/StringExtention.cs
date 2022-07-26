using System.Globalization;

namespace Orlum.Text.StringExtention
{
    /// <summary>
    /// <see cref="String"/> auxiliary functions
    /// </summary>
    public static class StringExtensionMethods
    {
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
        public static string CapitalizeFirstLetter(this string value)
        {
            return StringHelper.CapitalizeFirstLetter(value, CultureInfo.CurrentCulture);
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
        public static string CapitalizeFirstLetter(this string value, CultureInfo culture)
        {
            return StringHelper.CapitalizeFirstLetter(value, culture);
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
        public static string UncapitalizeFirstLetter(this string value)
        {
            return StringHelper.UncapitalizeFirstLetter(value, CultureInfo.CurrentCulture);
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
        public static string UncapitalizeFirstLetter(this string value, CultureInfo culture)
        {
            return StringHelper.UncapitalizeFirstLetter(value, culture);
        }
    }
}
