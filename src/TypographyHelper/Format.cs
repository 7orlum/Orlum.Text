using System;
using System.Collections.Generic;
using System.Globalization;


namespace Orlum.TypographyHelper
{
    public class Format
    {
        private static readonly Dictionary<CultureInfo, NumericalPhraseFormatProvider> _numericalPhraseFormatProviders = new();
        private static readonly Dictionary<CurrencyValueFormatProviderKey, CurrencyValueFormatProvider> _currencyValueFormatProviders = new();


        public static string NP(FormattableString formattableString, CultureInfo cultureInfo = null)
        {
            if (!_numericalPhraseFormatProviders.ContainsKey(cultureInfo ?? CultureInfo.CurrentCulture))
                _numericalPhraseFormatProviders.Add(cultureInfo ?? CultureInfo.CurrentCulture, new NumericalPhraseFormatProvider(cultureInfo));

            return formattableString.ToString(_numericalPhraseFormatProviders[cultureInfo ?? CultureInfo.CurrentCulture]);
        }


        public static string CV(FormattableString formattableString, string isoCurrencySymbol, CultureInfo cultureInfo = null)
        {
            return CV(formattableString, isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns: false, cultureInfo);
        }


        public static string CV(FormattableString formattableString, string isoCurrencySymbol, bool useCurrencySpecificPositiveNegativePatterns, CultureInfo cultureInfo = null)
        {
            var key = new CurrencyValueFormatProviderKey(isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns, cultureInfo);

            if (!_currencyValueFormatProviders.ContainsKey(key))
                _currencyValueFormatProviders.Add(key, new CurrencyValueFormatProvider(isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns, cultureInfo));

            return formattableString.ToString(_currencyValueFormatProviders[key]);
        }


        private record CurrencyValueFormatProviderKey(string isoCurrencySymbol, bool useCurrencySpecificPositiveNegativePatterns, CultureInfo cultureInfo);
    }
}