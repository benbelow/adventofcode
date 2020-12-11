using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Day05
{
    [TestFixture]
    public class Day05Tests
    {
        [Test]
        public void Part1()
        {
            Day05.Part1().Should().Be(9654885);
        }

        [Test]
        public void Part2()
        {
            var answer = Day05.Part2();
            answer.Should().BeLessThan(15058689);
            answer.Should().Be(7079459);
        }
    }
}