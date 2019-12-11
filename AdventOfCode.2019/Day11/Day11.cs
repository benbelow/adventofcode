using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2019.Common;
using AdventOfCode._2019.Common.IntCode;
using AdventOfCode._2019.Common.Models;

namespace AdventOfCode._2019.Day11
{
    public class Day11
    {
        public static int Part1()
        {
            var input = FileReader.ReadInputLines(11);
            var robot = new Robot(new IntCodeComputer(input.First()));
            var painted = robot.GetPanelPaintingHistory();
            return painted.Count(x => x.Value >= 1);
        }
        
        public class Robot
        {
            private readonly IntCodeComputer intCodeComputer;

            private readonly Dictionary<Coordinate, int> panelsPainted = new Dictionary<Coordinate, int>();
            private Coordinate coordinate;
            private Direction direction;
            
            public Robot(IntCodeComputer intCodeComputer)
            {
                this.intCodeComputer = intCodeComputer;
                coordinate = new Coordinate(0,0);
                direction = Direction.Up;
            }

            public Dictionary<Coordinate, int> GetPanelPaintingHistory()
            {
                while (true)
                {
                    var panelColor = PanelColor(coordinate);
                    intCodeComputer.QueueInput(panelColor == Color.Black ? 0 : 1);
                    var firstOutput = intCodeComputer.NextOutput();
                    if (firstOutput.IsComplete)
                    {
                        return panelsPainted;
                    }

                    var newColor = ParseColor(firstOutput.Output.Value);
                    if (panelColor != newColor)
                    {
                        PaintPanel();
                    }

                    var secondOutput = intCodeComputer.NextOutput();
                    
                    if (secondOutput.IsComplete)
                    {
                        return panelsPainted;
                    }

                    var rotationDirection = ParseRotationDirection(secondOutput.Output.Value);
                    switch (rotationDirection)
                    {
                        case Direction.Left:
                            TurnLeft90();
                            break;
                        case Direction.Right:
                            TurnRight90();
                            break;
                        default:
                        case Direction.Up:
                        case Direction.Down:
                            throw new ArgumentOutOfRangeException();
                    }
                    
                    Move(1);
                }
            }

            private Color PanelColor(Coordinate coordinate)
            {
                if (!panelsPainted.ContainsKey(coordinate))
                {
                    // not painted
                    return Color.Black;
                }

                // painted odd number = white, else black
                return panelsPainted[coordinate] % 2 == 0 ? Color.Black : Color.White;
            }

            private static Color ParseColor(long output)
            {
                return output switch
                {
                    0 => Color.Black,
                    1 => Color.White,
                    _ => throw new Exception()
                };
            }

            private static Direction ParseRotationDirection(long output)
            {
                return output switch
                {
                    0 => Direction.Left,
                    1 => Direction.Right,
                    _ => throw new Exception()
                };
            }
            
            private void PaintPanel()
            {
                if (!panelsPainted.ContainsKey(coordinate))
                {
                    panelsPainted.Add(coordinate, 1);
                    return;
                }

                panelsPainted[coordinate]++;
            }

            private void TurnLeft90()
            {
                switch (direction)
                {
                    case Direction.Up:
                        direction = Direction.Left;
                        return;
                    case Direction.Down:
                        direction = Direction.Right;
                        return;
                    case Direction.Left:
                        direction = Direction.Down;
                        return;
                    case Direction.Right:
                        direction = Direction.Up;
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            private void TurnRight90()
            {
                switch (direction)
                {
                    case Direction.Up:
                        direction = Direction.Right;
                        return;
                    case Direction.Down:
                        direction = Direction.Left;
                        return;
                    case Direction.Left:
                        direction = Direction.Up;
                        return;
                    case Direction.Right:
                        direction = Direction.Down;
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            private void Move(int number)
            {
                switch (direction)
                {
                    case Direction.Up:
                        coordinate = new Coordinate(coordinate.X, coordinate.Y + 1);
                        return;
                    case Direction.Down:
                        coordinate = new Coordinate(coordinate.X, coordinate.Y - 1);
                        return;
                    case Direction.Left:
                        coordinate = new Coordinate(coordinate.X - 1, coordinate.Y);
                        return;
                    case Direction.Right:
                        coordinate = new Coordinate(coordinate.X + 1, coordinate.Y);
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        
        private enum Color
        {
            Black, 
            White
        }
        private enum Direction
        {
            Up, 
            Down,
            Left, 
            Right
        }
    }
}