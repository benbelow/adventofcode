using System.Linq;
using AdventOfCode._2019.Common;
using AdventOfCode._2019.Common.IntCode;

namespace AdventOfCode._2019.Day9
{
    public class Day9
    {
        public static long? Part1()
        {
            return RunDay9(1);
        }
        
        public static long? Part2()
        {
            return RunDay9(2);
        }

        private static long? RunDay9(int input)
        {
            var intCode = FileReader.ReadInputLines(9).Single();

            var intCodeComputer = new IntCodeComputer(intCode, input);
            var results = intCodeComputer.RunToCompletion().ToList();
            return results.Where(o => o.Output.HasValue).Select(o => o.Output).Single();
        }
    }
}