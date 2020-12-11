using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day04
{
    [TestFixture]
    public class Day04Tests
    {
        [Test]
        public void Part1()
        {
            Day04.Part1().Should().NotBe(80);
            Day04.Part1().Should().NotBe(169);
            Day04.Part1().Should().Be(170);
        }
        
        [Test]
        public void Part2()
        {
            Day04.Part2().Should().NotBe(170);
            Day04.Part2().Should().Be(103);
        }
    }
}