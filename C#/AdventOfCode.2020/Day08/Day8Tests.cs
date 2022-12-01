using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day08
{
    [TestFixture]
    public class Day08Tests
    {
        [Test]
        public void Part1()
        {
            var answer = Day08.Part1();
            int.Parse("-100").Should().Be(-100);
            answer.Should().NotBe(-18);
            answer.Should().Be(1610);
        }
        
        [Test]
        public async Task Part2()
        {
            var answer = await Day08.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(1703);
        }
    }
}