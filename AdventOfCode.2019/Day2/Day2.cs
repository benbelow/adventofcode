using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2019.Common;
using AdventOfCode._2019.Common.IntCode;

namespace AdventOfCode._2019.Day2
{
    public static class Day2
    {
        public static int Part1()
        {
            var intCode = FileReader.ReadInputLines(2).Single();
            var initialState = IntCodeComputer.ParseIntCode(intCode);

            var result = IntCodeComputer.RunIntCode(initialState, noun: 12, verb: 2);
            return result.FinalState.First();
        }

        public static int Part2Manual(int noun, int verb)
        {
            var intCode = FileReader.ReadInputLines(2).Single();
            var initialState = IntCodeComputer.ParseIntCode(intCode);
            
            var result = IntCodeComputer.RunIntCode(initialState, noun: noun, verb: verb);
            return result.FinalState.First();
        }

        public static int Part2(int target)
        {
            var intCode = FileReader.ReadInputLines(2).Single();
            var initialState = IntCodeComputer.ParseIntCode(intCode);

            var result = 0;
            var noun = 0;
            var verb = 0;

            while (result != target)
            {
                if (result / 1000 == target / 1000)
                {
                    verb++;
                }
                else
                {
                    noun++;
                }
                result = IntCodeComputer.RunIntCode(new List<int>(initialState), noun: noun, verb: verb).FinalState.First();
            }

            return 100 * noun + verb;
        }
    }
}