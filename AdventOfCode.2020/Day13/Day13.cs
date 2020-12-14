using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;
using NUnit.Framework;

namespace AdventOfCode._2020.Day13
{
    public static class Day13
    {
        private const int Day = 13;

        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var offset = int.Parse(lines.First());
            var busTimes = lines.Last().Split(",").ToList();

            var runningBuses = busTimes.Where(x => x != "x").Select(int.Parse).ToList();

            var i = offset;

            var diffs = runningBuses.Select(b => (b, b - (offset % b)));

            var bus = diffs.OrderBy(x => x.Item2).First();

            return bus.Item2 * bus.Item1;

            return -1;
        }

        // TODO: Write actual code for this one day? 
        public static long Part2(string line = null)
        {
            var myLine = line ?? FileReader.ReadInputLines(Day).ToList().Last();
            var splitLines = myLine.Split(",").ToList();

            var targets = new List<(int, int)>();
            for (int i = 0; i < splitLines.Count(); i++)
            {
                var busId = splitLines[i];
                if (busId == "x")
                {
                    continue;
                }

                targets.Add((int.Parse(busId), i));
            }

            foreach (var target in targets)
            {
                Console.Write($"(t + {target.Item2}) mod {target.Item1} = 0,");
            }

            return 0;
        }

        public static IEnumerable<long> Multiples(long x)
        {
            var i = 1;
            while (true)
            {
                yield return x * i;
                i++;
            }
        }
    }
}