using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2019.Common;
using AdventOfCode._2019.Common.Models;

namespace AdventOfCode._2019.Day10
{
    public static class Day10
    {
        public static int Part1()
        {
            var lines = FileReader.ReadInputLines(10);
            return NumberOfAsteroidsFromBestLocation(lines);
        }
        
        public static int Part2()
        {
            var lines = FileReader.ReadInputLines(10);
            var nthVaporisedAsteroid = GetNthVaporisedAsteroid(lines, 200);
            return (nthVaporisedAsteroid.X * 100) + nthVaporisedAsteroid.Y;
        }

        public static Coordinate GetNthVaporisedAsteroid(IEnumerable<string> lines, int n = 200)
        {
            var asteroids = ParseAsteroids(lines.ToList());
            var bestAsteroid = asteroids.Values.OrderByDescending(a => a.LinesOfSight(asteroids)).First();
            return bestAsteroid.OrderedDestruction(asteroids.Values)[n-1].Coordinate;
        }

        public static int NumberOfAsteroidsFromBestLocation(IEnumerable<string> lines)
        {
            var asteroids = ParseAsteroids(lines.ToList());

            return asteroids.Values.Select(a => a.LinesOfSight(asteroids)).Max();
        }

        private static Dictionary<Coordinate, Asteroid> ParseAsteroids(IReadOnlyList<string> lines)
        {
            var asteroids = new Dictionary<Coordinate, Asteroid>();

            for (var y = 0; y < lines.Count; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] == '#')
                    {
                        var asteroid = new Asteroid(x, y);
                        asteroids.Add(asteroid.Coordinate, asteroid);
                    }
                }
            }

            return asteroids;
        }

        public static List<Coordinate> GetCoordinatesInLineOfSight(Coordinate a, Coordinate b)
        {
            var lowerX = Math.Min(a.X, b.X);
            var higherX = Math.Max(a.X, b.X);
            var lowerY = Math.Min(a.Y, b.Y);
            var higherY = Math.Max(a.Y, b.Y);

            // same coord
            if (a.X == b.X && a.Y == b.Y)
            {
                return new List<Coordinate>();
            }

            // vertical line
            if (a.X == b.X)
            {
                return Enumerable.Range(lowerY, higherY - lowerY + 1).Select(y => new Coordinate(a.X, y)).ToList();
            }

            // horizontal line
            if (a.Y == b.Y)
            {
                return Enumerable.Range(lowerX, higherX - lowerX + 1).Select(x => new Coordinate(x, a.Y)).ToList();
            }

            var m = decimal.Divide(a.Y - b.Y, a.X - b.X);
            var c = a.Y - (m * a.X);

            decimal GetY(decimal x) => (m * x) + c;
            decimal GetX(decimal y) => decimal.Divide(y - c, m);

            var coordinatesInLineOfSightX = Enumerable.Range(lowerX, higherX - lowerX + 1).SelectMany(x =>
                {
                    var y = Math.Round(GetY(x), 4);
                    return y % 1 == 0
                        ? new List<Coordinate> {new Coordinate(x, (int) y)}
//                        : new[] {new Coordinate(x, (int) Math.Floor(y)), new Coordinate(x, (int) Math.Ceiling(y))};
                        : new List<Coordinate> { };
                })
                .Where(x => x != null)
                .ToList();
            var coordinatesInLineOfSightY = Enumerable.Range(lowerY, higherY - lowerY + 1).SelectMany(y =>
                {
                    var x = Math.Round(GetX(y), 4);
                    return x % 1 == 0
                        ? new List<Coordinate> {new Coordinate((int) x, y)}
//                        : new List<Coordinate> {new Coordinate((int) Math.Floor(x), y), new Coordinate((int) Math.Ceiling(x), y)};
                        : new List<Coordinate>();
                })
                .Where(x => x != null)
                .ToList();

            return coordinatesInLineOfSightX.Concat(coordinatesInLineOfSightY)
                .OrderBy(co => Math.Abs(co.X - a.X) + Math.Abs(co.Y - a.Y))
                .ToList();
        }

        public class Asteroid
        {
            public Asteroid(int x, int y)
            {
                Coordinate = new Coordinate(x, y);
            }

            public Coordinate Coordinate { get; private set; }

            public int X => Coordinate.X;
            public int Y => Coordinate.Y;

            public double AngleTo(Coordinate target)
            {
                const double halfPi = (Math.PI / 2);
                var xDiff = target.X - X;
                var yDiff = Y - target.Y;
                var atan = xDiff == 0 ? halfPi : Math.Atan((double) (Math.Abs(target.X - X)) / (Math.Abs(target.Y - Y)));
                return (xDiff, yDiff) switch
                {
                    (0, _) when yDiff > 0 => 0,
                    (0, _) when yDiff < 0 => halfPi * 2,
                    (_, _) when yDiff >= 0 && xDiff >= 0 => atan,
                    (_, _) when yDiff < 0 && xDiff >= 0 => atan + halfPi,
                    (_, _) when yDiff >= 0 && xDiff < 0 => atan + 3 * halfPi,
                    (_, _) when yDiff < 0 && xDiff < 0 => atan + 2 * halfPi,
                };
            }

            public double DistanceTo(Coordinate target)
            {
                return Math.Abs(X - target.X) + Math.Abs(Y - target.Y);
            }
            
            public bool IsBlockedBy(Asteroid target, Asteroid potentialBlock) => IsBlockedBy(target.Coordinate, potentialBlock.Coordinate);

            public bool IsBlockedBy(Coordinate target, Coordinate potentialBlock)
            {
                var xDiffTarget = (target.X - X);
                var xDiffBlock = (potentialBlock.X - X);
                var yDiffTarget = (target.Y - Y);
                var yDiffBlock = (potentialBlock.Y - Y);

                var isFurtherAway = Math.Abs(xDiffTarget) >= Math.Abs(xDiffBlock) && Math.Abs(yDiffTarget) >= Math.Abs(yDiffBlock);

                var isSameDirection = Math.Sign(xDiffTarget) == Math.Sign(xDiffBlock) && Math.Sign(yDiffTarget) == Math.Sign(yDiffBlock);

                var targetXAligned = X == target.X;
                var blockXAligned = X == potentialBlock.X;
                var targetYAligned = Y == target.Y;
                var blockYAligned = Y == potentialBlock.Y;

                var isAlignedAlongAxis = (targetXAligned && blockXAligned) || (targetYAligned && blockYAligned);
                var isMisaligned = xDiffBlock == 0 ^ xDiffTarget == 0 || yDiffBlock == 0 ^ yDiffTarget == 0;
                var isAlignedNotAlongAxis = !isMisaligned && !isAlignedAlongAxis && xDiffTarget % xDiffBlock == 0 && yDiffTarget % yDiffBlock == 0
                                            && xDiffTarget / xDiffBlock == yDiffTarget / yDiffBlock;

                return isSameDirection && isFurtherAway && !isMisaligned && (isAlignedAlongAxis || isAlignedNotAlongAxis);
            }

            public int LinesOfSight(Dictionary<Coordinate, Asteroid> asteroids)
            {
                var otherAsteroids = asteroids.Where(a => a.Value != this).ToDictionary(x => x.Key, x => x.Value);

//                var lineOfSightAsteroids = otherAsteroids
//                    .Values
//                    .OrderBy(a => Math.Abs(X - a.X) + Math.Abs(Y - a.Y))
//                    .Aggregate<Asteroid, IEnumerable<Asteroid>>(new List<Asteroid>(), (blocks, asteroid) =>
//                    {
//                        blocks = blocks.ToList();
//                        return blocks.Any(b => IsBlockedBy(asteroid, b))
//                            ? blocks
//                            : blocks.Concat(new List<Asteroid> {asteroid});
//                    });

//                LineOfSightCount = lineOfSightAsteroids.Count();

                return otherAsteroids.Values.Select(o => AngleTo(o.Coordinate)).Distinct().Count();
            }

            public Dictionary<double, IOrderedEnumerable<Asteroid>> AsteroidsByAngle(IEnumerable<Asteroid> asteroids)
            {
                var otherAsteroids = asteroids.Where(a => a != this);
                return otherAsteroids
                    .GroupBy(o => AngleTo(o.Coordinate))
                    .ToDictionary(o => o.Key, o => o.OrderBy(x => DistanceTo(x.Coordinate)));
            }

            public IList<Asteroid> OrderedDestruction(IEnumerable<Asteroid> asteroids)
            {
                var lookup = AsteroidsByAngle(asteroids).OrderBy(l => l.Key).ToDictionary(x => x.Key, x => x.Value);
                var max = lookup.Values.Select(l => l.Count()).Max();

                return lookup.Select(l => l.Value.First()).ToList();
                
//                return Enumerable.Range(0, max).Aggregate<int, IEnumerable<Asteroid>>(new List<Asteroid>(), (roids, i) =>
//                        roids.Concat(lookup.Select(l => l.Value.ElementAtOrDefault(i)).Where(x => x != null))
//                ).ToList();
            }
        }
    }
}