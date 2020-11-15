using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;


namespace Orlum.Text
{
    /// <summary>
    /// Implementation of <see cref="INumberAgreement"/> for contemporary Russian.
    /// <remarks>
    /// In Russian, the form of noun following the numeral is nominative singular if the numeral ends in "one", genitive singular if the numeral ends in "two" to "four", and genitive plural otherwise. 
    /// As an exception, the form of noun is also genitive plural if the numeral ends in 11 to 14. 
    /// Also, some words (for example, many measure words, such as units) have a special "count form" (счётная форма) for use in numerical phrases instead of genitive (for some words mandatory, for others optional), 
    /// for example, восемь мегабайт, пять килограмм and пять килограммов, три ряда́ and три ря́да, and полтора часа́.
    /// </remarks>
    /// </summary>
    public sealed class RussianNumberAgreement : INumberAgreement
    {
        /// <summary>
        /// Enumerates all distinguishing grammatical number values in the Russian language.
        /// </summary>
        public IList<GrammaticalNumber> GrammaticalNumbers => ImmutableArray.Create<GrammaticalNumber>(
            GrammaticalNumber.Singular, GrammaticalNumber.Paucal, GrammaticalNumber.Plural);


        /// <summary>
        /// Describes how to get correct format string.
        /// </summary>
        public string DescriptionOfFormatString =>
            $"Expected {GrammaticalNumbers.Count} forms of a phrase inflected for number and splited by semicolon. " +
            "Specify inflections of the phrase required to be compatible with numbers 1, 2 and 5 in that exact order, " +
            "for example {0:NP;RU;рубль;рубля;рублей}";


        /// <summary>
        /// Finds the concord grammatical number value to agree numerical phrase with specified number.
        /// </summary>
        public GrammaticalNumber MatchGrammaticalNumber(double number)
        {
            Contract.Ensures(GrammaticalNumbers.Contains(Contract.Result<GrammaticalNumber>()));

            //Fractional numbers
            if (Math.Round(number) != number)
                return GrammaticalNumber.Paucal;

            //Integer numbers
            switch (Math.Abs(number) % 100)
            {
                case double n when n > 10 && n < 20:
                    return GrammaticalNumber.Plural;
                default:
                    switch (Math.Abs(number) % 10)
                    {
                        case 1:
                            return GrammaticalNumber.Singular;
                        case double n when n > 1 && n < 5:
                            return GrammaticalNumber.Paucal;
                        default:
                            return GrammaticalNumber.Plural;
                    }
            }
        }
    }
}
