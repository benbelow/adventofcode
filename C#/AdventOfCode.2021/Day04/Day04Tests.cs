using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day04
{
    [TestFixture]
    public class Day04Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day04.Part1(true);
            answer.Should().Be(4512);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day04.Part2(true);
            answer.Should().Be(1924);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day04.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(10374);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day04.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(24742);
        }
    }
}