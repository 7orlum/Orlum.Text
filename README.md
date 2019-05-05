# What is TypographyHelper?

NumericalPhraseFormatter class is the main part of TypographyHelper. 
You can use it to internationalize programs or generate the correct matched phrases in accordance with language agreement.
NumericalPhraseFormatter is a ICustomFormatter that choose the correct form of the phrase depending on the given numeric argument. 
English, Polish and Russian language number agreements are now implemented.

# How can it be used?

Call `String.Format` function with NumericalPhraseFormatter custom formatter to format string  
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
  
Note that different languages use different numbers of gramatical number forms: 3 in Russian, 2 in English, 4 in Polish, and inflection rules depends of language.

```cs
using orlum.TypographyHelper;
    
public class NumericalPhraseFormatterExample
{
	public static void Main()
	{
		var f = new NumericalPhraseFormatter();

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

You can continue to use predefined set of format strings with NumericalPhraseFormatter. 
NumericalPhraseFormatter just adds new Format String `NP` that allows to select compatible to the given number form of phrase.  
NumericalPhraseFormatter works independently of current culture, you specify language directly in the format string after `NP` tag: `{0:NP;EN;ruble;rubles}` 
but predefined set of format strings is culture-sensitive and you should specify cultureInfo in NumericalPhraseFormatter constructor `var f = new NumericalPhraseFormatter(CultureInfo.InvariantCulture);`.