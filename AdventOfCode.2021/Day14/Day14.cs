using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2021.Day14
{
    public static class Day14
    {
        private const int Day = 14;
        
        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();

            var polymer = lines.First();
            var rules = lines.Skip(2)
                .Select(l => l.Split(" -> "))
                .ToDictionary(s => (s.First().First(), s.First().Last()), s => s.Last());

            for (int i = 0; i < 10; i++)
            {
                var pairs = Enumerable.Range(0, polymer.Length - 1)
                    .Select(i => (polymer[i], polymer[i+1])).ToList();
                var insertions = Enumerable.Range(0, polymer.Length - 1)
                    .Select(i =>
                    {
                        var pairAtI = pairs[i];
                        if (rules.ContainsKey(pairAtI))
                        {
                            return rules[pairAtI];
                        }

                        return null;
                    }).ToList();
                var newPolymer = Enumerable.Range(0, polymer.Length)
                    .Aggregate("", (p, i) =>
                    {
                        var originalC = polymer[i];
                        var shouldInsert = insertions.Count > i && insertions[i] != null;
                        var insertion = (shouldInsert ? insertions[i] : "");
                        return p + originalC + insertion;
                    });
                polymer = newPolymer;
            }

            var polymerByChar = polymer.GroupBy(x => x).OrderBy(x => x.Count());
            return polymerByChar.Last().Count() - polymerByChar.First().Count();
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}