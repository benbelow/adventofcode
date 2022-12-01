using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoreLinq;

namespace AdventOfCode._2020.Day20
{
    internal class MegaTile
    {
        public List<List<bool>> Spaces;

        public int Size => Spaces.Count;

        public MegaTile(List<Tile> tiles)
        {
            // Assume all tiles same size! Known for this puzzle.
            var tileSize = tiles.First().Size;

            var sizeDouble = Math.Sqrt(tiles.Count);
            if (sizeDouble % 1 != 0)
            {
                throw new Exception("Expecting a square number of pieces for this puzzle!");
            }

            var size = (int) sizeDouble;

            var grid = Enumerable.Range(0, size)
                .Select(_ => Enumerable.Range(0, size).Select(_ => null as Tile).ToList()).ToList();

            // tiles.Count().Should().Be(144);
            var corners = tiles.Where(t => t.PotentialNeighbours(tiles).ToList().Count(x => x == 0) == 2).ToList();
            var edges = tiles.Where(t => t.PotentialNeighbours(tiles).ToList().Count(x => x == 0) == 1).ToList();

            var placedTileIds = new HashSet<long>();

            Tile GetTile(int x, int y)
            {
                if (x < 0 || x >= size || y < 0 || y >= size)
                {
                    return null;
                }

                return grid[y][x];
            }

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
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
                        var isBottom = y == size - 1;
                        var isLeft = x == 0;
                        var isRight = x == size - 1;

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

            bool IsOrientationCorrect(int x, int y, Tile tile = null)
            {
                // Can only look left and up, as when building grid.

                tile ??= GetTile(x, y);

                var leftNeighbour = GetTile(x - 1, y);
                var upNeighbour = GetTile(x, y - 1);

                var leftNeighbourCorrect = leftNeighbour != null && tile.MatchesInSpecifiedOrientation(leftNeighbour, Ordinal.Left, Ordinal.Right);
                var upNeighbourCorrect = upNeighbour != null && tile.MatchesInSpecifiedOrientation(upNeighbour, Ordinal.Up, Ordinal.Down);

                var leftEdgeCorrect = leftNeighbour == null && tile.NumberOfNeighbours(tiles, Ordinal.Left) == 0;
                var upEdgeCorrect = upNeighbour == null && tile.NumberOfNeighbours(tiles, Ordinal.Up) == 0;

                return (leftNeighbourCorrect || leftEdgeCorrect) && (upNeighbourCorrect || upEdgeCorrect);
            }

            var operationOrder = new[]
            {
                (Rotation.Left90, Flip2D.None),
                (Rotation.None, Flip2D.Vertical),
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

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
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

            var gridSize = (tileSize - 2) * size;
            var emptyGrid = Enumerable.Range(0, gridSize)
                .Select(_ => Enumerable.Range(0, gridSize).Select(_ => false).ToList()).ToList();

            var trimmedGrid = grid.Select(row => row.Select(tile => tile.TrimEdges()).ToList()).ToList();
            var trimmedTileSize = trimmedGrid.First().First().Size;

            for (int outerX = 0; outerX < size; outerX++)
            {
                for (int outerY = 0; outerY < size; outerY++)
                {
                    var tile = trimmedGrid[outerY][outerX];

                    for (int innerX = 0; innerX < trimmedTileSize; innerX++)
                    {
                        for (int innerY = 0; innerY < trimmedTileSize; innerY++)
                        {
                            var cell = tile.GetCell(innerX, innerY);

                            var newX = innerX + (outerX * trimmedTileSize);
                            var newY = innerY + (outerY * trimmedTileSize);

                            emptyGrid[newY][newX] = cell;
                        }
                    }
                }
            }
            
            Console.WriteLine("Stripped edges.");

            Spaces = emptyGrid;
        }

        public MegaTile(List<List<bool>> spaces)
        {
            Spaces = spaces;
        }

        public MegaTile(string debugInput)
        {
            var lines = debugInput.Split(Environment.NewLine);
            var grid = BuildEmptyGrid(lines.Length);

            for (int x = 0; x < lines.Length; x++)
            {
                for (int y = 0; y < lines.Length; y++)
                {
                    grid[y][x] = lines[y][x] == '#';
                }
            }

            Spaces = grid;
        }
        
        // OOB = false
        private bool GetCell(int x, int y)
        {
            if (x < 0 || x >= Spaces.Count() || y < 0 || y >= Spaces.Count())
            {
                return false;
            }

            return Spaces[y][x];
        }

        public bool IsMonsterBoundingBoxTopLeft(int x, int y)
        {
            var monsterDebug = @"
..................#.
#....##....##....###
.#..#..#..#..#..#...
";

            return GetCell(x + 18, y)
                   && GetCell(x, y + 1)
                   && GetCell(x + 5, y + 1)
                   && GetCell(x + 6, y + 1)
                   && GetCell(x + 11, y + 1)
                   && GetCell(x + 12, y + 1)
                   && GetCell(x + 17, y + 1)
                   && GetCell(x + 18, y + 1)
                   && GetCell(x + 19, y + 1)
                   && GetCell(x + 1, y + 2)
                   && GetCell(x + 4, y + 2)
                   && GetCell(x + 7, y + 2)
                   && GetCell(x + 10, y + 2)
                   && GetCell(x + 13, y + 2)
                   && GetCell(x + 16, y + 2);
        }

        public int NumberOfMonsters()
        {
            var monsters = 0;

            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if (IsMonsterBoundingBoxTopLeft(x, y))
                    {
                        monsters++;
                    }
                }
            }

            return monsters;
        }

        // Assume monsters don't overlap
        public int CountOfNonMonsterTiles()
        {
            return Spaces.Sum(row => row.Count(x => x)) - NumberOfMonsters() * 15;
        }
        
        public MegaTile Rotate(Rotation rotation)
        {
            var newGrid = rotation switch
            {
                Rotation.None => Spaces,
                Rotation.Right90 => Right90(Spaces),
                Rotation.Right180 => Right90(Right90(Spaces)),
                Rotation.Left90 => Right90(Right90(Right90(Spaces))),
                _ => throw new ArgumentOutOfRangeException(nameof(rotation), rotation, null)
            };

            return new MegaTile(newGrid);
        }

        public MegaTile Flip(Flip2D flip2D)
        {
            var newGrid = flip2D switch
            {
                Flip2D.None => Spaces,
                Flip2D.Horizontal => FlipHorizontal(Spaces),
                Flip2D.Vertical => FlipVertical(Spaces),
                _ => throw new ArgumentOutOfRangeException(nameof(flip2D))
            };
            return new MegaTile(newGrid);
        }
        
        public List<List<bool>> Right90(List<List<bool>> original)
        {
            var rotated = BuildEmptyGrid(Size);

            for (var y = 0; y < Size; y++)
            {
                for (var x = 0; x < Size; x++)
                {
                    rotated[y][x] = original[Size - x - 1][y];
                }
            }

            return rotated;
        }

        public List<List<bool>> FlipHorizontal(List<List<bool>> original)
        {
            var rotated = BuildEmptyGrid(Size);

            for (var y = 0; y < Size; y++)
            {
                for (var x = 0; x < Size; x++)
                {
                    rotated[y][x] = original[y][Size - x - 1];
                }
            }

            return rotated;
        }

        public List<List<bool>> FlipVertical(List<List<bool>> original)
        {
            var rotated = BuildEmptyGrid(Size);
            for (var y = 0; y < Size; y++)
            {
                for (var x = 0; x < Size; x++)
                {
                    rotated[y][x] = original[Size - y - 1][x];
                }
            }

            return rotated;
        }

        private static List<List<bool>> BuildEmptyGrid(int size)
        {
            return Enumerable.Range(0, size)
                .Select(_ => Enumerable.Range(0, size).Select(_ => false).ToList()).ToList();
        }
        
        public string ToString()
        {
            var sb = new StringBuilder();
            foreach (var row in Spaces)
            {
                foreach (var item in row)
                {
                    sb.Append(item ? '#' : '.');
                }

                sb.Append(Environment.NewLine);
            }

            return sb.ToString().Trim();
        }

    }
}