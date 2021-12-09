using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day09
{
    [TestFixture]
    public class Day09Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day09.Part1(true);
            answer.Should().Be(15);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day09.Part2(true);
            answer.Should().Be(-1);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day09.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(0);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day09.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(0);
        }
    }
}