using System;
using System.Collections.Generic;

public class FirstDuplicateFinder
{
    /// <summary>
    /// Finds the first duplicated character in a given string.
    /// </summary>
    /// <param name="input">The string to be checked</param>
    /// <returns>The first duplicated character, or null if no duplicates exist</returns>
    public static char? FindFirstDuplicate(string input)
    {
        // Handle boundary conditions: null or empty strings
        if (string.IsNullOrEmpty(input))
        {
            return null;
        }

        // Create a Set to keep track of the characters we have already seen
        var seenCharacters = new HashSet<char>();

        // Iterate through each character in the string
        foreach (char c in input)
        {
            // If the character is already in the set, we found our first duplicate!
            if (seenCharacters.Contains(c))
            {
                return c;
            }
            
            // Otherwise, add the character to the set and continue
            seenCharacters.Add(c);
        }

        // If the loop finishes and we found no duplicates, return null
        return null;
    }

    // A simple method to test the scenarios mentioned in the lesson
    public static void Run()
    {
        Console.WriteLine(FindFirstDuplicate("abcddef"));  // Output: d
        Console.WriteLine(FindFirstDuplicate("abcddeef")); // Output: d
        Console.WriteLine(FindFirstDuplicate("abcdefde")); // Output: d
        Console.WriteLine(FindFirstDuplicate("abc"));      // Output: (nothing / null)
    }
}