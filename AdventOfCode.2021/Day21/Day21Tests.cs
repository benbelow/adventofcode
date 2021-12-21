using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day21
{
    [TestFixture]
    public class Day21Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day21.Part1(4, 8);
            answer.Should().Be(739785);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day21.Part2(4, 8);
            answer.Should().Be(444356092776315);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day21.Part1(5, 9);
            answer.Should().NotBe(-1);
            answer.Should().Be(989352);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day21.Part2(5, 9);
            answer.Should().NotBe(-2);
            answer.Should().Be(0);
        }
    }
}