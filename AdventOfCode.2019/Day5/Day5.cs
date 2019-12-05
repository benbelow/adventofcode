using System;
using System.Linq;
using AdventOfCode._2019.Common;
using AdventOfCode._2019.Common.IntCode;

namespace AdventOfCode._2019.Day5
{
    public static class Day5
    {
        public static int Part1()
        {
            var lines = FileReader.ReadInputLines(5).ToList();
            var output = IntCodeComputer.ParseAndRunIntCode(lines.First(), () => 1);
            
            if (output.Outputs.Count(x => x != 0) != 1)
            {
                throw new Exception($"Some Tests Failed: See output {output.Outputs}");
            }

            return output.Outputs.Single(x => x != 0);
        }

        public static int Part2()
        {
            var lines = FileReader.ReadInputLines(4).ToList();
            return 5;
        }
    }
}