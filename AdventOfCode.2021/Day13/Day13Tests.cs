using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day13
{
    [TestFixture]
    public class Day13Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day13.Part1(true);
            answer.Should().Be(17);
        }

        [Test]
        public void Part1()
        {
            var answer = Day13.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(818);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day13.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(101);
        }
    }
}