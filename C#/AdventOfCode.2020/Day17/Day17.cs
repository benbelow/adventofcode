using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2020.Day17
{
    public static class Day17
    {
        private const int Day = 17;

        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var dimension = new PocketDimension(lines);
            for (int i = 0; i < 6; i++)
            {
                dimension.Tick();
            }

            return dimension.ActiveCubes();
        }

        public class PocketDimension
        {
            private Dictionary<(long, long, long), bool> knownCubes = new Dictionary<(long, long, long), bool>();

            public PocketDimension(List<string> inputLines)
            {
                for (int y = 0; y < inputLines.Count; y++)
                {
                    var line = inputLines[y];
                    for (int x = 0; x < line.Length; x++)
                    {
                        var character = line[x];
                        knownCubes[(x, y, 0)] = character == '#';
                    }
                }
            }

            public void Tick()
            {
                var newState = new Dictionary<(long, long, long), bool>();

                for (var x = MinX() - 1; x <= MaxX() + 1; x++)
                {
                    for (var y = MinY() - 1; y <= MaxY() + 1; y++)
                    {
                        for (var z = MinZ() - 1; z <= MaxZ() + 1; z++)
                        {
                            var coord = (x, y, z);
                            newState[coord] = NewState(coord);
                        }
                    }
                }

                knownCubes = newState;
            }

            public bool NewState((long, long, long) coord)
            {
                var currentState = IsActive(coord);
                var neighbours = ActiveNeighbours(coord);
                return currentState ? neighbours == 2 || neighbours == 3 : neighbours == 3;
            }

            public long MaxX() => Xs().Max();
            public long MinX() => Xs().Min();
            public long MaxY() => Ys().Max();
            public long MinY() => Ys().Min();
            public long MaxZ() => Zs().Max();
            public long MinZ() => Zs().Min();

            private IEnumerable<long> Xs() => knownCubes.Keys.Select(k => k.Item1);
            private IEnumerable<long> Ys() => knownCubes.Keys.Select(k => k.Item2);
            private IEnumerable<long> Zs() => knownCubes.Keys.Select(k => k.Item3);

            public bool IsActive((long, long, long) coord) => knownCubes.ContainsKey(coord) && knownCubes[coord];

            public int ActiveNeighbours((long, long, long) coord)
            {
                var count = 0;
                for (var x = coord.Item1 - 1; x <= coord.Item1 + 1; x++)
                {
                    for (var y = coord.Item2 - 1; y <= coord.Item2 + 1; y++)
                    {
                        for (var z = coord.Item3 - 1; z <= coord.Item3 + 1; z++)
                        {
                            if (x == coord.Item1 && y == coord.Item2 && z == coord.Item3)
                            {
                                continue;
                            }

                            count += IsActive((x, y, z)) ? 1 : 0;
                        }
                    }
                }

                return count;
            }

            public long ActiveCubes() => knownCubes.Count(x => x.Value);
        }


        public class PocketDimension_4D
        {
            private Dictionary<(long, long, long, long), bool> knownCubes = new Dictionary<(long, long, long, long), bool>();

            public PocketDimension_4D(List<string> inputLines)
            {
                for (int y = 0; y < inputLines.Count; y++)
                {
                    var line = inputLines[y];
                    for (int x = 0; x < line.Length; x++)
                    {
                        var character = line[x];
                        var coord = (x, y, 0, 0);
                        knownCubes[coord] = character == '#';                                
                        SetThresholds(UpdatedThresholds(coord, Thresholds));
                    }
                }
            }

            public void Tick()
            {
                var newState = new Dictionary<(long, long, long, long), bool>();
                var thresholds = Thresholds;

                for (var x = MinX - 1; x <= MaxX + 1; x++)
                {
                    for (var y = MinY - 1; y <= MaxY + 1; y++)
                    {
                        for (var z = MinZ - 1; z <= MaxZ + 1; z++)
                        {
                            for (var w = MinW - 1; w <= MaxW + 1; w++)
                            {
                                var coord = (x, y, z, w);
                                newState[coord] = NewState(coord);
                                thresholds = UpdatedThresholds(coord, thresholds);
                            }
                        }
                    }
                }

                knownCubes = newState;
                SetThresholds(thresholds);
            }

            public bool NewState((long, long, long, long) coord)
            {
                var currentState = IsActive(coord);
                var neighbours = ActiveNeighbours(coord);
                return currentState ? neighbours == 2 || neighbours == 3 : neighbours == 3;
            }

            private long MinX = 0L;
            private long MaxX = 0L;
            private long MinY = 0L;
            private long MaxY = 0L;
            private long MinZ = 0L;
            private long MaxZ = 0L;
            private long MinW = 0L;
            private long MaxW = 0L;

            private (long, long, long, long, long, long, long, long) Thresholds => (MinX, MaxX, MinY, MaxY, MinZ, MaxZ, MinW, MaxW);

            private void SetThresholds((long, long, long, long, long, long, long, long) thresholds)
            {
                MinX = thresholds.Item1;
                MaxX = thresholds.Item2;
                MinY = thresholds.Item3;
                MaxY = thresholds.Item4;
                MinZ = thresholds.Item5;
                MaxZ = thresholds.Item6;
                MinW = thresholds.Item7;
                MaxW = thresholds.Item8;
            }
            
            private (long, long, long, long, long, long, long, long) UpdatedThresholds((long, long, long, long) coord, (long, long, long, long, long, long, long, long) existingThresholds)
            {
                var (minX, maxX, minY, maxY, minZ, maxZ, minW, maxW) = existingThresholds;
                var (x, y, z, w) = coord;
                if (x < minX)
                {
                    minX = x;
                }

                if (x > maxX)
                {
                    maxX = x;
                }

                if (y < minY)
                {
                    minY = y;
                }

                if (y > maxY)
                {
                    maxY = y;
                }

                if (z < minZ)
                {
                    minZ = z;
                }

                if (z > maxZ)
                {
                    maxZ = z;
                }

                if (w < minW)
                {
                    minW = w;
                }

                if (w > maxW)
                {
                    maxW = w;
                }

                return (minX, maxX, minY, maxY, minZ, maxZ, minW, maxW);
            }

            private IEnumerable<long> Xs() => knownCubes.Keys.Select(k => k.Item1);
            private IEnumerable<long> Ys() => knownCubes.Keys.Select(k => k.Item2);
            private IEnumerable<long> Zs() => knownCubes.Keys.Select(k => k.Item3);
            private IEnumerable<long> Ws() => knownCubes.Keys.Select(k => k.Item4);

            public bool IsActive((long, long, long, long) coord) => knownCubes.ContainsKey(coord) && knownCubes[coord];

            public int ActiveNeighbours((long, long, long, long) coord)
            {
                var count = 0;
                for (var x = coord.Item1 - 1; x <= coord.Item1 + 1; x++)
                {
                    for (var y = coord.Item2 - 1; y <= coord.Item2 + 1; y++)
                    {
                        for (var z = coord.Item3 - 1; z <= coord.Item3 + 1; z++)
                        {
                            for (var w = coord.Item4 - 1; w <= coord.Item4 + 1; w++)
                            {
                                if (x == coord.Item1 && y == coord.Item2 && z == coord.Item3 & w == coord.Item4)
                                {
                                    continue;
                                }

                                count += IsActive((x, y, z, w)) ? 1 : 0;
                            }
                        }
                    }
                }

                return count;
            }

            public long ActiveCubes() => knownCubes.Count(x => x.Value);
        }

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var dimension = new PocketDimension_4D(lines);
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine($"Iteration {i}. Cubes: {dimension.ActiveCubes()}");
                dimension.Tick();
            }

            return dimension.ActiveCubes();
        }
    }
}