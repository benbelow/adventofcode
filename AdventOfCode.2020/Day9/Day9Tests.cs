using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day9
{
    [TestFixture]
    public class Day9Tests
    {
        [Test]
        public void Part1()
        {
            var answer = Day9.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(0);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day9.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(0);
        }
    }
}