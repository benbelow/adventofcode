using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day12
{
    [TestFixture]
    public class Day12Tests
    {
        [Test]
        public void Part1()
        {
            var answer = Day12.Part1();
            answer.Should().NotBe(9860);
            answer.Should().NotBe(8956);
            answer.Should().NotBe(10538);
            answer.Should().Be(2228);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day12.Part2();
            answer.Should().NotBe(66);
            answer.Should().Be(42908L);
        }
    }
}