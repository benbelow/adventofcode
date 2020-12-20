using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020.Day20
{
    internal class MegaTile
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

                            switch ((Top: neighbours.North, Left: neighbours.West))
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