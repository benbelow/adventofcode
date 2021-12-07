using System;
using System.Linq;
using AdventOfCode.Common;
using Microsoft.AspNetCore.DataProtection;

namespace AdventOfCode._2021.Day07
{
    public static class Day07
    {
        private const int Day = 07;
        
        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var positions = lines.Single().Split(",").Select(int.Parse).ToList();

            var orderedPositions = positions.OrderBy(x => x).ToList();

            var fuelCostsToAlign = orderedPositions.Select(pos =>
            {
                return orderedPositions.Sum(o => Math.Abs(pos - o));
            });

            return fuelCostsToAlign.Min();
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}