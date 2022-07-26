# What is Orlum.Text?

The library helping internationalize your program. It generate correctly matched phrases in accordance with language agreement.
English, Polish and Russian language number agreements are now implemented. You can add new languages by implementing `INumberAgreement` interface.

# How can it be used?

Call `Format.NumericalPhrase` static function to format string depending on number and CurrentCulture 
```cs
Format.NumericalPhrase($"{number:NP;RU;Запрошен;Запрошено;Запрошено} {number} {number:NP;RU;рубль;рубля;рублей}");
```   
Depending on `number` value it will return `"Запрошено 3.5 рубля"`, `"Запрошен 1 рубль"` or `"Запрошено 0 рублей"`.  
  
If you replace the format string to it english version  
```cs
"Requested {number} {number:NP;EN;dollar;dollars}"
```  
function will return `"Requested 3.5 dollars"`, `"Requested 1 dollar"` and `"Requested 0 dollars"` for the same values of `{0}` argument.  
  
If you replace the format string to it polish version  
```cs
"Zażądano {number} {number:NP;PL;złoty;złote;złotych;złoty}"
```  
function will return `"Zażądano 3.5 złotych"`, `"Zażądano 1 złoty"` and `"Zażądano 0 złoty"` for the same values of `{0}` argument.  
  
```cs
using Orlum.Text;
    
public class FormatterExample
{
   public static void Main()
   {
       var value1 = 3.5;
       var value2 = 1;
       var value3 = 0;
       
       //Using interpolated strings and Orlum.Text.Format.NP function
       Console.WriteLine(Format.NumericalPhrase($"{value1:NP;RU;Запрошен;Запрошено;Запрошено} {value1} {value1:NP;RU;рубль;рубля;рублей}"));
       Console.WriteLine(Format.NumericalPhrase($"{value2:NP;RU;Запрошен;Запрошено;Запрошено} {value2} {value2:NP;RU;рубль;рубля;рублей}"));
       Console.WriteLine(Format.NumericalPhrase($"{value3:NP;RU;Запрошен;Запрошено;Запрошено} {value3} {value3:NP;RU;рубль;рубля;рублей}"));
       
       //Using regular format strings and Orlum.Text.Format.NumericalPhrase function
       Console.WriteLine(Format.NumericalPhrase("{0:NP;RU;Запрошен;Запрошено;Запрошено} {0} {0:NP;RU;рубль;рубля;рублей}", value1));
       Console.WriteLine(Format.NumericalPhrase("{0:NP;RU;Запрошен;Запрошено;Запрошено} {0} {0:NP;RU;рубль;рубля;рублей}", value2));
       Console.WriteLine(Format.NumericalPhrase("{0:NP;RU;Запрошен;Запрошено;Запрошено} {0} {0:NP;RU;рубль;рубля;рублей}", value3));
       Console.WriteLine();
       
       //Using regular format strings and System.String.Format function
       Console.WriteLine(string.Format(new NumericalPhraseFormatter(), "{0:NP;RU;Запрошен;Запрошено;Запрошено} {0} {0:NP;RU;рубль;рубля;рублей}", value1));
       Console.WriteLine(string.Format(new NumericalPhraseFormatter(), "{0:NP;RU;Запрошен;Запрошено;Запрошено} {0} {0:NP;RU;рубль;рубля;рублей}", value2));
       Console.WriteLine(string.Format(new NumericalPhraseFormatter(), "{0:NP;RU;Запрошен;Запрошено;Запрошено} {0} {0:NP;RU;рубль;рубля;рублей}", value3));
       Console.WriteLine();
   }
}

/*
This code produces the following output:

Запрошено 3,5 рубля
Запрошен 1 рубль
Запрошено 0 рублей
       
Запрошено 3,5 рубля
Запрошен 1 рубль
Запрошено 0 рублей
       
Запрошено 3,5 рубля
Запрошен 1 рубль
Запрошено 0 рублей

*/
```

Note that different languages use different numbers of gramatical number forms and inflection rules depends of language.  
* For English you should specify singular and plural forms of the inflected phrase in that exact order, for example `{0:NP;en;cow;cows}`.  
* For Polish you should specify 4 forms of a phrase inflected for number and splited by semicolon. Specify inflections of the phrase required to be compatible with numbers 1, 2, 5 and ½ in that exact order, for example `{0:NP;pl;litr;litry;litrów;litra}`.  
* For Russian you should specify 3 forms of the phrase inflected for number and splited by semicolon. Specify inflections of the phrase required to be compatible with numbers 1, 2 and 5 in that exact order, for example `{0:NP;ru;рубль;рубля;рублей}`.  

You can continue to use predefined set of format strings with `NumericalPhraseFormatProvider`, 
`NumericalPhraseFormatProvider` just adds new format string `NP` that allows to select compatible to the given number form of phrase.  
`NumericalPhraseFormatProvider` works independently of current culture, you specify language directly in the format string after `NP` tag: `{0:NP;EN;ruble;rubles}`, 
but predefined set of format strings is culture-sensitive and you should specify `cultureInfo` in `NumericalPhraseFormatProvider` constructor. When `cultireInfo` is not specified it obtain the current locale setting of the operating system.

```cs
using Orlum.Text;
    
public class NumericalPhraseFormatProviderExample
{
	public static void Main()
	{
		Console.WriteLine(string.Format(new NumericalPhraseFormatProvider(CultureInfo.GetCultureInfo("en-US")), "Requested {0} {0:NP;EN;ruble;rubles}", 3.5));
		Console.WriteLine(string.Format(new NumericalPhraseFormatProvider(CultureInfo.GetCultureInfo("ru-RU")), "Requested {0} {0:NP;EN;ruble;rubles}", 3.5));
	}
}
    
/*
This code produces the following output.
     
Requested 3.5 rubles
Requested 3,5 rubles
     
*/
```