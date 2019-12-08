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

            while (state.OpCode != 99)
            {
                switch (state.OpCode)
                {
                    // ADD
                    case 1:
                        state = state.ApplyOperation((x, y) => x + y);
                        break;
                    // MULTIPLY
                    case 2:
                        state = state.ApplyOperation((x, y) => x * y);
                        break;
                    // INPUT
                    case 3:
                        state = state.ApplyInput(inputs.Dequeue);
                        break;
                    // OUTPUT
                    case 4:
                        state = state.ApplyOutput(x => { outputs.Add(x); });
                        yield return new IntCodeOutput
                        {
                            Output = outputs.Last(), IsComplete = false, CurrentState = state.State
                        };
                        break;
                    // JUMP IF TRUE
                    case 5:
                        state = state.JumpIf(true);
                        break;
                    // JUMP IF FALSE
                    case 6:
                        state = state.JumpIf(false);
                        break;
                    // LESS THAN
                    case 7:
                        state = state.Compare((x, y) => x < y);
                        break;
                    // EQUALS
                    case 8:
                        state = state.Compare((x, y) => x == y);
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