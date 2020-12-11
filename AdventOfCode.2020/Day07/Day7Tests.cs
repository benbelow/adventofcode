using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day07
{
    [TestFixture]
    public class Day07Tests
    {
        [Test]
        public void Part1()
        {
            var answer = Day07.Part1();
            answer.Should().NotBe(350);
            answer.Should().NotBe(351);
            answer.Should().Be(278);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day07.Part2();
            answer.Should().NotBe(130);
            answer.Should().Be(45157);
        }
    }
}