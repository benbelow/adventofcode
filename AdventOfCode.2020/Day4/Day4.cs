using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2020.Day4
{
    public static class Day4
    {
        private const string hclAllowed = "qwertyuiopasdfghjklzxcvbnm1234567890";
        private static string[] AllowedEyes = new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};

        public static Dictionary<string, Func<string, bool>> Required = new Dictionary<string, Func<string, bool>>
        {
            {"byr", s => s.Count() == 4 && int.Parse(s) >= 1920 && int.Parse(s) <= 2020},
            {"iyr", s => s.Count() == 4 && int.Parse(s) >= 2010 && int.Parse(s) <= 2020},
            {"eyr", s => s.Count() == 4 && int.Parse(s) >= 2020 && int.Parse(s) <= 2030},
            {
                "hgt", s =>
                {
                    if (s.Contains("cm"))
                    {
                        var value = int.Parse(s.Split("cm")[0]);
                        var chaff = s.Split("cm")[1];
                        return value >= 150 && value <= 193 && chaff == "";
                    }

                    if (s.Contains("in"))
                    {
                        var value = int.Parse(s.Split("in")[0]);
                        var chaff = s.Split("in")[1];
                        return value >= 59 && value <= 76 && chaff == "";
                    }

                    return false;
                }
            },
            {"hcl", s => s.Length >= 1 && s.First() == '#' && s.Length == 7 && s.Skip(1).All(c => hclAllowed.Contains(c))},
            {"ecl", s => AllowedEyes.Contains(s)},
            {"pid", s => s.Length == 9 && long.TryParse(s, out var _)},
        };

        public static string[] Optional = new[] {"cid"};

        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(4).ToList();

            var required = Required.Keys.ToList();
            var validCount = 0;

            foreach (var line in lines)
            {
                if (line == "")
                {
                    if (required.Count == 0)
                    {
                        validCount++;
                    }

                    required = Required.Keys.ToList();
                    continue;
                }

                var fields = line.Split(" ");
                var keys = fields.Select(f => f.Split(":")[0]);
                required = required.Except(keys.ToList()).ToList();
            }

            if (!required.Any())
            {
                validCount++;
            }

            return validCount;
        }

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(4).ToList();

            var required = Required.Keys.ToList();
            // valid until proven not
            var currentValid = true;
            var validCount = 0;

            foreach (var line in lines)
            {
                if (line == "")
                {
                    if (!required.Any() && currentValid)
                    {
                        validCount++;
                    }

                    required = Required.Keys.ToList();
                    currentValid = true;
                    continue;
                }

                var fields = line.Split(" ");
                var keys = fields.Select(f => f.Split(":")[0]);
                var results = fields.Select(f => (f, ValidateField(f))).ToList();
                if (!fields.All(ValidateField))
                {
                    currentValid = false;
                }

                required = required.Except(keys.ToList()).ToList();
            }

            if (!required.Any() && currentValid)
            {
                validCount++;
            }

            return validCount;
        }

        private static bool ValidateField(string f)
        {
            var key = f.Split(":")[0];
            var rule = Required.ContainsKey(key) ? Required[key] : x => true;
            return rule(f.Split(":")[1]);
        }
    }
}