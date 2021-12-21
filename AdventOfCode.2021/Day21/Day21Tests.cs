using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day21
{
    [TestFixture]
    public class Day21Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day21.Part1(4, 8);
            answer.Should().Be(739785);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day21.Part2(4, 8);
            var expected = 444356092776315;
            var diff = expected - answer;
            var mag = answer.ToString().Length;
            mag.Should().Be(expected.ToString().Length);
            // diff.Should().Be(0);
            answer.Should().Be(expected);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day21.Part1(5, 9);
            answer.Should().NotBe(-1);
            answer.Should().Be(989352);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day21.Part2(5, 9);
            answer.Should().NotBe(-2);
            answer.Should().Be(430229563871565L);
        }
    }
}