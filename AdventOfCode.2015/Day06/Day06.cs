using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2015.Day06
{
    public static class Day06
    {
        private const int Day = 06;

        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var lightGrid = new LightGrid();

            foreach (var line in lines)
            {
                lightGrid.ApplyInstruction(line);
            }

            return lightGrid.LightsOn();
        }

        public class LightGrid
        {
            private enum Operation
            {
                On,
                Off,
                Toggle
            }

            private readonly List<List<bool>> lights;

            public LightGrid()
            {
                var row = Enumerable.Range(0, 1000).Select(_ => false).ToList();
                lights = Enumerable.Range(0, 1000).Select(_ => row.Select(x => x).ToList()).ToList();
            }

            public void ApplyInstruction(string instruction)
            {
                var operation = instruction.Contains("toggle") ? Operation.Toggle : instruction.Contains("turn on") ? Operation.On : Operation.Off;
                var start = instruction.Split("through")[0].Trim().Split().Last().Split(",").Select(int.Parse);
                var end = instruction.Split("through")[1].Trim().Split(",").Select(int.Parse);

                for (int x = start.First(); x <= end.First(); x++)
                {
                    for (int y = start.Last(); y <= end.Last(); y++)
                    {
                        var existing = lights[x][y];
                        var newStatus = operation switch
                        {
                            Operation.On => true,
                            Operation.Off => false,
                            Operation.Toggle => !existing,
                        };
                        lights[x][y] = newStatus;
                    }
                }
            }

            public long LightsOn() => lights.Sum(r => r.Count(x => x));
        }

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var lightGrid = new DimmableLightGrid();

            foreach (var line in lines)
            {
                lightGrid.ApplyInstruction(line);
            }

            return lightGrid.LightsBrightness();
        }

        public class DimmableLightGrid
        {
            private enum Operation
            {
                Up,
                Down,
                DoubleUp
            }

            private readonly IList<IList<int>> lights;

            public DimmableLightGrid()
            {
                var row = Enumerable.Range(0, 1000).Select(_ => 0).ToList();
                lights = Enumerable.Range(0, 1000).Select(_ => row.Clone()).ToList();
            }

            public void ApplyInstruction(string instruction)
            {
                var operation = instruction.Contains("toggle") ? Operation.DoubleUp : instruction.Contains("turn on") ? Operation.Up : Operation.Down;
                var start = instruction.Split("through")[0].Trim().Split().Last().Split(",").Select(int.Parse);
                var end = instruction.Split("through")[1].Trim().Split(",").Select(int.Parse);

                for (int x = start.First(); x <= end.First(); x++)
                {
                    for (int y = start.Last(); y <= end.Last(); y++)
                    {
                        var existing = lights[x][y];
                        var newStatus = operation switch
                        {
                            Operation.Up => existing+1,
                            Operation.Down => Math.Max(0, existing-1),
                            Operation.DoubleUp => existing +2,
                        };
                        lights[x][y] = newStatus;
                    }
                }
            }

            public long LightsBrightness() => lights.Sum(r => r.Sum(x => x));
        }
    }
}