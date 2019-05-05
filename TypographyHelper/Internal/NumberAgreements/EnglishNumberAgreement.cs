using System;
using System.Linq;
using System.Diagnostics.Contracts;


namespace orlum.TypographyHelper
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
    internal class EnglishNumberAgreement : INumberAgreement
    {
        /// <summary>
        /// Enumerate two available gramatical number forms in English language 
        /// </summary>
        public GramaticalNumber[] AvailableForms => new GramaticalNumber[] { GramaticalNumber.Singular, GramaticalNumber.Plural };


        /// <summary>
        /// Finds the concord gramatical number to agree numerical phrase with specified number
        /// </summary>
        public GramaticalNumber ConcordForm(double number)
        {
            Contract.Ensures(AvailableForms.Contains(Contract.Result<GramaticalNumber>()));

            if (Math.Truncate(number) == number)
            {
                //Integer numbers
                if (Math.Abs(number) == 1)
                    return GramaticalNumber.Singular;
                else
                    return GramaticalNumber.Plural;
            }
            else
            {
                //Fractional numbers
                if (Math.Truncate(number) == 0)
                    return GramaticalNumber.Singular;
                else
                    return GramaticalNumber.Plural;
            }
        }


        /// <summary>
        /// Describes how to write correct format string for this language
        /// </summary>
        public string DescriptionOfFormatString => "Expected singular and plural forms of the inflected phrase in that exact order, for example {0:NP;en;cow;cows}";
    }
}
