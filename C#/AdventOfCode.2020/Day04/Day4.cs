using System.Linq;
using AdventOfCode._2020.Passports;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2020.Day04
{
    public static partial class Day04
    {
        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(4);

            var passportInputs = lines.Split("");
            var passports = passportInputs.Select(pi => new Passport(pi)).ToList();

            return passports.Count(p => p.HasAllRequiredFields());
        }

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(4);

            var passportInputs = lines.Split("");
            var passports = passportInputs.Select(pi => new Passport(pi)).ToList();

            return passports.Count(p => p.IsValid());
        }
    }
}