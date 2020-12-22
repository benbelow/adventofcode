using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day22
{
    [TestFixture]
    public class Day22Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day22.Part1(true);
            answer.Should().Be(306);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day22.Part2(true);
            answer.Should().Be(291);
        }

        [Test]
        public void Part1()
        {
            var answer = Day22.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(32102);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day22.Part2();
            answer.Should().NotBe(31359L);
            answer.Should().BeGreaterThan(31359L);
            answer.Should().Be(34173L);
        }
    }
}