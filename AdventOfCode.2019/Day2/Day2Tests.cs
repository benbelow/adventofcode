using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Day2
{
    [TestFixture]
    public class Day2Tests
    {
        [Test]
        public void Part1()
        {
            // 1719512 too low
            Day2.Part1().Should().Be(5866714);
        }

        [Test]
        public void Part2Manual()
        {
            Day2.Part2Manual(1, 1).Should().Be(2065113);
            Day2.Part2Manual(1, 50).Should().Be(2065162);
            Day2.Part2Manual(1, 99).Should().Be(2065211);
            Day2.Part2Manual(50, 1).Should().Be(18999513);
            Day2.Part2Manual(50, 50).Should().Be(18999562);
            Day2.Part2Manual(50, 99).Should().Be(18999611);
            Day2.Part2Manual(99, 1).Should().Be(35933913);
            Day2.Part2Manual(99, 50).Should().Be(35933962);
            Day2.Part2Manual(99, 99).Should().Be(35934011);
            Day2.Part2Manual(52, 8).Should().Be(19690720);
        }

        [Test]
        public void Part2()
        {
            // 41600 too high
            Day2.Part2(19690720).Should().Be(5208);
        }
    }
}