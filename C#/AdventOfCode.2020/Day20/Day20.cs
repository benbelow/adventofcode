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

        public static long Part1(string input = null)
        {
            var lines = input == null ? FileReader.ReadInputLines(Day).ToList() : input.Split(Environment.NewLine).ToList();
            var blocks = SplitExtension.Split(lines, "");
            var tiles = blocks.Select(b => new Tile(b.ToList())).ToList();

            var neighbours = tiles.Select(t => t.PotentialNeighbours(tiles));
            var corners = tiles.Where(t => t.NumberOfNeighbours(tiles) == 2);

            corners.Count().Should().Be(4);
            return corners.Aggregate(1L, (a, x) => a * x.Id);
        }

        public static long Part2(string input = null)
        {
            var lines = input == null ? FileReader.ReadInputLines(Day).ToList() : input.Split(Environment.NewLine).ToList();
            var blocks = SplitExtension.Split(lines, "");
            var tiles = blocks.Select(b => new Tile(b.ToList())).ToList();

            var neighbours = tiles.Select(t => t.PotentialNeighbours(tiles));
            var edges = tiles.Where(t => t.PotentialNeighbours(tiles).ToList().Count(x => x == 0) >= 1);

            // edges.Count().Should().Be(44);

            var megaTile = new MegaTile(tiles);
            
            return IterateMegaTilePart2(megaTile);
        }

        internal static long IterateMegaTilePart2(MegaTile megaTile)
        {
            var operationOrder = new[]
            {
                (Rotation.None, Flip2D.None),                
                
                (Rotation.Right90, Flip2D.Vertical),

                (Rotation.Left90, Flip2D.None),
                (Rotation.Right180, Flip2D.None),
                (Rotation.Right90, Flip2D.None),
                (Rotation.None, Flip2D.Horizontal),
                (Rotation.Left90, Flip2D.Horizontal),
                (Rotation.Right180, Flip2D.Horizontal),
                (Rotation.Right90, Flip2D.Horizontal),
                (Rotation.None, Flip2D.Vertical),
                (Rotation.Left90, Flip2D.Vertical),
                (Rotation.Right180, Flip2D.Vertical),
            };

            foreach (var (rotate, flip) in operationOrder)
            {
                var toTest = megaTile.Rotate(rotate).Flip(flip);
                var monsters = toTest.NumberOfMonsters();
                if (monsters != 0)
                {
                    return toTest.CountOfNonMonsterTiles();
                }
            }

            return -1;
        }
    }
}