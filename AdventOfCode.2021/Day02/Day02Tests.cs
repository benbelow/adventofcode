using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day02
{
    [TestFixture]
    public class Day02Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day02.Part1(true);
            answer.Should().Be(150);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day02.Part2(true);
            answer.Should().Be(900);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day02.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(1728414L);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day02.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(1765720035L);
        }
    }
}