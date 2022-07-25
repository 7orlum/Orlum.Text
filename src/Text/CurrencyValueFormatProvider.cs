using System.Globalization;


namespace Orlum.Text
{
    public class CurrencyValueFormatProvider : IFormatProvider
    {
        private CultureInfo? _cultureInfo;
        private string _isoCurrencySymbol;
        private bool _useCurrencySpecificPositiveNegativePatterns;
        private bool _updateNumberFormatInfo = true;
        private NumberFormatInfo? _numberFormatInfo;


        public CultureInfo? CultureInfo 
        {
            get => _cultureInfo;
            set
            {
                if (_cultureInfo != value)
                {
                    _cultureInfo = value;
                    _updateNumberFormatInfo = true;
                }
            }
        }


        public string ISOCurrencySymbol
        {
            get => _isoCurrencySymbol;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(ISOCurrencySymbol));

                if (value.Length != 3)
                    throw new ArgumentException("Value must be three-character ISO 4217 currency symbol", nameof(ISOCurrencySymbol));

                if (_isoCurrencySymbol != value)
                {
                    _isoCurrencySymbol = value;
                    _updateNumberFormatInfo = true;
                }
            }
        }


        public bool UseCurrencySpecificPositiveNegativePatterns
        {
            get => _useCurrencySpecificPositiveNegativePatterns;
            set
            {
                if (_useCurrencySpecificPositiveNegativePatterns != value)
                {
                    _useCurrencySpecificPositiveNegativePatterns = value;
                    _updateNumberFormatInfo = true;
                }
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyValueFormatProvider"/> class based on the specified culture information.
        /// </summary>
        /// <param name="cultureInfo">Culture-specific information using for dates and numbers formatting.</param>
        public CurrencyValueFormatProvider(string isoCurrencySymbol, CultureInfo? cultureInfo = null)
        {
            CultureInfo = cultureInfo;
            ISOCurrencySymbol = isoCurrencySymbol;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyValueFormatProvider"/> class based on the specified culture information.
        /// </summary>
        /// <param name="cultureInfo">Culture-specific information using for dates and numbers formatting.</param>
        public CurrencyValueFormatProvider(string isoCurrencySymbol, bool useCurrencySpecificPositiveNegativePatterns, CultureInfo? cultureInfo = null)
        {
            CultureInfo = cultureInfo;
            ISOCurrencySymbol = isoCurrencySymbol;
            UseCurrencySpecificPositiveNegativePatterns = useCurrencySpecificPositiveNegativePatterns;
        }


        /// <summary>
        /// Returns an object that provides formatting services for the specified type.
        /// </summary>
        /// <param name="formatType">An object that specifies the type of format object to return.</param>
        /// <returns></returns>
        public object? GetFormat(Type? formatType)
        {
            if (_updateNumberFormatInfo)
            {
                _numberFormatInfo = CloneAndUpdateNumberFormatInfo(
                    (this.CultureInfo ?? CultureInfo.CurrentCulture).NumberFormat, ISOCurrencySymbol, 
                    updateCurrencySymbol: true, updateCurrencyPositiveNegativePatterns: UseCurrencySpecificPositiveNegativePatterns);
                _updateNumberFormatInfo = false;
            }

            return _numberFormatInfo!.GetFormat(formatType);
        }


        /// <summary>
        /// Clone the number format info and change currency symbol and positive negative patterns to ones from specified currency number format info.
        /// </summary>
        /// <param name="sourceNumberFormatInfo">Number format to be modified</param>
        /// <param name="isoCurrencySymbol">Three-character ISO 4217 currency symbol</param>
        /// <returns></returns>
        private static NumberFormatInfo CloneAndUpdateNumberFormatInfo(NumberFormatInfo sourceNumberFormatInfo, string isoCurrencySymbol, bool updateCurrencySymbol, bool updateCurrencyPositiveNegativePatterns)
        {
            var currencyNumberFormatInfo = GetCurrencyNumberFormatInfo(isoCurrencySymbol);

            var result = (NumberFormatInfo)sourceNumberFormatInfo.Clone();
            
            if (updateCurrencySymbol)
                result.CurrencySymbol = currencyNumberFormatInfo.CurrencySymbol;

            if (updateCurrencyPositiveNegativePatterns)
            {
                result.CurrencyPositivePattern = currencyNumberFormatInfo.CurrencyPositivePattern;
                result.CurrencyNegativePattern = currencyNumberFormatInfo.CurrencyNegativePattern;
            }

            return result;
        }


        /// <summary>
        /// Looks for a country/region using specified currency and returns that region culture information.
        /// </summary>
        /// <param name="isoCurrencySymbol">Three-character ISO 4217 currency symbol</param>
        /// <returns>Culture information of the country/region using specified currency.</returns>
        private static NumberFormatInfo GetCurrencyNumberFormatInfo(string isoCurrencySymbol)
        {
            var result = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Where(culture => string.Compare(new RegionInfo(culture.LCID).ISOCurrencySymbol, isoCurrencySymbol, true) == 0)
                .OrderByDescending(culture =>
                    (string.Compare(culture.NumberFormat.CurrencySymbol, isoCurrencySymbol, ignoreCase: true) == 0 ? 0 : 1) +
                    (string.Compare(culture.NumberFormat.CurrencySymbol, CultureInfo.InvariantCulture.NumberFormat.CurrencySymbol, ignoreCase: true) == 0 ? 0 : 1));

            if (result.Any())
                return result.First().NumberFormat;
            else
                return CultureInfo.InvariantCulture.NumberFormat;
        }
    }
}
