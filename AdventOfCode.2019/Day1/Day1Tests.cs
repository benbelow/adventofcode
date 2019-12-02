using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Day1
{
    [TestFixture]
    public class Day1Tests
    {
        [TestCase(12, 2)]
        [TestCase(14, 2)]
        [TestCase(1969, 654)]
        [TestCase(100756, 33583)]
        public void CalculateFuel(int mass, int expectedFuel)
        {
            Day1.CalculateFuelForMass(mass).Should().Be(expectedFuel);
        }

        [Test]
        public void Part1()
        {
            Day1.Part1().Should().Be(3425624);
        }

        [TestCase(14, 2)]
        [TestCase(1969, 966)]
        [TestCase(100756, 50346)]
        public void CalculateTotalFuel(int mass, int expectedFuel)
        {
            Day1.CalculateTotalFuel(mass).Should().Be(expectedFuel);
        }
        
        [Test]
        public void Part2()
        {
            Day1.Part2().Should().Be(5135558);
        }
    }
}