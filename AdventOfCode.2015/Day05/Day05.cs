using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2015.Day05
{
    public static class Day05
    {
        private const int Day = 05;
        
        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();

            return lines.Count(IsNice);
        }

        public static HashSet<char> vowels = new HashSet<char> {'a', 'e', 'i', 'o', 'u'};
        public static HashSet<string> forbidden = new HashSet<string> {"ab", "cd", "pq", "xy"};
        
        public static bool IsNice(string s)
        {
            if (s.Count(c => vowels.Contains(c)) < 3)
            {
                return false;
            }

            var isDouble = false;
            for (int i = 0; i < s.Length -1; i++)
            {
                var current = s[i];
                var next = s[i+1];
                if (current == next)
                {
                    isDouble = true;
                    break;
                }
            }

            if (!isDouble)
            {
                return false;
            }

            return !forbidden.Any(s.Contains);
        }

        public static bool IsNice2(string s)
        {
            var isPairTwice = false;
            
            for (int i = 0; i < s.Length - 2; i++)
            {
                var pair = "" + s[i] + s[i + 1];
                if (s.Skip(i + 2).Aggregate("", (x, y) => x + y).Contains(pair))
                {
                    isPairTwice = true;
                    break;
                }
            }

            if (!isPairTwice)
            {
                return false;
            }

            var isSandwich = false;

            for (int i = 1; i < s.Length-1; i++)
            {
                var prev = s[i - 1];
                var next = s[i + 1];
                if (next == prev)
                {
                    isSandwich = true;
                    break;
                }
            }
            
            return isSandwich;
        }
        
        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();

            return lines.Count(IsNice2);
        }
    }
}