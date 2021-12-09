using System.Collections.Generic;
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

            var values = lines.Select(l => l.Select(c => int.Parse(c.ToString())).ToList()).ToList();
            var lineLength = values.First().Count();

            var lowPoints = new List<(int, int)>();

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
                        lowPoints.Add((x, y));
                    }
                }
            }

            int GetValueAtCoord((int, int) c)
            {
                var (x, y) = c;
                return values[y][x];
            }
            
            List<(int, int)> HigherNeighbours((int, int) c, List<(int, int)> exclude)
            {
                var (x, y) = c;
                var val = GetValueAtCoord(c);
                
                var left = (x,y - 1);
                var right = (x,(y + 1));
                var up = ((x - 1),(y));
                var down = ((x + 1),(y));

                var inBounds = new[] { left, right, up, down }
                    .Where(c2 => c2.Item1 >= 0 && c2.Item1 < lineLength && c2.Item2 >= 0 && c2.Item2 < lines.Count);
                var notExcluded = inBounds
                    .Where(c2 => !exclude.Contains(c2));
                return notExcluded
                    .Where(c2 =>
                    {
                        var valueAtCoord = GetValueAtCoord(c2);
                        return valueAtCoord != 9 && valueAtCoord >= val ;
                    })
                    .ToList();
            }

            var sizes = new List<int>();
            
            foreach (var lowPoint in lowPoints)
            {
                var seen = new [] {lowPoint}.ToList();

                var toCheck = new[] { lowPoint }.ToList();
                
                List<(int, int)> higherNeighbours;

                do
                {
                    var toCheckNext = new List<(int, int)>();
                    foreach (var cToCheck in toCheck)
                    {
                        higherNeighbours = HigherNeighbours(cToCheck, seen);
                        seen.AddRange(higherNeighbours);
                        toCheckNext.AddRange(higherNeighbours);
                    }

                    toCheck = toCheckNext;
                } while (toCheck.Any());
                
                sizes.Add(seen.Count());
            }

            return sizes.OrderByDescending(x => x).Take(3).Aggregate(1, (i, i1) => i * i1);
        }
    }
}