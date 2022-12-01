using System;
using System.Linq;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

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

        public static Direction Left(Direction x) => Right(Right(Right(x)));
        
        public static (int, int) Translate((int, int) original, int value, Direction direction)
        {
            var (x, y) = original;
            return direction switch
            {
                Direction.North => (x, y + value),
                Direction.South => (x, y - value),
                Direction.East => (x + value, y),
                Direction.West => (x - value, y),
            };
        }
        
        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var instructions = lines.Select(l => (l.First(), int.Parse(l.Skip(1).CharsToString())));

            var ship = (0, 0);
            var shipDirection = Direction.East;

            foreach (var (instruction, value) in instructions)
            {
                void MoveShip(Direction dir)
                {
                    ship = Translate(ship, value, dir);
                }

                switch (instruction)
                {
                    case 'N':
                        MoveShip(Direction.North);
                        break;
                    case 'S':
                        MoveShip(Direction.South);
                        break;
                    case 'E':
                        MoveShip(Direction.East);
                        break;
                    case 'W':
                        MoveShip(Direction.West);
                        break;
                    case 'R':
                        for (int j = 0; j < value; j += 90)
                        {
                            shipDirection = Right(shipDirection);
                        }
                        break;
                    case 'L':
                        for (int j = 0; j < value; j += 90)
                        {
                            shipDirection = Left(shipDirection);
                        }
                        break;

                    case 'F':
                        MoveShip(shipDirection);
                        break;
                }
            }

            return Math.Abs(ship.Item1) + Math.Abs(ship.Item2);
        }

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var instructions = lines.Select(l => (l.First(), int.Parse(new string(l.Skip(1).ToArray()))));

            var ship = (0, 0);
            var waypoint = (10, 1);
            
            foreach (var (instruction, value) in instructions)
            {
                void MoveWaypoint(Direction dir)
                {
                    waypoint = Translate(waypoint, value, dir);
                }
                
                switch (instruction)
                {
                    case 'N':
                        MoveWaypoint(Direction.North);
                        break;
                    case 'S':
                        MoveWaypoint(Direction.South);
                        break;
                    case 'E':
                        MoveWaypoint(Direction.East);
                        break;
                    case 'W':
                        MoveWaypoint(Direction.West);
                        break;
                    case 'R':
                        for (int j = 0; j < value; j += 90)
                        {
                            waypoint = Right2(waypoint);
                        }
                        break;
                    case 'L':
                        for (int j = 0; j < value; j += 90)
                        {
                            waypoint = Left2(waypoint);
                        }
                        break;

                    case 'F':
                        for (int j = 0; j < value; j++)
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