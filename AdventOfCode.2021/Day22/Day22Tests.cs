using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day22
{
    [TestFixture]
    public class Day22Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day22.Part1(true);
            answer.Should().Be(590784);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day22.Part2(true);
            answer.Should().Be(2758514936282235);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day22.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(600458);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day22.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(0);
        }
    }
}