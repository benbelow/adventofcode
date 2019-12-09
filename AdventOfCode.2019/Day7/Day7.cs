using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2019.Common;
using AdventOfCode._2019.Common.IntCode;
using AdventOfCode._2019.Common.IntCode.Models;

namespace AdventOfCode._2019.Day7
{
    public class Day7
    {
        public static long Part1()
        {
            var program = FileReader.ReadInputLines(7).Single();
            return GetMaximumThrust(program);
        }
        
        public static long Part2()
        {
            var program = FileReader.ReadInputLines(7).Single();
            return GetMaximumThrustWithFeedback(program);
        }

        public static long GetMaximumThrust(string program)
        {
            var phases = new List<int> {0, 1, 2, 3, 4};
            var outputs = phases.AllPermutations().Select(p => RunPhasePermutation(program, p));

            return outputs.Max();
        }

        public static long GetMaximumThrustWithFeedback(string program)
        {
            var phases = new List<int> {5, 6, 7, 8, 9};
            var outputs = phases.AllPermutations().Select(p => RunPhasePermutationWithFeedback(program, p));
            return outputs.Max();
        }

        /// <param name="phases">Should contain exactly 5 unique phases (but will work with any number)</param>
        private static long RunPhasePermutation(string program, IEnumerable<int> phases)
        {
            return phases.Aggregate((long) 0, (current, phase) =>
                IntCodeLogic.ParseAndRunIntCode(program, new Queue<long>().WithValues(phase, current))
                    .ToEnumerable()
                    .Single(x => !x.IsComplete).Output ?? -1
            );
        }

        private static long RunPhasePermutationWithFeedback(string program, IList<int> phases)
        {
            phases = phases.ToList();
            var outputs = new List<long?>();
            var outputE = new IntCodeOutput {Output = 0};

            var amplifierA = new IntCodeComputer(program, phases[0]);
            var amplifierB = new IntCodeComputer(program, phases[1]);
            var amplifierC = new IntCodeComputer(program, phases[2]);
            var amplifierD = new IntCodeComputer(program, phases[3]);
            var amplifierE = new IntCodeComputer(program, phases[4]);

            while (true)
            {
                var outputA = amplifierA.NextOutput(outputE.Output);
                var outputB = amplifierB.NextOutput(outputA.Output);
                var outputC = amplifierC.NextOutput(outputB.Output);
                var outputD = amplifierD.NextOutput(outputC.Output);
                outputE = amplifierE.NextOutput(outputD.Output);

                outputs.Add(outputE.Output);
                if (outputE.IsComplete)
                {
                    return outputs.Where(o => o != null).Select(o => o.Value).Max();
                }
            }
        }
    }
}