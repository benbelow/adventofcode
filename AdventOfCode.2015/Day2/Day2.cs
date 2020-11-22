using System;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2015.Day2
{
    public static class Day2
    {
        public static int Part1()
        {
            var lines = FileReader.ReadInputLines(2);
            return lines.Select(l =>
                {
                    var strings = l.Split('x').Select(int.Parse).ToList();
                    return (strings[0], strings[1], strings[2]);
                })
                .Aggregate(0, (total, dimensions) => total + PaperNecessary(dimensions.Item1, dimensions.Item2, dimensions.Item3));
        }

        public static int Part2()
        {
            return 0;
        }

        public static int PaperNecessary(int width, int height, int length)
        {
            var (side1, side2, side3) = (width * length, width * height, height * length);

            var smallestSide = Math.Min(side1, Math.Min(side2, side3));

            return 2 * (side1 + side2 + side3) + smallestSide;
        }
    }
}