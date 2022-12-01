using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2019.Common;

namespace AdventOfCode._2019.Day03
{
    public static class Day03
    {
        public static int Part1()
        {
            var lines = FileReader.ReadInputLines(3).ToList();
            return GetClosestIntersection(lines.First(), lines.Last());
        }
        
        public static int Part2()
        {
            var lines = FileReader.ReadInputLines(3).ToList();
            return GetFewestStepsToCrossOver(lines.First(), lines.Last());
        }

        public static int GetClosestIntersection(string wireInstructions1, string wireInstructions2)
        {
            var wire1 = BuildWire(wireInstructions1.Split(','));
            var wire2 = BuildWire(wireInstructions2.Split(','));
            var crossovers = GetCrossoverPoints(wire1, wire2);
            return crossovers.Select(c => Math.Abs(c.Item1) + Math.Abs(c.Item2)).Where(d => d > 0).Min();
        }

        public static int GetFewestStepsToCrossOver(string wireInstructions1, string wireInstructions2)
        {
            var wire1 = BuildWire(wireInstructions1.Split(','));
            var wire2 = BuildWire(wireInstructions2.Split(','));
            var crossovers = GetCrossoverPoints(wire1, wire2);

            return crossovers
                .Select(c => wire1.DistanceTravelledAt(c.Item1, c.Item2) + wire2.DistanceTravelledAt(c.Item1, c.Item2))
                .Where(s => s > 0)
                .Min();
        }

        /// <returns>
        /// </returns>
        private static Wire BuildWire(IEnumerable<string> wireInstructions)
        {
            var wire = new Wire();
            foreach (var instruction in wireInstructions)
            {
                var direction = instruction.First();
                var distance = int.Parse(instruction.Substring(1));

                switch (direction)
                {
                    case 'D':
                        wire.MoveVertical(-distance);
                        break;
                    case 'U':
                        wire.MoveVertical(distance);
                        break;
                    case 'R':
                        wire.MoveHorizontal(distance);
                        break;
                    case 'L':
                        wire.MoveHorizontal(-distance);
                        break;
                    default:
                        throw new Exception($"Unrecognised instruction: {direction}");
                }
            }

            return wire;
        }

        private static IEnumerable<Tuple<int, int>> GetCrossoverPoints(Wire wire1, Wire wire2)
        {
            return wire1.AllVisitedCoords().Where(c => wire2.Visited(c.Item1, c.Item2));
        }

        private class Wire
        {
            /// Nested Dictionary
            /// Key1 = x
            /// Key2 = y
            /// Value = number of steps to get here
            private readonly IDictionary<int, IDictionary<int, int?>> visitedCoordinates = new Dictionary<int, IDictionary<int, int?>>();

            private int xCoord = 0;
            private int yCoord = 0;
            private int distanceTravelled = 0;

            public void MoveHorizontal(int xMove)
            {
                var newX = xCoord + xMove;

                if (xMove < 0)
                {
                    for (var i = xCoord - 1; i >= newX; i--)
                    {
                        Visit(i, yCoord);
                    }
                }

                if (xMove > 0)
                {
                    for (var i = xCoord + 1; i <= newX; i++)
                    {
                        Visit(i, yCoord);
                    }
                }
                
                xCoord = newX;
            }

            public void MoveVertical(int yMove)
            {
                var newY = yCoord + yMove;
                if (yMove < 0)
                {
                    for (var i = yCoord - 1; i >= newY; i--)
                    {
                        Visit(xCoord, i);
                    }
                }

                if (yMove > 0)
                {
                    for (var i = yCoord + 1; i <= newY; i++)
                    {
                        Visit(xCoord, i);
                    }
                }
                yCoord = newY;
            }

            private void Visit(int x, int y)
            {
                distanceTravelled++;
                
                visitedCoordinates.TryGetValue(x, out var yCoords);
                if (yCoords == null)
                {
                    visitedCoordinates[x] = new Dictionary<int, int?> {{y, distanceTravelled}};
                }
                else
                {
                    yCoords.TryGetValue(y, out var visited);
                    if (visited == null)
                    {
                        yCoords[y] = distanceTravelled;
                    }
                }
            }

            public bool Visited(int x, int y)
            {
                visitedCoordinates.TryGetValue(x, out var yCoords);
                if (yCoords == null)
                {
                    return false;
                }

                yCoords.TryGetValue(y, out var visited);
                return visited != null;
            }

            public IEnumerable<Tuple<int, int>> AllVisitedCoords()
            {
                return visitedCoordinates.SelectMany(x => x.Value.Select(y => new Tuple<int, int>(x.Key, y.Key)));
            }

            public int DistanceTravelledAt(int x, int y)
            {
                if (!Visited(x, y))
                {
                    throw new Exception($"Cannot work out distance travelled at a point the wire did not cross: {x}, {y}");
                }

                return visitedCoordinates[x][y] ?? throw new Exception($"No distance stored for coordinates: {x}, {y}");
            }
        }
    }
}