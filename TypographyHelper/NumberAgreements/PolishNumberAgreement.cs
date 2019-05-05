using System;
using System.Linq;
using System.Diagnostics.Contracts;


namespace orlum.TypographyHelper
{
    /// <summary>
    /// Implementation of <see cref="INumberAgreement"/> for contemporary Polish language
    /// <remarks>
    /// In Polish numerals from "two" to "four" are always followed by a noun in the same plural case, but higher numerals (if in the nominative) are followed by a noun in the genitive plural.
    /// Fractional numerals (if in the nominative) are followed by a noun in the genitive singular.
    /// </remarks>
    /// </summary>
    public class PolishNumberAgreement : INumberAgreement
    {
        /// <summary>
        /// Enumerate three available gramatical number forms in Polish language 
        /// </summary>
        public GramaticalNumber[] AvailableForms => 
            new GramaticalNumber[] { GramaticalNumber.Singular, GramaticalNumber.Paucal, GramaticalNumber.Plural, GramaticalNumber.Fractional };


        /// <summary>
        /// Finds the concord gramatical number to agree numerical phrase with specified number
        /// </summary>
        public GramaticalNumber ConcordForm(double number)
        {
            Contract.Ensures(AvailableForms.Contains(Contract.Result<GramaticalNumber>()));

            //Fractional numbers
            if (Math.Round(number) != number)
                return GramaticalNumber.Fractional;

            //Integer numbers
            switch (Math.Abs(number))
            {
                case 1:
                    return GramaticalNumber.Singular;
                case double n when n > 1 && n < 5:
                    return GramaticalNumber.Paucal;
                default:
                    return GramaticalNumber.Plural;
            }
        }


        /// <summary>
        /// Describes how to write correct format string for this language
        /// </summary>
        public string DescriptionOfFormatString => 
            $"Expected {AvailableForms.Length} forms of a phrase inflected for number and splited by semicolon. " +
            "Specify inflections of the phrase required to be compatible with numbers 1, 2, 5 and ½ in that exact order, " +
            "for example {0:NP;pl;litr;litry;litrów;litra}";
    }
}
