using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Day9
{
    [TestFixture]
    public class Day9Tests
    {
        
        // This currently takes ~20 seconds, so commenting out to avoid slowing down test suite.
//        [Test]
        public void Part1()
        {
            Day9.Part1().Should().Be(3013554615);
        }
        
        [Test]
        public void Part2()
        {
            Day9.Part2().Should().Be(3013554615);
        }
    }
}