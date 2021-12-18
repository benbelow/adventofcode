using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day17
{
    [TestFixture]
    public class Day17Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day17.Part1(20,30,-10,-5);
            answer.Should().Be(45);
        }
        
        [Test]
        public void Part2_Example()
        {            var answer = Day17.Part2(20,30,-10,-5);

            answer.Should().Be(112);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day17.Part1(209,238,-86,-59);
            answer.Should().BeGreaterThan(351);
            answer.Should().Be(0);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day17.Part2(209,238,-86,-59);
            answer.Should().BeLessThan(9546);
            answer.Should().Be(0);
        }
    }
}