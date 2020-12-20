using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020.Day20
{
    internal class MegaTile
    {
        private readonly int Size;

        public MegaTile(List<Tile> tiles)
        {
            var size = Math.Sqrt(tiles.Count);
            if (size % 1 != 0)
            {
                throw new Exception("Expecting a square number of pieces for this puzzle!");
            }

            Size = (int) size;

            var grid = Enumerable.Range(0, Size)
                .Select(_ => Enumerable.Range(0, Size).Select(_ => null as Tile).ToList()).ToList();

            // tiles.Count().Should().Be(144);
            var corners = tiles.Where(t => t.PotentialNeighbours(tiles).ToList().Count(x => x == 0) == 2).ToList();
            var edges = tiles.Where(t => t.PotentialNeighbours(tiles).ToList().Count(x => x == 0) == 1).ToList();

            var placedTileIds = new HashSet<long>();

            Tile GetTile(int x, int y)
            {
                if (x < 0 || x >= Size || y < 0 || y >= Size)
                {
                    return null;
                }

                return grid[y][x];
            }

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Tile toPlace = null;

                    // first corner
                    if ((x, y) == (0, 0))
                    {
                        toPlace = corners.First();
                    }

                    else
                    {
                        var leftNeighbour = GetTile(x - 1, y);
                        var upNeighbour = GetTile(x, y - 1);
                        var isTop = y == 0;
                        var isBottom = y == Size - 1;
                        var isLeft = x == 0;
                        var isRight = x == Size - 1;

                        var isVerticalEdge = isLeft || isRight;
                        var isHorizontalEdge = isTop || isBottom;

                        var isEdge = isVerticalEdge || isHorizontalEdge;
                        var isCorner = isVerticalEdge && isHorizontalEdge;

                        if (leftNeighbour == null && upNeighbour == null)
                        {
                            throw new Exception(
                                $"Found un-placeable position (no-placed-neighbours)! This should only be the case for (0,0). Actually: ({x}, {y})");
                        }

                        toPlace = tiles.First(t => !placedTileIds.Contains(t.Id)
                                                   && (leftNeighbour == null || t.IsAdjacentTo(tiles, leftNeighbour.Id))
                                                   && (upNeighbour == null || t.IsAdjacentTo(tiles, upNeighbour.Id))
                                                   && (!isEdge || t.NumberOfNeighbours(tiles) <= 3)
                                                   && (!isCorner || t.NumberOfNeighbours(tiles) == 2)
                        );
                    }

                    if (toPlace == null)
                    {
                        throw new Exception($"Could not find a piece to place! ({x}, {y})");
                    }

                    grid[y][x] = toPlace;
                    placedTileIds.Add(toPlace.Id);
                }
            }

            Console.WriteLine("Placed all tiles.");
            // Placed all tiles, need to orient!
            var xppp = 0;

            bool IsOrientationCorrect(int x, int y, Tile tile = null)
            {
                // Can only look left and up, as when building grid.

                tile ??= GetTile(x, y);

                var leftNeighbour = GetTile(x - 1, y);
                var upNeighbour = GetTile(x, y - 1);
                // var rightNeighbour = GetTile(x + 1, y);
                // var downNeighbour = GetTile(x, y + 1);

                var leftNeighbourCorrect = leftNeighbour != null && tile.MatchesInSpecifiedOrientation(leftNeighbour, Ordinal.Left, Ordinal.Right);
                var upNeighbourCorrect = upNeighbour != null && tile.MatchesInSpecifiedOrientation(upNeighbour, Ordinal.Up, Ordinal.Down);

                var leftEdgeCorrect = leftNeighbour == null && tile.NumberOfNeighbours(tiles, Ordinal.Left) == 0;
                var upEdgeCorrect = upNeighbour == null && tile.NumberOfNeighbours(tiles, Ordinal.Up) == 0;

                return (leftNeighbourCorrect || leftEdgeCorrect) && (upNeighbourCorrect || upEdgeCorrect);
            }

            var operationOrder = new[]
            {
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
                (Rotation.Right90, Flip2D.Vertical),
            };

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    var tile = GetTile(x, y);
                    var toPlace = tile.Rotate(Rotation.None);
                    var clone = tile.Rotate(Rotation.None);
                    var operationId = 0;
                    
                    while (!IsOrientationCorrect(x, y, toPlace))
                    {
                        if (operationId >= operationOrder.Length)
                        {
                            throw new Exception($"Ran out of possibilities to tweak orientation! ({x}, {y})");
                        }
                        var (rotate, flip) = operationOrder[operationId];
                        toPlace = clone.Rotate(rotate).Flip(flip);
                        operationId++;
                    }
                    
                    grid[y][x] = toPlace;
                }
            }
            
            Console.WriteLine("Oriented pieces.");
        }
    }
}