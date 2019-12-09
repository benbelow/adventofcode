using System.Collections.Generic;

namespace AdventOfCode._2019.Common.IntCode.Models
{
    public class IntCodeOutput
    {
        public IEnumerable<long> CurrentState { get; set; }
        public long? Output { get; set; }
        public bool IsComplete { get; set; }
    }
}