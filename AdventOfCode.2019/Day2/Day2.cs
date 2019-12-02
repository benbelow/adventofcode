using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2019.Common;

namespace AdventOfCode._2019.Day2
{
    public static class Day2
    {
        public static int Part1()
        {
            var intCode = FileReader.ReadInputLines(2).Single();
            var initialState = ParseIntCode(intCode);

            var result = RunIntCode(initialState, 12, 2);
            return result.First();
        }

        public static int Part2Manual(int noun, int verb)
        {
            var intCode = FileReader.ReadInputLines(2).Single();
            var initialState = ParseIntCode(intCode);
            
            var result = RunIntCode(initialState, noun, verb);
            return result.First();
        }

        public static int Part2(int target)
        {
            var intCode = FileReader.ReadInputLines(2).Single();
            var initialState = ParseIntCode(intCode);

            var result = 0;
            var noun = 0;
            var verb = 0;

            while (result != target)
            {
                if (result / 1000 == target / 1000)
                {
                    verb++;
                }
                else
                {
                    noun++;
                }
                result = RunIntCode(new List<int>(initialState), noun, verb).First();
            }

            return 100 * noun + verb;
        }
        
        public static IEnumerable<int> ParseAndRunIntCode(string intCode)
        {
            var initialState = ParseIntCode(intCode);
            return RunIntCode(initialState);
        }

        private static List<int> ParseIntCode(string intCode)
        {
            return intCode.Split(",").Select(int.Parse).ToList();
        }

        private static IEnumerable<int> RunIntCode(IList<int> initialState, int? noun = null, int? verb = null)
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

            while (value != 99)
            {
                switch (value)
                {
                    case 1:
                        state = ApplyOperation(state, index, (x, y) => x + y);
                        break;
                    case 2:
                        state = ApplyOperation(state, index, (x, y) => x * y);
                        break;
                    default:
                        throw new Exception($"Unrecognised instruction: {value}");
                }

                index += 4;
                value = state.ElementAt(index);
            }

            return state;
        }

        private static IList<int> ApplyOperation(IList<int> state, int index, Func<int, int, int> operation)
        {
            var indexX = state.ElementAtWrapped(index + 1);
            var indexY = state.ElementAtWrapped(index + 2);
            var outputIndex = state.ElementAtWrapped(index + 3);
            
            var output = operation(state.ElementAtWrapped(indexX), state.ElementAtWrapped(indexY));
            var indexToUpdate = state.WrappedIndex(outputIndex);
            state[indexToUpdate] = output;
            return state;
        }
    }
}