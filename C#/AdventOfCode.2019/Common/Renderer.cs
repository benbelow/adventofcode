using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2019.Common.Models;

namespace AdventOfCode._2019.Common
{
    public class Renderer
    {
        public static void Render<T>(Dictionary<Coordinate, T> grid, Func<T, string> renderItem)
        {
            var minX = grid.Min(x => x.Key.X);
            var maxX = grid.Max(x => x.Key.X);

            var minY = grid.Min(x => x.Key.Y);
            var maxY = grid.Max(x => x.Key.Y);

            for (var y = maxY; y >= minY; y--)
            {
                for (var x = minX; x <= maxX; x++)
                {
                    grid.TryGetValue(new Coordinate(x, y), out var item);
                    Console.Write(renderItem(item));
                }
                Console.WriteLine();
            }
        }
    }
}