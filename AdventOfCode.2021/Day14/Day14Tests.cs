using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day14
{
    [TestFixture]
    public class Day14Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day14.Part1(true);
            answer.Should().Be(1588);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day14.Part2(true);
            answer.Should().Be(-1);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day14.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(0);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day14.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(0);
        }
    }
}