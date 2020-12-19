using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day19
{
    [TestFixture]
    public class Day19Tests
    {
        [Test]
        public void Part1Example()
        {
            const string rules = @"0: 4 1 5
1: 2 3 | 3 2
2: 4 4 | 5 5
3: 4 5 | 5 4
4: ""a""
5: ""b""";

            var ruleSet = new Day19.RuleSet(rules.Split('\n').Select(x => x.Trim()));

            ruleSet.ApplyRule("a", 4).Should().Be(true);
            ruleSet.ApplyRule("b", 4).Should().Be(false);
            ruleSet.ApplyRule("a", 5).Should().Be(false);
            ruleSet.ApplyRule("b", 5).Should().Be(true);

            ruleSet.ApplyRule("ba", 3).Should().Be(true);
            ruleSet.ApplyRule("aa", 3).Should().Be(false);
            ruleSet.ApplyRule("bb", 3).Should().Be(false);
            
            ruleSet.ApplyRule("ababbb", 0).Should().Be(true);
            ruleSet.ApplyRule("abbbab", 0).Should().Be(true);
            ruleSet.ApplyRule("bababa", 0).Should().Be(false);
            ruleSet.ApplyRule("aaabbb", 0).Should().Be(false);
            ruleSet.ApplyRule("aaaabbb", 0).Should().Be(false);
        } 
        
        [Test]
        public void Part1()
        {
            var answer = Day19.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(0);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day19.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(0);
        }
    }
}