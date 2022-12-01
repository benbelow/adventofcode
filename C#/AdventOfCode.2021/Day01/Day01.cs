using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2021.Day01
{
    public static class Day01
    {
        private const int Day = 01;

        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var intLines = lines.Select(int.Parse).ToList();

            return NumberOfIncreases(intLines);
        }

        private static long NumberOfIncreases(List<int> nums)
        {
            var diffs = nums.Select((value, i) =>
            {
                if (i == 0)
                {
                    return (int?) null;
                }

                var previousValue = nums[i - 1];
                return value - previousValue;
            });

            return diffs.Count(d => d > 0);
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var intLines = lines.Select(int.Parse).ToList();

            var windows = intLines.Select((value, i) =>
            {
                if (i < 2)
                {
                    return (int?) null;
                }

                var previousValue = intLines[i - 1];
                var previousValue2 = intLines[i - 2];
                return value + previousValue + previousValue2;
            }).Where(x => x != null).Select(x => x.Value).ToList();

            return NumberOfIncreases(windows);
        }
    }
}