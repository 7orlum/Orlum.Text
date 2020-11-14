using System;
using System.Collections.Generic;
using System.Globalization;


namespace Orlum.TypographyHelper
{
    public class Format
    {
        private static readonly Dictionary<CultureInfo, NumericalPhraseFormatProvider> _numericalPhraseFormatProviders = new Dictionary<CultureInfo, NumericalPhraseFormatProvider>();


        public static string NP(FormattableString formattableString, CultureInfo cultureInfo = null)
        {
            if (!_numericalPhraseFormatProviders.ContainsKey(cultureInfo ?? CultureInfo.CurrentCulture))
                _numericalPhraseFormatProviders.Add(cultureInfo ?? CultureInfo.CurrentCulture, new NumericalPhraseFormatProvider(cultureInfo));

            return formattableString.ToString(_numericalPhraseFormatProviders[cultureInfo ?? CultureInfo.CurrentCulture]);
        }
    }
}
