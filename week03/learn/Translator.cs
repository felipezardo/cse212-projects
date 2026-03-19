using System;
using System.Collections.Generic;

public class Translator
{
    public static void Run()
    {
        var englishToGerman = new Translator();
        englishToGerman.AddWord("House", "Haus");
        englishToGerman.AddWord("Car", "Auto");
        englishToGerman.AddWord("Plane", "Flugzeug");
        
        Console.WriteLine(englishToGerman.Translate("Car")); // Auto
        Console.WriteLine(englishToGerman.Translate("Plane")); // Flugzeug
        Console.WriteLine(englishToGerman.Translate("Train")); // ???
    }

    private Dictionary<string, string> _words = new();

    /// <summary>
    /// Add the translation from 'fromWord' to 'toWord'
    /// For example, in a english to german dictionary:
    /// 
    /// myTranslator.AddWord("book","buch");
    /// </summary>
    /// <param name="fromWord">The word to translate from</param>
    /// <param name="toWord">The word to translate to</param>
    public void AddWord(string fromWord, string toWord)
    {
        // Associates the translated word (value) with the original word (key)
        _words[fromWord] = toWord;
    }

    /// <summary>
    /// Translates the fromWord into the word that this stores as the translation
    /// </summary>
    /// <param name="fromWord">The word to translate</param>
    /// <returns>The translated word or "???" if no translation is available</returns>
    public string Translate(string fromWord)
    {
        // Checks if the dictionary contains the word before trying to access it
        if (_words.ContainsKey(fromWord))
        {
            return _words[fromWord];
        }

        // Returns "???" if the translation is not found
        return "???";
    }
}