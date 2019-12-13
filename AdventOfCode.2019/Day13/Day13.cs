using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2019.Common;
using AdventOfCode._2019.Common.IntCode;
using AdventOfCode._2019.Common.Models;

namespace AdventOfCode._2019.Day13
{
    public static class Day13
    {
        public static int Part1()
        {
            var intCode = FileReader.ReadInputLines(13).Single();
            var cabinet = new ArcadeCabinet(intCode);
            cabinet.Start();
            return cabinet.CountTiles(TileType.Block);
        }
    }

    public class ArcadeCabinet
    {
        private readonly IntCodeComputer computer;
        
        private readonly Dictionary<Coordinate, TileType> tiles;
        
        public ArcadeCabinet(string intCode)
        {
            computer = new IntCodeComputer(intCode);
            tiles = new Dictionary<Coordinate, TileType>();
        }

        public void Start()
        {
            while (true)
            {
                var output1 = computer.NextOutput();
                var output2 = computer.NextOutput();
                var output3 = computer.NextOutput();

                if (output1.IsComplete)
                {
                    return;
                }
                
                var coordinate = new Coordinate((int) output1.Output.Value, (int) output2.Output.Value);
                var tileType = ParseTile(output3.Output.Value);
                
                tiles[coordinate] = tileType;
            }
        }

        public int CountTiles(TileType type)
        {
            return tiles.Values.Count(t => t == type);
        }

        private static TileType ParseTile(long code)
        {
            return code switch
            {
                0 => TileType.Empty,
                1 => TileType.Wall,
                2 => TileType.Block,
                3 => TileType.Paddle,
                4 => TileType.Ball,
                _ => throw new Exception("Tile Type not recognised")
            };
        }
    }

    public enum TileType
    {
        Empty, 
        Wall,
        Block,
        Paddle,
        Ball
    }
}