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

        public enum Side
        {
            Top,
            Bottom,
            Left,
            Right
        }

        public enum Rotation
        {
            None,
            Right90,
            Right180,
            Left90
        }

        public static Side Rotate(this Side side, Rotation rotation) => rotation switch
        {
            Rotation.None => side,
            Rotation.Right90 => side switch
            {
                Side.Top => Side.Right,
                Side.Bottom => Side.Left,
                Side.Left => Side.Bottom,
                Side.Right => Side.Top,
            },
            Rotation.Right180 => side.Rotate(Rotation.Right90),
            Rotation.Left90 => side.Rotate(Rotation.Right180).Rotate(Rotation.Right90),
        };

        public enum Flipped2D
        {
            None, Horizontal, Vertical, Both
        }
        
        public class Oriented<T>
        {
            public T Top { get; set; }
            public T Bottom { get; set; }
            public T Left { get; set; }
            public T Right { get; set; }

            public Oriented(Func<Side, T> factory)
            {
                Top = factory(Side.Top);
                Bottom = factory(Side.Bottom);
                Left = factory(Side.Left);
                Right = factory(Side.Right);
            }

            public T Get(Side side) => side switch
            {
                Side.Top => Top,
                Side.Bottom => Bottom,
                Side.Left => Left,
                Side.Right => Right,
            };

            public Oriented<T> Rotate(Rotation rotation) => new Oriented<T>(side => Get(side.Rotate(rotation)));

            public List<T> ToList()
            {
                return new List<T> {Top, Bottom, Left, Right};
            }
        }

        public class Tile
        {
            public long Id { get; set; }

            public Rotation Rotation { get; set; } = Rotation.None;

            public List<List<bool>> spaces = Enumerable.Range(0, 10)
                .Select(_ => Enumerable.Range(0, 10).Select(_ => false).ToList()).ToList();

            public int Count => spaces.Sum(row => row.Count(c => c));

            public int CountExceptBorders()
            {
                return spaces.Skip(1).Take(8).Sum(row => row.Skip(1).Take(8).Count(x => x));
            }

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

            /// <summary>
            /// Top, Bottom, Left, Right
            /// </summary>
            /// <param name="others"></param>
            /// <returns></returns>
            public Oriented<int> PotentialNeighbours(List<Tile> others)
            {
                others = others.Where(o => o.Id != Id).ToList();

                int CountMatches(Side side)
                {
                    return others.Sum(o => MatchesInAnyOrientation(o, side));
                }

                return new Oriented<int>(CountMatches);
            }

            public Oriented<List<long>> IdsOfPotentialNeighbours(List<Tile> others)
            {
                others = others.Where(o => o.Id != Id).ToList();

                List<long> NeighbourIds(Side side)
                {
                    return others.Where(o => MatchesInAnyOrientation(o, side) != 0).Select(o => o.Id).ToList();
                }
                
                return new Oriented<List<long>>(NeighbourIds);
            }
        }

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var blocks = SplitExtension.Split(lines, "");
            var tiles = blocks.Select(b => new Tile(b.ToList())).ToList();

            var allEdges = tiles.SelectMany(t => t.Edges);

            var grouped = allEdges.GroupBy(e => e).OrderBy(x => x.Count()).ToList();

            var neighbours = tiles.Select(t => t.PotentialNeighbours(tiles));
            var edges = tiles.Where(t => t.PotentialNeighbours(tiles).ToList().Count(x => x == 0) >= 1);

            // edges.Count().Should().Be(44);

            var megaTile = new MegaTile(tiles);
            
            return -100;
        }

        public class MegaTile
        {
            public List<List<Tile>> grid = Enumerable.Range(0, 10)
                .Select(_ => Enumerable.Range(0, 10).Select(_ => null as Tile).ToList()).ToList();

            public List<Tile> PlacedPieces => grid.SelectMany(y => y.Select(x => x).ToList())
                .Where(p => p != null)
                .ToList();

            public List<long> PlacedIds => PlacedPieces.Select(p => p.Id).ToList();

            public MegaTile(List<Tile> tiles)
            {
                // tiles.Count().Should().Be(144);
                var corners = tiles.Where(t => t.PotentialNeighbours(tiles).ToList().Count(x => x == 0) == 2).ToList();
                var edges = tiles.Where(t => t.PotentialNeighbours(tiles).ToList().Count(x => x == 0) == 1).ToList();
                
                var maxCount = tiles.Sum(t => t.CountExceptBorders());

                var monsterCount = 15;

                for (int i = 32; i < 40; i++)
                {
                    Console.WriteLine($"Monsters: {i}, Answer: {maxCount - (monsterCount * i)}");
                }
                
                for (int y = 0; y < 10; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        // first corner
                        if ((x, y) == (0, 0))
                        {
                            var topLeft = corners.First();
                            var neighbours = topLeft.PotentialNeighbours(tiles);

                            switch ((neighbours.Top, neighbours.Left))
                            {
                                case (0,0):
                                    topLeft.Rotation = Rotation.None;
                                    break;
                            }
                            
                                grid[y][x] = topLeft;
                        }

                        // top row
                        else if (y == 0)
                        {
                            grid[y][x] = edges.Single(e => !PlacedIds.Contains(e.Id));
                        }
                    }
                }
            }
        }
    }
}