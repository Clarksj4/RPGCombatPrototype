using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System;

public class TextCommandParser
{
    // TODO: self targets self cell
    // TODO: maybe automatically target cell if there's only EVER one target cell.

    private static Dictionary<string, int> numberTable = new Dictionary<string, int>
    {
        {"zero",0},{"one",1},{"two",2},{"three",3},{"four",4},
        {"five",5},{"six",6},{"seven",7},{"eight",8},{"nine",9},
        {"ten",10},{"eleven",11},{"twelve",12},{"thirteen",13},
        {"fourteen",14},{"fifteen",15},{"sixteen",16},
        {"seventeen",17},{"eighteen",18},{"nineteen",19},{"twenty",20},
        {"thirty",30},{"forty",40},{"fifty",50},{"sixty",60},
        {"seventy",70},{"eighty",80},{"ninety",90},{"hundred",100},
        {"thousand",1000},{"million",1000000},{"billion",1000000000}
    };

    private static string[] cancelKeywords = new string[] { "cancel", "back", "no", "n", "undo" };
    private static string[] endTurnKeywords = new string[] { "end turn", "end" };
    private static string[] confirmKeywords = new string[] { "confirm", "yes", "y", "go" };
    private static char[] splitCharacters = new char[] { ' ', ',', '-', '.', '\n', '_' };

    public Command Parse(string text)
    {
        string[] words = Split(text);

        // Always check for cancel command first
        if (words.Any(w => CollectionContainsWord(w, cancelKeywords)))
            return new CancelCommand();

        // Check for confirmation to use current 
        // ability at current target
        if (ActionManager.Instance.HasTarget)
        {
            if (words.Any(w => CollectionContainsWord(w, confirmKeywords)))
                return new ConfirmCommand();
        }

        // Check for target selection
        else if (ActionManager.Instance.HasAction)
        {
            // Extract x and y axis' from text.
            List<int> numbers = ToInts(words);

            // Convert numbers to coordinate and target
            // the associated position.
            if (numbers != null &&
                numbers.Count >= 2)
            {
                Vector2Int coordinate = new Vector2Int(numbers[0], numbers[1]);
                return new TargetCommand() { Coordinate = coordinate };
            }
        }
        
        else
        {
            // Check for end turn.
            if (words.Any(w => CollectionContainsWord(w, endTurnKeywords)))
                return new EndTurnCommand();

            else
            {
                // Check for a number to index the corresponding ability
                List<int> numbers = ToInts(words);
                if (numbers != null && 
                    numbers.Count >= 1)
                    return new AbilityCommand() { AbilityIndex = numbers[0] };

                // Failing that, check for an ability name
                int i = 0;
                foreach (string actionName in ActionManager.Instance.SelectedActor.Actions)
                {
                    if (CollectionContainsWord(actionName, words))
                        return new AbilityCommand() { AbilityName = actionName };
                }
            }
        }

        return null;
    }

    private static bool CollectionContainsWord(string word, IEnumerable<string> words)
    {
        return words.Any(w => TextMatches(word, w));
    }

    private static bool TextMatches(string text1, string text2)
    {
        return text1.ToLowerInvariant() == text2.ToLowerInvariant();
    }

    private static string[] Split(string text)
    {
        return text.Split(splitCharacters);
    }

    private static List<int> ToInts(string[] numbers)
    {
        List<int> ints = new List<int>(numbers.Length);
        for (int i = 0; i < numbers.Length; i++)
        {
            int? number = ToInt(numbers[i]);
            if (number != null)
                ints.Add((int)number);
        }

        // Return list of numbers or null
        return ints.Count > 0 ? ints : null;
    }

    private static int? ToInt(string numberString)
    {
        if (int.TryParse(numberString, out int number))
            return number;

        var numbers = Regex.Matches(numberString, @"\w+").Cast<Match>()
             .Select(m => m.Value.ToLowerInvariant())
             .Where(v => numberTable.ContainsKey(v))
             .Select(v => numberTable[v]);

        if (numbers == null || numbers.Count() == 0)
            return null;

        int acc = 0;
        int total = 0;

        foreach (var n in numbers)
        {
            if (n >= 1000)
            {
                total += (acc * n);
                acc = 0;
            }

            else if (n >= 100)
                acc *= n;

            else acc += n;
        }

        return (total + acc) * (numberString.StartsWith("minus", StringComparison.InvariantCultureIgnoreCase) ? -1 : 1);
    }

}
