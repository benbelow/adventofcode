using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2021.Day11
{
    public static class Day11
    {
        private const int Day = 11;

        public static long Part1(bool isExample = false, List<string> overrideInput = null)
        {
            var lines = overrideInput ?? FileReader.ReadInputLines(Day, isExample).ToList();
            var grid = lines.Select(l => l.Select(c => int.Parse(c.ToString())).ToList()).ToList();

            var flashCount = 0;

            var width = lines.First().Length;
            var height = lines.Count;

            bool InBounds((int, int) c)
            {
                var (x, y) = c;
                return x >= 0 && y >= 0 && x < width && y < height;
            }

            IEnumerable<(int, int)> AllCoords()
            {
                for (int y = 0; y < lines.Count; y++)
                {
                    for (int x = 0; x < lines.First().Length; x++)
                    {
                        yield return (x, y);
                    }
                }
            }

            int Octopus((int, int) c)
            {
                if (!InBounds(c))
                {
                    throw new Exception("OUT OF BOUND");
                }

                var (x, y) = c;
                return grid[y][x];
            }

            void SetOctopus((int, int) c, int v)
            {
                if (!InBounds(c))
                {
                    throw new Exception("OUT OF BOUND");
                }

                var (x, y) = c;
                grid[y][x] = v;
            }

            void TryIncrementOctopus((int, int) c)
            {
                if (InBounds(c))
                {
                    SetOctopus(c, Math.Min(10, Octopus(c) + 1));
                }
            }

            List<(int, int)> Neighbours((int, int) c)
            {
                var (x, y) = c;
                return new[]
                {
                    (x - 1, y - 1),
                    (x - 1, y),
                    (x - 1, y + 1),
                    (x, y - 1),
                    (x, y + 1),
                    (x + 1, y - 1),
                    (x + 1, y),
                    (x + 1, y + 1),
                }.Where(InBounds).ToList();
            }

            void Tick()
            {
                foreach (var (x, y) in AllCoords())
                {
                    var coord = (x, y);
                    TryIncrementOctopus(coord);
                }

                var flashed = new List<(int, int)>();

                while (AllCoords().Any(c => !flashed.Contains(c) && Octopus(c) > 9))
                {
                    var newFlashes = AllCoords().Where(c => !flashed.Contains(c) && Octopus(c) > 9).ToList();
                    flashCount += newFlashes.Count;

                    foreach (var flash in newFlashes)
                    {
                        foreach (var neighbour in Neighbours(flash))
                        {
                            TryIncrementOctopus(neighbour);
                        }

                        flashed.Add(flash);
                    }
                }

                foreach (var fullOctopus in AllCoords().Where(c => Octopus(c) > 9))
                {
                    SetOctopus(fullOctopus, 0);
                }
            }

            for (int i = 0; i < 100; i++)
            {
                Tick();
            }

            return flashCount;
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}