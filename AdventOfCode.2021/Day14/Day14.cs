using System;
using System.Collections.Generic;
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

            return RunPolymerEfficient(lines, 10);
        }

        private static long RunPolymer(List<string> lines, int iterations)
        {
            var polymer = lines.First();
            var rules = lines.Skip(2)
                .Select(l => l.Split(" -> "))
                .ToDictionary(s => (s.First().First(), s.First().Last()), s => s.Last());

            for (int i = 0; i < iterations; i++)
            {
                var pairs = Enumerable.Range(0, polymer.Length - 1)
                    .Select(i => (polymer[i], polymer[i + 1])).ToList();
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

                var polymerByChar1 = polymer.GroupBy(x => x).OrderBy(x => x.Count());
                foreach (var poly in polymerByChar1)
                {
                    Console.WriteLine("" + poly.Key + poly.Count());
                }

                Console.WriteLine(polymer);

                foreach (var pair in pairs.GroupBy(x => x))
                {
                    Console.WriteLine("" + pair.Key.Item1 + pair.Key.Item2 + ":" + pair.Count());
                }


                Console.WriteLine("_______");
            }

            var polymerByChar = polymer.GroupBy(x => x).OrderBy(x => x.Count());
            return polymerByChar.Last().Count() - polymerByChar.First().Count();
        }

        private static long RunPolymerEfficient(List<string> lines, int iterations)
        {
            var polymer = lines.First();
            var rules = lines.Skip(2)
                .Select(l => l.Split(" -> "))
                .ToDictionary(s => (s.First().First(), s.First().Last()), s => s.Last());

            var pairRules = rules.ToDictionary(rule => rule.Key, rule =>
            {
                var newPair1 = (rule.Key.Item1, rule.Value.Single());
                var newPair2 = (rule.Value.Single(), rule.Key.Item2);

                return new[] { newPair1, newPair2 };
            });


            var pairs = Enumerable.Range(0, polymer.Length - 1)
                .Select(i => (polymer[i], polymer[i + 1])).ToList();

            var pairCounts = pairs.GroupBy(x => x).ToDictionary(p => p.Key, p => (long)p.Count());

            var queuedPairCounts = new Dictionary<(char, char), long>();

            long GetPairCount((char, char) pair) => pairCounts.ContainsKey(pair) ? pairCounts[pair] : 0;
            long GetQueuedPairCount((char, char) pair) => queuedPairCounts.ContainsKey(pair) ? queuedPairCounts[pair] : 0;

            void QueuePairCountBump((char, char) pair, long toBump)
            {
                var existing = GetQueuedPairCount(pair);
                queuedPairCounts[pair] = existing + toBump;
            }

            void BumpPairCount((char, char) pair, long toBump)
            {
                var existing = GetPairCount(pair);
                pairCounts[pair] = existing + toBump;
            }

            void ApplyQueue()
            {
                foreach (var pairCountsKey in pairCounts.Keys)
                {
                    pairCounts[pairCountsKey] = 0;
                }
                
                foreach (var queuedPairCount in queuedPairCounts)
                {
                    BumpPairCount(queuedPairCount.Key, queuedPairCount.Value);
                }

                queuedPairCounts = new Dictionary<(char, char), long>();
            }

            for (int i = 0; i < iterations; i++)
            {
                foreach (var rule in pairRules)
                {
                    var pairsToIncrease = rule.Value;
                    var numberToIncreaseBy = GetPairCount(rule.Key);

                    foreach (var pairToIncrease in pairsToIncrease)
                    {
                        QueuePairCountBump(pairToIncrease, numberToIncreaseBy);
                    }
                }
                
                ApplyQueue();
                var charsCount = GetCharCount(polymer, pairCounts);
                foreach (var cc in charsCount)
                {
                    Console.WriteLine("" + cc.Key + cc.Value);
                }
                Console.WriteLine("______");

                foreach (var pc in pairCounts.Where(pc => pc.Value != 0))
                {
                    Console.WriteLine("" + pc.Key + pc.Value);
                }
                Console.WriteLine("______");
            }

            var countsByC = GetCharCount(polymer, pairCounts);

            return (countsByC.Values.Max() - countsByC.Values.Min()) / 2;
        }

        private static Dictionary<char, long> GetCharCount(string polymer, Dictionary<(char, char), long> pairCounts)
        {
            var firstC = polymer.First();
            var lastC = polymer.Last();

            var countsByC = new Dictionary<char, long>
            {
                { firstC, 1 },
                { lastC, 1 },
            };
            foreach (var pairCount in pairCounts)
            {
                var (c1, c2) = pairCount.Key;
                var existing1 = countsByC.ContainsKey(c1) ? countsByC[c1] : 0;
                countsByC[c1] = existing1 + pairCount.Value;
                
                var existing2 = countsByC.ContainsKey(c2) ? countsByC[c2] : 0;
                countsByC[c2] = existing2 + pairCount.Value;
            }

            return countsByC;
        }


        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return RunPolymerEfficient(lines, 40);
        }
    }
}