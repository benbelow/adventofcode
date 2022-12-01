using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day23
{
    [TestFixture]
    public class Day23Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day23.Part1(true);
            answer.Should().Be(12521);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day23.Part2(true);
            answer.Should().Be(44169);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day23.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(18170);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day23.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(50208);
        }
    }
}