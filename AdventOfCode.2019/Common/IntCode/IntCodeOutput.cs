using System.Collections.Generic;

namespace AdventOfCode._2019.Common.IntCode
{
    public class IntCodeOutput
    {
        public IEnumerable<int> FinalState { get; set; }
        public IEnumerable<int> Outputs { get; set; }
    }
}