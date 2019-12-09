using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Day9
{
    [TestFixture]
    public class Day9Tests
    {
        [Test]
        public void Part1()
        {
            Day9.Part1().Should().BeGreaterThan(203);
            Day9.Part1().Should().Be(1);
        }
    }
}