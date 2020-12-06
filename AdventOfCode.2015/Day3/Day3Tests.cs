using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2015.Day3
{
    [TestFixture]
    public class Day3Tests
    {
        [Test]
        public void Part1()
        {
            var answer = Day3.Part1();
            answer.Should().NotBe(7077);
            answer.Should().BeLessThan(7077);
            answer.Should().Be(2081);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day3.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(2341);
        }
    }
}