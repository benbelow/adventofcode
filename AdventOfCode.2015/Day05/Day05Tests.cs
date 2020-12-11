using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2015.Day05
{
    [TestFixture]
    public class Day05Tests
    {
        [TestCase("ugknbfddgicrmopn", true)]
        [TestCase("aaa", true)]
        [TestCase("jchzalrnumimnmhp", false)]
        [TestCase("haegwjzuvuyypxyu", false)]
        [TestCase("dvszwmarrgswjxmb", false)]
        public void Nice(string input, bool expected)
        {
            Day05.IsNice(input).Should().Be(expected);
        }
        
        [TestCase("qjhvhtzxzqqjkmpb", true)]
        [TestCase("xxyxx", true)]
        [TestCase("uurcxstgmygtbstg", false)]
        [TestCase("ieodomkazucvgmuy", false)]
        public void Nice2(string input, bool expected)
        {
            Day05.IsNice2(input).Should().Be(expected);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day05.Part1();
            answer.Should().NotBe(568);
            answer.Should().Be(255);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day05.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(55);
        }
    }
}