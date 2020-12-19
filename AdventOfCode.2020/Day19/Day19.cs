using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2020.Day19
{
    public static class Day19
    {
        private const int Day = 19;

        public class RuleSet
        {
            private readonly Dictionary<int, Rule> rules;

            private Rule FetchRule(int id) => rules[id];

            public RuleSet(IEnumerable<string> rawRules)
            {
                rules = rawRules.ToDictionary(
                    Rule.ParseRuleId,
                    r => new Rule(r, FetchRule)
                );
            }

            public bool ApplyRule(string target, int ruleId)
            {
                var rule = FetchRule(ruleId);
                return rule.ApplyRule(target);
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
            var targets = split.Last();
            return targets.Count(t => ruleCollection.ApplyRule(t, 0));
        }

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            return -1;
        }
    }
}