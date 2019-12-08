using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// The value given by the current index.
        /// </summary>
        public int Value => State.ElementAtWrapped(Index);
        
        /// <summary>
        /// The two-digit Op-Code at the current index.
        /// Instructions and values are not differentiated, so if the index reaches an invalid state, this could produce an unrecognised code.
        /// </summary>
        public int OpCode => int.Parse(string.Concat(Value.ToString().Reverse().Take(2).Reverse()));

        /// <summary>
        /// A list of all parameter modes at the current index.
        /// Instructions and values are not differentiated, so if the index reaches an invalid state, this could attempt to parse invalid modes.
        /// </summary>
        public List<ParameterMode> ParameterModes => Value.ToString()
            .Reverse()
            .Skip(2)
            .Select(c => (ParameterMode) int.Parse(c.ToString()))
            .ToList();

        
    }
}