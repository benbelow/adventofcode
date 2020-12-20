using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;
using FluentAssertions;
using MoreLinq;
using MoreLinq.Extensions;

namespace AdventOfCode._2020.Day20
{
    public static class Day20
    {
        private const int Day = 20;

        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var blocks = SplitExtension.Split(lines, "");
            var tiles = blocks.Select(b => new Tile(b.ToList())).ToList();

            var neighbours = tiles.Select(t => t.PotentialNeighbours(tiles));
            var corners = tiles.Where(t => t.PotentialNeighbours(tiles).ToList().Count(x => x == 0) == 2);

            corners.Count().Should().Be(4);
            return corners.Aggregate(1L, (a, x) => a * x.Id);
        }

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var blocks = SplitExtension.Split(lines, "");
            var tiles = blocks.Select(b => new Tile(b.ToList())).ToList();

            var neighbours = tiles.Select(t => t.PotentialNeighbours(tiles));
            var edges = tiles.Where(t => t.PotentialNeighbours(tiles).ToList().Count(x => x == 0) >= 1);

            // edges.Count().Should().Be(44);

            var megaTile = new MegaTile(tiles);
            
            return -100;
        }
    }
}