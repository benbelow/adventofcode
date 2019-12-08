using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Common.IntCode
{
    public static class IntCodeLogic
    {
        public static IEnumerator<IntCodeOutput> ParseAndRunIntCode(
            string intCode,
            int? firstInput = null,
            Func<int> getNextInput = null,
            int? noun = null,
            int? verb = null)
        {
            getNextInput ??= () => 0;
            var initialState = Parser.ParseIntCode(intCode);
            
            var usedInitial = false;
            int GetInput()
            {
                if (usedInitial || firstInput == null)
                {
                    return getNextInput();
                }
                usedInitial = true;
                return firstInput.Value;
            }

            return RunIntCode(initialState, getInput: GetInput, noun: noun, verb: verb);
        }

        public static IEnumerator<IntCodeOutput> RunIntCode(
            IList<int> initialState,
            int? noun = null,
            int? verb = null,
            Func<int> getInput = null)
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
                    // ADD
                    case 1:
                        state = state.ApplyOperation(index, (x, y) => x + y, modes);
                        index += 4;
                        break;
                    // MULTIPLY
                    case 2:
                        state = state.ApplyOperation(index, (x, y) => x * y, modes);
                        index += 4;
                        break;
                    // INPUT
                    case 3:
                        state = state.ApplyInput(index, getInput);
                        index += 2;
                        break;
                    // OUTPUT
                    case 4:
                        state = state.ApplyOutput(index, x => { outputs.Add(x); }, modes);
                        index += 2;
                        yield return new IntCodeOutput
                        {
                            Output = outputs.Last(), IsComplete = false, CurrentState = state
                        };
                        break;
                    // JUMP IF TRUE
                    case 5:
                        index = state.JumpIf(index, modes, true);
                        break;
                    // JUMP IF FALSE
                    case 6:
                        index = state.JumpIf(index, modes, false);
                        break;
                    // LESS THAN
                    case 7:
                        state = state.Compare(index, modes, (x, y) => x < y);
                        index += 4;
                        break;
                    // EQUALS
                    case 8:
                        state = state.Compare(index, modes, (x, y) => x == y);
                        index += 4;
                        break;
                    default:
                        throw new Exception($"Unrecognised instruction: {value}");
                }

                value = state.ElementAt(index);
            }

            yield return new IntCodeOutput {IsComplete = true, CurrentState = state};
        }

        private static IList<int> ApplyOperation(this IList<int> state, int index, Func<int, int, int> operation,
            IList<ParameterMode> modes)
        {
            var outputIndex = state.ElementAtWrapped(index + 3);

            var operandX = state.GetOperand(index + 1, modes.ElementAtOrDefault(0));
            var operandY = state.GetOperand(index + 2, modes.ElementAtOrDefault(1));

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

        private static IList<int> ApplyOutput(this IList<int> state, int index, Action<int> applyOutput,
            IEnumerable<ParameterMode> modes)
        {
            var output = state.GetOperand(index + 1, modes.SingleOrDefault());
            applyOutput(output);
            return state;
        }

        private static int JumpIf(this IList<int> state, int index, IList<ParameterMode> modes, bool jumpBehaviour)
        {
            var param1 = state.GetOperand(index + 1, modes.ElementAtOrDefault(0));
            var param2 = state.GetOperand(index + 2, modes.ElementAtOrDefault(1));
            if (jumpBehaviour)
            {
                return param1 != 0 ? param2 : index + 3;
            }

            return param1 == 0 ? param2 : index + 3;
        }

        private static IList<int> Compare(this IList<int> state, int index, IList<ParameterMode> modes,
            Func<int, int, bool> comparator)
        {
            var param1 = state.GetOperand(index + 1, modes.ElementAtOrDefault(0));
            var param2 = state.GetOperand(index + 2, modes.ElementAtOrDefault(1));

            var indexToUpdate = state.ElementAtWrapped(index + 3);

            var valueToStore = comparator(param1, param2) ? 1 : 0;

            state[indexToUpdate] = valueToStore;
            return state;
        }

        private static int GetOperand(this IList<int> state, int index, ParameterMode? parameterMode)
        {
            var value = state.ElementAtWrapped(index);

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