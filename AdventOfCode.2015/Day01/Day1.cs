using System;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2015.Day01
{
    public static class Day01
    {
        public static int Part1()
        {
            var input = FileReader.ReadSingleLine(1);
            return input.Aggregate(0, (acc, c) =>
            {
                return c switch
                {
                    '(' => acc + 1,
                    ')' => acc - 1,
                    _ => throw new Exception($"Unexpected character: {c}")
                };
            });
        }

        public static int Part2()
        {
            var input = FileReader.ReadSingleLine(1);

            var floor = 0;
            var position = 0;
            foreach (var c in input)
            {
                position++;
                var operation = c == ')' ? -1 : +1;
                floor += operation;
                if (floor == -1)
                {
                    return position;
                }
            }
            
            throw new Exception("Uh Oh.");
        }
    }
}