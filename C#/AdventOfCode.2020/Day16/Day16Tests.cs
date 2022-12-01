using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day16
{
    [TestFixture]
    public class Day16Tests
    {
        [Test]
        public void Part1()
        {
            var answer = Day16.Part1();
            answer.Should().NotBe(55);
            answer.Should().Be(25059);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day16.Part2();
            answer.Should().BeGreaterThan(778);
            answer.Should().Be(3253972369789L);
        }
    }
}