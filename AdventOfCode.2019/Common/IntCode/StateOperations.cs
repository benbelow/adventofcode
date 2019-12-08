using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2019.Common.IntCode.Models;

namespace AdventOfCode._2019.Common.IntCode
{
    public static class StateOperations
    {
        public static IntCodeState ApplyOperation(this IntCodeState state, Func<int, int, int> operation)
        {
            var modes = state.ParameterModes;
            var outputIndex = state.State.ElementAtWrapped(state.Index + 3);
            var operandX = state.State.GetOperand(state.Index + 1, modes.ElementAtOrDefault(0));
            var operandY = state.State.GetOperand(state.Index + 2, modes.ElementAtOrDefault(1));

            var output = operation(operandX, operandY);
            var indexToUpdate = state.State.WrappedIndex(outputIndex);
            state.State[indexToUpdate] = output;
            return new IntCodeState
            {
                State = state.State,
                Index = state.Index + 4
            };
        }

        public static IntCodeState ApplyInput(this IntCodeState state, Func<int> getInput)
        {
            var indexOfInputStore = state.State.ElementAtWrapped(state.Index + 1);
            var input = getInput();
            state.State[indexOfInputStore] = input;
            return new IntCodeState
            {
                State = state.State,
                Index = state.Index + 2
            };
        }

        public static IntCodeState ApplyOutput(this IntCodeState state, Action<int> applyOutput)
        {
            var output = state.State.GetOperand(state.Index + 1, state.ParameterModes.SingleOrDefault());
            applyOutput(output);
            return new IntCodeState
            {
                State = state.State,
                Index = state.Index + 2
            };
        }

        public static IntCodeState JumpIf(this IntCodeState state, bool jumpBehaviour)
        {
            var modes = state.ParameterModes;
            var param1 = state.State.GetOperand(state.Index + 1, modes.ElementAtOrDefault(0));
            var param2 = state.State.GetOperand(state.Index + 2, modes.ElementAtOrDefault(1));

            var shouldJump = jumpBehaviour && param1 != 0 || !jumpBehaviour && param1 == 0;
            var newIndex = shouldJump ? param2 : state.Index + 3;

            return new IntCodeState
            {
                State = state.State,
                Index = newIndex
            };
        }

        public static IntCodeState Compare(this IntCodeState state, Func<int, int, bool> comparator)
        {
            var modes = state.ParameterModes;
            var param1 = state.State.GetOperand(state.Index + 1, modes.ElementAtOrDefault(0));
            var param2 = state.State.GetOperand(state.Index + 2, modes.ElementAtOrDefault(1));

            var indexToUpdate = state.State.ElementAtWrapped(state.Index + 3);

            var valueToStore = comparator(param1, param2) ? 1 : 0;

            state.State[indexToUpdate] = valueToStore;
            return new IntCodeState
            {
                State = state.State,
                Index = state.Index + 4
            };
        }

        private static int GetOperand(this IList<int> state, int index, ParameterMode? parameterMode)
        {
            var value = state.ElementAtWrapped(index);

            if (parameterMode == null)
            {
                parameterMode = ParameterMode.Position;
            }

            return parameterMode switch
            {
                ParameterMode.Position => state.ElementAtWrapped(value),
                ParameterMode.Immediate => value,
                _ => throw new ArgumentOutOfRangeException(nameof(parameterMode), parameterMode, null)
            };
        }
    }
}