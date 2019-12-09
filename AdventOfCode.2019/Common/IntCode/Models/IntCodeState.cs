using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Common.IntCode.Models
{
    public class IntCodeState
    {
        /// <summary>
        /// The current state of the intCode being run
        /// </summary>
        public IList<long> State { get; set; }

        /// <summary>
        /// The index of the IntCode to apply next
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// The value given by the current index.
        /// </summary>
        public long Value => State.ElementAtWrapped(Index);

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

        /// <summary>
        /// Depending on the parameter mode, either returns:
        /// - The value at a memory address
        /// - The value at the location indicated by the memory address (using index as a pointer)
        /// </summary>
        /// <param name="parameterIndex">
        /// The number of the parameter to fetch.
        /// Param 1 = index + 1, Param 2 = index + 2 etc.
        /// </param>
        public long ReadParameter(int parameterIndex)
        {
            var parameterMode = ParameterModes.ElementAtOrDefault(parameterIndex - 1);
            var parameterValue = State.ElementAtWrapped(Index + parameterIndex);
            return parameterMode switch
            {
                ParameterMode.Position => State.ElementAtWrapped((int) parameterValue),
                ParameterMode.Immediate => parameterValue,
                _ => throw new ArgumentOutOfRangeException(nameof(parameterMode), parameterMode, null)
            };
        }

        /// <summary>
        /// Parameters used for writes rather than reads will *NEVER* be in immediate mode.
        /// A position mode input parameter would return the value at the index indicated by the param.
        /// But a write parameter in position mode should return the index, as it will be used as an index by the consumer to write. 
        /// </summary>
        /// <param name="parameterNumber"></param>
        /// <returns></returns>
        public int WriteParameter(int parameterNumber)
        {
            var newIndex = parameterNumber + Index;
            return (int) State.ElementAtWrapped(newIndex);
        }

        /// <summary>
        /// Sets the value at the given memory address, returning the new intCode state 
        /// </summary>
        public IList<long> SetAt(int index, long value)
        {
            State[State.WrappedIndex(index)] = value;
            return State;
        }
    }
}