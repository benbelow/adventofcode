using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Common.IntCode.Models
{
    public class IntCodeState
    {
        public IntCodeState()
        {
        }

        public IntCodeState(IntCodeState previousState)
        {
            State = previousState.State;
            Index = previousState.Index;
            RelativeBase = previousState.RelativeBase;
        }

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
        public long Value
        {
            get
            {
                LazilyAddMemory(Index);
                return State[Index];
            }
        }

        public long RelativeBase { get; set; } = 0;

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
        /// <param name="parameterNumber">
        /// The number of the parameter to fetch.
        /// Param 1 = index + 1, Param 2 = index + 2 etc.
        /// </param>
        public long ReadParameter(int parameterNumber)
        {
            var parameterMode = ParameterModes.ElementAtOrDefault(parameterNumber - 1);
            var parameterValue = State[Index + parameterNumber];
            var relativeParameterValue = parameterValue + RelativeBase;
            LazilyAddMemory((int) Math.Max(parameterValue, relativeParameterValue));
            return parameterMode switch
            {
                ParameterMode.Position => State[(int) parameterValue],
                ParameterMode.Immediate => parameterValue,
                ParameterMode.Relative => State[(int) relativeParameterValue],
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
            var parameterMode = ParameterModes.ElementAtOrDefault(parameterNumber - 1);
            var indexOfParameter = parameterNumber + Index;
            var positionValue = (int) State[indexOfParameter];

            return parameterMode switch
            {
                ParameterMode.Position => positionValue,
                ParameterMode.Immediate => throw new Exception(),
                ParameterMode.Relative => (int) (positionValue + RelativeBase),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        /// <summary>
        /// Sets the value at the given memory address, returning the new intCode state 
        /// </summary>
        public IList<long> SetAt(int index, long value)
        {
            LazilyAddMemory(index);
            State[index] = value;
            return State;
        }

        private void LazilyAddMemory(int newSize)
        {
            if (State.Count <= newSize)
            {
                for (var i = State.Count; i <= newSize; i++)
                {
                    State.Add(0);
                }
            }
        }
    }
}