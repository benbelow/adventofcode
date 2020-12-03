using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day3
{
    [TestFixture]
    public class Day3Tests
    {

        [Test]
        public void Part1()
        {
            Day3.Part1().Should().NotBe(80);
            Day3.Part1().Should().Be(153);
        }
        
        [Test]
        public void Part2()
        {
            Day3.Part2().Should().Be(2421944712);
        }
    }
}