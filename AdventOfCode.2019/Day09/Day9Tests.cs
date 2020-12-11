using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Day09
{
    [TestFixture]
    public class Day09Tests
    {
        [Test]
        public void Part1()
        {
            Day09.Part1().Should().Be(3013554615);
        }
        
        [Test]
        public void Part2()
        {
            Day09.Part2().Should().Be(50158);
        }
    }
}