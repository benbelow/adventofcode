using System;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2021.Day02
{
    public static class Day02
    {
        private const int Day = 02;

        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var commands = lines.Select(ParseCommand);
            var deets = commands.Aggregate((0,0), ApplyCommand);

            return deets.Item1 * deets.Item2;
        }

        private static (int, int) ApplyCommand((int, int) deets, Command command)
        {
            return command.Type switch
            {
                CommandType.Forward => (deets.Item1 + command.Value, deets.Item2),
                CommandType.Backwards => (deets.Item1 - command.Value, deets.Item2),
                CommandType.Up => (deets.Item1, deets.Item2 - command.Value),
                CommandType.Down => (deets.Item1, deets.Item2 + command.Value),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public enum CommandType
        {
            Forward,
            Backwards,
            Up,
            Down
        }

        public class Command
        {
            public CommandType Type { get; set; }
            public int Value { get; set; }
        }

        public static Command ParseCommand(string s)
        {
            var split = s.Split(" ");
            var type = split[0] switch
            {
                "forward" => CommandType.Forward,
                "backward" => CommandType.Backwards,
                "up" => CommandType.Up,
                "down" => CommandType.Down,
            };
            
            return new Command
            {
                Type = type,
                Value = int.Parse(split[1])
            };
        }

    public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var commands = lines.Select(ParseCommand);
            var deets = commands.Aggregate((0,0,0), ApplyCommand2);
            return deets.Item1 * deets.Item2;
        }

    private static (int, int, int) ApplyCommand2((int, int, int) deets, Command command)
    {
        var multiplier = deets.Item3 * command.Value;
        
        return command.Type switch
        {
            CommandType.Forward => (deets.Item1 + command.Value, deets.Item2 + multiplier, deets.Item3),
            CommandType.Up => (deets.Item1, deets.Item2, deets.Item3 - command.Value),
            CommandType.Down => (deets.Item1, deets.Item2, deets.Item3 + command.Value),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    }
}