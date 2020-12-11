using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2015.Day06
{
    [TestFixture]
    public class Day06Tests
    {
        [TestCase("turn on 0,0 through 999,999", 1000000)]
        [TestCase("toggle 0,0 through 999,0", 1000)]
        [TestCase("turn on 499,499 through 500,500", 4)]
        public void Part1Examples(string instruction, int lightsOn)
        {
            var lg = new Day06.LightGrid();
            lg.ApplyInstruction(instruction);
            lg.LightsOn().Should().Be(lightsOn);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day06.Part1();
            answer.Should().NotBe(224000L);
            answer.Should().NotBe(322000L);
            answer.Should().Be(0);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day06.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(0);
        }
    }
}