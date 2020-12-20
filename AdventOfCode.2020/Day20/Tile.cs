using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2020.Day20
{
    internal class Tile
    {
        public long Id { get; set; }

        public Rotation Rotation { get; set; } = Rotation.None;

        public readonly List<List<bool>> Spaces = Enumerable.Range(0, 10)
            .Select(_ => Enumerable.Range(0, 10).Select(_ => false).ToList()).ToList();

        #region PreCalculatedForPerformance

        public readonly Oriented<long> EdgesAsLongs;

        // options arise from flipping each side
        public readonly Oriented<(long, long)> EdgeOptions;
        
        #endregion
        
        public Tile(IReadOnlyList<string> inputLines)
        {
            Id = long.Parse(inputLines[0].Split()[1].Replace(":", ""));
            for (var y = 0; y < 10; y++)
            {
                for (var x = 0; x < 10; x++)
                {
                    Spaces[y][x] = inputLines[y + 1][x] == '#';
                }
            }
            
            EdgesAsLongs = new Oriented<long>(ordinal => ordinal switch
            {
                Ordinal.North => EdgeAsLong(Row(0)),
                Ordinal.South => EdgeAsLong(Row(9)),
                Ordinal.West => EdgeAsLong(Column(0)),
                Ordinal.East => EdgeAsLong(Column(9)),
                _ => throw new ArgumentOutOfRangeException(nameof(ordinal))
            });
            
            EdgeOptions = new Oriented<(long, long)>(ordinal =>
            {
                var selfLongs = EdgesAsLongs;
                return ordinal switch
                {
                    Ordinal.North => (selfLongs.North, Reverse(selfLongs.North)),
                    Ordinal.South =>(selfLongs.South, Reverse(selfLongs.South)),
                    Ordinal.West =>(selfLongs.West, Reverse(selfLongs.West)),
                    Ordinal.East =>(selfLongs.East, Reverse(selfLongs.East)),
                    _ => throw new ArgumentOutOfRangeException(nameof(ordinal))
                };
            });
            
        }

        public int Count => Spaces.Sum(row => row.Count(c => c));

        public int CountExceptBorders()
        {
            return Spaces.Skip(1).Take(8).Sum(row => row.Skip(1).Take(8).Count(x => x));
        }

        public long EdgeAsLong(List<bool> edge)
        {
            var sRep = edge.Aggregate("", (current, c) => current + (c ? '1' : '0'));
            return Convert.ToInt64(sRep, 2);
        }

        public long Reverse(long edge)
        {
            var sRep = Convert.ToString(edge, 2);
            sRep = sRep.PadLeft(10, '0');
            var reversed = sRep.Reverse().CharsToString();
            return Convert.ToInt64(reversed, 2);
        }

        public List<bool> Row(int i) => Spaces[i];
        public List<bool> Column(int i) => Spaces.Select(l => l[i]).ToList();

        public static bool AnyPairwiseMatch((long, long) one, (long, long) two) =>
            one.Item1 == two.Item1 || one.Item1 == two.Item2 || one.Item2 == two.Item1 || one.Item2 == two.Item2;

        public int MatchesInAnyOrientation(Tile other, Ordinal ordinal)
        {
            var sides = EdgeOptions.Get(ordinal);
            return other.EdgeOptions.Map((otherOptions, _) => AnyPairwiseMatch(sides, otherOptions) ? 1 : 0)
                .ToList()
                .Sum();
        }

        /// <summary>
        /// North, South, West, East
        /// </summary>
        /// <param name="others"></param>
        /// <returns></returns>
        public Oriented<int> PotentialNeighbours(List<Tile> others)
        {
            others = others.Where(o => o.Id != Id).ToList();

            int CountMatches(Ordinal side)
            {
                return others.Sum(o => MatchesInAnyOrientation(o, side));
            }

            return new Oriented<int>(CountMatches);
        }

        public Oriented<List<long>> IdsOfPotentialNeighbours(List<Tile> others)
        {
            others = others.Where(o => o.Id != Id).ToList();

            List<long> NeighbourIds(Ordinal side)
            {
                return others.Where(o => MatchesInAnyOrientation(o, side) != 0).Select(o => o.Id).ToList();
            }

            return new Oriented<List<long>>(NeighbourIds);
        }
    }
}