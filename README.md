# What is Orlum.Text?

`NumericalPhraseFormatProvider` class is the main part of Orlum.Text. 
You can use it to internationalize programs or generate the correct matched phrases in accordance with language agreement.
`NumericalPhraseFormatProvider` is a `ICustomFormatter` that choose the correct form of the phrase depending on the given numeric argument. 
English, Polish and Russian language number agreements are now implemented. You can add new languages by implementing `INumberAgreement` interface.

# How can it be used?

Call `String.Format` function with `NumericalPhraseFormatProvider` custom formatter to format string  
```cs
"{0:NP;RU;Запрошен;Запрошено;Запрошено} {0} {0:NP;RU;рубль;рубля;рублей}"
```   
Depending on `{0}` argument it will return `"Запрошено 3.5 рубля"`, `"Запрошен 1 рубль"` or `"Запрошено 0 рублей"`.  
  
If you replace the format string to it english version  
```cs
"Requested {0} {0:NP;EN;ruble;rubles}"
```  
function will return `"Requested 3.5 rubles"`, `"Requested 1 ruble"` and `"Requested 0 rubles"` for the same values of `{0}` argument.  
  
If you replace the format string to it polish version  
```cs
"Zażądano {0} {0:NP;PL;rubel;ruble;rubli;rubla}"
```  
function will return `"Zażądano 3.5 rubla"`, `"Zażądano 1 rubel"` and `"Zażądano 0 rubli"` for the same values of `{0}` argument.  
  
```cs
using Orlum.Text;
    
public class NumericalPhraseFormatProviderExample
{
	public static void Main()
	{
		var f = new NumericalPhraseFormatProvider();

		Console.WriteLine(string.Format(f, "{0:NP;RU;Запрошен;Запрошено;Запрошено} {0} {0:NP;RU;рубль;рубля;рублей}", 3.5));
		Console.WriteLine(string.Format(f, "{0:NP;RU;Запрошен;Запрошено;Запрошено} {0} {0:NP;RU;рубль;рубля;рублей}", 1));
		Console.WriteLine(string.Format(f, "{0:NP;RU;Запрошен;Запрошено;Запрошено} {0} {0:NP;RU;рубль;рубля;рублей}", 0));
		Console.WriteLine(string.Format(f, "Requested {0} {0:NP;EN;ruble;rubles}", 3.5));
		Console.WriteLine(string.Format(f, "Requested {0} {0:NP;EN;ruble;rubles}", 1));
		Console.WriteLine(string.Format(f, "Requested {0} {0:NP;EN;ruble;rubles}", 0));
		Console.WriteLine(string.Format(f, "Zażądano {0} {0:NP;PL;rubel;ruble;rubli;rubla}", 3.5));
		Console.WriteLine(string.Format(f, "Zażądano {0} {0:NP;PL;rubel;ruble;rubli;rubla}", 1));
		Console.WriteLine(string.Format(f, "Zażądano {0} {0:NP;PL;rubel;ruble;rubli;rubla}", 0));
	}
}
    
/*
This code produces the following output.
     
Запрошено 3.5 рубля
Запрошен 1 рубль
Запрошено 0 рублей
Requested 3.5 rubles
Requested 1 ruble
Requested 0 rubles
Zażądano 3.5 rubla
Zażądano 1 rubel
Zażądano 0 rubli
     
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