using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day05
{
    [TestFixture]
    public class Day05Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day05.Part1(true);
            answer.Should().Be(5);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day05.Part2(true);
            answer.Should().Be(12);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day05.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(8060);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day05.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(21577);
        }
    }
}