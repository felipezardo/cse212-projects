using System.Collections;
using System.Collections.Generic;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it.  Remember to both express the solution 
    /// in terms of recursive call on a smaller problem and 
    /// to identify a base case (terminating case).  If the value of
    /// n <= 0, just return 0.   A loop should not be used.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // Base case: if n is 0 or less, return 0
        if (n <= 0)
            return 0;

        // Recursive step: n^2 + sum of squares for (n-1)
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.  This function
    /// should assume that each letter is unique (i.e. the 
    /// function does not need to find unique permutations).
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // Base case: if the word length matches the target size, add it to results
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // Recursive step: try adding each available letter
        foreach (char letter in letters)
        {
            // Only add the letter if it's not already in the current word
            if (!word.Contains(letter))
            {
                PermutationsChoose(results, letters, size, word + letter);
            }
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// To run this function for larger values of 's', you will need
    /// to update this function to use memoization.  The parameter
    /// 'remember' has already been added as an input parameter to 
    /// the function for you to complete this task.
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Initialize the memoization dictionary on the first call
        if (remember == null)
            remember = new Dictionary<int, decimal>();

        // Base Cases
        if (s == 0)
            return 0;
        if (s == 1)
            return 1;
        if (s == 2)
            return 2;
        if (s == 3)
            return 4;

        // Check if we have already calculated the result for 's'
        if (remember.ContainsKey(s))
            return remember[s];

        // Solve using recursion and pass the dictionary
        decimal ways = CountWaysToClimb(s - 1, remember) + 
                       CountWaysToClimb(s - 2, remember) + 
                       CountWaysToClimb(s - 3, remember);
        
        // Save the calculated result in the dictionary before returning
        remember[s] = ways;
        
        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// Using recursion, insert all possible binary strings for a given pattern into the results list.  You might find 
    /// some of the string functions like IndexOf and [..X] / [X..] to be useful in solving this problem.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // Find the index of the first wildcard '*'
        int index = pattern.IndexOf('*');

        // Base case: if there are no wildcards, add the completed pattern to results
        if (index == -1)
        {
            results.Add(pattern);
            return;
        }

        // Replace the first wildcard with '0' and recurse
        string pattern0 = pattern[..index] + "0" + pattern[(index + 1)..];
        WildcardBinary(pattern0, results);

        // Replace the first wildcard with '1' and recurse
        string pattern1 = pattern[..index] + "1" + pattern[(index + 1)..];
        WildcardBinary(pattern1, results);
    }

    /// <summary>
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // If this is the first time running the function, then we need
        // to initialize the currPath list.
        if (currPath == null) {
            currPath = new List<ValueTuple<int, int>>();
        }
        
        // Add current position to the path
        currPath.Add((x, y));

        // If we reached the end, convert the path to a string and add to results
        if (maze.IsEnd(x, y))
        {
            // Assumes an AsString() extension method or similar is available in your full project structure 
            // as mentioned in the instructions.
            results.Add(currPath.AsString());
        }
        else
        {
            // Define the 4 possible movements: Right, Down, Left, Up
            (int, int)[] directions = { (1, 0), (0, 1), (-1, 0), (0, -1) };

            foreach (var dir in directions)
            {
                int nextX = x + dir.Item1;
                int nextY = y + dir.Item2;

                // Check if the next move is valid before recursing
                if (maze.IsValidMove(currPath, nextX, nextY))
                {
                    SolveMaze(results, maze, nextX, nextY, currPath);
                }
            }
        }

        // Backtrack: Remove the current position to explore other potential paths from previous steps
        currPath.RemoveAt(currPath.Count - 1);
    }
}