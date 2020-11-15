using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;


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
            Contract.Ensures(GrammaticalNumbers.Contains(Contract.Result<GrammaticalNumber>()));

            if (Math.Truncate(number) == number)
            {
                //Integer numbers
                if (Math.Abs(number) == 1)
                    return GrammaticalNumber.Singular;
                else
                    return GrammaticalNumber.Plural;
            }
            else
            {
                //Fractional numbers
                if (Math.Truncate(number) == 0)
                    return GrammaticalNumber.Singular;
                else
                    return GrammaticalNumber.Plural;
            }
        }
    }
}
