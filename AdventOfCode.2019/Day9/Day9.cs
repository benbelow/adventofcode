using System.Linq;
using AdventOfCode._2019.Common;
using AdventOfCode._2019.Common.IntCode;

namespace AdventOfCode._2019.Day9
{
    public class Day9
    {
        public static long? Part1()
        {
            var intCode = FileReader.ReadInputLines(9).Single();

            var intCodeComputer = new IntCodeComputer(intCode, 1);
            var results = intCodeComputer.RunToCompletion().ToList();
            return results.Where(o => o != null).Select(o => o.Output).Single();
        }
    }
}