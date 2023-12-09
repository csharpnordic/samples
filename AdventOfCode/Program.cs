using System.Net;
using System.Text.RegularExpressions;

namespace AdventOfCode;

/// <summary>
/// https://adventofcode.com/2023
/// </summary>
internal static class Program
{
    private static Dictionary<string, int> Digits = new()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 }        
    };

    private static int Parse(string s)
    {
        if (int.TryParse(s, out int digit))
            return digit;
        return Digits[s];
    }

    private static int Puzzle1()
    {
        string[] lines = System.IO.File.ReadAllLines("day1.txt");
        int summa = 0;
        foreach (string line in lines)
        {
            var match = Regex.Match(line, @"^\D*?(\d|one|two|three|four|five|six|seven|eight|nine).*(\d|one|two|three|four|five|six|seven|eight|nine)\D*?$");
            if (!match.Success)
            {
                match = Regex.Match(line, @"^\D*?(\d|one|two|three|four|five|six|seven|eight|nine)\D*?$");
                if (!match.Success)
                {
                    /// Console.WriteLine(line);
                    continue;
                }
                int digit = Parse(match.Groups[1].Value);
                int number1 = 10 * digit + digit;
                summa += number1;
                /// Console.WriteLine($"{line} = {number1}");
                continue;
            }
            int digit1 = Parse(match.Groups[1].Value);
            int digit2 = Parse(match.Groups[2].Value);
            int number2 = 10 * digit1 + digit2;
            /// Console.WriteLine($"{line} = {number2}");
            summa += number2;
        }
        return summa;
    }

    static void Main(string[] args)
    {
        Console.WriteLine($"Day 1 = {Puzzle1()}");

    }
}
