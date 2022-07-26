using System.Globalization;

namespace Orlum.Text
{
    public static class Format
    {
        private static readonly Dictionary<CultureInfo, NumericalPhraseFormatProvider> _numericalPhraseFormatProviders = new();
        private static readonly Dictionary<CurrencyValueFormatProviderKey, CurrencyValueFormatProvider> _currencyValueFormatProviders = new();

        public static string NumericalPhrase(FormattableString formattableString)
        {
            return NumericalPhrase(null, formattableString);
        }

        public static string NumericalPhrase(CultureInfo? cultureInfo, FormattableString formattableString)
        {
            if (formattableString == null)
                throw new ArgumentNullException(nameof(formattableString));

            if (!_numericalPhraseFormatProviders.ContainsKey(cultureInfo ?? CultureInfo.CurrentCulture))
                _numericalPhraseFormatProviders.Add(cultureInfo ?? CultureInfo.CurrentCulture, new NumericalPhraseFormatProvider(cultureInfo));

            return formattableString.ToString(_numericalPhraseFormatProviders[cultureInfo ?? CultureInfo.CurrentCulture]);
        }

        public static string NumericalPhrase(RawString format, params object[] args)
        {
            return NumericalPhrase(null, format, args);
        }

        public static string NumericalPhrase(CultureInfo? cultureInfo, RawString format, params object[] args)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));

            if (!_numericalPhraseFormatProviders.ContainsKey(cultureInfo ?? CultureInfo.CurrentCulture))
                _numericalPhraseFormatProviders.Add(cultureInfo ?? CultureInfo.CurrentCulture, new NumericalPhraseFormatProvider(cultureInfo));

            return string.Format(_numericalPhraseFormatProviders[cultureInfo ?? CultureInfo.CurrentCulture], format.Value, args);
        }

        public static string CurrencyValue(string isoCurrencySymbol, FormattableString formattableString)
        {
            return CurrencyValue(null, isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns: false, formattableString);
        }

        public static string CurrencyValue(CultureInfo? cultureInfo, string isoCurrencySymbol, FormattableString formattableString)
        {
            return CurrencyValue(cultureInfo, isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns: false, formattableString);
        }

        public static string CurrencyValue(string isoCurrencySymbol, bool useCurrencySpecificPositiveNegativePatterns, FormattableString formattableString)
        {
            return CurrencyValue(null, isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns, formattableString);
        }

        public static string CurrencyValue(CultureInfo? cultureInfo, string isoCurrencySymbol, bool useCurrencySpecificPositiveNegativePatterns, FormattableString formattableString)
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

        public static string CurrencyValue(string isoCurrencySymbol, RawString format, params object[] args)
        {
            return CurrencyValue(null, isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns: false, format, args);
        }

        public static string CurrencyValue(string isoCurrencySymbol, bool useCurrencySpecificPositiveNegativePatterns, string format, params object[] args)
        {
            return CurrencyValue(null, isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns, format, args);
        }

        public static string CurrencyValue(CultureInfo? cultureInfo, string isoCurrencySymbol, RawString format, params object[] args)
        {
            return CurrencyValue(cultureInfo, isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns: false, format, args);
        }

        public static string CurrencyValue(CultureInfo? cultureInfo, string isoCurrencySymbol, bool useCurrencySpecificPositiveNegativePatterns, RawString format, params object[] args)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));

            if (isoCurrencySymbol == null)
                throw new ArgumentNullException(nameof(isoCurrencySymbol));

            var key = new CurrencyValueFormatProviderKey(isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns, cultureInfo);

            if (!_currencyValueFormatProviders.ContainsKey(key))
                _currencyValueFormatProviders.Add(key, new CurrencyValueFormatProvider(isoCurrencySymbol, useCurrencySpecificPositiveNegativePatterns, cultureInfo));

            return string.Format(_currencyValueFormatProviders[key], format.Value, args);
        }

        private record CurrencyValueFormatProviderKey(string isoCurrencySymbol, bool useCurrencySpecificPositiveNegativePatterns, CultureInfo? cultureInfo);
    }
}