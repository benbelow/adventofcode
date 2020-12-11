using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day06
{
    [TestFixture]
    public class Day06Tests
    {
        [Test]
        public void Part1()
        {
            Day06.Part1().Should().NotBe(14959);
            Day06.Part1().Should().Be(6521);
        }
        
        [Test]
        public void Part2()
        {
            Day06.Part2().Should().NotBe(5);
            Day06.Part2().Should().NotBe(182);
            Day06.Part2().Should().Be(3305);
        }
    }
}