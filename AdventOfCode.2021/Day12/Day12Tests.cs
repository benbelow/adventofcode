using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day12
{
    [TestFixture]
    public class Day12Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day12.Part1(true);
            answer.Should().Be(226);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day12.Part2(true);
            answer.Should().Be(3509);
            // answer.Should().Be(36);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day12.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(5254);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day12.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(149385L);
        }
    }
}