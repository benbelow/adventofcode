using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2019.Common;

namespace AdventOfCode._2019.Day3
{
    public static class Day3
    {
        public static int Part1()
        {
            var lines = FileReader.ReadInputLines(3).ToList();
            return GetClosestIntersection(lines.First(), lines.Last());
        }

        public static int GetClosestIntersection(string wireInstructions1, string wireInstructions2)
        {
            var wire1 = BuildWire(wireInstructions1.Split(","));
            var wire2 = BuildWire(wireInstructions2.Split(","));
            var crossovers = GetCrossoverPoints(wire1, wire2);
            return crossovers.Select(c => Math.Abs(c.Item1) + Math.Abs(c.Item2)).Where(d => d > 0).Min();
        }

        public static int GetFewestStepsToCrossOver(string wireInstructions1, string wireInstructions2)
        {
            var wire1 = BuildWire(wireInstructions1.Split(","));
            var wire2 = BuildWire(wireInstructions2.Split(","));
            var crossovers = GetCrossoverPoints(wire1, wire2);

            return crossovers
                .Select(c => wire1.DistanceTravelledAt(c.Item1, c.Item2) + wire2.DistanceTravelledAt(c.Item1, c.Item2))
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
                VisitHorizontalRange(yCoord, Math.Min(newX, xCoord), Math.Max(newX, xCoord));
                xCoord = newX;
            }

            public void MoveVertical(int yMove)
            {
                var newY = yCoord + yMove;
                VisitVerticalRange(xCoord, Math.Min(newY, yCoord), Math.Max(newY, yCoord));
                yCoord = newY;
            }

            private void VisitHorizontalRange(int y, int xMin, int xMax)
            {
                for (var i = xMin; i <= xMax; i++)
                {
                    Visit(i, y);
                }
            }

            private void VisitVerticalRange(int x, int yMin, int yMax)
            {
                for (var i = yMin; i <= yMax; i++)
                {
                    Visit(x, i);
                }
            }

            private void Visit(int x, int y)
            {
                visitedCoordinates.TryGetValue(x, out var yCoords);
                if (yCoords == null)
                {
                    visitedCoordinates[x] = new Dictionary<int, int?> {{y, distanceTravelled}};
                }
                else
                {
                    yCoords.TryGetValue(x, out var visited);
                    if (visited == null)
                    {
                        yCoords[y] = distanceTravelled;
                    }
                }

                distanceTravelled++;
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