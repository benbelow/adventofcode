using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Common.IntCode
{
    public static class IntCodeComputer
    {
        public static IntCodeOutput ParseAndRunIntCode(string intCode, Func<int> getInput = null)
        {
            var initialState = ParseIntCode(intCode);
            return RunIntCode(initialState, getInput: getInput);
        }

        public static List<int> ParseIntCode(string intCode)
        {
            return intCode.Split(",").Select(int.Parse).ToList();
        }

        public static IntCodeOutput RunIntCode(IList<int> initialState, int? noun = null, int? verb = null, Func<int> getInput = null)
        {
            if (!(noun == null || (noun >= 0 && noun <= 99) && verb == null || (verb >= 0 && verb <= 99)))
            {
                throw new Exception($"Noun: {noun} and/or verb: {verb} are invalid");
            }

            if (noun != null)
            {
                initialState[1] = noun.Value;
            }

            if (verb != null)
            {
                initialState[2] = verb.Value;
            }

            var state = initialState;

            var index = 0;
            var value = state.ElementAtWrapped(index);
            var outputs = new List<int>();

            while (value != 99)
            {
                var instructionString = value.ToString().Reverse().ToList();
                var operation = int.Parse(string.Concat(instructionString.Take(2).Reverse()));
                var modes = instructionString.Skip(2).Select(c => (ParameterMode) int.Parse(c.ToString())).ToList();

                switch (operation)
                {
                    case 1:
                        state = state.ApplyOperation(index, (x, y) => x + y, modes);
                        index += 4;
                        break;
                    case 2:
                        state = state.ApplyOperation(index, (x, y) => x * y, modes);
                        index += 4;
                        break;
                    case 3:
                        state = state.ApplyInput(index, getInput);
                        index += 2;
                        break;
                    case 4:
                        state = state.ApplyOutput(index, x => { outputs.Add(x); }, modes);
                        index += 2;
                        break;
                    default:
                        throw new Exception($"Unrecognised instruction: {value}");
                }

                value = state.ElementAt(index);
            }

            return new IntCodeOutput
            {
                FinalState = state,
                Outputs = outputs
            };
        }

        private static IList<int> ApplyOperation(this IList<int> state, int index, Func<int, int, int> operation, IList<ParameterMode> modes)
        {
            var valueX = state.ElementAtWrapped(index + 1);
            var valueY = state.ElementAtWrapped(index + 2);
            var outputIndex = state.ElementAtWrapped(index + 3);

            var operandX = state.GetOperand(valueX, modes.ElementAtOrDefault(0));
            var operandY = state.GetOperand(valueY, modes.ElementAtOrDefault(1));

            var output = operation(operandX, operandY);
            var indexToUpdate = state.WrappedIndex(outputIndex);
            state[indexToUpdate] = output;
            return state;
        }

        private static IList<int> ApplyInput(this IList<int> state, int index, Func<int> getInput)
        {
            var indexOfInputStore = state.ElementAtWrapped(index + 1);
            var input = getInput();
            state[indexOfInputStore] = input;
            return state;
        }

        private static IList<int> ApplyOutput(this IList<int> state, int index, Action<int> applyOutput, IEnumerable<ParameterMode> modes)
        {
            var outputValue = state.ElementAtWrapped(index + 1);
            var output = state.GetOperand(outputValue, modes.SingleOrDefault());
            applyOutput(output);
            return state;
        }

        private static int GetOperand(this IEnumerable<int> state, int value, ParameterMode? parameterMode)
        {
            if (parameterMode == null)
            {
                parameterMode = ParameterMode.Position;
            }

            switch (parameterMode)
            {
                case ParameterMode.Position:
                    return state.ElementAtWrapped(value);
                case ParameterMode.Immediate:
                    return value;
                default:
                    throw new ArgumentOutOfRangeException(nameof(parameterMode), parameterMode, null);
            }
        }
    }
}