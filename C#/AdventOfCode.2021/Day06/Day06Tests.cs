using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day06
{
    [TestFixture]
    public class Day06Tests
    {
        [TestCase(1, 5)]
        [TestCase(2, 6)]
        [TestCase(8, 10)]
        [TestCase(10, 12)]
        [TestCase(16, 21)]
        [TestCase(18, 26)]
        [TestCase(80, 5934)]
        public void Part1_Example(int iterations, int expected)
        {
            var answer = Day06.Part1(true, iterations);
            answer.Should().Be(expected);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day06.Part2(true);
            answer.Should().Be(26984457539);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day06.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(359344L);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day06.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(1629570219571L);
        }
    }
}