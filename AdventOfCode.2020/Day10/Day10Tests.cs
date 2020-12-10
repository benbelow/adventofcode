using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day10
{
    [TestFixture]
    public class Day10Tests
    {
        [Test]
        public void Part1()
        {
            var answer = Day10.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(0);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day10.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(0);
        }
    }
}