using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day03
{
    [TestFixture]
    public class Day03Tests
    {

        [Test]
        public void Part1()
        {
            Day03.Part1().Should().NotBe(80);
            Day03.Part1().Should().Be(153);
        }
        
        [Test]
        public void Part2()
        {
            Day03.Part2().Should().Be(2421944712);
        }
    }
}