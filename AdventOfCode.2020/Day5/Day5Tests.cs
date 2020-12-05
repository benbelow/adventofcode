using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day5
{
    [TestFixture]
    public class Day5Tests
    {
        [Test]
        public void Part1()
        {
            Day5.SeatId("FBFBBFFRLR").Should().Be(357);
            
            Day5.Part1().Should().Be(801);
        }
        
        [Test]
        public void Part2()
        {
            Day5.Part2().Should().Be(597);
        }
    }
}