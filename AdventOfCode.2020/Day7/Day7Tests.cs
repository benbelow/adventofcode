using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day7
{
    [TestFixture]
    public class Day7Tests
    {
        [Test]
        public void Part1()
        {
            var answer = Day7.Part1();
            answer.Should().NotBe(350);
            answer.Should().NotBe(351);
            answer.Should().Be(0);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day7.Part2();
            answer.Should().NotBe(130);
            answer.Should().Be(0);
        }
    }
}