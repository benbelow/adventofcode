using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2020.Day14
{
    public static class Day14
    {
        private const int Day = 14;

        private const char FloatingChar = 'X';

        public static long Part1(string input = null)
        {
            var lines = input?.Split("\n").ToList();
            lines ??= FileReader.ReadInputLines(Day).ToList();

            return RunProgram(lines);
        }

        public static long RunProgram(IEnumerable<string> lines)
        {
            var state = new Dictionary<long, long>();
            var bitmask = "";

            foreach (var line in lines)
            {
                if (line.Contains("mask ="))
                {
                    var maskString = line.Split("= ")[1];
                    bitmask = maskString;
                }
                else
                {
                    var address = long.Parse(line.Split("[")[1].Split("]")[0]);
                    var value = long.Parse(line.Split("=")[1]);

                    var binaryValue = Convert.ToString(value, 2).PadLeft(36, '0');

                    var newStringAsList = binaryValue.Select(_ => 'c').ToList();
                    for (int i = 0; i < binaryValue.Length; i++)
                    {
                        if (bitmask[i] == '1')
                        {
                            newStringAsList[i] = '1';
                        }
                        else if (bitmask[i] == '0')
                        {
                            newStringAsList[i] = '0';
                        }
                        else
                        {
                            newStringAsList[i] = binaryValue[i];
                        }
                    }

                    state[address] = Convert.ToInt64(newStringAsList.CharsToString(), 2);
                }
            }

            return state.Values.Sum();
        }

        public static (long, long) ParseBitmask(string bitmask)
        {
            bitmask = bitmask.Trim();
            var orsString = bitmask.Select(bit => bit == '1' ? '1' : '0')
                .Reverse().Take(36).Reverse()
                .CharsToString();
            var andsString = bitmask.Select(bit => bit == '0' ? '0' : '1')
                .Reverse().Take(36).Reverse()
                .CharsToString();

            var ands = Convert.ToInt64(andsString, 2);
            var ors = Convert.ToInt64(orsString, 2);

            return (ands, ors);
        }

        public static long Part2(string input = null)
        {
            var lines = input?.Split("\n").ToList();
            lines ??= FileReader.ReadInputLines(Day).ToList();

            // return Calculate(true, lines, new Dictionary<string, long>());

            return RunProgram2(lines);
        }


        public static long RunProgram2(IEnumerable<string> lines)
        {
            var state = new Dictionary<string, long>();
            var bitmask = "";

            foreach (var line in lines)
            {
                if (line.Contains("mask ="))
                {
                    var maskString = line.Split("= ")[1];
                    bitmask = maskString.Trim();
                }
                else
                {
                    var address = long.Parse(line.Split("[")[1].Split("]")[0]);
                    var value = long.Parse(line.Split("=")[1]);

                    var binaryAddress = Convert.ToString(address, 2).PadLeft(36, '0');

                    var newStringAsList = binaryAddress.Select(_ => 'c').ToList();
                    for (int i = 0; i < binaryAddress.Length; i++)
                    {
                        if (bitmask[i] == '1')
                        {
                            newStringAsList[i] = '1';
                        }
                        else if (bitmask[i] == '0')
                        {
                            newStringAsList[i] = binaryAddress[i];
                        }
                        else
                        {
                            newStringAsList[i] = FloatingChar;
                        }
                    }

                    foreach (var permutation in GenerateCombinations(newStringAsList.CharsToString()))
                    {
                        state[permutation] = value;
                    }
                }
            }

            var state2 = state.ToDictionary(x => Convert.ToInt64(x.Key, 2), x => x.Value);
            return state.Values.Sum();

            return state.Select(a => CountAddress(a.Key, a.Value)).Sum();
        }

        public static int NthIndexOfC(this string input, char charToFind, int n)
        {
            var seen = 0;
            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (c == charToFind)
                {
                    seen++;
                }

                if (n == seen)
                {
                    return i;
                }
            }

            return -1;
        }

        public static long CountAddress(string address, long value)
        {
            var multiplier = 0;
            foreach (var c in address.Reverse())
            {
                if (c == '?')
                {
                    multiplier++;
                }
            }


            var finalMultiplier = 1;
            for (int i = 0; i < multiplier; i++)
            {
                finalMultiplier *= 2;
            }

            return (finalMultiplier) * value;
        }

        #region cheatagain

        private static List<string> GenerateCombinations(string value)
        {
            if (!value.Contains(FloatingChar))
            {
                return new List<string> {value};
            }

            var zeroMask = ReplaceFirstMatch(value, "X", "0");
            var oneMask = ReplaceFirstMatch(value, "X", "1");
            return GenerateCombinations(zeroMask).Concat(GenerateCombinations(oneMask)).ToList();
        }

        private static string ReplaceFirstMatch(string value, string mask, string replacement)
        {
            var firstMaskIndex = value.IndexOf(mask);
            if (firstMaskIndex < 0)
            {
                return value;
            }

            return value.Remove(firstMaskIndex, 1).Insert(firstMaskIndex, replacement);
        }

        #endregion
    }
}