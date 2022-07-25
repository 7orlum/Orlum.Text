using System.Collections.Immutable;


namespace Orlum.Text
{
    /// <summary>
    /// Implementation of <see cref="INumberAgreement"/> for contemporary Polish.
    /// <remarks>
    /// In Polish numerals from "two" to "four" are always followed by a noun in the same plural case, but higher numerals (if in the nominative) are followed by a noun in the genitive plural.
    /// Fractional numerals (if in the nominative) are followed by a noun in the genitive singular.
    /// </remarks>
    /// </summary>
    public sealed class PolishNumberAgreement : INumberAgreement
    {
        /// <summary>
        /// Enumerates all distinguishing grammatical number values in the Polish language.
        /// </summary>
        public IList<GrammaticalNumber> GrammaticalNumbers => ImmutableArray.Create<GrammaticalNumber>(
            GrammaticalNumber.Singular, GrammaticalNumber.Paucal, GrammaticalNumber.Plural, GrammaticalNumber.Fractional);


        /// <summary>
        /// Describes how to get correct format string.
        /// </summary>
        public string DescriptionOfFormatString =>
            $"Expected {GrammaticalNumbers.Count} forms of a phrase inflected for number and splited by semicolon. " +
            "Specify inflections of the phrase required to be compatible with numbers 1, 2, 5 and ½ in that exact order, " +
            "for example {0:NP;PL;litr;litry;litrów;litra}";


        /// <summary>
        /// Finds the concord grammatical number value to agree numerical phrase with specified number.
        /// </summary>
        public GrammaticalNumber MatchGrammaticalNumber(double number)
        {
            var isInteger = Math.Truncate(number) == number;
            
            var result = (isInteger, Math.Abs(number)) switch
            {
                (true, 1) => GrammaticalNumber.Singular,
                (true, > 1 and < 5) => GrammaticalNumber.Paucal,
                (true, _) => GrammaticalNumber.Plural,
                (false, _) => GrammaticalNumber.Fractional
            };

            if (!GrammaticalNumbers.Contains(result))
                throw new ApplicationException("Unexpected GrammaticalNumber value");

            return result;
        }
    }
}
