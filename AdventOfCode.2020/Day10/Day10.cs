using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using NUnit.Framework;

namespace AdventOfCode._2020.Day10
{
    public static class Day10
    {
        private const int Day = 10;
        
        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var adapters = lines.Select(int.Parse);
            var sorted = adapters.OrderBy(x => x).ToList();

            var value = 0;
            var used = new List<int>();
            var oneDiffs = 0;
            var twoDiffs = 0;
            var threeDiffs = 0;
            for (int i = 0; i < sorted.Count(); i++)
            {
                var current = sorted[i];
                if (value + 1 == current)
                {
                    used.Add(current);
                    value = current;
                    oneDiffs++;
                    continue;
                }
                if (value + 2 == current)
                {
                    used.Add(current);
                    value = current;
                    twoDiffs++;
                    continue;
                }
                
                if (value + 3 == current)
                {
                    used.Add(current);
                    value = current;
                    threeDiffs++;
                    continue;
                }
                throw new Exception("no new adapter!");
                
            }
            
            return oneDiffs * (threeDiffs+1);
        }

        public static long Part2(List<string> lines = null)
        {
            lines ??= FileReader.ReadInputLines(Day).ToList();
            var adapters = lines.Select(int.Parse).ToList();
            var sortedAsc = adapters.OrderBy(x => x).ToList();
            var sortedDesc = adapters.OrderByDescending(x => x).ToList();

            var numberOfWaysOfArriving = new Dictionary<int, long>();
            
            for (int i = 0; i < sortedDesc.Count; i++)
            {
                var current = sortedDesc[i];

                var arrivals = 0;
                
                if (current == 1)
                {
                    arrivals = 1;
                    numberOfWaysOfArriving[current] = arrivals;
                    continue;
                }
                if (sortedDesc.Contains(current - 1))
                {
                    arrivals++;
                }
                if (sortedDesc.Contains(current - 2))
                {
                    arrivals++;
                }
                if (sortedDesc.Contains(current - 3))
                {
                    arrivals++;
                }

                numberOfWaysOfArriving[current] = arrivals;
            }
            
            var numberOfWaysOfLeaving = new Dictionary<int, long>();
            
            for (int i = 0; i < sortedAsc.Count; i++)
            {
                var current = sortedAsc[i];

                var leaveses = 0;
                
                if (i == sortedAsc.Count()-1)
                {
                    leaveses = 1;
                    numberOfWaysOfLeaving[current] = leaveses;
                    continue;
                }
                if (sortedDesc.Contains(current + 1))
                {
                    leaveses++;
                }
                if (sortedDesc.Contains(current + 2))
                {
                    leaveses++;
                }
                if (sortedDesc.Contains(current + 3))
                {
                    leaveses++;
                }

                numberOfWaysOfLeaving[current] = leaveses;
            }


            var pathLookup = new Dictionary<int, long>{{0, 1}};

            long GetPaths(int adapter, int jump)
            {
                var i = adapter - jump;
                return pathLookup.ContainsKey(i) ? pathLookup[i] : 0;
            }

            foreach (var adapter in sortedAsc)
            {
                var paths = 0L;
                paths += GetPaths(adapter, 1);
                paths += GetPaths(adapter, 2);
                paths += GetPaths(adapter, 3);
                pathLookup[adapter] = paths;
            }


            return pathLookup.Last().Value;
        }

        private static Dictionary<int, long> cacheYo = new Dictionary<int, long>();
        
        // TOO SLOW
        private static long CountPaths(Dictionary<int,long> numberOfWays, int value = 0)
        {
            if (cacheYo.ContainsKey(value))
            {
                return cacheYo[value];
            }
            
            if (!numberOfWays.ContainsKey(value) && value != 0)
            {
                cacheYo[value] = 0;
                return 0;
            }

            if (value == numberOfWays.Keys.Max())
            {
                cacheYo[value] = 1;
                return 1;
            }

            return CountPaths(numberOfWays, value + 1) + CountPaths(numberOfWays, value + 2) + CountPaths(numberOfWays, value + 3);
        }
    }
}