using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AdventOfCode._2019.Common;
using AdventOfCode._2019.Common.IntCode;
using AdventOfCode._2019.Common.Models;

namespace AdventOfCode._2019.Day13
{
    public static class Day13
    {
        public static string GetPart2IntCode()
        {
            var intCode = FileReader.ReadInputLines(13).Single();
            var sb = new StringBuilder();
            sb.Append("2");
            sb.Append(intCode.Substring(1));
            return sb.ToString();
        }

        public static int Part1()
        {
            var intCode = FileReader.ReadInputLines(13).Single();
            var cabinet = new ArcadeCabinet(intCode);
            cabinet.RunToCompletion();
            return cabinet.CountTiles(TileType.Block);
        }

        public static long Part2()
        {
            var cabinet = new ArcadeCabinet(GetPart2IntCode());
            cabinet.RunToCompletion();
            return cabinet.CurrentScore;
        }
    }

    public class ArcadeCabinet
    {
        private readonly IntCodeComputer computer;

        private readonly Dictionary<Coordinate, TileType> tiles;

        private JoystickPosition joystickPosition = JoystickPosition.Left;

        public long CurrentScore;

        private Dictionary<Coordinate, TileType> renderedState;

        private int ballX = 0;
        private JoystickPosition ballDX = JoystickPosition.Center;

        private int tickCount = 0;

        public ArcadeCabinet(string intCode)
        {
            computer = new IntCodeComputer(intCode, getInput: GetInput);
            tiles = new Dictionary<Coordinate, TileType>();
            CurrentScore = 0;

            var ballStart = TileType.Ball;

            // Hardcoded for input - answer to part 1. Sets up board upfront before playing
            while (CountTiles(TileType.Ball) < 1 || ballStart == TileType.Ball)
            {
                tiles.TryGetValue(new Coordinate(18, -21), out ballStart);
                GetFrame();
            }
        }

        public bool GetFrame()
        {
            var finished = Tick();
            return finished;
        }

        public bool DrawFrame()
        {
            var finished = false;
            while (renderedState == tiles)
            {
                finished = Tick();
            }

            Render();
            return finished;
        }

        public JoystickPosition Flip(JoystickPosition pos)
        {
            return pos switch
            {
                JoystickPosition.Left => JoystickPosition.Right,
                JoystickPosition.Right => JoystickPosition.Left,
                _ => JoystickPosition.Center
            };
        }
        
        public long RunToCompletion()
        {
            while (true)
            {
                var finished = Tick();
                if (CountTiles(TileType.Ball) == 1 && CountTiles(TileType.Paddle) == 1)
                {
                    var newBallX = ballDX switch
                    {
                        JoystickPosition.Right => ballX + 1,
                        JoystickPosition.Left => ballX - 1,
                        _ => ballX
                    };

                    var xDiff = newBallX - Paddle().X;
                    joystickPosition = xDiff switch
                    {
                        _ when xDiff > 0 => JoystickPosition.Right,
                        _ when xDiff < 0 => JoystickPosition.Left,
                        _ => JoystickPosition.Center
                    };
                }

                if (finished)
                {
                    return CurrentScore;
                }
            }
        }

        public void MoveJoystick(JoystickPosition position)
        {
            joystickPosition = position;
        }

        private bool Tick()
        {
            tickCount++;
            var output1 = computer.NextOutput();
            var output2 = computer.NextOutput();
            var output3 = computer.NextOutput();

            if (output1.IsComplete)
            {
                return true;
            }

            var coordinate = new Coordinate((int) output1.Output.Value, (int) -output2.Output.Value);

            // special case for scoring
            if (coordinate.X == -1 && coordinate.Y == 0)
            {
                CurrentScore = output3.Output.Value;
                return false;
            }

            var tileType = ParseTile(output3.Output.Value);


            tiles[coordinate] = tileType;


            if (tileType == TileType.Ball)
            {
                if (ballX < Ball().X)
                {
                    ballDX = JoystickPosition.Right;
                }

                if (ballX > Ball().X)
                {
                    ballDX = JoystickPosition.Left;
                }

                else
                {
                    ballDX = JoystickPosition.Center;
                }

                ballX = Ball().X;
            }

            return false;
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
            Render();
            return joystickPosition switch
            {
                JoystickPosition.Left => -1,
                JoystickPosition.Center => 0,
                JoystickPosition.Right => 1,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void Render()
        {
            if (tickCount > 1040)
            {
                Console.WriteLine("TICK:" + tickCount);
                Console.WriteLine("SCORE:" + CurrentScore);
                Console.WriteLine("Xs: Paddle: " + Paddle()?.X + ", Ball: " + Ball()?.X);
                Console.WriteLine("Ball Is Moving: " + ballDX);
                Console.WriteLine("Joystick Is: " + joystickPosition);
                Renderer.Render(tiles, type =>
                {
                    return type switch
                    {
                        TileType.Empty => " ",
                        TileType.Wall => "x",
                        TileType.Block => "*",
                        TileType.Paddle => "_",
                        TileType.Ball => "O",
                        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
                    };
                });
                renderedState = new Dictionary<Coordinate, TileType>(tiles);
            }
        }

        private Coordinate Ball()
        {
            return GetSingleElement(TileType.Ball);
        }

        private Coordinate Paddle()
        {
            return GetSingleElement(TileType.Paddle);
        }

        private Coordinate GetSingleElement(TileType type)
        {
            var keyValuePair = tiles.Where(t => t.Value == type).ToList();
            return !keyValuePair.Any() ? null : keyValuePair.Single().Key;
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