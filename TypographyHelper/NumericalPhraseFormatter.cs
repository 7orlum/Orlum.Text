using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Diagnostics.Contracts;


namespace orlum.TypographyHelper
{
    /// <summary>
    /// Custom string formatter to choose the correct in accordance with language agreement form of the phrase depending on the given numeric argument
    /// </summary>
    /// <example>The following example shows how to use <see cref="NumericalPhraseFormatter"/> to format string.
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
    public class NumericalPhraseFormatter : IFormatProvider, ICustomFormatter
    {
        /// <summary>
        /// Culture-specific information using for dates and numbers formatting.
        /// </summary>
        public CultureInfo CultureInfo { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="NumericalPhraseFormatter"/> class based on the specified culture information.
        /// </summary>
        public NumericalPhraseFormatter(CultureInfo cultureInfo = null)
        {
            CultureInfo = cultureInfo;

            _numberAgreements = new Dictionary<string, INumberAgreement>(StringComparer.OrdinalIgnoreCase);
            _numberAgreements.Add("en", new EnglishNumberAgreement());
            _numberAgreements.Add("pl", new PolishNumberAgreement());
            _numberAgreements.Add("ru", new RussianNumberAgreement());
        }


        /// <summary>
        /// Returns an object that provides formatting services for the specified type.
        /// </summary>
        /// <param name="formatType">An object that specifies the type of format object to return.</param>
        /// <returns></returns>
        public object GetFormat(Type formatType)
        {
            Contract.Ensures(Contract.Result<object>() == null || Contract.Result<object>() is NumericalPhraseFormatter);

            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }


        /// <summary>
        /// Choose the correct form of the phrase from the format string required to be compatible with the specified number.
        /// </summary>
        /// <param name="format">A format string containing inflections in accordance with russian language agreement of the phrase 
        /// required to be compatible with numbers 1, 2 and 5 in that exact order and splited by semicolon like {0:^рубль;рубля;рублей}.</param>
        /// <param name="arg">A number to choose compatible with it form of the phrase.</param>
        /// <param name="formatProvider">An object that supplies format information about the current instance.</param>
        /// <exception cref="ArgumentException">The value of <paramref name="arg"/> is not convertable to double.</exception>
        /// <exception cref="FormatException">Format string is incorrect.</exception>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            if (formatProvider == null || !formatProvider.Equals(this))
                return null;

            if (string.IsNullOrEmpty(format))
                return HandleOtherFormats(format, arg);

            Match match = Regex.Match(format, @"^NP;(?<language>.*?)(?:;(?<form>.*?))+$", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            if (!match.Success)
                return HandleOtherFormats(format, arg);

            var agreement = _numberAgreements[match.Groups["language"].Value.ToLower()];

            if (match.Groups["form"].Captures.Count != agreement.AvailableForms.Length)
                throw new FormatException(agreement.DescriptionOfFormatString);

            double number;

            try
            {
                number = Convert.ToDouble(arg);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Argument must be convertable to double", e);
            }

            var formIndex = Array.IndexOf(agreement.AvailableForms, agreement.ConcordForm(number));
            return match.Groups["form"].Captures[formIndex].Value;
        }


        string HandleOtherFormats(string format, object arg)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            if (arg is IFormattable)
                return ((IFormattable)arg).ToString(format, CultureInfo);
            else if (arg != null)
                return arg.ToString();
            else
                return String.Empty;
        }


        readonly Dictionary<string, INumberAgreement> _numberAgreements;
    }
}
