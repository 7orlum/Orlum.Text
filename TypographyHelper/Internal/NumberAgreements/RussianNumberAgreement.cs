using System;
using System.Linq;
using System.Diagnostics.Contracts;


namespace orlum.TypographyHelper
{
    /// <summary>
    /// Implementation of <see cref="INumberAgreement"/> for contemporary Russian language
    /// <remarks>
    /// In Russian, the form of noun following the numeral is nominative singular if the numeral ends in "one", genitive singular if the numeral ends in "two" to "four", and genitive plural otherwise. 
    /// As an exception, the form of noun is also genitive plural if the numeral ends in 11 to 14. 
    /// Also, some words (for example, many measure words, such as units) have a special "count form" (счётная форма) for use in numerical phrases instead of genitive (for some words mandatory, for others optional), 
    /// for example, восемь мегабайт, пять килограмм and пять килограммов, три ряда́ and три ря́да, and полтора часа́.
    /// </remarks>
    /// </summary>
    internal class RussianNumberAgreement : INumberAgreement
    {
        /// <summary>
        /// Enumerate three available gramatical number forms in Russian language 
        /// </summary>
        public GramaticalNumber[] AvailableForms => new GramaticalNumber[] { GramaticalNumber.Singular, GramaticalNumber.Paucal, GramaticalNumber.Plural };


        /// <summary>
        /// Finds the concord gramatical number to agree numerical phrase with specified number
        /// </summary>
        public GramaticalNumber ConcordForm(double number)
        {
            Contract.Ensures(AvailableForms.Contains(Contract.Result<GramaticalNumber>()));

            //Fractional numbers
            if (Math.Round(number) != number)
                return GramaticalNumber.Paucal;

            //Integer numbers
            switch (Math.Abs(number) % 100)
            {
                case double n when n > 10 && n < 20:
                    return GramaticalNumber.Plural;
                default:
                    switch (Math.Abs(number) % 10)
                    {
                        case 1:
                            return GramaticalNumber.Singular;
                        case double n when n > 1 && n < 5:
                            return GramaticalNumber.Paucal;
                        default:
                            return GramaticalNumber.Plural;
                    }
            }
        }


        /// <summary>
        /// Describes how to write correct format string for this language
        /// </summary>
        public string DescriptionOfFormatString =>
            $"Expected {AvailableForms.Length} forms of a phrase inflected for number and splited by semicolon. " +
            "Specify inflections of the phrase required to be compatible with numbers 1, 2 and 5 in that exact order, " +
            "for example {0:NP;ru;рубль;рубля;рублей}";
    }
}
