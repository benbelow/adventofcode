using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

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
        
        // 8 : abcdefg (7)

        // 1 : cf (2)
        // 7 : acf (3)
        // 4 : bcdf (4)

        // 6 : abdefg (6)
        // 5 : abdfg (5)
        // 9 : abcdfg (6)
        // 0 : abcefg (6) 
        // 3 : acdfg (5)

        // 2 : acdeg (5)

        private static int GetOutputsForLine(string line)
        {
            var splitLine = line.Split("|");
            var watchedDigits = splitLine.First()
                .Split(" ")
                .Select(x => x.Trim().ToHashSet())
                .ToList();
            var finalDigits = splitLine.Last().Split(" ")
                .Where(x => x.Trim() != "")
                .Select(x => x.Trim().ToHashSet()).ToList();

            var digitMap = new Dictionary<string, int>();

            var one = watchedDigits.Single(d => d.Count == 2);
            var four = watchedDigits.Single(d => d.Count == 4);
            var seven = watchedDigits.Single(d => d.Count == 3);
            var eight = watchedDigits.Single(d => d.Count == 7);

            var six = watchedDigits.Single(x => x.Count == 6 && !x.IsSupersetOf(seven));
            var five = watchedDigits.Single(x => x.Count == 5 && x.IsSubsetOf(six));
            var nine = watchedDigits.Single(x => x.Count == 6 && x.IsSupersetOf(four));

            var zero = watchedDigits.Single(x => x.Count == 6 && !x.SetEquals(nine) && !x.SetEquals(six));
            var three = watchedDigits.Single(x => x.Count == 5 && x.IsSupersetOf(seven));
            var two = watchedDigits.Single(x => x.Count == 5 && !x.SetEquals(three)&& !x.SetEquals(five));

            digitMap[zero.ToList().OrderBy(x => x).CharsToString()] = 0;
            digitMap[one.ToList().OrderBy(x => x).CharsToString()] = 1;
            digitMap[two.ToList().OrderBy(x => x).CharsToString()] = 2;
            digitMap[three.ToList().OrderBy(x => x).CharsToString()] = 3;
            digitMap[four.ToList().OrderBy(x => x).CharsToString()] = 4;
            digitMap[five.ToList().OrderBy(x => x).CharsToString()] = 5;
            digitMap[six.ToList().OrderBy(x => x).CharsToString()] = 6;
            digitMap[seven.ToList().OrderBy(x => x).CharsToString()] = 7;
            digitMap[eight.ToList().OrderBy(x => x).CharsToString()] = 8;
            digitMap[nine.ToList().OrderBy(x => x).CharsToString()] = 9;

            var outputs = finalDigits.Select(f => digitMap[f.ToList().OrderBy(f => f).CharsToString()]);
            return int.Parse(outputs.Select(o => o.ToString().Single()).CharsToString());
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var outputs = lines.Select(GetOutputsForLine);
            return outputs.Sum();
        }
    }
}