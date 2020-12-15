using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2020.Day15
{
    public static class Day15
    {
        private const int Day = 15;

        public static long Part1(string line = null)
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            line ??= lines.Single();
            var seed = line.Split(",").Select(long.Parse).ToList();
            return XThNumber(seed, 2020);
        }

        public static long XThNumber(IList<long> seed, int x)
        {
            var enumerable = SpeakNumbers(seed);
            return enumerable.Skip(x - 1).Take(1).Single();
        }

        public static IEnumerable<long> SpeakNumbers(IList<long> seed)
        {
            var history = new Dictionary<long, IList<long>> {{0, new List<long>()}};
            var index = 1L;
            long lastSpoken = -1;

            foreach (var original in seed)
            {
                history[original] = new List<long> {index};
                index++;
                lastSpoken = original;
                yield return lastSpoken;
            }

            while (true)
            {
                var historyOfLastSpoken = history[lastSpoken];
                long toSpeak;
                if (historyOfLastSpoken.Count == 1)
                {
                    toSpeak = 0;
                }
                else
                {
                    var diff = historyOfLastSpoken[^1] - historyOfLastSpoken[^2];
                    toSpeak = diff;
                }

                lastSpoken = toSpeak;
                history[toSpeak] = history.ContainsKey(toSpeak) ? history[toSpeak].Concat(new[] {index}).ToList() : new List<long> {index};
                yield return lastSpoken;
                index++;
            }
        }

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            return -1;
        }
    }
}