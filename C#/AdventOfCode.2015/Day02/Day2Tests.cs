using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2015.Day02
{
    [TestFixture]
    public class Day02Tests
    {
        [TestCase(2, 3, 4, 58)]
        [TestCase(1, 1, 10, 43)]
        public void PaperTests(int w, int h, int l, int expected)
        {
            Day02.PaperNecessary(w, h, l).Should().Be(expected);
        }
        
        [TestCase(2, 3, 4, 34)]
        [TestCase(1, 1, 10, 14)]
        public void RibbonTests(int w, int h, int l, int expected)
        {
            Day02.RibbonNecessary(w, h, l).Should().Be(expected);
        }
        
        [Test]
        public void Part1()
        {
            Day02.Part1().Should().Be(1606483);
        }
        
        [Test]
        public void Part2()
        {
            Day02.Part2().Should().Be(3842356);
        }
    }
}