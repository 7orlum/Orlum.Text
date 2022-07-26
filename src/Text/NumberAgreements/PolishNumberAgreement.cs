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

            var result = (isInteger, Math.Abs(number), Math.Abs(number) % 100, Math.Abs(number) % 10) switch
            {
                (true, 1, _, _) => GrammaticalNumber.Singular,
                (true, _, >= 10 and <= 19, _) => GrammaticalNumber.Plural,
                (true, _, _, >= 2 and <= 4) => GrammaticalNumber.Paucal,
                (true, _, _, _) => GrammaticalNumber.Plural,
                (false, _, _, _) => GrammaticalNumber.Fractional
            };

            if (!GrammaticalNumbers.Contains(result))
                throw new ApplicationException("Unexpected GrammaticalNumber value");

            return result;
        }
    }
}
