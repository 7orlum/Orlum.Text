using System.Collections.Generic;


namespace Orlum.TypographyHelper
{
    /// <summary>
    /// Interface to supply <see cref="NumericalPhraseFormatter"/> language-specific number agreement information.
    /// </summary>
    public interface INumberAgreement
    {
        /// <summary>
        /// Enumerates all distinguishing grammatical number values in the language.
        /// </summary>
        IList<GrammaticalNumber> GrammaticalNumbers { get; }


        /// <summary>
        /// Finds the concord grammatical number value to agree numerical phrase with specified number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        GrammaticalNumber MatchGrammaticalNumber(double number);


        /// <summary>
        /// Describes how to get correct format string.
        /// </summary>
        string DescriptionOfFormatString { get; }
    }
}
