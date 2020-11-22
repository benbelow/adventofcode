using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2015.Day2
{
    [TestFixture]
    public class Day2Tests
    {
        [TestCase(2, 3, 4, 58)]
        [TestCase(1, 1, 10, 43)]
        public void PaperTests(int w, int h, int l, int expected)
        {
            Day2.PaperNecessary(w, h, l).Should().Be(expected);
        }
        
        [Test]
        public void Part1()
        {
            Day2.Part1().Should().BeGreaterThan(1050010);
            Day2.Part1().Should().Be(0);
        }
        
        [Test]
        public void Part2()
        {
            Day2.Part2().Should().Be(0);
        }
    }
}