using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2015.Day02
{
    public static class Day02
    {
        public static int Part1()
        {
            var lines = FileReader.ReadInputLines(2);
            return ParseDimensions(lines)
                .Aggregate(0, (total, dimensions) => total + PaperNecessary(dimensions.Item1, dimensions.Item2, dimensions.Item3));
        }

        private static IEnumerable<(int, int, int)> ParseDimensions(this IEnumerable<string> lines)
        {
            return lines.Select(l =>
            {
                var strings = l.Split('x').Select(int.Parse).ToList();
                return (strings[0], strings[1], strings[2]);
            });
        }

        public static int Part2()
        {
            var dimensions = FileReader.ReadInputLines(2).ParseDimensions();
            return dimensions.Sum(x => RibbonNecessary(x.Item1, x.Item2, x.Item3));
        }

        public static int PaperNecessary(int width, int height, int length)
        {
            var (side1, side2, side3) = (width * length, width * height, height * length);

            var smallestSide = Math.Min(side1, Math.Min(side2, side3));

            return 2 * (side1 + side2 + side3) + smallestSide;
        }

        public static int RibbonNecessary(int width, int height, int length)
        {
            var dimensions = new[] {width, height, length};
            var twoSmallest = dimensions.OrderBy(x => x).Take(2);
            
            var nonBow = twoSmallest.Sum() * 2;
            var bow = width * height * length;

            return nonBow + bow;
        }
    }
}