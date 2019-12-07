using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2019.Common;
using AdventOfCode._2019.Common.IntCode;

namespace AdventOfCode._2019.Day7
{
    public class Day7
    {
        public static int Part1()
        {
            var program = FileReader.ReadInputLines(7).Single();
            return GetMaximumThrust(program);
        }
        
        public static int GetMaximumThrust(string program)
        {
            var phases = new List<int> {0, 1, 2, 3, 4};
            var outputs = phases.AllPermutations().Select(p => RunPhasePermutation(program, p));

            return outputs.Max();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="program"></param>
        /// <param name="phases">Should contain exactly 5 unique phases (but will work with any number)</param>
        /// <returns></returns>
        private static int RunPhasePermutation(string program, IEnumerable<int> phases)
        {
            return phases.Aggregate(0, (current, phase) =>
                IntCodeComputer.ParseAndRunIntCode(program, phase, current).Outputs.Single()
            );
        }
    }
}