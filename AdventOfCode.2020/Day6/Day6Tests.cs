using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day6
{
    [TestFixture]
    public class Day6Tests
    {
        [Test]
        public void Part1()
        {
            Day6.Part1().Should().NotBe(14959);
            Day6.Part1().Should().Be(6521);
        }
        
        [Test]
        public void Part2()
        {
            Day6.Part2().Should().NotBe(5);
            Day6.Part2().Should().NotBe(182);
            Day6.Part2().Should().Be(3305);
        }
    }
}