using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Diagnostics.Contracts;


namespace Orlum.TypographyHelper
{
    /// <summary>
    /// The custom string formatter that choose the correct, in accordance with language agreement, form of the phrase depending on the given numeric argument
    /// </summary>
    /// <example>The following example shows how to use <see cref="NumericalPhraseFormatProvider"/> to format string.
    /// <code>
    /// using orlum.TypographyHelper;
    ///
    /// public class FormatterExample
    /// {
    ///    public static void Main()
    ///    {
    ///        Console.WriteLine(string.Format(new NumericalPhraseFormatter(), "{0:NP;RU;Запрошен;Запрошено;Запрошено} {0} {0:NP;RU;рубль;рубля;рублей}", 3.5));
    ///        Console.WriteLine(string.Format(new NumericalPhraseFormatter(), "{0:NP;RU;Запрошен;Запрошено;Запрошено} {0} {0:NP;RU;рубль;рубля;рублей}", 1));
    ///        Console.WriteLine(string.Format(new NumericalPhraseFormatter(), "{0:NP;RU;Запрошен;Запрошено;Запрошено} {0} {0:NP;RU;рубль;рубля;рублей}", 0));
    ///    }
    /// }
    ///
    /// /*
    /// This code produces the following output.
    /// 
    /// Запрошено 3,5 рубля
    /// Запрошен 1 рубль
    /// Запрошено 0 рублей
    /// 
    /// */
    ///</code>
    ///</example>
    public class NumericalPhraseFormatProvider : IFormatProvider
    {
        private readonly NumericalPhraseFormatter _formatter = new NumericalPhraseFormatter();


        /// <summary>
        /// Language codes and corresponding number agreements to choose a correct phrase form.
        /// </summary>
        public Dictionary<string, INumberAgreement> NumberAgreements { get; private set; } = new Dictionary<string, INumberAgreement>(StringComparer.OrdinalIgnoreCase);


        /// <summary>
        /// A culture-specific information using for dates and numbers formatting.
        /// </summary>
        public CultureInfo CultureInfo { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="NumericalPhraseFormatProvider"/> class based on the specified culture information.
        /// </summary>
        /// <param name="cultureInfo">Culture-specific information using for dates and numbers formatting.</param>
        public NumericalPhraseFormatProvider(CultureInfo cultureInfo = null)
        {
            CultureInfo = cultureInfo;
            NumberAgreements.Add("en", new EnglishNumberAgreement());
            NumberAgreements.Add("pl", new PolishNumberAgreement());
            NumberAgreements.Add("ru", new RussianNumberAgreement());
        }


        /// <summary>
        /// Returns an object that provides formatting services for the specified type.
        /// </summary>
        /// <param name="formatType">An object that specifies the type of format object to return.</param>
        /// <returns></returns>
        public object GetFormat(Type formatType)
        {
            Contract.Ensures(Contract.Result<object>() == null || Contract.Result<object>() is NumericalPhraseFormatProvider);

            if (typeof(ICustomFormatter).IsAssignableFrom(formatType))
                return _formatter;
            else
                return null;
        }


        private class NumericalPhraseFormatter : ICustomFormatter
        {
            /// <summary>
            /// Choose the correct form of the phrase from the format string required to be compatible with the specified number.
            /// </summary>
            /// <param name="format">A format string containing inflections in accordance with russian language agreement of the phrase 
            /// required to be compatible with numbers 1, 2 and 5 in that exact order and splited by semicolon like {0:^рубль;рубля;рублей}.</param>
            /// <param name="arg">A number to choose compatible with it form of the phrase.</param>
            /// <param name="formatProvider">An object that supplies format information about the current instance.</param>
            /// <exception cref="ArgumentException">The value of <paramref name="arg"/> is not convertable to double.</exception>
            /// <exception cref="FormatException">Format string is incorrect.</exception>
            /// <exception cref="KeyNotFoundException">Number agreement for specifiied language is not found.</exception>
            public string Format(string format, object arg, IFormatProvider formatProvider)
            {
                Contract.Ensures(Contract.Result<string>() != null);

                var numericalPhraseFormatProvider = formatProvider as NumericalPhraseFormatProvider;
                if (numericalPhraseFormatProvider == null)
                    return null;

                if (!TryParseFormat(format, out var language, out var forms))
                    return HandleOtherFormats(format, arg, numericalPhraseFormatProvider.CultureInfo);

                if (string.IsNullOrEmpty(language))
                    throw new FormatException("Expected format string like {0:NP;EN;cow;cows}");

                var agreement = numericalPhraseFormatProvider.NumberAgreements[language];
                if (forms.Length != agreement.GrammaticalNumbers.Count)
                    throw new FormatException(agreement.DescriptionOfFormatString);

                double number;
                try
                {
                    number = Convert.ToDouble(arg, CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Argument must be convertable to double", e);
                }

                return forms[agreement.GrammaticalNumbers.IndexOf(agreement.MatchGrammaticalNumber(number))];
            }

            
            private bool TryParseFormat(string format, out string language, out string[] forms)
            {
                language = null;
                forms = null;
                
                if (string.IsNullOrEmpty(format))
                    return false;

                var match = Regex.Match(format, @"^NP(;(?<language>.*?))?((?:;(?<form>.*?))+)?$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                if (!match.Success)
                    return false;

                language = match.Groups["language"].Value;
                forms = match.Groups["form"].Captures.Select(capture => capture.Value).ToArray();

                return true;
            }

            
            private string HandleOtherFormats(string format, object arg, CultureInfo culture)
            {
                Contract.Ensures(Contract.Result<string>() != null);

                if (arg is IFormattable)
                    return ((IFormattable)arg).ToString(format, culture);
                else if (arg == null)
                    return String.Empty;
                else
                    return arg.ToString();
            }
        }
    }
}
