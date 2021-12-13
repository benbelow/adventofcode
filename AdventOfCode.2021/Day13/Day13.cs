using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2021.Day13
{
    public static class Day13
    {
        private const int Day = 13;

        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();

            var coords = new Dictionary<(int, int), bool>();

            var folds = new List<string>();

            var section2 = false;
            foreach (var line in lines)
            {
                if (line == "")
                {
                    section2 = true;
                    continue;
                }

                if (!section2)
                {
                    var vals = line.Split(",");
                    var x = int.Parse(vals.First());
                    var y = int.Parse(vals.Last());
                    coords[(x, y)] = true;
                }

                else
                {
                    folds.Add(line);
                }
            }

            int CalculateMaxX() => coords.Select(v => v.Key.Item1).Max();
            int CalculateMinX() => coords.Select(v => v.Key.Item1).Min();
            int CalculateMaxY() => coords.Select(v => v.Key.Item2).Max();
            int CalculateMinY() => coords.Select(v => v.Key.Item2).Min();

            var maxX = 0;
            var maxY = 0;
            var minX = 0;
            var minY = 0;

            void CalculateMinMax()
            {
                maxX = CalculateMaxX();
                maxY = CalculateMaxY();
                minX = CalculateMinX();
                minY = CalculateMinY();
            }

            bool GetAtC((int, int) c)
            {
                if (!coords.ContainsKey(c))
                {
                    coords[c] = false;
                }

                return coords[c];
            }

            void Draw()
            {
                CalculateMinMax();
                var l = "";
                var isChaff = true;
                for (int x = minX; x <= maxX; x++)
                {
                    l = "";
                    for (int y = minY; y <= maxY; y++)
                    {
                        var c = GetAtC((x, y));
                        l += c ? "#" : ".";
                    }

                    if (l.Contains("#"))
                    {
                        isChaff = false;
                    }

                    if (!isChaff)
                    {
                        Console.WriteLine(l);
                    }
                }

                Console.WriteLine("      ");
            }

            CalculateMinMax();
            for (int i = 0; i <= maxX; i++)
            {
                for (int j = 0; j <= maxY; j++)
                {
                    if (!coords.ContainsKey((i, j)))
                    {
                        coords[(i, j)] = false;
                    }
                }
            }

            foreach (var fold in folds.Take(1))
            {
                CalculateMinMax();
                var split1 = fold.Split("=");
                var foldC = int.Parse(split1[1]);
                var foldAxis = split1[0].Split(" ").Last();

                if (foldAxis == "y")
                {
                    for (int y = foldC; y <= maxY; y++)
                    {
                        for (int x = minX; x <= maxX; x++)
                        {
                            var existingValue = GetAtC((x, y));
                            var newCoord = (x, foldC - (y - foldC));
                            coords[newCoord] = GetAtC(newCoord) || existingValue;
                            coords.Remove((x, y));
                        }
                    }
                }
                else
                {
                    for (int y = minY; y <= maxY; y++)
                    {
                        for (int x = foldC; x <= maxX; x++)
                        {
                            var existingValue = GetAtC((x, y));
                            var newCoord = (foldC - (x - foldC), y);
                            coords[newCoord] = GetAtC(newCoord) || existingValue;
                            coords[(x, y)] = false;
                        }
                    }
                }
            }

            Draw();
            return coords.Count(c => c.Value);
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();

            var coords = new Dictionary<(int, int), bool>();

            var folds = new List<string>();

            var section2 = false;
            foreach (var line in lines)
            {
                if (line == "")
                {
                    section2 = true;
                    continue;
                }

                if (!section2)
                {
                    var vals = line.Split(",");
                    var x = int.Parse(vals.First());
                    var y = int.Parse(vals.Last());
                    coords[(x, y)] = true;
                }

                else
                {
                    folds.Add(line);
                }
            }

            int CalculateMaxX() => coords.Select(v => v.Key.Item1).Max();
            int CalculateMinX() => coords.Select(v => v.Key.Item1).Min();
            int CalculateMaxY() => coords.Select(v => v.Key.Item2).Max();
            int CalculateMinY() => coords.Select(v => v.Key.Item2).Min();

            var maxX = 0;
            var maxY = 0;
            var minX = 0;
            var minY = 0;

            void CalculateMinMax()
            {
                maxX = CalculateMaxX();
                maxY = CalculateMaxY();
                minX = CalculateMinX();
                minY = CalculateMinY();
            }

            bool GetAtC((int, int) c)
            {
                if (!coords.ContainsKey(c))
                {
                    coords[c] = false;
                }

                return coords[c];
            }

            void Draw()
            {
                CalculateMinMax();
                var l = "";
                var lines = new List<string>();
                for (int x = minX; x <= maxX; x++)
                {
                    l = "";
                    for (int y = minY; y <= maxY; y++)
                    {
                        var c = GetAtC((x, y));
                        l += c ? "#" : ".";
                    }

                    lines.Add(l);
                }

                foreach (var goodL in lines.Where(l=> l.Contains("#")))
                {
                    Console.WriteLine(goodL);
                }
                
                Console.WriteLine("      ");
            }

            CalculateMinMax();
            for (int i = 0; i <= maxX; i++)
            {
                for (int j = 0; j <= maxY; j++)
                {
                    if (!coords.ContainsKey((i, j)))
                    {
                        coords[(i, j)] = false;
                    }
                }
            }

            foreach (var fold in folds)
            {
                CalculateMinMax();
                var split1 = fold.Split("=");
                var foldC = int.Parse(split1[1]);
                var foldAxis = split1[0].Split(" ").Last();

                if (foldAxis == "y")
                {
                    for (int y = foldC; y <= maxY; y++)
                    {
                        for (int x = minX; x <= maxX; x++)
                        {
                            var existingValue = GetAtC((x, y));
                            var newCoord = (x, foldC - (y - foldC));
                            coords[newCoord] = GetAtC(newCoord) || existingValue;
                            coords.Remove((x, y));
                        }
                    }
                }
                else
                {
                    for (int y = minY; y <= maxY; y++)
                    {
                        for (int x = foldC; x <= maxX; x++)
                        {
                            var existingValue = GetAtC((x, y));
                            var newCoord = (foldC - (x - foldC), y);
                            coords[newCoord] = GetAtC(newCoord) || existingValue;
                            coords[(x, y)] = false;
                        }
                    }
                }
            }

            Draw();
            return coords.Count(c => c.Value);
        }
    }
}