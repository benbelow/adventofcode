using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2019.Common.IntCode.Models;

namespace AdventOfCode._2019.Common.IntCode
{
    public static class StateOperations
    {
        /// <summary>
        /// Applies an arbitrary operation to the first *TWO* parameters, outputting based on the third parameter.
        /// Will need extending if it needs to be applied to more than two parameters.
        /// </summary>
        public static IntCodeState ApplyOperation(this IntCodeState state, Func<long, long, long> operation)
        {
            var operationResult = operation(state.ReadParameter(1), state.ReadParameter(2));
            
            return new IntCodeState(state)
            {
                State = state.SetAt(state.WriteParameter(3), operationResult),
                Index = state.Index + 4
            };
        }

        /// <summary>
        /// Accepts a single input, applying it to the memory address stored at the single parameter.
        /// </summary>
        public static IntCodeState ApplyInput(this IntCodeState state, long input)
        {
            return new IntCodeState(state)
            {
                State = state.SetAt(state.WriteParameter(1), input),
                Index = state.Index + 2
            };
        }

        /// <summary>
        /// Outputs the value of the single parameter.
        /// Outputs are not tracked within the intCode state, so a callback is used to apply it as necessary  
        /// </summary>
        public static IntCodeState ApplyOutput(this IntCodeState state, Action<long> applyOutput)
        {
            applyOutput(state.ReadParameter(1));
            return new IntCodeState(state)
            {
                State = state.State,
                Index = state.Index + 2
            };
        }

        /// <summary>
        /// If the condition is fulfilled, jumps to a new memory address - if not, proceeds as normal through the code.
        /// Condition is based on a single parameter
        /// </summary>
        public static IntCodeState JumpIf(this IntCodeState state, Func<long, bool> shouldJump)
        {
            return new IntCodeState(state)
            {
                State = state.State,
                Index = shouldJump(state.ReadParameter(1)) ? (int) state.ReadParameter(2) : state.Index + 3
            };
        }

        /// <summary>
        /// Compares the first two values using the given comparator.
        /// Sets the value in the index given by param 3 to either 1 or 0, reflecting the result of the comparison
        /// </summary>
        public static IntCodeState Compare(this IntCodeState state, Func<long, long, bool> comparator)
        {
            var valueToStore = comparator(state.ReadParameter(1), state.ReadParameter(2)) ? 1 : 0;

            return new IntCodeState(state)
            {
                State = state.SetAt(state.WriteParameter(3), valueToStore),
                Index = state.Index + 4
            };
        }

        public static IntCodeState AdjustRelativeBase(this IntCodeState state)
        {
            var baseChange = state.ReadParameter(1);
            
            return new IntCodeState(state)
            {
                Index = state.Index + 2,
                RelativeBase = state.RelativeBase + baseChange
            };
        }
    }
}