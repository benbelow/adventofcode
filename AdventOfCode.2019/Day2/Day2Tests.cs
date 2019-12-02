using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Day2
{
    [TestFixture]
    public class Day2Tests
    {
        [TestCase("1,9,10,3,2,3,11,0,99,30,40,50", 0, 3500)]
        [TestCase("1,0,0,0,99", 0, 2)]
        [TestCase("2,3,0,3,99", 3, 6)]
        [TestCase("2,4,4,5,99,0", 5, 9801)]
        [TestCase("1,1,1,4,99,5,6,0,99", 0, 30)]
        [TestCase("1,1,1,4,99,5,6,0,99", 4, 2)]
        public void Parse_And_RunIntCode(string intCode, int indexOfCheck, int expectedResult)
        {
            Day2.ParseAndRunIntCode(intCode).Skip(indexOfCheck).First().Should().Be(expectedResult);
        }

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