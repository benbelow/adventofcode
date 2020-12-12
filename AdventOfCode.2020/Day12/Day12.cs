using System;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2020.Day12
{
    public static class Day12
    {
        private const int Day = 12;

        public enum Direction
        {
            North,
            South,
            East,
            West
        }

        public static Direction Right(Direction x)
        {
            return x switch
            {
                Direction.North => Direction.East,
                Direction.South => Direction.West,
                Direction.East => Direction.South,
                Direction.West => Direction.North,
            };
        }

        public static Direction Left(Direction x)
        {
            return x switch
            {
                Direction.North => Direction.West,
                Direction.South => Direction.East,
                Direction.East => Direction.North,
                Direction.West => Direction.South,
            };
        }

        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var deets = lines.Select(l => (l.First(), int.Parse(new string(l.Skip(1).ToArray()))));

            var coord = (0, 0);
            var direction = Direction.East;
            foreach (var i in deets)
            {
                switch (i.Item1)
                {
                    case 'N':
                        coord = (coord.Item1, coord.Item2 + i.Item2);
                        break;

                    case 'S':
                        coord = (coord.Item1, coord.Item2 - i.Item2);
                        break;

                    case 'E':
                        coord = (coord.Item1 + i.Item2, coord.Item2);
                        break;

                    case 'W':
                        coord = (coord.Item1 - i.Item2, coord.Item2);
                        break;

                    case 'R':
                        for (int j = 0; j < i.Item2; j += 90)
                        {
                            direction = Right(direction);
                        }

                        break;
                    case 'L':
                        for (int j = 0; j < i.Item2; j += 90)
                        {
                            direction = Left(direction);
                        }

                        break;

                    case 'F':
                        switch (direction)
                        {
                            case Direction.North:
                                coord = (coord.Item1, coord.Item2 + i.Item2);
                                break;
                            case Direction.South:
                                coord = (coord.Item1, coord.Item2 - i.Item2);
                                break;
                            case Direction.East:
                                coord = (coord.Item1 + i.Item2, coord.Item2);
                                break;
                            case Direction.West:
                                coord = (coord.Item1 - i.Item2, coord.Item2);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        break;
                }
            }

            return Math.Abs(coord.Item1) + Math.Abs(coord.Item2);
        }

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var deets = lines.Select(l => (l.First(), int.Parse(new string(l.Skip(1).ToArray()))));

            var ship = (0, 0);
            var waypoint = (10, 1);
            foreach (var i in deets)
            {
                switch (i.Item1)
                {
                    case 'N':
                        waypoint = (waypoint.Item1, waypoint.Item2 + i.Item2);
                        break;

                    case 'S':
                        waypoint = (waypoint.Item1, waypoint.Item2 - i.Item2);
                        break;

                    case 'E':
                        waypoint = (waypoint.Item1 + i.Item2, waypoint.Item2);
                        break;

                    case 'W':
                        waypoint = (waypoint.Item1 - i.Item2, waypoint.Item2);
                        break;

                    case 'R':
                        for (int j = 0; j < i.Item2; j += 90)
                        {
                            waypoint = Right2(waypoint);
                        }

                        break;
                    case 'L':
                        for (int j = 0; j < i.Item2; j += 90)
                        {
                            waypoint = Left2(waypoint);
                        }

                        break;

                    case 'F':
                        for (int j = 0; j < i.Item2; j++)
                        {
                            ship = (ship.Item1 + waypoint.Item1, ship.Item2 + waypoint.Item2);
                        }

                        break;
                }
            }

            return Math.Abs(ship.Item1) + Math.Abs(ship.Item2);
        }

        private static (int, int) Right2((int, int) way)
        {
            return (way.Item2, -way.Item1);
        }

        private static (int, int) Left2((int, int) way)
        {
            return Right2(Right2(Right2(way)));
        }
    }
}