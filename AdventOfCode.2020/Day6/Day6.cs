using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2020.Day6
{
    public static class Day6
    {
        private const int Day = 6;
        
        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var groups = lines.Split("");

            var counts = groups.Select(g =>
            {
                var dict = new Dictionary<char, int>();
                foreach (var s in g)
                {
                    foreach (var c in s)
                    {
                        if (dict.ContainsKey(c))
                        {
                            dict[c]++;
                        }
                        else
                        {
                            dict[c] = 1;
                        }
                    }
                }

                return dict;
            }).ToList();

            return counts.Select(dict => dict.Count()).Sum();
        }

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var groups = lines.Split("").ToList();

            var counts = groups.Select(g =>
            {
                var dict = new Dictionary<char, int>();
                foreach (var s in g)
                {
                    foreach (var c in s)
                    {
                        if (dict.ContainsKey(c))
                        {
                            dict[c]++;
                        }
                        else
                        {
                            dict[c] = 1;
                        }
                    }
                }

                return dict;
            }).ToList();

            var count = 0;
            
            for (int i = 0; i < counts.Count; i++)
            {
                var dict = counts[i];
                var inputs = groups[i];
                count += dict.Count(x => x.Value == inputs.Count());
            }

            return count;
        }
    }
}