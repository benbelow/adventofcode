using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2021.Day05
{
    public static class Day05
    {
        private const int Day = 05;

        private class Grid
        {

            private Dictionary<(int, int), int> Cells { get; set; } = new Dictionary<(int, int), int>();

            public void ApplyLine((int, int) start, (int, int) end)
            {
                var xDiff = Math.Abs(end.Item1 - start.Item1);
                var yDiff = Math.Abs(end.Item2 - start.Item2);

                foreach (var point in Common.Utils.LineUtils.Line(start.Item1, start.Item2, end.Item1, end.Item2))
                {
                    Cells.TryGetValue(point, out var visitedCount);
                    visitedCount++;
                    Cells[point] = visitedCount;
                }
            }

            public int CellsVisitedTwice()
            {
                return Cells.Count(c => c.Value > 1);
            }
        }
        
        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var coords = lines.Select(l => (l.Split("->").First(), l.Split("->").Last()))
                .Select(stringC => (
                    (
                        int.Parse(stringC.Item1.Split(",")[0]),
                        int.Parse(stringC.Item1.Split(",")[1])
                    ),
                    (
                        int.Parse(stringC.Item2.Split(",")[0]),
                        int.Parse(stringC.Item2.Split(",")[1])
                    )
                ));

            var grid = new Grid();

            foreach (var coord in coords.Where(c => c.Item1.Item1 == c.Item2.Item1 || c.Item1.Item2 == c.Item2.Item2))
            {
                grid.ApplyLine(coord.Item1, coord.Item2);
            }

            return grid.CellsVisitedTwice();
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var coords = lines.Select(l => (l.Split("->").First(), l.Split("->").Last()))
                .Select(stringC => (
                    (
                        int.Parse(stringC.Item1.Split(",")[0]),
                        int.Parse(stringC.Item1.Split(",")[1])
                    ),
                    (
                        int.Parse(stringC.Item2.Split(",")[0]),
                        int.Parse(stringC.Item2.Split(",")[1])
                    )
                ));

            var grid = new Grid();

            foreach (var coord in coords)
            {
                grid.ApplyLine(coord.Item1, coord.Item2);
            }

            return grid.CellsVisitedTwice();
        }
    }
}