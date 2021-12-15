using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day15
{
    [TestFixture]
    public class Day15Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day15.Part1(true);
            answer.Should().Be(40);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day15.Part2(true);
            answer.Should().Be(315);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day15.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(698);
        }
        
        [Test]
        [Ignore("wow this is slow.")]
        public void Part2()
        {
            var answer = Day15.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(3022);
        }
    }
}