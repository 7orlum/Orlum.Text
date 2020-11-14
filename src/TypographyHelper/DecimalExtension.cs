using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;


namespace Orlum.TypographyHelper.DecimalExtention
{
    public static class DecimalExtensionMethods
    {
        /// <summary>
        /// Changes default currency symbol to a currency symbol for specified currency and formats number. Negative and positive patterns also will be changed.
        /// </summary>
        /// <param name="amount">Currency amount</param>
        /// <param name="isoCurrency">Three-character ISO 4217 currency symbol</param>
        /// <param name="cultureInfo">Prefered culture-specific information that will be used for formatting numeric values</param>
        /// <returns></returns>
        public static string FormatCurrency(this decimal amount, string isoCurrency, CultureInfo cultureInfo = null)
        {
            if (isoCurrency == null)
                throw new ArgumentNullException(nameof(isoCurrency));

            if (cultureInfo == null)
                cultureInfo = CultureInfo.CurrentCulture;

            var customFormatter = CustomizeNumberFormatProvider(cultureInfo.NumberFormat, isoCurrency);
            return string.Format(customFormatter, "{0:C}", amount);
        }


        /// <summary>
        /// Change currency symbol and positive negative patterns in number format to specific ones for specified currency.
        /// </summary>
        /// <param name="numberFormatInfo">Number format to be modified</param>
        /// <param name="isoCurrency">Three-character ISO 4217 currency symbol</param>
        /// <returns></returns>
        private static NumberFormatInfo CustomizeNumberFormatProvider(NumberFormatInfo numberFormatInfo, string isoCurrency)
        {
            if (numberFormatInfo == null)
                throw new ArgumentNullException(nameof(numberFormatInfo));

            if (isoCurrency == null)
                throw new ArgumentNullException(nameof(isoCurrency));

            var currencyNumberFormatInfo = GetNumberFormatProviderForCurrency(isoCurrency);

            var result = (NumberFormatInfo)numberFormatInfo.Clone();
            result.CurrencySymbol = currencyNumberFormatInfo.CurrencySymbol;
            result.CurrencyPositivePattern = currencyNumberFormatInfo.CurrencyPositivePattern;
            result.CurrencyNegativePattern = currencyNumberFormatInfo.CurrencyNegativePattern;
            return result;
        }


        /// <summary>
        /// Looks for a country/region using specified currency and returns that region culture information.
        /// </summary>
        /// <param name="isoCurrency">Three-character ISO 4217 currency symbol</param>
        /// <returns>Culture information of the country/region using specified currency.</returns>
        private static NumberFormatInfo GetNumberFormatProviderForCurrency(string isoCurrency)
        {
            if (isoCurrency == null)
                throw new ArgumentNullException(nameof(isoCurrency));

            if (_currencyCultureInfoDictionary == null)
                _currencyCultureInfoDictionary = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                        .Select(culture => (Culture: culture, Currency: new RegionInfo(culture.LCID).ISOCurrencySymbol))
                        .GroupBy(cultureCurrencyPair => cultureCurrencyPair.Currency)
                        .ToDictionary(group => group.Key, group => group
                            .Select(element => element.Culture)
                            .OrderByDescending(element => 
                                (string.Compare(element.NumberFormat.CurrencySymbol, isoCurrency, ignoreCase: true) == 0 ? 0 : 1) + 
                                (string.Compare(element.NumberFormat.CurrencySymbol, CultureInfo.InvariantCulture.NumberFormat.CurrencySymbol, ignoreCase: true) == 0 ? 0 : 1))
                            .First(), StringComparer.OrdinalIgnoreCase);

            if (_currencyCultureInfoDictionary.ContainsKey(isoCurrency))
                return _currencyCultureInfoDictionary[isoCurrency].NumberFormat;
            else
                return CultureInfo.InvariantCulture.NumberFormat;
        }


        private static Dictionary<string, CultureInfo> _currencyCultureInfoDictionary = null;
    }
}
