using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day4
{
    [TestFixture]
    public class Day4Tests
    {
        [Test]
        public void Part1()
        {
            Day4.Part1().Should().NotBe(80);
            Day4.Part1().Should().NotBe(169);
            Day4.Part1().Should().Be(170);
        }
        
        [Test]
        public void Part2()
        {
            Day4.Part2().Should().NotBe(170);
            Day4.Part2().Should().Be(103);
        }
    }
}