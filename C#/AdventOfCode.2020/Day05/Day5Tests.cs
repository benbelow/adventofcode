using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day05
{
    [TestFixture]
    public class Day05Tests
    {
        [Test]
        public void Part1()
        {
            Day05.SeatId("FBFBBFFRLR").Should().Be(357);
            
            Day05.Part1().Should().Be(801);
        }
        
        [Test]
        public void Part2()
        {
            Day05.Part2().Should().Be(597);
        }
    }
}