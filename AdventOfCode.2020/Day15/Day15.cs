using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

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
            var history = new Dictionary<long, Queue<long>> {{0, new Queue<long>(2)}};
            var index = 1L;
            long lastSpoken = -1;

            foreach (var original in seed)
            {
                var newQueue = new Queue<long>(2);
                newQueue.Enqueue(index);
                history[original] = newQueue;
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
                    var earliest = historyOfLastSpoken.Dequeue();
                    var later = historyOfLastSpoken.Peek();
                    var diff = later - earliest;
                    toSpeak = diff;
                }

                lastSpoken = toSpeak;
                if (!history.ContainsKey(toSpeak))
                {
                    var newQueue = new Queue<long>(2);
                    newQueue.Enqueue(index);
                    history[toSpeak] = newQueue;
                }
                else
                {
                    var oldQueue = history[toSpeak];
                    oldQueue.Enqueue(index);
                    history[toSpeak] = oldQueue;
                }
                yield return lastSpoken;
                index++;
            }
        }

        public static long Part2(string line = null)
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            line ??= lines.Single();
            var seed = line.Split(",").Select(long.Parse).ToList();
            return XThNumber(seed, 30000000);
        }
    }
}