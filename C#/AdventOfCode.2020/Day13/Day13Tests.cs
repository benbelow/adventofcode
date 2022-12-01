using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day13
{
    [TestFixture]
    public class Day13Tests
    {
        [Test]
        public void Part1()
        {
            var answer = Day13.Part1();
            answer.Should().NotBe(17);
            answer.Should().Be(171);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day13.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(0);
        }

        [Ignore("")]
        [TestCase("7,13,x,x,59,x,31,19", 1068781)]
        [TestCase("17,x,13,19", 3417)]
        [TestCase("67,7,59,61", 754018)]
        [TestCase("67,x,7,59,61", 779210)]
        [TestCase("67,7,x,59,61", 1261476)]
        [TestCase("1789,37,47,1889", 1202161486L)]
        public void Part2_Examples(string line, long expected)
        {
            Day13.Part2(line).Should().Be(expected);
        }
    }
}