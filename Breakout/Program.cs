using System;
using AdventOfCode._2019.Day13;

namespace Breakout
{
    class Program
    {
        static void Main(string[] args)
        {
            var cabinet = new ArcadeCabinet(Day13.GetPart2IntCode());
            
            while (true)
            {
                var key = Console.ReadKey();
                Console.Clear();
                switch (key.Key)
                {
                    case ConsoleKey.Q:
                        return;
                    case ConsoleKey.LeftArrow:
                        cabinet.MoveJoystick(JoystickPosition.Left);
                        break;
                    case ConsoleKey.RightArrow:
                        cabinet.MoveJoystick(JoystickPosition.Right);
                        break;
                    case ConsoleKey.UpArrow:
                        cabinet.MoveJoystick(JoystickPosition.Center);
                        break;
                    default:
                        break;
                }
                cabinet.DrawFrame();
            }
        }
    }
}