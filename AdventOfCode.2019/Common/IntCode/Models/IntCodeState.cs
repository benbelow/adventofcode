using System.Collections.Generic;

namespace AdventOfCode._2019.Common.IntCode.Models
{
    public class IntCodeState
    {
        /// <summary>
        /// The current state of the intCode being run
        /// </summary>
        public IList<int> State { get; set; }
        
        /// <summary>
        /// The index of the IntCode to apply next
        /// </summary>
        public int Index { get; set; }

        public int Value => State.ElementAtWrapped(Index);
    }
}