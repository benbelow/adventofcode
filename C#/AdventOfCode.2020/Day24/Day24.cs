using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2020.Day24
{
    public static class Day24
    {
        private const int Day = 24;

        public enum Direction
        {
            East,
            SouthEast,
            SouthWest,
            West,
            NorthWest,
            NorthEast
        }

        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();

            var instructions = lines.Select(ParseLine);

            // start white, so false = white
            var visited = new Dictionary<(int, int), bool>();
            var coord = (0, 0);

            void Flip((int, int) coord)
            {
                var existing = visited.ContainsKey(coord) && visited[coord];
                visited[coord] = !existing;
            }


            foreach (var instructionSet in instructions)
            {
                coord = (0, 0);

                var visitedDebug = new HashSet<(int, int)>();

                foreach (var instruction in instructionSet)
                {
                    switch (instruction)
                    {
                        case Direction.East:
                            coord = (coord.Item1 + 1, coord.Item2);
                            break;
                        case Direction.SouthEast:
                            coord = (coord.Item1 + 1, coord.Item2 - 1);
                            break;
                        case Direction.SouthWest:
                            coord = (coord.Item1, coord.Item2 - 1);
                            break;
                        case Direction.West:
                            coord = (coord.Item1 - 1, coord.Item2);
                            break;
                        case Direction.NorthWest:
                            coord = (coord.Item1 - 1, coord.Item2 + 1);
                            break;
                        case Direction.NorthEast:
                            coord = (coord.Item1, coord.Item2 + 1);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                visitedDebug.Add(coord);
                Flip(coord);
            }

            return visited.Count(x => x.Value);
        }

        public static IEnumerable<Direction> ParseLine(string line)
        {
            var skip = false;
            for (int i = 0; i < line.Length; i++)
            {
                if (skip)
                {
                    skip = false;
                    continue;
                }

                var c = line[i];
                var nextC = i < line.Length - 1 ? line[i + 1] : 'x';

                switch (c)
                {
                    case 's':
                        skip = true;
                        switch (nextC)
                        {
                            case 'w':
                                yield return Direction.SouthWest;
                                break;
                            case 'e':
                                yield return Direction.SouthEast;
                                break;
                        }

                        break;
                    case 'n':
                        skip = true;
                        switch (nextC)
                        {
                            case 'w':
                                yield return Direction.NorthWest;
                                break;
                            case 'e':
                                yield return Direction.NorthEast;
                                break;
                        }

                        break;
                    case 'e':
                        yield return Direction.East;
                        break;
                    case 'w':
                        yield return Direction.West;
                        break;
                }
            }
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();

            var instructions = lines.Select(ParseLine);

            // start white, so false = white
            var visited = new Dictionary<(int, int), bool>();

            void Flip((int, int) coord)
            {
                var existing = GetAt(coord);
                visited[coord] = !existing;
            }

            bool GetAt((int, int) c)
            {
                return visited.ContainsKey(c) && visited[c];
            }

            bool ShouldFlip((int, int) c)
            {
                var existing = GetAt(c);

                var neighbours = new[]
                {
                    GetAt((c.Item1 + 1, c.Item2 - 1)),
                    GetAt((c.Item1 - 1, c.Item2 + 1)),
                    GetAt((c.Item1 + 1, c.Item2)),
                    GetAt((c.Item1 - 1, c.Item2)),
                    GetAt((c.Item1, c.Item2 + 1)),
                    GetAt((c.Item1, c.Item2 - 1)),
                };

                var nCount = neighbours.Count(x => x);

                if (existing)
                {
                    return nCount == 0 || nCount > 2;
                }
                else
                {
                    return nCount == 2;
                }
            }

            Dictionary<(int, int), bool> CalculateFlips(HashSet<(int, int)> toFlip)
            {
                var extraNeighbours = new HashSet<(int, int)>();
                foreach (var c in toFlip)
                {
                    extraNeighbours.Add((c.Item1 + 1, c.Item2 - 1));
                    extraNeighbours.Add((c.Item1 - 1, c.Item2 + 1));
                    extraNeighbours.Add((c.Item1 + 1, c.Item2));
                    extraNeighbours.Add((c.Item1 - 1, c.Item2));
                    extraNeighbours.Add((c.Item1, c.Item2 + 1));
                    extraNeighbours.Add((c.Item1, c.Item2 - 1));
                }
                
                return toFlip.Concat(extraNeighbours).ToHashSet().ToDictionary(x => x, ShouldFlip);
            }

            // initial set up
            foreach (var instructionSet in instructions)
            {
                var initialSetUpCoord = (0, 0);

                var visitedDebug = new HashSet<(int, int)>();

                foreach (var instruction in instructionSet)
                {
                    switch (instruction)
                    {
                        case Direction.East:
                            initialSetUpCoord = (initialSetUpCoord.Item1 + 1, initialSetUpCoord.Item2);
                            break;
                        case Direction.SouthEast:
                            initialSetUpCoord = (initialSetUpCoord.Item1 + 1, initialSetUpCoord.Item2 - 1);
                            break;
                        case Direction.SouthWest:
                            initialSetUpCoord = (initialSetUpCoord.Item1, initialSetUpCoord.Item2 - 1);
                            break;
                        case Direction.West:
                            initialSetUpCoord = (initialSetUpCoord.Item1 - 1, initialSetUpCoord.Item2);
                            break;
                        case Direction.NorthWest:
                            initialSetUpCoord = (initialSetUpCoord.Item1 - 1, initialSetUpCoord.Item2 + 1);
                            break;
                        case Direction.NorthEast:
                            initialSetUpCoord = (initialSetUpCoord.Item1, initialSetUpCoord.Item2 + 1);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                visitedDebug.Add(initialSetUpCoord);
                Flip(initialSetUpCoord);
            }

            void Tick()
            {
                var flips = CalculateFlips(visited.Keys.ToHashSet());

                foreach (var flip in flips)
                {
                    if (flip.Value)
                    {
                        Flip(flip.Key);
                    }
                }
            }


            for (int i = 0; i < 100; i++)
            {
                Tick();
            }

            return visited.Count(x => x.Value);
        }
    }
}