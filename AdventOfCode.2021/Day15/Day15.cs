using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2021.Day15
{
    public static class Day15
    {
        private const int Day = 15;

        private static int shortestFullPathSeen = 999999;

        class Chiton
        {
            public readonly (int, int) coord;
            public readonly int val;

            public bool Visited { get; set; } = false;
            public int tentativeDistance { get; set; }

            public Chiton((int, int) coord, int val, int width = 0, int height = 0)
            {
                tentativeDistance = coord == (0, 0) ? 0 : int.MaxValue;

                this.coord = coord;
                this.val = val;
            }
        }

        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var width = lines.First().Count();
            var height = lines.Count();

            var grid = lines.Select(l => l.Select(c => int.Parse(c.ToString())).ToList())
                .SelectMany((line, y) => line.Select((val, x) => new KeyValuePair<(int, int), Chiton>((x, y), new Chiton((x, y), val, width, height))))
                .ToDictionary(q => q.Key, q => q.Value);

            return DestNodeTentativeDistance(width, height, grid);
        }

        private static long DestNodeTentativeDistance(int width, int height, Dictionary<(int, int), Chiton> grid)
        {
            var target = (width - 1, height - 1);

            var unvisited = grid.Values.ToList();

            var withTentative = new HashSet<Chiton>();
            
            bool InBounds((int, int) c) => c.Item1 >= 0 && c.Item2 >= 0 && c.Item1 < width && c.Item2 < height;

            void ConsiderNeighbours(Chiton current)
            {
                var c = current.coord;

                var ncs = new[]
                {
                    (c.Item1 + 1, c.Item2),
                    (c.Item1 - 1, c.Item2),
                    (c.Item1, c.Item2 + 1),
                    (c.Item1, c.Item2 - 1),
                };

                foreach (var nc in ncs.Where(InBounds).Where(nc => !grid[nc].Visited))
                {
                    var neighbour = grid[nc];
                    var newTentative = current.tentativeDistance + neighbour.val;
                    withTentative.Add(neighbour);
                    neighbour.tentativeDistance = Math.Min(neighbour.tentativeDistance, newTentative);
                }

                current.Visited = true;
                unvisited.Remove(current);
                withTentative.Remove(current);
            }

            ConsiderNeighbours(grid[(0, 0)]);

            var destNode = grid[target];

            while (!destNode.Visited)
            {
                var minNode = withTentative.OrderBy(q => q.tentativeDistance).First();
                ConsiderNeighbours(minNode);
            }

            return destNode.tentativeDistance;
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var width = lines.First().Count();
            var height = lines.Count();


            var grid = new Dictionary<(int, int), Chiton>();

            for (int x = 0; x < width * 5; x++)
            {
                for (int y = 0; y < height * 5; y++)
                {
                    var originalVal = int.Parse(lines[y % height][x % width].ToString());
                    var newValUnmodified = (originalVal + (y / height) + (x / width));
                    var newVal = newValUnmodified == 9 ? 9 : newValUnmodified % 9;
                    grid[(x, y)] = new Chiton((x, y), newVal);
                }
            }

            return DestNodeTentativeDistance(width * 5, height * 5, grid);
        }
    }
}