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

        public static List<int> ToList(this (int, int, int, int) input) => new List<int>
        {
            input.Item1, input.Item2, input.Item3, input.Item4
        };

        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var blocks = SplitExtension.Split(lines, "");
            var tiles = blocks.Select(b => new Tile(b.ToList())).ToList();

            var allEdges = tiles.SelectMany(t => t.Edges);

            var grouped = allEdges.GroupBy(e => e).OrderBy(x => x.Count()).ToList();

            var neighbours = tiles.Select(t => t.PotentialNeighbours(tiles));
            var corners = tiles.Where(t => t.PotentialNeighbours(tiles).ToList().Count(x => x == 0) == 2);

            corners.Count().Should().Be(4);
            return corners.Aggregate(1L, (a, x) => a * x.Id);
        }

        public class Tile
        {
            public enum Side
            {
                Top,
                Bottom,
                Left,
                Right
            }

            public long Id { get; set; }

            public List<List<bool>> spaces = Enumerable.Range(0, 10)
                .Select(_ => Enumerable.Range(0, 10).Select(_ => false).ToList()).ToList();

            public Tile(List<string> inputLines)
            {
                Id = long.Parse(inputLines[0].Split()[1].Replace(":", ""));
                for (int y = 0; y < 10; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        spaces[y][x] = inputLines[y + 1][x] == '#';
                    }
                }
            }

            public long EdgeAsLong(List<bool> edge)
            {
                var sRep = "";
                foreach (var c in edge)
                {
                    sRep += c ? '1' : '0';
                }

                return Convert.ToInt64(sRep, 2);
            }

            public long Reverse(long edge)
            {
                var sRep = Convert.ToString(edge, 2);
                sRep = sRep.PadLeft(10, '0');
                var reversed = sRep.Reverse().CharsToString();
                return Convert.ToInt64(reversed, 2);
            }

            public long Top => EdgeAsLong(spaces[0]);
            public long Bottom => EdgeAsLong(spaces[9]);
            public long Left => EdgeAsLong(spaces.Select(l => l[0]).ToList());
            public long Right => EdgeAsLong(spaces.Select(l => l[9]).ToList());

            public List<long> Edges => new List<long>
            {
                Top, Bottom, Left, Right
            };

            public (long, long) Tops => (Top, Reverse(Top));
            public (long, long) Bottoms => (Bottom, Reverse(Bottom));
            public (long, long) Lefts => (Left, Reverse(Left));
            public (long, long) Rights => (Right, Reverse(Right));

            public bool AnyMatch((long, long) one, (long, long) two) =>
                one.Item1 == two.Item1 || one.Item1 == two.Item2 || one.Item2 == two.Item1 || one.Item2 == two.Item2;

            public (long, long) Sides(Side side) => side switch
            {
                Side.Top => Tops,
                Side.Bottom => Bottoms,
                Side.Left => Lefts,
                Side.Right => Rights,
            };

            public int MatchesInAnyOrientation(Tile other, Side side)
            {
                var sides = Sides(side);

                return new[]
                    {
                        AnyMatch(sides, other.Tops),
                        AnyMatch(sides, other.Bottoms),
                        AnyMatch(sides, other.Lefts),
                        AnyMatch(sides, other.Rights),
                    }.Select(x => x ? 1 : 0)
                    .Sum();
            }

            public (int, int, int, int) PotentialNeighbours(List<Tile> others)
            {
                others = others.Where(o => o.Id != Id).ToList();

                int CountMatches(Side side)
                {
                    return others.Sum(o => MatchesInAnyOrientation(o, side));
                }

                return (CountMatches(Side.Top), CountMatches(Side.Bottom), CountMatches(Side.Left), CountMatches(Side.Right));
            }
        }

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            return -1;
        }
    }
}