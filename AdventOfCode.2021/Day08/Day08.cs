using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2021.Day08
{
    public static class Day08
    {
        private const int Day = 08;
        
        // 1478
        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();

            return lines.Sum(l =>
            {
                var splitLine = l.Split("|");
                var finalDigits = splitLine.Last().Split(" ").Select(x => x.Trim().ToHashSet());
                return finalDigits.Count(d => d .Count is 2 or 4 or 3 or 7);
            });
        }

        // 0 : abcefg (6) 
        // 1 : cf (2)
        // 2 : acdeg (5)
        // 3 : acdfg (6)
        // 4 : bcdf (4)
        // 5 : abdfg (5)
        // 6 : abdefg (6)
        // 7 : acf (3)
        // 8 : abcdefg (7)
        // 9 : abcdfg (6)
        
        private static IEnumerable<int> GetOutputsForLine(string line)
        {
            var splitLine = line.Split("|");
            var watchedDigits = splitLine.First()
                .Split(" ")
                .Select(x => x.Trim().ToHashSet())
                .ToList();
            var finalDigits = splitLine.Last().Split(" ").Select(x => x.Trim().ToHashSet()).ToList();

            var charMap = new Dictionary<char, char>();
            var digitMap = new Dictionary<HashSet<char>, int>();

            // some code

            var one = watchedDigits.SingleOrDefault(d => d.Count == 2);
            var seven = watchedDigits.SingleOrDefault(d => d.Count == 3);

            return finalDigits.Select(d => digitMap[d]);
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}