using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2021.Day20
{
    public static class Day20
    {
        private const int Day = 20;

        public class Grid
        {
            public Dictionary<(long, long), bool> pixels = new Dictionary<(long, long), bool>();

            public bool defaultValue = false;
            public int leeway = 2;
            public long minX => pixels.Keys.Select(q => q.Item1).Min();
            public long maxX => pixels.Keys.Select(q => q.Item1).Max();
            public long minY => pixels.Keys.Select(q => q.Item2).Min();
            public long maxY => pixels.Keys.Select(q => q.Item2).Max();

            public Grid(List<string> lines)
            {
                var height = lines.Count;
                var width = lines.First().Length;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        pixels[(x, y)] = lines[y][x] == '#';
                    }
                }
            }

            public bool GetAtCoord((long, long) c)
            {
                return pixels.ContainsKey(c) ? pixels[c] : defaultValue;
            }

            public void SetAtCoord((long, long) c, bool val)
            {
                pixels[c] = val;
            }

            public void ApplyEnhancementAlgorithm(List<bool> source)
            {
                var newGrid = pixels.Clone().ToDictionary(x => x.Key, x => x.Value);
                var locals = (minX, minY, maxX, maxY);
                for (var x = locals.minX - leeway; x <= locals.maxX + leeway; x++)
                {
                    for (var y = locals.minY - leeway; y <= locals.maxY + leeway; y++)
                    {
                        var lookupBools = new[]
                        {
                            GetAtCoord((x - 1, y - 1)),
                            GetAtCoord((x, y - 1)),
                            GetAtCoord((x + 1, y - 1)),
                            GetAtCoord((x - 1, y)),
                            GetAtCoord((x, y)),
                            GetAtCoord((x + 1, y)),
                            GetAtCoord((x - 1, y + 1)),
                            GetAtCoord((x, y + 1)),
                            GetAtCoord((x + 1, y + 1)),
                        };
                        var binaryString = lookupBools.Aggregate("", (s, b) => s + (b ? "1" : "0"));
                        var binaryNum = Convert.ToInt32(binaryString, 2);
                        var newVal = source[binaryNum];
                        newGrid[(x, y)] = newVal;
                    }
                }

                pixels = newGrid;
                leeway += 2;
                defaultValue = source[0];
            }

            public long CountPixels => pixels.Values.Count(x => x);

            public void Draw()
            {
                for (var y = minY; y <= maxY; y++)
                {
                    for (var x = minX; x <= maxX; x++)
                    {
                        Console.Write(GetAtCoord((x, y)) ? "#" : ".");
                    }

                    Console.Write(Environment.NewLine);
                }

                Console.WriteLine("________________________________");
            }
        }

        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var lookup = lines.First().Select(x => x == '#').ToList();

            var grid = new Grid(lines.Skip(2).ToList());

            grid.Draw();

            grid.ApplyEnhancementAlgorithm(lookup);
            grid.Draw();

            grid.ApplyEnhancementAlgorithm(lookup);
            grid.Draw();

            return grid.CountPixels;
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}