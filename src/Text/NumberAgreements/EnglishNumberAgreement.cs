using System.Collections.Immutable;


namespace Orlum.Text
{
    /// <summary>
    /// Implementation of <see cref="INumberAgreement"/> for contemporary English language
    /// <remarks>
    /// English is typical of most world languages, in distinguishing only between singular and plural number. 
    /// The plural form of a noun is usually created by adding the suffix -(e)s. 
    /// The pronouns have irregular plurals, as in "I" versus "we", because they are ancient and frequently used words going back to when English had a well developed system of declension. 
    /// English verbs distinguish singular from plural number in the third person present tense ("He goes" versus "They go"). 
    /// English treats zero with the plural number. Old English did contain dual grammatical numbers.
    /// </remarks>
    /// </summary>
    public sealed class EnglishNumberAgreement : INumberAgreement
    {
        /// <summary>
        /// Enumerates all distinguishing grammatical number values in the English language.
        /// </summary>
        public IList<GrammaticalNumber> GrammaticalNumbers => ImmutableArray.Create<GrammaticalNumber>(
            GrammaticalNumber.Singular, GrammaticalNumber.Plural);


        /// <summary>
        /// Describes how to get correct format string.
        /// </summary>
        public string DescriptionOfFormatString => "Expected singular and plural forms of the inflected phrase in that exact order, for example {0:NP;EN;cow;cows}";


        /// <summary>
        /// Finds the concord grammatical number value to agree numerical phrase with specified number.
        /// </summary>
        public GrammaticalNumber MatchGrammaticalNumber(double number)
        {
            var isInteger = Math.Truncate(number) == number;
            var modulusIsOne = Math.Abs(number) == 1;
            var modulusIsLessThanOne = Math.Truncate(number) == 0;

            var result = (isInteger, modulusIsOne, modulusIsLessThanOne) switch
            {
                (true, true, _) => GrammaticalNumber.Singular,
                (true, _, _) => GrammaticalNumber.Plural,
                (false, _, true) => GrammaticalNumber.Singular,
                (false, _, _) => GrammaticalNumber.Plural
            };

            if (!GrammaticalNumbers.Contains(result))
                throw new ApplicationException("Unexpected GrammaticalNumber value");

            return result;
        }
    }
}
