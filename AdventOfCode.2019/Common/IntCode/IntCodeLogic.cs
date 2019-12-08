using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2019.Common.IntCode.Models;

namespace AdventOfCode._2019.Common.IntCode
{
    public static class IntCodeLogic
    {
        public static IEnumerator<IntCodeOutput> ParseAndRunIntCode(
            string intCode,
            Queue<int> inputs = null,
            int? noun = null,
            int? verb = null)
        {
            inputs ??= new Queue<int>();
            var initialState = Parser.ParseIntCode(intCode);

            return RunIntCode(initialState, inputs, noun, verb);
        }

        private static IEnumerator<IntCodeOutput> RunIntCode(
            IList<int> initialState,
            Queue<int> inputs,
            int? noun = null,
            int? verb = null)
        {
            var state = new IntCodeState
            {
                State = initialState.ApplyNounAndVerb(noun, verb),
                Index = 0
            };
            var outputs = new List<int>();

            while (state.Value != 99)
            {
                var instructionString = state.Value.ToString().Reverse().ToList();
                var operation = int.Parse(string.Concat(instructionString.Take(2).Reverse()));
                var modes = instructionString.Skip(2).Select(c => (ParameterMode) int.Parse(c.ToString())).ToList();

                switch (operation)
                {
                    // ADD
                    case 1:
                        state = state.ApplyOperation((x, y) => x + y, modes);
                        break;
                    // MULTIPLY
                    case 2:
                        state = state.ApplyOperation((x, y) => x * y, modes);
                        break;
                    // INPUT
                    case 3:
                        state = state.ApplyInput(inputs.Dequeue);
                        break;
                    // OUTPUT
                    case 4:
                        state = state.ApplyOutput(x => { outputs.Add(x); }, modes);
                        yield return new IntCodeOutput
                        {
                            Output = outputs.Last(), IsComplete = false, CurrentState = state.State
                        };
                        break;
                    // JUMP IF TRUE
                    case 5:
                        state = state.JumpIf(modes, true);
                        break;
                    // JUMP IF FALSE
                    case 6:
                        state = state.JumpIf(modes, false);
                        break;
                    // LESS THAN
                    case 7:
                        state = state.Compare(modes, (x, y) => x < y);
                        break;
                    // EQUALS
                    case 8:
                        state = state.Compare(modes, (x, y) => x == y);
                        break;
                    default:
                        throw new Exception($"Unrecognised instruction: {state.Value}");
                }
            }

            yield return new IntCodeOutput {IsComplete = true, CurrentState = state.State};
        }

        private static IList<int> ApplyNounAndVerb(this IList<int> state, int? noun, int? verb)
        {
            if (!(noun == null || (noun >= 0 && noun <= 99) && verb == null || (verb >= 0 && verb <= 99)))
            {
                throw new Exception($"Noun: {noun} and/or verb: {verb} are invalid");
            }

            if (noun != null)
            {
                state[1] = noun.Value;
            }

            if (verb != null)
            {
                state[2] = verb.Value;
            }

            return state;
        }
    }
}