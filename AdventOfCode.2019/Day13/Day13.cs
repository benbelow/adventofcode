using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        
        public static long Part2()
        {
            var intCode = FileReader.ReadInputLines(13).Single();
            var sb = new StringBuilder();
            sb.Append("2");
            sb.Append(intCode.Substring(1));
            var cabinet = new ArcadeCabinet(sb.ToString());
            cabinet.Start();
            return cabinet.CurrentScore;
        }
    }

    public class ArcadeCabinet
    {
        private readonly IntCodeComputer computer;
        
        private readonly Dictionary<Coordinate, TileType> tiles;

        private JoystickPosition joystickPosition;

        public long CurrentScore;
        
        public ArcadeCabinet(string intCode)
        {
            computer = new IntCodeComputer(intCode, getInput: GetInput);
            tiles = new Dictionary<Coordinate, TileType>();
            CurrentScore = 0;
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

                // special case for scoring
                if (coordinate.X == -1 && coordinate.Y == 0)
                {
                    CurrentScore = output3.Output.Value;
                    continue;
                }
                
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

        private long GetInput()
        {
            return joystickPosition switch
            {
                JoystickPosition.Left => -1,
                JoystickPosition.Center => 0,
                JoystickPosition.Right => 1,
                _ => throw new ArgumentOutOfRangeException()
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

    public enum JoystickPosition
    {
        Left,
        Center,
        Right
    }
}