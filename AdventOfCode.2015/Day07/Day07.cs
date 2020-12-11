using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Common;
using NUnit.Framework;

namespace AdventOfCode._2015.Day07
{
    public static class Day07
    {
        private const int Day = 07;

        public static long Part1()
        {
            var wires = FileReader
                .ReadInputLines(Day)
                .Select(x => new ParsedInstruction(x))
                .ToDictionary(x => x.TargetKey, x => x);

            var memo = new Dictionary<string, int>();

            bool CanApplyWithoutRecursion(ParsedInstruction i)
            {
                return !memo.ContainsKey(i.TargetKey) && i.CanCalculate(memo);
            }

            int Apply(ParsedInstruction instruction)
            {
                if (memo.ContainsKey(instruction.TargetKey))
                {
                    return memo[instruction.TargetKey];
                }
                var val1 = GetValue(instruction.Input1Key);
                var val2 = instruction.Input2Key == null ? 0 : GetValue(instruction.Input2Key);
                var result = instruction.Operation switch
                {
                    Operation.Not => ~val1,
                    Operation.And => val1 & val2,
                    Operation.Or => val1 | val2,
                    Operation.LShift => val1 << val2,
                    Operation.RShift => val1 >> val2,
                    Operation.Value => val1,
                    _ => 0
                };
                memo[instruction.TargetKey] = result;
                return result;
            }

            int GetValue(string x) => Regex.IsMatch(x.Trim(), "^[a-z]+$")
                ? Apply(wires[x.Trim()])
                : int.Parse(x.Trim());


            List<ParsedInstruction> canDos;
            do
            {
                canDos = wires.Values.Where(CanApplyWithoutRecursion).ToList();
                foreach (var canDo in canDos)
                {
                    Apply(canDo);
                }
            } while (canDos.Any());


            return Apply(wires["a"]);
        }

        public enum Operation
        {
            Not,
            And,
            Or,
            LShift,
            RShift,
            Value
        }

        public class ParsedInstruction
        {
            public string Original { get; set; }

            public Operation Operation { get; set; }

            public string Input1Key { get; set; }

            public string Input2Key { get; set; }

            public string TargetKey { get; set; }

            public bool CanCalculate(Dictionary<string, int> dictionary)
            {
                bool CanCalculateValue(string key) 
                {
                    var isInt = int.TryParse(key, out var _);
                    var isCached = dictionary.ContainsKey(key);
                    return isInt || isCached;
                }

                return CanCalculateValue(Input1Key) && (Input2Key == null || CanCalculateValue(Input2Key));
            }

            public ParsedInstruction(string fullInstruction)
            {
                var instruction = fullInstruction.Split("->")[0].Trim();
                TargetKey = fullInstruction.Split("->")[1].Trim();

                Original = instruction;

                var splitInstructions = instruction.Split();
                if (instruction.Contains("NOT"))
                {
                    Operation = Operation.Not;
                    Input1Key = splitInstructions[1];
                    return;
                }

                if (!Regex.Match(instruction.Trim(), "^[0-9a-z]+$").Success)
                {
                    Operation = splitInstructions[1] switch
                    {
                        "AND" => Operation.And,
                        "OR" => Operation.Or,
                        "LSHIFT" => Operation.LShift,
                        "RSHIFT" => Operation.RShift
                    };
                    Input1Key = splitInstructions[0];
                    Input2Key = splitInstructions[2];
                    return;
                }

                Operation = Operation.Value;
                Input1Key = instruction.Trim();
            }
        }

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            return -1;
        }
    }
}