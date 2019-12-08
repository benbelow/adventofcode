using System;
using System.Collections.Generic;
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
            var inputs = new Queue<int>().WithValues(1);
            var output = IntCodeLogic.ParseAndRunIntCode(lines.First(), inputs).ToEnumerable().ToList();
            
            if (output.Count(x => !x.IsComplete && x.Output != 0) != 1)
            {
                throw new Exception($"Some Tests Failed: See output {output}");
            }

            return output.Single(x => x.Output != 0 && !x.IsComplete).Output ?? -1;
        }

        public static int Part2()
        {
            var lines = FileReader.ReadInputLines(5).ToList();
            var inputs = new Queue<int>().WithValues(5);
            var output = IntCodeLogic.ParseAndRunIntCode(lines.First(), inputs).ToEnumerable();

            return output.Single(x => !x.IsComplete).Output ?? -1;
        }
    }
}