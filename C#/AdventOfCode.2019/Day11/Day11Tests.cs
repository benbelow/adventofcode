using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Day11
{
    [TestFixture]
    public class Day11Tests
    {
        [Test]
        public void Part1()
        {
            Day11.Part1().Should().BeGreaterThan(313);
            Day11.Part1().Should().Be(1709);
        }
        [Test]
        public void Part2()
        {
            var part2 = Day11.Part2();
            part2.Should().NotBe("benEHCaH");
            part2.Should().NotBe("HJCHEUGP");
            part2.Should().Be("PGUEHCJH");
        }
    }
}