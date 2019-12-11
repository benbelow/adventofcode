using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Day11
{
    [TestFixture]
    public class Day11Tests
    {
        [Test]
        public void Part1()
        {
            Day11.Part1().Should().BeGreaterThan(313);
            Day11.Part1().Should().Be(1709);
        }
    }
}