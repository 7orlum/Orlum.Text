using System.Runtime.CompilerServices;


namespace orlum.TypographyHelper
{
    /// <summary>
    /// In linguistics, grammatical number is a grammatical category of nouns, pronouns, and adjective and verb agreement that expresses count distinctions (such as "one", "two", or "three or more").
    /// English and other languages present number categories of singular or plural, both of which are cited by using the hash sign (#) or by the numero signs "No." and "Nos." respectively. 
    /// Some languages also have a dual, trial, and paucal number or other arrangements.
    /// </summary>
    public enum GrammaticalNumber
    {
        /// <summary>The singular, for one instance of a concept.</summary>
        Singular,
        /// <summary>The distinction between a "singular" number (one) and a "plural" number (more than one) found in English is not the only possible classification. 
        /// Another one is "singular" (one), "dual" (two) and "plural" (more than two). 
        /// Dual number existed in Proto-Indo-European, persisted in many ancient Indo-European languages that descended from it—
        /// Sanskrit, Ancient Greek, Gothic, Old Norse, and Old English for example—and can still be found in a few modern Indo-European languages such as Slovene.</summary>
        Dual,
        /// <summary>
        /// The trial number is a grammatical number referring to 'three items', in contrast to 'singular' (one item), 'dual' (two items), and 'plural' (four or more items). 
        /// Several Austronesian languages such as Tolomako, Lihir, and Manam; the Kiwaian languages; 
        /// and the Austronesian-influenced creole languages Bislama and Tok Pisin have the trial number in their pronouns. 
        /// </summary>
        Trial,
        /// <summary>
        /// Paucal number, for a few (as opposed to many) instances of the referent (e.g. in Hopi, Warlpiri, some Oceanic languages including Fijian, Motuna, Serbo-Croatian, and in Arabic for some nouns). 
        /// Paucal number has also been documented in some Cushitic languages of Ethiopia, including Baiso, which marks singular, paucal, plural. 
        /// When paucal number is used in Arabic, it generally refers to ten or fewer instances.
        /// </summary>
        Paucal,
        /// <summary>
        /// Distributive plural number, for many instances viewed as independent individuals (for example, in Navajo).
        /// </summary>
        DistributivePlural,
        /// <summary>
        /// The Austronesian languages of Sursurunga and Lihir have extremely complex grammatical number systems, with singular, dual, paucal, greater paucal, and plural.
        /// </summary>
        GreaterPaucal,
        /// <summary>The plural, for more than one instance in most languages. In many languages, there is also a dual number (used for indicating two objects). 
        /// Some other grammatical numbers present in various languages include trial (for three objects) and paucal (for an imprecise but small number of objects). 
        /// In languages with dual, trial, or paucal numbers, plural refers to numbers higher than those.</summary>
        Plural,
        /// <summary>
        /// Some languages (like Mele-Fila) distinguish between a plural and a greater plural. A greater plural refers to an abnormally large number for the object of discussion. 
        /// It should also be noted that the distinction between the paucal, the plural, and the greater plural is often relative to the type of object under discussion. 
        /// For example, in discussing oranges, the paucal number might imply fewer than ten, whereas for the population of a country, it might be used for a few hundred thousand.
        /// </summary>
        GreaterPlural,
        /// <summary>
        /// Specific agreement with fractional numbers in Polish for example
        /// </summary>
        Fractional
    }
}
