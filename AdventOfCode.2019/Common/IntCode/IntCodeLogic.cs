using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2019.Common.IntCode.Models;

namespace AdventOfCode._2019.Common.IntCode
{
    /// <summary>
    /// Repsonsible for *running* intcode.
    /// - Parses string intcode
    /// - Applies noun and verb before beginning
    /// - Runs OpCodes as necessary
    /// </summary>
    public static class IntCodeLogic
    {
        public static IEnumerator<IntCodeOutput> ParseAndRunIntCode(
            string intCode,
            Queue<long> inputs = null,
            long? noun = null,
            long? verb = null)
        {
            inputs ??= new Queue<long>();
            var initialState = Parser.ParseIntCode(intCode);
            
            var state = new IntCodeState
            {
                State = initialState.ApplyNounAndVerb(noun, verb),
                Index = 0
            };
            var outputs = new List<long>();

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
                        state = state.ApplyInput(inputs.Dequeue());
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
                        state = state.JumpIf(x => x != 0);
                        break;
                    // JUMP IF FALSE
                    case 6:
                        state = state.JumpIf(x => x == 0);
                        break;
                    // LESS THAN
                    case 7:
                        state = state.Compare((x, y) => x < y);
                        break;
                    // EQUALS
                    case 8:
                        state = state.Compare((x, y) => x == y);
                        break;
                    // EQUALS
                    case 9:
                        state = state.AdjustRelativeBase();
                        break;
                    default:
                        throw new Exception($"Unrecognised instruction: {state.Value}");
                }
            }

            yield return new IntCodeOutput {IsComplete = true, CurrentState = state.State};
        }

        private static IList<long> ApplyNounAndVerb(this IList<long> state, long? noun, long? verb)
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