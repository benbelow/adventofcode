using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Day8
{
    [TestFixture]
    public class Day8Tests
    {
        [TestCase("123456789012", 3, 2, 1)]
        public void CheckSum(string imageData, int width, int height, int expectedCheckSum)
        {
            Day8.CheckSum(imageData, width, height).Should().Be(expectedCheckSum);
        }

        [Test]
        public void Part1()
        {
            Day8.Part1().Should().Be(2480);
        }
    }
}