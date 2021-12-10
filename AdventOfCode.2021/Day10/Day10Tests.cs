using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day10
{
    [TestFixture]
    public class Day10Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day10.Part1(true);
            answer.Should().Be(26397);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day10.Part2(true);
            answer.Should().Be(288957L);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day10.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(215229L);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day10.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(1105996483L);
        }
    }
}