using System.Collections.Generic;

namespace AdventOfCode._2019.Common.IntCode.Models
{
    public class IntCodeOutput
    {
        public IEnumerable<int> CurrentState { get; set; }
        public int? Output { get; set; }
        public bool IsComplete { get; set; }
    }
}