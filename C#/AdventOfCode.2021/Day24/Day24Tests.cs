using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day24
{
    [TestFixture]
    public class Day24Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day24.Part1(true);
            answer.Should().Be(-1);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day24.Part2(true);
            answer.Should().Be(-1);
        }
        
        [Test]
        public void Part1()
        {
            // Day24.IsValidForInput("13579246899999").Should().Be(true);
            
            // 55555555555555 too low 
            var answer = Day24.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(0);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day24.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(0);
        }
    }
}