using System;
using System.Collections.Generic;
using System.Text;

namespace orlum.TypographyHelper
{
    /// <summary>
    /// Interface to supply <see cref="NumericalPhraseFormatter"/> language-specific number agreement information
    /// </summary>
    public interface INumberAgreement
    {
        /// <summary>
        /// Enumerate all the available gramatical number forms in a specific language 
        /// </summary>
        GramaticalNumber[] AvailableForms { get; }


        /// <summary>
        /// Finds the concord gramatical number to agree numerical phrase with specified number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        GramaticalNumber ConcordForm(double number);


        /// <summary>
        /// Describes how to write correct format string for this language
        /// </summary>
        string DescriptionOfFormatString { get; }
    }
}
