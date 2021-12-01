using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day01
{
    [TestFixture]
    public class Day01Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day01.Part1(true);
            answer.Should().Be(7);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day01.Part2(true);
            answer.Should().Be(5);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day01.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(1665);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day01.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(1702);
        }
    }
}