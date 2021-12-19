using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day19
{
    [TestFixture]
    public class Day19Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day19.Part1(true);
            answer.Should().Be(79);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day19.Part2(true);
            answer.Should().Be(-1);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day19.Part1();
            answer.Should().NotBe(-1);
            answer.Should().BeLessThan(573);
            answer.Should().BeLessThan(432);
            answer.Should().BeLessThan(402);
            answer.Should().NotBe(351);
            answer.Should().Be(0);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day19.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(0);
        }
    }
}