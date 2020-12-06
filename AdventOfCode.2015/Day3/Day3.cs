using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2015.Day3
{
    public static class Day3
    {
        private const int Day = 3;
        
        public static long Part1()
        {
            var instructions = FileReader.ReadInputLines(Day).ToList().Single();
            var visited = SantaGo(instructions);
            return visited.Count;
        }

        private static Dictionary<(int, int), int?> SantaGo(string instructions, int offset = 0, int modulo = 1)
        {
            var coords = (0, 0);
            var visited = new Dictionary<(int, int), int?> {{coords, 1}};

            for (int i = offset; i < instructions.Length; i+=modulo)
            {
                var instruction = instructions[i];
                switch (instruction)
                {
                    case '^':
                        coords = (coords.Item1 + 1, coords.Item2);
                        break;
                    case 'v':
                        coords = (coords.Item1 - 1, coords.Item2);
                        break;
                    case '>':
                        coords = (coords.Item1, coords.Item2 + 1);
                        break;
                    case '<':
                        coords = (coords.Item1, coords.Item2 - 1);
                        break;
                }

                var existing = visited.GetValueOrDefault(coords);
                if (existing.HasValue)
                {
                    visited[coords] = existing + 1;
                }
                else
                {
                    visited[coords] = 1;
                }
            }

            return visited;
        }

        public static long Part2()
        {
            var instructions = FileReader.ReadInputLines(Day).ToList().Single();
            var visitedReal = SantaGo(instructions, 0, 2);
            var visitedRobo = SantaGo(instructions, 1, 2);
            return visitedReal.Keys.Concat(visitedRobo.Keys).ToHashSet().Count;
        }
    }
}