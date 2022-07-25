using System;
using System.Collections.Generic;
using System.Globalization;

namespace Orlum.Text
{
    public static class Format
    {
        private static readonly Dictionary<CultureInfo, NumericalPhraseFormatProvider> _numericalPhraseFormatProviders = new();
        private static readonly Dictionary<CurrencyValueFormatProviderKey, CurrencyValueFormatProvider> _currencyValueFormatProviders = new();
        
        public static string NP(FormattableString formattableString)
        {
            return NP(null, formattableString);
        }

        public static string NP(CultureInfo? cultureInfo, FormattableString formattableString)
        {
            if (formattableString == null)
                throw new ArgumentNullException(nameof(formattableString));

            if (!_numericalPhraseFormatProviders.ContainsKey(cultureInfo ?? CultureInfo.CurrentCulture))
                _numericalPhraseFormatProviders.Add(cultureInfo ?? CultureInfo.CurrentCulture, new NumericalPhraseFormatProvider(cultureInfo));

            return formattableString.ToString(_numericalPhraseFormatProviders[cultureInfo ?? CultureInfo.CurrentCulture]);
        }

        public static string NumericalPhrase(string format, params object[] args)
        {
            return NumericalPhrase(null, format, args);
        }

        public static string NumericalPhrase(CultureInfo? cultureInfo, string format, params object[] args)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));

            if (!_numericalPhraseFormatProviders.ContainsKey(cultureInfo ?? CultureInfo.CurrentCulture))
                _numericalPhraseFormatProviders.Add(cultureInfo ?? CultureInfo.CurrentCulture, new NumericalPhraseFormatProvider(cultureInfo));

            return string.Format(_numericalPhraseFormatProviders[cultureInfo ?? CultureInfo.CurrentCulture], format, args);
        }

        public static string CV(string isoCurrencySymbol, FormattableString formattableString)
        {
            return CV(null, isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns: false, formattableString);
        }

        public static string CV(CultureInfo? cultureInfo, string isoCurrencySymbol, FormattableString formattableString)
        {
            return CV(cultureInfo, isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns: false, formattableString);
        }

        public static string CV(string isoCurrencySymbol, bool useCurrencySpecificPositiveNegativePatterns, FormattableString formattableString)
        {
            return CV(null, isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns, formattableString);
        }

        public static string CV(CultureInfo? cultureInfo, string isoCurrencySymbol, bool useCurrencySpecificPositiveNegativePatterns, FormattableString formattableString)
        {
            if (formattableString == null)
                throw new ArgumentNullException(nameof(formattableString));

            if (isoCurrencySymbol == null)
                throw new ArgumentNullException(nameof(isoCurrencySymbol));

            var key = new CurrencyValueFormatProviderKey(isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns, cultureInfo);

            if (!_currencyValueFormatProviders.ContainsKey(key))
                _currencyValueFormatProviders.Add(key, new CurrencyValueFormatProvider(isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns, cultureInfo));

            return formattableString.ToString(_currencyValueFormatProviders[key]);
        }

        public static string CurrencyValue(string isoCurrencySymbol, string format, params object[] args)
        {
            return CurrencyValue(null, isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns: false, format, args);
        }

        public static string CurrencyValue(string isoCurrencySymbol, bool useCurrencySpecificPositiveNegativePatterns, string format, params object[] args)
        {
            return CurrencyValue(null, isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns, format, args);
        }

        public static string CurrencyValue(CultureInfo? cultureInfo, string isoCurrencySymbol, string format, params object[] args)
        {
            return CurrencyValue(cultureInfo, isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns: false, format, args);
        }

        public static string CurrencyValue(CultureInfo? cultureInfo, string isoCurrencySymbol, bool useCurrencySpecificPositiveNegativePatterns, string format, params object[] args)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));

            if (isoCurrencySymbol == null)
                throw new ArgumentNullException(nameof(isoCurrencySymbol));

            var key = new CurrencyValueFormatProviderKey(isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns, cultureInfo);

            if (!_currencyValueFormatProviders.ContainsKey(key))
                _currencyValueFormatProviders.Add(key, new CurrencyValueFormatProvider(isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns, cultureInfo));

            return string.Format(_currencyValueFormatProviders[key], format, args);
        }
        
        [Obsolete("This function signature is obsolete. Use public static string NP(CultureInfo cultureInfo, String format, params object[] args)")]
        public static string NP(FormattableString formattableString, CultureInfo? cultureInfo = null)
        {
            if (formattableString == null)
                throw new ArgumentNullException(nameof(formattableString));

            if (!_numericalPhraseFormatProviders.ContainsKey(cultureInfo ?? CultureInfo.CurrentCulture))
                _numericalPhraseFormatProviders.Add(cultureInfo ?? CultureInfo.CurrentCulture, new NumericalPhraseFormatProvider(cultureInfo));

            return formattableString.ToString(_numericalPhraseFormatProviders[cultureInfo ?? CultureInfo.CurrentCulture]);
        }

        [Obsolete("This function signature is obsolete. Use public static string CV(CultureInfo cultureInfo, string isoCurrencySymbol, FormattableString formattableString)")]
        public static string CV(FormattableString formattableString, string isoCurrencySymbol, CultureInfo? cultureInfo = null)
        {
            return CV(formattableString, isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns: false, cultureInfo);
        }

        [Obsolete("This function signature is obsolete. Use public static string CV(CultureInfo cultureInfo, string isoCurrencySymbol, bool useCurrencySpecificPositiveNegativePatterns, FormattableString formattableString)")]
        public static string CV(FormattableString formattableString, string isoCurrencySymbol, bool useCurrencySpecificPositiveNegativePatterns, CultureInfo? cultureInfo = null)
        {
            if (formattableString == null)
                throw new ArgumentNullException(nameof(formattableString));

            if (isoCurrencySymbol == null)
                throw new ArgumentNullException(nameof(isoCurrencySymbol));

            var key = new CurrencyValueFormatProviderKey(isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns, cultureInfo);

            if (!_currencyValueFormatProviders.ContainsKey(key))
                _currencyValueFormatProviders.Add(key, new CurrencyValueFormatProvider(isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns, cultureInfo));

            return formattableString.ToString(_currencyValueFormatProviders[key]);
        }

        private record CurrencyValueFormatProviderKey(string isoCurrencySymbol, bool useCurrencySpecificPositiveNegativePatterns, CultureInfo? cultureInfo);
    }
}