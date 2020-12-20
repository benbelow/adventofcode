using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day20
{
    [TestFixture]
    public class Day20Tests
    {
        [Test]
        public void Part1()
        {
            var answer = Day20.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(83775126454273);
        }

        [Test]
        public void Part2()
        {
            var answer = Day20.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(1993);
        }
    }
}