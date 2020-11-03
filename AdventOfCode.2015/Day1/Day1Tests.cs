using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2015.Day1
{
    [TestFixture]
    public class Day1Tests
    {
        [Test]
        public void Part1()
        {
            Day1.Part1().Should().Be(138);
        }
        
        [Test]
        public void Part2()
        {
            Day1.Part2().Should().Be(1771);
        }
    }
}