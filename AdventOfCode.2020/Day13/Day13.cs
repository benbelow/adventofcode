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

            var generatorFactories = targets.Select<(int, int), Func<IEnumerable<long>>>(t =>
                () => Multiples(t.Item1)
            ).ToList();

            var generators = generatorFactories.Select(f => f()).ToList();

            var enumerators = generators.Select(g => g.GetEnumerator()).ToList();

            List<long> values = null;

            var biggestGenerator = targets.OrderByDescending(t => t.Item1).First();
            
            
            
            foreach (var multiple in Multiples(biggestGenerator.Item1))
            {
                if (multiple < 100000000000000)
                {
                    continue;
                }
                var offset = biggestGenerator.Item2;
                var x = multiple - offset; 
                if (targets.All(t => { return (x + t.Item2) % t.Item1 == 0;}))
                {
                    return x;
                }
            }
            
            while (true)
            {
                if (values == null)
                {
                    values = enumerators.Select(g =>
                    {
                        using var enumerator = g;
                        enumerator.MoveNext();
                        return enumerator.Current;
                    }).ToList();
                }

                if (Enumerable.Range(0, targets.Count).All(i => { return values[i] == targets[i].Item1 - targets[i].Item2; }))
                {
                    return values.First();
                }
                
                
                var min = values.Min();
                var minIndex = values.IndexOf(min);
                var enumeratorMin = enumerators[minIndex];
                enumeratorMin.MoveNext();
                values[minIndex] = enumeratorMin.Current;
                
                
            }

            return -2;
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