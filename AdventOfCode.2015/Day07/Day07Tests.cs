using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2015.Day07
{
    [TestFixture]
    public class Day07Tests
    {
        [Test]
        public void Part1()
        {
            var answer = Day07.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(3176);
        }

        [Test]
        public void Part2()
        {
            var answer = Day07.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(14710L);
        }
    }
}