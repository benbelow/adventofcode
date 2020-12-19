using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;
using FluentAssertions;
using static MoreLinq.Extensions.BatchExtension;

namespace AdventOfCode._2020.Day19
{
    public static class Day19
    {
        public const int Day = 19;

        // 8 = 42 | 42 8
        // 11 = 42 31 | 42 11 31 
        
        // 8 = (8c) [8c * n]
        // 11 = (8c) [16c * n] (8c)
        
        // min = 24. diff = in 8s.
        
        // max diff = 72
        // max 8 diff = 72/8 = 9
        // max 11 diff = 72/16 = 4.5
        
        public class RuleSet
        {
            private readonly Dictionary<int, Rule> rules;

            private static readonly int MinTarget = 24;
            private static readonly int MaxTarget = 96;
            private static readonly int TargetDiff = MaxTarget - MinTarget;
            private static readonly int RecursionDepth = 10;
            
            private Rule FetchRule(int id) => rules[id];

            public RuleSet(IEnumerable<string> rawRules, bool part2 = false)
            {
                rules = rawRules.ToDictionary(
                    Rule.ParseRuleId,
                    r => new Rule(r, FetchRule)
                );

                if (!part2)
                {
                    return;
                }

                var eightsRange = 200;
                rules[8] = new Rule($"8: {eightsRange}", FetchRule);
                rules[eightsRange] = new Rule($"{eightsRange}: 42 | {eightsRange + 1}", FetchRule);

                // (newId, levelsOfRecursion) 
                var eights = new List<(int, int)>();

                // 8s
                for (int i = 1; i < RecursionDepth + 1; i++)
                {
                    var nextRule = eightsRange + i;
                    var referencedRule = nextRule + 1;

                    var ruleText = i == RecursionDepth - 1 ? $"{nextRule}: 42" : $"{nextRule}: 42 | {referencedRule}";

                    rules[nextRule] = new Rule(ruleText, FetchRule);
                    eights.Add((nextRule, i + 1));
                }

                var elevensRange = 300;
                // (newId, levelsOfRecursion) 
                var elevens = new List<(int, int)>();

                rules[11] = new Rule($"11: {elevensRange}", FetchRule);
                rules[elevensRange] = new Rule($"11: 42 31 | 42 {elevensRange + 1} 31", FetchRule);

                // 11s
                for (int i = 0; i < RecursionDepth; i++)
                {
                    var nextRule = elevensRange + i + 1;
                    var referencedRule = nextRule + 1;

                    var ruleText = i == RecursionDepth - 1 ? $"{nextRule}: 42 31" : $"{nextRule}: 42 31 | 42 {referencedRule} 31";

                    rules[nextRule] = new Rule(ruleText, FetchRule);
                    elevens.Add((nextRule, i + 1));
                }

                // Alternate 0s
                var originalLength = 24;
                var knownLengths = new[] {32, 40, 48, 56, 64, 72, 80, 88, 96};

                ruleRanges[originalLength] = (0, 0);

                var newRangeIndex = 3;
                
                foreach (var knownLength in knownLengths)
                {
                    var diff = knownLength - originalLength;
                    var rangeStart = newRangeIndex * 200;

                    for (int i = 0; i <= diff; i++)
                    {
                        var extraEights = i;
                        var extraElevens = diff - i;

                        rules[rangeStart + i] = new Rule($"{rangeStart + i}: {eightsRange + extraEights}", FetchRule);
                        rules[rangeStart + diff + i] = new Rule($"{rangeStart + diff + i}: {elevensRange + extraElevens}", FetchRule);
                    }

                    ruleRanges[knownLength] = (rangeStart, rangeStart + diff * 2);
                    newRangeIndex++;
                }

            }

            // inclusive
            private Dictionary<int, (int, int)> ruleRanges= new Dictionary<int, (int, int)>();
            
            public (int, int) GetRuleRange(int targetLength) => ruleRanges[targetLength];

            public bool TestPart2(string target, int i8, int i11)
            {
                
                var Options42 = rules[42].AllowedTargets();
                var Options31 = rules[31].AllowedTargets();


                var singleSection = Options42.First().Length;
                
                var extra8s = singleSection * i8;
                var extra11s = singleSection * i11 * 2;

                var total8s = 1 + i8;
                var total11s = 1 + i11;

                var total42From8 = total8s;
                var total42From11 = total11s;
                var total31From11 = total11s;


                if (target.Length != (singleSection * 3) + extra8s + extra11s)
                {
                    return false;
                }

                (total42From8 + total42From11 + total31From11).Should().Be(target.Length / singleSection);

                var blocksOf8 = target.Batch(singleSection).ToList();

                var marker1 = total42From8;
                // check 42s for 8s
                for (int x = 0; x < marker1; x++)
                {
                    var substring = blocksOf8[x].CharsToString();
                    if (!Options42.Contains(substring))
                    {
                        return false;
                    }
                }

                var marker2 = total42From8 + total42From11;
                // check 42s for 11s
                for (int x = marker1; x < marker2; x++)
                {
                    var substring = blocksOf8[x].CharsToString();
                    if (!Options42.Contains(substring))
                    {
                        return false;
                    }
                }

                var marker3 = total42From8 + total42From11 + total31From11;
                marker3.Should().Be(blocksOf8.Count());
                // check 31s for 11s
                for (int x = marker2; x < marker3; x++)
                {
                    var substring = blocksOf8[x].CharsToString();
                    if (!Options31.Contains(substring))
                    {
                        return false;
                    }
                }


                return true;
            }
            
            public bool ApplyRule(string target, int ruleId, bool part2 = false)
            {
                // if (ruleId == 0)
                // {
                    // var (start, end) = GetRuleRange(target.Length);
                    // return Enumerable.Range(start, end - start + 1).Any(r => FetchRule(r).ApplyRule(target));
                // }

                if (part2)
                {
                    for (int i8 = 0; i8 <= 9; i8++)
                    {
                        for (int i11 = 0; i11 <= 5; i11++)
                        {
                            var possible = TestPart2(target, i8, i11);
                            if (possible)
                            {
                                return true;
                            }
                        }
                    }

                    return false;
                }
                
                var rule = FetchRule(ruleId);
                return rule.ApplyRule(target);
            }

            public List<string> AllowedValues(int ruleId)
            {
                var rule = rules[ruleId];
                return rule.AllowedTargets();
            }
        }

        public class Rule
        {
            private readonly string rawRule;
            private readonly Func<int, Rule> fetchOtherRule;

            private int id;
            private readonly char? ruleChar = null;

            private List<List<int>> nestedRuleIds;

            /// <summary>
            /// don't access until we're sure all rules are in dict!
            /// </summary>
            private List<List<Rule>> nestedRules => nestedRuleIds?
                .Select(ruleIds => ruleIds.Select(fetchOtherRule).ToList())
                .ToList();

            private int? ruleLength;

            public int RuleLength
            {
                get
                {
                    if (ruleLength.HasValue)
                    {
                        return ruleLength.Value;
                    }

                    if (nestedRules == null && ruleChar != null)
                    {
                        return 1;
                    }

                    var nestedLengths = nestedRules.Select(r => r.Sum(r2 => r2.RuleLength)).ToList();

                    if (nestedLengths.Distinct().Count() != 1)
                    {
                        throw new Exception("Assumption broken - not all options for a rule are the same length!");
                    }

                    ruleLength = nestedLengths.Distinct().Single();
                    return ruleLength.Value;
                }
            }

            public List<string> cachedTargets = null;

            public static int ParseRuleId(string rawRule) => int.Parse(rawRule.Split(":")[0]);

            public Rule(string rawRule, Func<int, Rule> fetchOtherRule)
            {
                this.rawRule = rawRule;
                this.fetchOtherRule = fetchOtherRule;

                id = ParseRuleId(rawRule);
                var ruleWithoutId = rawRule.Split(':')[1].Trim();
                var rulesText = ruleWithoutId.Split('|');

                var firstRule = rulesText[0];

                if (!Regex.IsMatch(firstRule, "[0-9]"))
                {
                    ruleChar = firstRule.Replace("\"", "").Trim().Single();
                }
                else
                {
                    nestedRuleIds = rulesText
                        .Select(rt => rt.Trim().Split().Select(int.Parse).ToList())
                        .ToList();
                }
            }

            public List<string> AllowedTargets()
            {
                if (cachedTargets != null)
                {
                    return cachedTargets;
                }
                
                if (ruleChar != null)
                {
                    cachedTargets = new[] {ruleChar.ToString()}.ToList();
                    return cachedTargets;
                }

                var options = nestedRules.Select(compositeRule =>
                {
                    var singleOptions = compositeRule.Select(rule => rule.AllowedTargets()).ToList();
                    return singleOptions.AllCombos();
                });

                cachedTargets = options.SelectMany(x => x).Distinct().ToList();
                return cachedTargets;
            }
            
            // Returns ordered list of allowed chars at each position
            public List<List<char>> AllowedValues()
            {
                if (ruleChar.HasValue)
                {
                    var allowedCharsAtPosition0 = new List<char> {ruleChar.Value};
                    return new List<List<char>> {allowedCharsAtPosition0};
                }

                var allowedValuesPerOptionalRule = nestedRules.Select(nestedRuleCollection =>
                {
                    return nestedRuleCollection.Aggregate(new List<List<char>>(), (chars, rule) =>
                        chars.Concat(rule.AllowedValues()).ToList()
                    );
                }).ToList();

                return Enumerable.Range(0, allowedValuesPerOptionalRule.First().Count).Select(i =>
                {
                    return allowedValuesPerOptionalRule.SelectMany(av => av[i]).ToList();
                }).ToList();
            }

            public bool ApplyRule(string target)
            {
                if (ruleChar != null)
                {
                    return target.Length == 1 && target.Single() == ruleChar;
                }

                return nestedRules.Any(nestedRuleCollection =>
                {
                    var ruleLengths = nestedRuleCollection.Select(r => r.RuleLength).ToList();
                    if (target.Length != ruleLengths.Sum())
                    {
                        return false;
                    }

                    var processedTargetIndexes = 0;

                    for (var i = 0; i < ruleLengths.Count(); i++)
                    {
                        var length = ruleLengths[i];
                        var rule = nestedRuleCollection[i];
                        var subTarget = target.Skip(processedTargetIndexes).Take(length).CharsToString();

                        processedTargetIndexes += length;
                        if (!rule.ApplyRule(subTarget))
                        {
                            return false;
                        }
                    }

                    return true;
                });
            }
        }

        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var split = lines.Split("").ToList();

            var ruleCollection = new RuleSet(split.First());
            var targets = split.Last().ToList();
            foreach (var target in targets.OrderBy(tr => tr.Length).Select(x => x.Length).Distinct())
            {
                Console.WriteLine(target);
            }
            
            Console.WriteLine($"MAX___{targets.Select(t => t.Length).Max()}");
            var valid = targets.Where(t => ruleCollection.ApplyRule(t, 0)).ToList();
            Console.WriteLine($"VALID_LENGTH____{valid[0].Length}");
            foreach (var validString in valid)
            {
                Console.WriteLine(validString);
            }
            return valid.Count();
        }

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var split = lines.Split("").ToList();

            var ruleCollection = new RuleSet(split.First(), true);
            var targets = split.Last();
            return targets.Count(t => ruleCollection.ApplyRule(t, 0, true));
        }
    }
}