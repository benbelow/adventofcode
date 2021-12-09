using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2021.Day09
{
    public static class Day09
    {
        private const int Day = 09;

        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();

            var values = lines.Select(l => l.Select(c => int.Parse(c.ToString())).ToList()).ToList();
            var lineLength = values.First().Count();

            var total = 0;

            for (int y = 0; y < values.Count; y++)
            {
                for (int x = 0; x < lineLength; x++)
                {
                    var val = values[y][x];
                    var left = x == 0 ? 999999 : values[y][x - 1];
                    var right = x >= lineLength - 1 ? 999999 : values[y][x + 1];
                    var up = y == 0 ? 999999 : values[y - 1][x];
                    var down = y >= lines.Count - 1 ? 999999 : values[y + 1][x];

                    if (val < left && val < right && val < up && val < down)
                    {
                        total += val + 1;
                    }
                }
            }

            return total;
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}