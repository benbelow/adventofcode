using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day03
{
    [TestFixture]
    public class Day03Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day03.Part1(true);
            answer.Should().Be(198);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day03.Part2(true);
            answer.Should().Be(-1);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day03.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(0);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day03.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(0);
        }
    }
}