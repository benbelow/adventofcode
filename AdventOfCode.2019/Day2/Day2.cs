using System.Linq;
using AdventOfCode._2019.Common;
using AdventOfCode._2019.Common.IntCode;

namespace AdventOfCode._2019.Day2
{
    public static class Day2
    {
        public static long Part1()
        {
            var intCode = FileReader.ReadInputLines(2).Single();

            var result = IntCodeLogic.ParseAndRunIntCode(intCode, noun: 12, verb: 2).ToEnumerable();
            return result.Single().CurrentState.First();
        }

        public static long Part2Manual(int noun, int verb)
        {
            var intCode = FileReader.ReadInputLines(2).Single();

            var result = IntCodeLogic.ParseAndRunIntCode(intCode, noun: noun, verb: verb).ToEnumerable();
            return result.Single().CurrentState.First();
        }

        public static long Part2(int target)
        {
            var intCode = FileReader.ReadInputLines(2).Single();

            long result = 0;
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

                result = IntCodeLogic
                    .ParseAndRunIntCode(intCode, noun: noun, verb: verb)
                    .ToEnumerable()
                    .Single()
                    .CurrentState
                    .First();
            }

            return 100 * noun + verb;
        }
    }
}