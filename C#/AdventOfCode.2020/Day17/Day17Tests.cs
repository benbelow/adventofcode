using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day17
{
    [TestFixture]
    public class Day17Tests
    {
        [Test]
        public void Part1()
        {
            var answer = Day17.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(448);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day17.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(2400);
        }
    }
}